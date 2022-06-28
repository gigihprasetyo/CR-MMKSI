#Region "Custom Namespace Imports"
Imports KTB.DNet.Parser
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General

#End Region

Public Class frmStockTarget
    Inherits System.Web.UI.Page



#Region "Custom Variable Declaration"
    Private StockTargetArrayList As ArrayList
    Private sessionHelper As New SessionHelper

    Private currentPageMode As PageMode = PageMode.NONE

    Private Enum PageMode
        EDIT_MODE
        UPLOAD_MODE
        SAVE_MODE
        NONE
    End Enum

    Private _vstPageTotalRow As String = "_vstPageTotalRow"
    Public Property PageTotalRow() As Integer
        Get
            Dim tot As Integer = 0
            If Not IsNothing(Me.ViewState.Item(Me._vstPageTotalRow)) Then
                tot = CType(Me.ViewState.Item(Me._vstPageTotalRow), Integer)
            End If
            Return tot
        End Get
        Set(ByVal value As Integer)
            Me.ViewState.Add(Me._vstPageTotalRow, value)
        End Set
    End Property

    Private ViewStockPrivelege As Boolean = SecurityProvider.Authorize(Context.User, SR.Lihat_Stock_Ratio_Privilege)
    Private InputStockPrivelege As Boolean = SecurityProvider.Authorize(Context.User, SR.Input_Stock_Ratio_Privilege)
    Private UpdateStockPrivelege As Boolean = SecurityProvider.Authorize(Context.User, SR.Ubah_Stock_Ratio_Privilege)
    Private DeleteStockPrivelege As Boolean = SecurityProvider.Authorize(Context.User, SR.Delete_Stock_Ratio_Privilege)
    Private DownloadStockPrivelege As Boolean = SecurityProvider.Authorize(Context.User, SR.Download_Stock_Ratio_Privilege)

#End Region

#Region "Custom Method"



    Private Sub ParseFile()
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            'cek maxFileFirst
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If DataFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                'Dim _webServer As String = "172.17.104.90"
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Try
                    success = imp.Start()
                    If success Then
                        DataFile.PostedFile.SaveAs(DestFile)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    'MessageBox.Show(SR.DownloadFail(LinkButton.Text))
                End Try
                Dim parser As IExcelParser = New UploadPKStockTargetExcelParser
                StockTargetArrayList = CType(parser.ParseExcelNoTransaction(DestFile, "[Sheet1$]", "User"), ArrayList)

                sessionHelper.SetSession("StockTarget", StockTargetArrayList)
                Me.PageTotalRow = StockTargetArrayList.Count
            End If
        Else
            MessageBox.Show("Pilih Lokasi File")
        End If
    End Sub

    Private Sub CheckDataIntegrity(ByRef aSTs As ArrayList)

    End Sub

    Private Sub BindToGrid(Optional ByVal totalRow As Integer = 0) 'donas 20151209:support paging
        StockTargetArrayList = sessionHelper.GetSession("StockTarget")
        If Not ((StockTargetArrayList Is Nothing) OrElse (StockTargetArrayList.Count <= 0)) Then
            dtgStockTarget.DataSource = StockTargetArrayList
            'donas 20151209:support paging
            dtgStockTarget.VirtualItemCount = Me.PageTotalRow
            dtgStockTarget.DataBind()
            For Each item As StockTarget In StockTargetArrayList
                If item.ErrorMessage <> String.Empty Then
                    btnSimpan.Enabled = False
                    Exit Sub
                End If
            Next
        Else
            btnSimpan.Enabled = False
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then

            trBlockRow.Visible = False
            FillCategory()
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnGetDealer.Style("display") = "none"
        End If

    End Sub

    Private Sub ActivateUserPrivilege()
        If Not ViewStockPrivelege Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Stock Ratio")
        End If
        btnUpload.Visible = InputStockPrivelege
        DataFile.Disabled = Not InputStockPrivelege
        btnDownLoad.Visible = DownloadStockPrivelege
    End Sub

    Private Sub HapusRow(source As Object, e As DataGridCommandEventArgs)
        Try
            Dim aSTs As ArrayList = sessionHelper.GetSession("StockTarget")
            If Not IsNothing(aSTs) AndAlso aSTs.Count > 0 Then
                Dim oST As StockTarget = CType(aSTs(e.Item.ItemIndex), StockTarget)
                Dim oSTFac As New StockTargetFacade(User) 'a

                If oST.ValidFrom <= DateSerial(Now.Year, Now.Month, Now.Day).AddDays(1) Then
                    MessageBox.Show("Hapus Tidak Diperbolehkan. Periode Mulai Stock Ratio Sudah Berjalan")
                Else
                    oSTFac.Delete(oST)
                    Me.BindData()
                    MessageBox.Show(SR.DeleteSucces())
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try

    End Sub


    Private Sub RubahRow(source As Object, e As DataGridCommandEventArgs)
        StockTargetArrayList = sessionHelper.GetSession("StockTarget")
        Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")

        ' Dim _stockTarget As StockTarget = CType(StockTargetArrayList.Item(CType(lbl1.Text, Integer) - 1), StockTarget)
        Dim _stockTarget As StockTarget = CType(StockTargetArrayList.Item(e.Item.ItemIndex), StockTarget)
        If (Not IsNothing(_stockTarget)) Then
            currentPageMode = PageMode.EDIT_MODE
            sessionHelper.SetSession("currentPageMode", currentPageMode)
            trBlockRow.Visible = True
            btnSimpan.Enabled = True
            txtDealerCode.Text = _stockTarget.Dealer.DealerCode
            ddlCategory.SelectedValue = _stockTarget.VechileModel.Category.ID
            FillModel(ddlCategory.SelectedValue)
            ddlModel.SelectedValue = _stockTarget.VechileModel.ID
            txtTarget.Text = _stockTarget.Target.ToString
            txtStockRatio.Text = _stockTarget.TargetRatio.ToString
            ccValidDate.Value = _stockTarget.ValidFrom
            chkDealer.Checked = _stockTarget.IsDealerBlock
            chkKTB.Checked = _stockTarget.IsKTBBlock
            dtgStockTarget.EditItemIndex = e.Item.ItemIndex
            ccValidDate.Enabled = True
            chkAllValid.Checked = False


        End If
    End Sub

    Sub dtgStockTarget_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgStockTarget.ItemCommand
        Select Case (CType(e.CommandSource, LinkButton)).CommandName.Trim.ToLower()

            Case "hapus"
                'dtgStockTarget_DeleteCommandManual(source, e)
                HapusRow(source, e)
            Case "rubah"
                'dtgStockTarget_EditCommandManual(source, e)
                RubahRow(source, e)
            Case Else
                ' Do nothing.

        End Select

    End Sub

    Private Sub dtgStockTarget_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgStockTarget.SortCommand
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

        BindData()
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        btnSimpan.Enabled = True
        btnCari.Enabled = False

        btnDownLoad.Enabled = False
        dtgStockTarget.AllowSorting = False

        dtgStockTarget.AllowPaging = False
        dtgStockTarget.AllowCustomPaging = False


        ParseFile()
        BindToGrid()
        currentPageMode = PageMode.UPLOAD_MODE
        sessionHelper.SetSession("currentPageMode", currentPageMode)
        trBlockRow.Visible = False
    End Sub

    Sub dtgStockTarget_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs) Handles dtgStockTarget.ItemDataBound
        StockTargetArrayList = sessionHelper.GetSession("StockTarget")
        If Not (StockTargetArrayList.Count = 0 Or E.Item.ItemIndex = -1) Then
            Dim lblNo As Label = CType(E.Item.FindControl("lblNo"), Label)
            Dim objStockTarget As StockTarget = StockTargetArrayList(E.Item.ItemIndex)
            Dim lbtnEdit As LinkButton = CType(E.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(E.Item.FindControl("lbtnDelete"), LinkButton)
            '--in case the controls not found
            If IsNothing(lblNo) Or IsNothing(objStockTarget) Or IsNothing(lbtnEdit) Or IsNothing(lbtnDelete) Then
                Exit Sub
            End If
            lbtnDelete.Text = "<img src=""../images/trash.gif"" border=""0"" alt=""Hapus"">"
            lbtnEdit.Text = "<img src=""../images/edit.gif"" border=""0"" alt=""Edit"">"
            lblNo.Text = (E.Item.ItemIndex + 1 + (dtgStockTarget.PageSize * dtgStockTarget.CurrentPageIndex)).ToString
            Dim chkDealerBlock As CheckBox = CType(E.Item.FindControl("chkIsDealerBlock"), CheckBox)
            chkDealerBlock.Checked = objStockTarget.IsDealerBlock
            Dim chkKTBBlock As CheckBox = CType(E.Item.FindControl("chkIsKTBBlock"), CheckBox)
            chkKTBBlock.Checked = objStockTarget.IsKTBBlock


            If btnSimpan.Enabled AndAlso btnCari.Enabled = False Then
                lbtnEdit.Visible = False
                lbtnEdit.Enabled = False
                lbtnDelete.Visible = False
                lbtnDelete.Enabled = False
            Else
                lbtnEdit.Visible = Me.UpdateStockPrivelege
                lbtnEdit.Enabled = Me.UpdateStockPrivelege
                lbtnDelete.Visible = Me.DeleteStockPrivelege
                lbtnDelete.Enabled = Me.DeleteStockPrivelege
            End If
        End If


    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim errorList As StringBuilder = New StringBuilder
        StockTargetArrayList = sessionHelper.GetSession("StockTarget")
        '--upload from excel
        currentPageMode = sessionHelper.GetSession("currentPageMode")
        If (currentPageMode = PageMode.UPLOAD_MODE) Then
            For Each item As StockTarget In StockTargetArrayList
                errorList.Append(TryInsertStockTarget(item).ToString)
            Next
        ElseIf currentPageMode = PageMode.EDIT_MODE Then  'from edit
            If txtDealerCode.Text.Trim = "" Then
                MessageBox.Show("Kode Dealer tidak boleh kosong")
                Exit Sub
            End If

            Dim objStockTargetFacade As StockTargetFacade = New StockTargetFacade(User)
            Dim lbl1 As Label = dtgStockTarget.Items.Item(dtgStockTarget.EditItemIndex).Cells(0).FindControl("lblNo")
            Dim _stockTarget As StockTarget = CType(StockTargetArrayList.Item(CType(lbl1.Text, Integer) - 1), StockTarget)

            _stockTarget.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)

            _stockTarget.VechileModel = New VechileModelFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))


            _stockTarget.Target = CType(txtTarget.Text, Integer)
            _stockTarget.TargetRatio = CType(txtStockRatio.Text, Decimal)
            _stockTarget.ValidFrom = ccValidDate.Value
            _stockTarget.IsDealerBlock = chkDealer.Checked
            _stockTarget.IsKTBBlock = chkKTB.Checked
            Try
                objStockTargetFacade.Update(_stockTarget)
            Catch ex As Exception
                errorList.Append("Update failed ID: #" & _stockTarget.ID & "#")
            End Try
            dtgStockTarget.EditItemIndex = -1
        ElseIf currentPageMode = PageMode.SAVE_MODE Then
            If txtDealerCode.Text.Trim = "" Then
                MessageBox.Show("Kode Dealer tidak boleh kosong")
                Exit Sub
            End If
            If txtTarget.Text.Trim = "" Then
                MessageBox.Show("Stock Target tidak boleh kosong")
                Exit Sub
            End If
            If txtStockRatio.Text.Trim = "" Then
                MessageBox.Show("Stock Ratio tidak boleh kosong")
                Exit Sub
            End If
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            Dim _dealerList As ArrayList = New DealerFacade(User).Retrieve(criterias)

            If IsNothing(_dealerList) OrElse _dealerList.Count = 0 Then
                MessageBox.Show("Kode Dealer tidak valid")
                Exit Sub
            End If
            For Each _dealer As Dealer In _dealerList
                Dim _stockTarget As New StockTarget
                _stockTarget.Dealer = _dealer

                _stockTarget.VechileModel = New VechileModelFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))


                _stockTarget.Target = CType(txtTarget.Text, Integer)
                _stockTarget.TargetRatio = CType(txtStockRatio.Text, Decimal)
                _stockTarget.ValidFrom = ccValidDate.Value
                _stockTarget.IsDealerBlock = chkDealer.Checked
                _stockTarget.IsKTBBlock = chkKTB.Checked
                _stockTarget.RowStatus = DBRowStatus.Active

                errorList.Append(TryInsertStockTarget(_stockTarget).ToString)
            Next

        End If
        If errorList.Length > 0 Then
            MessageBox.Show(SR.SaveFail & " : " & errorList.ToString)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        sessionHelper.RemoveSession("StockTarget")
        btnSimpan.Enabled = False
        btnCari.Enabled = True
        dtgStockTarget.DataSource = Nothing
        dtgStockTarget.DataBind()
        StockTargetArrayList = Nothing

        currentPageMode = PageMode.NONE
        sessionHelper.RemoveSession("currentPageMode")
        trBlockRow.Visible = False
        ddlCategory.SelectedValue = -1
        FillModel(-1)
        txtDealerCode.Text = ""
        txtStockRatio.Text = ""
        txtTarget.Text = ""

    End Sub
    Private Function TryInsertStockTarget(item As StockTarget) As StringBuilder
        Dim errorList As New StringBuilder

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "VechileModel.Description", MatchType.Exact, item.VechileModel.Description))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "ValidFrom", MatchType.Exact, item.ValidFrom))
        Dim objArrList As ArrayList = New StockTargetFacade(User).Retrieve(criterias)
        If (objArrList Is Nothing) OrElse (objArrList.Count <= 0) Then
            Dim objStockTargetFacade As StockTargetFacade = New StockTargetFacade(User)
            Try
                objStockTargetFacade.Insert(item)
            Catch ex As Exception
                errorList.Append("Insert failed Dealer code: #" & item.Dealer.DealerCode & "#")
            End Try
        Else
            item.ID = objArrList(0).id
            Dim objStockTargetFacade As StockTargetFacade = New StockTargetFacade(User)
            Try
                objStockTargetFacade.Update(item)
            Catch ex As Exception
                errorList.Append("#Update failed ID: " & item.ID & "#")
            End Try
        End If
        Return errorList
    End Function
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        'btnSimpan.Enabled = False
        '--set to none page mode
        dtgStockTarget.AllowSorting = True
        btnSimpan.Enabled = False
        trBlockRow.Visible = False
        currentPageMode = PageMode.NONE
        sessionHelper.SetSession("currentPageMode", currentPageMode)
        dtgStockTarget.EditItemIndex = -1


        dtgStockTarget.AllowPaging = True
        dtgStockTarget.AllowCustomPaging = True
        dtgStockTarget.CurrentPageIndex = 0

        '--remove all datas
        dtgStockTarget.DataSource = Nothing
        dtgStockTarget.DataBind()

        sessionHelper.RemoveSession("StockTarget")

        '--start search query and bind new data to grid
        BindData()
        If dtgStockTarget.Items.Count > 0 Then
            btnDownLoad.Enabled = True
        Else
            btnDownLoad.Enabled = False
        End If
    End Sub

    Private Function BindData(Optional ByVal IsForDownload = False) As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (Not String.IsNullOrEmpty(txtDealerCode.Text)) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If

        criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.ID", MatchType.No, 1))
        criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.Title", MatchType.Exact, "0"))
        criterias.opAnd(New Criteria(GetType(StockTarget), "Dealer.Status", MatchType.Exact, "1"))

        If (ddlCategory.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "VechileModel.Category.ID", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
        End If
        If (ddlModel.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "VechileModel.ID", MatchType.Exact, CType(ddlModel.SelectedValue, Integer)))
        End If

        If (Not String.IsNullOrEmpty(txtTarget.Text)) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "Target", MatchType.Exact, CType(txtTarget.Text.Trim, Integer)))
        End If
        If (Not String.IsNullOrEmpty(txtStockRatio.Text)) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "TargetRatio", MatchType.Exact, txtStockRatio.Text.Trim.Replace(",", ".")))
        End If
        If (Not chkAllValid.Checked) Then
            criterias.opAnd(New Criteria(GetType(StockTarget), "ValidFrom", MatchType.GreaterOrEqual, ccValidDate.Value))

        End If

        'criterias.opAnd(New Criteria(GetType(StockTarget), "IsDealerBlock", MatchType.Exact, CType(chkDealer.Checked, Byte)))
        'criterias.opAnd(New Criteria(GetType(StockTarget), "IsKTBBlock", MatchType.Exact, CType(chkKTB.Checked, Byte)))

        'StockTargetArrayList = New StockTargetFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        Dim totRow As Integer = 0
        If IsForDownload Then
            StockTargetArrayList = New StockTargetFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Return StockTargetArrayList
        Else
            StockTargetArrayList = New StockTargetFacade(User).RetrieveActiveList(criterias, (Me.dtgStockTarget.CurrentPageIndex + 1), Me.dtgStockTarget.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If
        sessionHelper.SetSession("StockTarget", StockTargetArrayList)
        Me.PageTotalRow = totRow 'donas 20151209:support paging
        If StockTargetArrayList.Count > 0 Then
            BindToGrid()
        Else
            dtgStockTarget.DataSource = StockTargetArrayList
            dtgStockTarget.DataBind()
            MessageBox.Show("Data Stock Ratio Tidak Ditemukan")
        End If
        Return Nothing
    End Function

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownLoad.Click

        Try
            Dim aSTs As ArrayList = Me.BindData(True)

            DoDownload(aSTs)
            aSTs.Clear()
            aSTs = Nothing
            GC.Collect()
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub

    Private Function StockTargetTransferData() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim oExArgs As New System.Collections.ArrayList
        Dim objStockTarget As New StockTargetFacade(User)

        For Each oDataGridItem In dtgStockTarget.Items
            Dim _StockTarget As New KTB.DNet.Domain.StockTarget
            _StockTarget.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
            _StockTarget = objStockTarget.Retrieve(_StockTarget.ID)
            oExArgs.Add(_StockTarget)
        Next
        Return oExArgs
    End Function
    Private Sub DoDownload(ByVal arlDPK As ArrayList)
        Dim sFileName As String
        sFileName = "StockRatio" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ""

        '-- Temp file must be a randomly named file!
        Dim oFile As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(oFile)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(oFile, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDataToExcell(sw, arlDPK)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByVal arlDPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DAFTAR STOCK RATIO")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        If (arlDPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("N0" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Model" & tab)
            itemLine.Append("Valid from(YYYYMMDD)" & tab)
            itemLine.Append("Target" & tab)
            itemLine.Append("Stok Ratio" & tab)
            itemLine.Append("Blok Dari Dealer" & tab)
            itemLine.Append("Blok Dari MKS" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As StockTarget In arlDPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    If Not IsNothing(item.VechileModel) Then
                        itemLine.Append(item.VechileModel.Category.Description.ToString & tab)
                        itemLine.Append(item.VechileModel.Description.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.ValidFrom) Then
                        itemLine.Append(item.ValidFrom.ToString("yyyyMMdd") & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.Target) Then
                        itemLine.Append(item.Target.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.TargetRatio) Then
                        itemLine.Append(item.TargetRatio.ToString() & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.IsDealerBlock) AndAlso item.IsDealerBlock Then
                        itemLine.Append("Ya" & tab)
                    Else
                        itemLine.Append("Tidak" & tab)
                    End If
                    If Not IsNothing(item.IsKTBBlock) AndAlso item.IsKTBBlock Then
                        itemLine.Append("Ya" & tab)
                    Else
                        itemLine.Append("Tidak" & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub
    Private Sub FillCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList(companyCode)
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        FillModel(-1)
    End Sub


    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
    End Sub

    Sub chkAllValid_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllValid.CheckedChanged
        ccValidDate.Enabled = chkAllValid.Checked.Equals(False)

    End Sub
#End Region

    Protected Sub btnGetDealer_Click(sender As Object, e As EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        End If
    End Sub

    Protected Sub chkIsKTBBlockAll_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As DataGridItem In dtgStockTarget.Items
            Dim chkbox As CheckBox = CType(row.FindControl("chkIsKTBBlock"), CheckBox)
            If (chkbox IsNot Nothing) Then
                chkbox.Checked = CType(sender, CheckBox).Checked
            End If
        Next

    End Sub

    Protected Sub chkIsDealerBlockAll_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As DataGridItem In dtgStockTarget.Items
            Dim chkbox As CheckBox = row.FindControl("chkIsDealerBlock")
            If (chkbox IsNot Nothing) Then
                chkbox.Checked = CType(sender, CheckBox).Checked
            End If
        Next

    End Sub

    Protected Sub btnBlock_Click(sender As Object, e As EventArgs) Handles btnBlock.Click
        Dim errorList As New StringBuilder
        For Each row As DataGridItem In dtgStockTarget.Items
            Dim _stockTarget As StockTarget = CType(row.DataItem, StockTarget)


            Dim arrStockTarget As ArrayList = sessionHelper.GetSession("StockTarget")

            If Not IsNothing(arrStockTarget) AndAlso arrStockTarget.Count > 0 Then

                '' For Each Objstock As StockTarget In arrStockTarget
                _stockTarget = CType(arrStockTarget(row.ItemIndex), StockTarget)
                Dim objStockTargetFacade As StockTargetFacade = New StockTargetFacade(User)
                _stockTarget.IsKTBBlock = CType(row.FindControl("chkIsKTBBlock"), CheckBox).Checked
                _stockTarget.IsDealerBlock = CType(row.FindControl("chkIsDealerBlock"), CheckBox).Checked
                objStockTargetFacade.Update(_stockTarget)
                '   Next

            End If


            'If (_stockTarget IsNot Nothing) Then
            '    Dim objStockTargetFacade As StockTargetFacade = New StockTargetFacade(User)
            '    Try
            '        objStockTargetFacade.Update(_stockTarget)
            '    Catch ex As Exception
            '        errorList.Append("#Update failed ID: " & _stockTarget.ID & "#")
            '    End Try
            'End If
        Next
        If errorList.Length > 0 Then
            MessageBox.Show(SR.SaveFail & " : " & errorList.ToString)
        Else
            MessageBox.Show(SR.SaveSuccess)
            btnCari_Click(Me, Nothing)
        End If
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModel.SelectedIndexChanged
        If (sessionHelper.GetSession("currentPageMode") IsNot Nothing) Then
            currentPageMode = sessionHelper.GetSession("currentPageMode")

        End If
        If (ddlCategory.SelectedValue = -1) Then
            If (currentPageMode <> PageMode.EDIT_MODE) Then
                currentPageMode = PageMode.NONE
                sessionHelper.SetSession("currentPageMode", currentPageMode)

            End If

            btnSimpan.Enabled = False
        Else
            If (currentPageMode <> PageMode.EDIT_MODE) Then
                currentPageMode = PageMode.SAVE_MODE
                sessionHelper.SetSession("currentPageMode", currentPageMode)
                trBlockRow.Visible = True
            End If
            btnSimpan.Enabled = True
        End If
    End Sub

    Private Sub dtgStockTarget_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgStockTarget.PageIndexChanged
        dtgStockTarget.CurrentPageIndex = e.NewPageIndex
        Me.BindData()
    End Sub
End Class