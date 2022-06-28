

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text

#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports Microsoft.VisualBasic
#End Region

Public Class FrmStockReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents dgStockList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtStockKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchStockDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgStock As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnitVal As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
    Private bDownloadPriv As Boolean
    Private bTransferPriv As Boolean
#End Region

#Region " Custom Method "
#Region "Check Privilage"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.FakturKendaraanStatusView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR - Daftar Status Faktur Kendaraan")
        'End If
        '_PCAccessAllowed = SecurityProvider.Authorize(context.User, SR.PKCategoryPC_Privilege)
        '_CVAccessAllowed = SecurityProvider.Authorize(context.User, SR.PKCategoryCV_Privilege)
        '_LCVAccessAllowed = SecurityProvider.Authorize(context.User, SR.PKCategoryLCV_Privilege)

        'If (Not _PCAccessAllowed) And (Not _CVAccessAllowed) And (Not _LCVAccessAllowed) Then
        '    Me.btnSearch.Visible = False
        '    Me.ddlCategory.Visible = False
        'End If

        If Not SecurityProvider.Authorize(context.User, SR.MarketStockView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DEALER REPORT - Stok Pasar")
        End If

        bTransferPriv = SecurityProvider.Authorize(context.User, SR.MarketStockTransfer_Privilege)
        bDownloadPriv = SecurityProvider.Authorize(context.User, SR.MarketStockDownload_Privilege)

        btnDnLoad.Visible = bDownloadPriv
    End Sub

#End Region


    Private Sub BindDropdownList()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim cat As String = ""
        'If _PCAccessAllowed Then
        '    cat = cat & "'PC',"
        'End If
        'If _CVAccessAllowed Then
        '    cat = cat & "'CV',"
        'End If
        'If _LCVAccessAllowed Then
        '    cat = cat & "'LCV',"
        'End If
        'If cat <> "" Then
        '    cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
        '    criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        'End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih Kategori", ""))  '-- Dummy blank item

        Dim enumSR As New EnumDNET
        ddlStatus.DataSource = enumSR.RetrieveStockReport()
        ddlStatus.DataTextField = "NameType"
        ddlStatus.DataValueField = "ValType"


        ddlStatus.DataBind()
        'Dim _list1 As New ListItem("Stok", "Stok")
        'Dim _list2 As New ListItem("Terkirim", "Terkirim")
        'ddlStatus.Items.Add(_list1)
        'ddlStatus.Items.Add(_list2)
        ddlStatus.Items.Insert(0, New ListItem("Pilih Status", ""))  '-- Dummy blank item
    End Sub
#End Region

#Region " EventHandler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'btnSearch.Attributes.Add("onclick", "MakeValid();")
        btnDnLoad.Attributes.Add("onclick", "MakeValid();")
        InitiateAuthorization()
        If Not IsPostBack Then
            'icStartValid.Enabled = True
            'icEndValid.Enabled = True

            BindDropdownList()
            ViewState("currSortColumn") = "ChassisNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        'lblSearchAllocationDealer.Attributes("onclick") = "ShowPPAllocationDealerSelection();"
        lblSearchStockDealer.Attributes("onclick") = "ShowPPStockDealerSelection();"
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click


        dgStock.CurrentPageIndex = 0
        BindResult(dgStock.CurrentPageIndex, True)
    End Sub

    Private Function GetListOfLastStock(ByVal _arrStock As ArrayList) As ArrayList
        Dim objStok As New StockMovement
        Dim _list As New ArrayList
        If _arrStock.Count <> 0 Then
            If _arrStock.Count > 1 Then
                Dim i As Integer = 0
                Dim _temp1 = CType(_arrStock(i), StockMovement)
                Dim _temp2 = CType(_arrStock(i + 1), StockMovement)
                While i < _arrStock.Count - 1
                    Dim j As Integer = i + 1
                    While j <= _arrStock.Count - 1
                        If CType(_arrStock(i), StockMovement).ChassisMaster.ChassisNumber = CType(_arrStock(j), StockMovement).ChassisMaster.ChassisNumber Then
                            If j = _arrStock.Count - 1 Then
                                _list.Add(CType(_arrStock(i), StockMovement))
                            End If
                        Else
                            If j = _arrStock.Count - 1 Then
                                _list.Add(CType(_arrStock(i), StockMovement))
                            End If
                        End If
                        j = j + 1
                    End While
                    i = i + 1
                End While
            Else
                objStok = CType(_arrStock(0), StockMovement)
                _list.Add(objStok)
            End If
        End If
        Return _list
    End Function

    Private Sub BindResult(ByVal indexPage As Integer, ByVal isSearch As Boolean)
        Dim _arrResultChassis As New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, 0), "(", True)
        criterias.opOr(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.IsNull, "null"), ")", False)

        criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockStatus", MatchType.No, "X"), "(", True)
        criterias.opOr(New Criteria(GetType(ChassisMaster), "StockStatus", MatchType.IsNull, "null"), ")", False)

        'If txtStockKodeDealer.Text <> String.Empty Then
        'dealer validation
        Dim strDealer As String = String.Empty
        Dim arlStockDealer As New ArrayList
        Dim objDealer As Dealer = New SessionHelper().GetSession("DEALER")
        Dim objStockDealer As Dealer
        Dim crStockDealer As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


        Dim isAllDealer As Boolean = False
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtStockKodeDealer.Text <> String.Empty Then
                crStockDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & txtStockKodeDealer.Text.Replace(";", "','") & "')"))
                arlStockDealer = New DealerFacade(User).Retrieve(crStockDealer)
            Else
                isAllDealer = True
            End If
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim ValidDealerCode As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
            If txtStockKodeDealer.Text <> String.Empty Then
                Dim ValidFilteredDealerCode As String = String.Empty
                For Each sItem As String In txtStockKodeDealer.Text.Split(";")
                    If sItem <> String.Empty Then
                        If ValidDealerCode.IndexOf(sItem.Trim) >= 0 Then
                            ValidFilteredDealerCode += sItem + ";"
                        End If
                    End If
                Next

                If ValidFilteredDealerCode.Trim <> String.Empty Then
                    ValidFilteredDealerCode = ValidFilteredDealerCode.Substring(0, ValidFilteredDealerCode.Length - 1)
                End If

                crStockDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & ValidFilteredDealerCode.Replace(";", "','") & "')"))
                arlStockDealer = New DealerFacade(User).Retrieve(crStockDealer)
            Else
                crStockDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, ValidDealerCode))
                arlStockDealer = New DealerFacade(User).Retrieve(crStockDealer)
            End If
        Else
            arlStockDealer = New DataOwner().GetDealerExistInGroup(txtStockKodeDealer.Text, objDealer)
        End If

        If arlStockDealer.Count > 0 Then
            For idx As Integer = 0 To arlStockDealer.Count - 1
                objStockDealer = arlStockDealer(idx)
                strDealer &= objStockDealer.ID & ";"
            Next
            strDealer = Left(Trim(strDealer), Len(strDealer) - 1)
        End If

        If Not isAllDealer Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockDealer", MatchType.InSet, "('" & strDealer.Replace(";", "','") & "')"))
        End If
        'End If

        If ddlCategory.SelectedValue <> String.Empty OrElse ddlCategory.SelectedIndex <> 0 Then
            Dim objCategory As Category = New CategoryFacade(User).Retrieve(ddlCategory.SelectedValue)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ID", MatchType.Exact, objCategory.ID))
        End If

        If txtChassisNo.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.StartsWith, txtChassisNo.Text.Trim))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            If ddlStatus.SelectedValue = "Stok" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockStatus", MatchType.No, "X"), "(", True)
                criterias.opOr(New Criteria(GetType(ChassisMaster), "StockStatus", MatchType.IsNull, "null"), ")", False)
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.ID", CompareMatchType.Exact, "StockDealer", True), "(", True)
                criterias.opOr(New Criteria(GetType(ChassisMaster), "StockDealer", MatchType.Exact, 0), ")", False)
            ElseIf ddlStatus.SelectedValue = "Terkirim" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockStatus", MatchType.Exact, "X"), "(", True)
                criterias.opOr(New Criteria(GetType(ChassisMaster), "Dealer.ID", CompareMatchType.No, "StockDealer", True), "(", True)
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "StockDealer", MatchType.No, 0), "))", False)
            End If
        End If

        Dim TotalRow As Integer

        sessHelp.SetSession("CriteriaSessStockReport", criterias)

        If isSearch Then
            _arrResultChassis = New ChassisMasterFacade(User).Retrieve(criterias, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            TotalRow = _arrResultChassis.Count
            sessHelp.SetSession("DATADOWNLOAD", _arrResultChassis)
        Else
            _arrResultChassis = New ChassisMasterFacade(User).RetrieveActiveListByCriteria(criterias, dgStock.CurrentPageIndex + 1, dgStock.PageSize, TotalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        End If



        dgStock.DataSource = _arrResultChassis
        dgStock.VirtualItemCount = TotalRow
        lblTotalUnitVal.Text = TotalRow.ToString("#,##0")
        dgStock.DataBind()


    End Sub

    Private Sub dgStock_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgStock.SortCommand
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

        'BindResult(dgStock.CurrentPageIndex, False)
        Dim totalRow As Integer = 0
        Dim _arrlst As New ArrayList
        _arrlst = New ChassisMasterFacade(User).RetrieveActiveListByCriteria(CType(sessHelp.GetSession("CriteriaSessStockReport"), CriteriaComposite), dgStock.CurrentPageIndex + 1, dgStock.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dgStock.DataSource = _arrlst
        dgStock.DataBind()
        
    End Sub

    Private Function SetStatusString(ByVal objdomain As ChassisMaster) As String
        If objdomain.StockStatus = "X" Then
            Return "Terkirim"
        Else
            If objdomain.StockDealer <> objdomain.Dealer.ID And objdomain.StockDealer <> 0 Then
                Return "Terkirim"
            Else
                Return "Stok"
            End If
        End If
    End Function

    Private Sub dgStock_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgStock.ItemDataBound
        Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgStock.CurrentPageIndex * dgStock.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim lblTglStok As Label = CType(e.Item.FindControl("lblTglStok"), Label)
            If RowValue.StockDate <= New Date(1900, 1, 1) Then
                lblTglStok.Text = ""
            Else
                lblTglStok.Text = RowValue.StockDate.ToString("dd/MM/yyyy")
            End If

            Dim lblStokDealer As Label = CType(e.Item.FindControl("lblStokDealer"), Label)
            If RowValue.StockDealer > 0 And RowValue.StockStatus <> "X" Then
                Dim tmpDealer As Dealer = New DealerFacade(User).Retrieve(RowValue.StockDealer)
                If tmpDealer.ID > 0 Then
                    lblStokDealer.Text = tmpDealer.DealerCode
                    lblStokDealer.ToolTip = tmpDealer.SearchTerm1
                Else
                    lblStokDealer.Text = ""
                    lblStokDealer.ToolTip = ""
                End If
            Else
                lblStokDealer.Text = ""
                lblStokDealer.ToolTip = ""
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatus.Text = SetStatusString(RowValue)

            'customize the edit button
            Dim lnkEdit As Label = CType(e.Item.FindControl("lnkEdit"), Label)

            'get user login
            Dim objUserInfo As UserInfo = sessHelp.GetSession("LOGINUSERINFO")

            If bTransferPriv Then
                If objUserInfo.Dealer.Title <> "1" Then
                    lnkEdit.Visible = False
                Else
                    lnkEdit.Visible = True
                End If
            Else
                lnkEdit.Visible = bTransferPriv
            End If

            Dim idMov As Integer = RowValue.ID
            Dim critsMove As New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critsMove.opAnd(New Criteria(GetType(StockMovement), "ChassisMaster.ID", MatchType.Exact, idMov))
            Dim arlMovement As ArrayList = New StockMovementFacade(User).RetrieveByCriteria(critsMove)
            Dim lnkDetail As Label = CType(e.Item.FindControl("lnkDetail"), Label)
            If arlMovement.Count > 0 Then
                lnkDetail.Attributes.Add("onclick", "showPopUp('../General/../PopUp/PopUpStockMovementDetail.aspx?ID=" & RowValue.ID & "', '', 600, 760);return false;")
                lnkDetail.Visible = True
            Else
                lnkDetail.Attributes.Add("onclick", "alert('Tidak Ada pergerakan untuk No Rangka " & RowValue.ChassisNumber & "');")
                lnkDetail.Visible = False
            End If

            CType(e.Item.FindControl("lnkEdit"), Label).Attributes.Add("onclick", "showPopUp('../General/../PopUp/PopUpEntryStockMovement.aspx?ID=" & RowValue.ID & "', '', 500, 500);return false;")
        End If
    End Sub

    Private Sub dgStock_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgStock.PageIndexChanged
        dgStock.CurrentPageIndex = e.NewPageIndex
        'BindResult(dgStock.CurrentPageIndex, False)
        Dim totalRow As Integer = 0
        Dim _arrlst As New ArrayList
        _arrlst = New ChassisMasterFacade(User).RetrieveActiveListByCriteria(CType(sessHelp.GetSession("CriteriaSessStockReport"), CriteriaComposite), dgStock.CurrentPageIndex + 1, dgStock.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dgStock.DataSource = _arrlst
        dgStock.DataBind()
       
    End Sub
#End Region

    Private Sub btnDnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        SetDownload()
    End Sub
    Private Sub SetDownload()

        Dim data As ArrayList = CType(sessHelp.GetSession("DATADOWNLOAD"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "StockReport" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        '-- Temp file must be a randomly named file!
        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteTraineeData(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Sales - Unit Stock Report")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Nomor Rangka" & tab)
            itemLine.Append("Tipe/Warna" & tab)
            itemLine.Append("Nomor Mesin" & tab)
            itemLine.Append("Alokasi Dealer" & tab)
            itemLine.Append("Stok Dealer" & tab)
            itemLine.Append("Stok Dealer Desc" & tab)
            itemLine.Append("Tgl Stok" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Description" & tab)
            sw.WriteLine(itemLine.ToString())
            'lblJr.Text = 
            'lblSr.Text = GetPassDateByCategory(passClassCourse, "SENIOR")
            'lblMr.Text = GetPassDateByCategory(passClassCourse, "MASTER")
            Dim i As Integer = 1
            For Each item As ChassisMaster In data
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.ChassisNumber & tab)
                itemLine.Append(item.VechileColor.MaterialNumber & tab)
                itemLine.Append(item.EngineNumber & tab)
                itemLine.Append(item.Dealer.DealerCode & "-" & item.Dealer.DealerName & tab)

                If item.StockStatus = "X" Then
                    itemLine.Append(New DealerFacade(User).Retrieve(item.StockDealer).DealerCode & tab)
                    itemLine.Append(New DealerFacade(User).Retrieve(item.StockDealer).SearchTerm1 & tab)
                    itemLine.Append(item.StockDate.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append("Terkirim" & tab)
                Else
                    If item.StockDealer <> item.Dealer.ID And item.StockDealer <> 0 Then
                        itemLine.Append(New DealerFacade(User).Retrieve(item.StockDealer).DealerCode & tab)
                        itemLine.Append(New DealerFacade(User).Retrieve(item.StockDealer).SearchTerm1 & tab)
                        itemLine.Append(item.StockDate.ToString("dd/MM/yyyy") & tab)
                        itemLine.Append("Terkirim" & tab)
                    Else
                        itemLine.Append(item.Dealer.DealerCode & tab)
                        itemLine.Append(item.Dealer.SearchTerm1 & tab)
                        itemLine.Append(item.DODate.ToString("dd/MM/yyyy") & tab)
                        itemLine.Append("Stok" & tab)
                    End If
                End If


                itemLine.Append(item.VechileColor.ColorIndName & tab)

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub


End Class
