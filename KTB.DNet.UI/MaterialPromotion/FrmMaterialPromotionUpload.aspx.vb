#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Parser
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmMaterialPromotionUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblFile As System.Web.UI.WebControls.Label
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblYearPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartMonthPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndMonthPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblYearPeriodVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartMonthPeriodVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndMonthPeriodVal As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblPeriodex As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents txtPeriodName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents txtPeriodID As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblError2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Private sHelper As SessionHelper = New SessionHelper
    Private totalRow As Integer = 0
    Private arlToDisplay As ArrayList = New ArrayList
    Private srcfile As String
    Private Const VALID_FILE_TYPE = "EXCEL"
    Private Const VALID_FILE_TYPE2 = "OCTET-STREAM"

#End Region

#Region "Event Handler"
    'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "YearPeriod", MatchType.Exact, sTemp))
    '        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "StartMonth", MatchType.Exact, sTemp))
    '        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "EndMonth", MatchType.Exact, sTemp))


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        InitiateAuthorization()
        If Not IsPostBack Then
            sHelper.SetSession("ItemError", 0)
        End If
        If CheckBtnPriv() Then
            fileUpload.Visible = True
            btnSave.Visible = True
            btnUpload.Visible = True
            btnCancel.Visible = True
        Else
            fileUpload.Visible = False
            btnSave.Visible = False
            btnUpload.Visible = False
            btnCancel.Visible = False
        End If

        txtPeriodName.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        sHelper.SetSession("ItemError", 0)
        sHelper.SetSession("GoodCount", 0)

        sHelper.SetSession("ErrMessage1", "")
        sHelper.SetSession("ErrMessage2", "")

        Dim NamaFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)
        If txtPeriodName.Text.Trim = "" Then
            MessageBox.Show("Pilih Periode yang akan di-upload.")
            dgAlokasi.DataSource = Nothing
            dgAlokasi.DataBind()
            Return
        End If
        If (fileUpload.PostedFile Is Nothing) OrElse fileUpload.PostedFile.ContentLength <= 0 Then
            MessageBox.Show("Pilih file yang akan di-upload.")
            dgAlokasi.DataSource = Nothing
            dgAlokasi.DataBind()
            Return
        Else
            If fileUpload.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                MessageBox.Show("Bukan file EXCEL. File anda " & fileUpload.PostedFile.ContentType)
                Return

            Else
                'cek maxFileSize First
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fileUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                Else
                    Dim filename As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                    Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & filename
                    Dim arlParseResult As New ArrayList

                    If IsFileExist(targetFile) Then
                        MessageBox.Show(SR.UploadFail("MaterialAllocation"))
                        Return
                    End If

                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        'MessageBox.Show("Temporary out of service :)")
                        'Return
                        SavingToFolder(targetFile, fileUpload.PostedFile)

                        Dim objUploadMPAParser As UploadMaterialPromotionAllocationParser = New UploadMaterialPromotionAllocationParser
                        Dim arSheet As ArrayList

                        arSheet = objUploadMPAParser.GetSheet(targetFile)
                        arlParseResult = objUploadMPAParser.ParsingExcel(targetFile, "[" & CType(arSheet(0), String) & "]", "User")
                        'arlParseResult = objUploadMPAParser.ParsingExcel(targetFile, CType(arSheet(0), String), "User")

                        Dim arlHeader As New ArrayList

                        arlHeader = objUploadMPAParser.ParsingExcelHeader(targetFile, "[" & CType(arSheet(0), String) & "]", "User")

                        'check : goodno sequence validation
                        Dim Total(19) As Long
                        Dim i As Integer
                        For i = 0 To 19
                            Total(i) = 0
                        Next
                        For Each dt As MaterialPromotionUpload In arlParseResult
                            Total(0) = Total(0) + dt.GoodNo1
                            Total(1) = Total(1) + dt.GoodNo2
                            Total(2) = Total(2) + dt.GoodNo3
                            Total(3) = Total(3) + dt.GoodNo4
                            Total(4) = Total(4) + dt.GoodNo5
                            Total(5) = Total(5) + dt.GoodNo6
                            Total(6) = Total(6) + dt.GoodNo7
                            Total(7) = Total(7) + dt.GoodNo8
                            Total(8) = Total(8) + dt.GoodNo9
                            Total(9) = Total(9) + dt.GoodNo10
                            Total(10) = Total(10) + dt.GoodNo11
                            Total(11) = Total(11) + dt.GoodNo12
                            Total(12) = Total(12) + dt.GoodNo13
                            Total(13) = Total(13) + dt.GoodNo14
                            Total(14) = Total(14) + dt.GoodNo15
                            Total(15) = Total(15) + dt.GoodNo16
                            Total(16) = Total(16) + dt.GoodNo17
                            Total(17) = Total(17) + dt.GoodNo18
                            Total(18) = Total(18) + dt.GoodNo19
                            Total(19) = Total(19) + dt.GoodNo20
                        Next
                        Dim IsAllowedZero As Boolean = True
                        Dim MaxValuedGoodNoIdx As Integer = 19
                        Dim strError As String = ""
                        Dim tmpStr As String

                        For i = 19 To 0 Step -1
                            If Total(i) > 0 Then
                                IsAllowedZero = False
                            Else
                                If Not IsAllowedZero Then
                                    MessageBox.Show("Kode barang harus berurut dari paling kiri.")
                                    Return
                                Else
                                    MaxValuedGoodNoIdx = i - 1
                                End If
                            End If
                        Next
                        sHelper.SetSession("GoodCount", MaxValuedGoodNoIdx + 1)
                        For i = 2 To arlHeader.Count - 1 + 1
                            tmpStr = CStr(arlHeader.Item(i - 1)).Trim
                            dgAlokasi.Columns(i).HeaderText = tmpStr
                            If Not IsExistingGoodNo(tmpStr) Then
                                dgAlokasi.Columns(i).HeaderStyle.BackColor = System.Drawing.Color.Red
                                'dgAlokasi.Columns(i).ItemStyle.BackColor = Color.Red
                                strError = strError & IIf(strError <> "", ", ", "") & tmpStr
                            End If
                        Next
                        If strError.Trim <> "" Then
                            lblError.Text = "Kode barang : " & strError & " tidak valid"
                            sHelper.SetSession("ErrMessage1", "Kode barang : " & strError & " tidak valid")
                        End If

                        strError = ""
                        arlParseResult = objUploadMPAParser.ParsingExcel(targetFile, "[" & CType(arSheet(0), String) & "]", "User")
                        i = 0
                        For Each dt As MaterialPromotionUpload In arlParseResult
                            tmpStr = dt.KodeDealer.Trim
                            If Not IsExistingDealerCode(tmpStr) Then
                                strError = strError & IIf(strError <> "", ", ", "") & tmpStr
                                'dgAlokasi.Items(i).Cells(1).BackColor = Color.Red
                            End If
                            i = i + 1
                        Next

                        If strError.Trim <> "" Then
                            lblError2.Text = "Kode Dealer : " & strError & " tidak valid"
                            sHelper.SetSession("ErrMessage2", "Kode Dealer : " & strError & " tidak valid")
                        End If
                        lblError2.Visible = True
                        If lblError.Text.Trim <> "" Or lblError2.Text.Trim <> "" Then
                            sHelper.SetSession("ItemError", CInt(sHelper.GetSession("ItemError")) + 1)
                        End If
                        dgAlokasi.DataSource = arlParseResult
                        dgAlokasi.DataBind()

                        btnSave.Enabled = True
                    Catch
                        Throw
                    Finally
                        sapImp.StopImpersonate()
                    End Try
                End If
            End If
        End If
    End Sub


    Private Function IsExistingGoodNo(ByVal pGoodNo As String) As Boolean
        Dim crtMP As CriteriaComposite
        Dim arlMP As ArrayList
        Dim i As Integer

        crtMP = New CriteriaComposite(New Criteria(GetType(MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtMP.opAnd(New Criteria(GetType(MaterialPromotion), "GoodNo", MatchType.Exact, pGoodNo))
        arlMP = New MaterialPromotionFacade(User).Retrieve(crtMP)
        If arlMP.Count <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function IsExistingDealerCode(ByVal pDealerCode As String) As Boolean
        Dim crtDealer As CriteriaComposite
        Dim arlDealer As ArrayList
        Dim i As Integer

        crtDealer = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, pDealerCode))
        arlDealer = New DealerFacade(User).Retrieve(crtDealer)
        If arlDealer.Count <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function



    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If CInt(sHelper.GetSession("ItemError")) > 0 Then
            MessageBox.Show("Masih Ada Kesalahan Data")
            lblError.Text = CStr(sHelper.GetSession("ErrMessage1"))
            lblError2.Text = CStr(sHelper.GetSession("ErrMessage2"))
            Exit Sub
        End If

        If dgAlokasi.Items.Count > 0 Then
            Dim ArlToInsert As ArrayList = New ArrayList
            Dim tmp As MaterialPromotionPeriod

            Dim i As Integer
            Dim criteriaPeriod As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaPeriod.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "PeriodName", MatchType.Exact, txtPeriodName.Text))
            Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criteriaPeriod)
            Dim objPeriod As MaterialPromotionPeriod = arlPeriod(0)
            Dim TotalQty As Integer = 0


            'Dim objUploadMPAParser As UploadMaterialPromotionAllocationParser = New UploadMaterialPromotionAllocationParser
            'Dim arSheet As ArrayList
            'Dim arlHeader As New ArrayList
            'arlHeader = objUploadMPAParser.ParsingExcelHeader(targetFile, "[" & CType(arSheet(0), String) & "]", "User")



            'check stock
            For i = 2 To CInt(sHelper.GetSession("GoodCount")) + 1 ' dgAlokasi.Columns.Count - 1
                TotalQty = 0
                Dim crtBarang As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtBarang.opAnd(New Criteria(GetType(MaterialPromotion), "GoodNo", MatchType.Exact, dgAlokasi.Columns(i).HeaderText.Trim))
                Dim arlBarang As ArrayList = New MaterialPromotionFacade(User).Retrieve(crtBarang)
                Dim objBarang As MaterialPromotion = arlBarang(0)
                TotalQty = 0
                For Each itemalokasi As DataGridItem In dgAlokasi.Items
                    Dim lblQty As Label = itemalokasi.FindControl("lblGoodNo" & (i - 1))
                    TotalQty = TotalQty + Val(lblQty.Text.ToString)
                Next
                If TotalQty > objBarang.Stock Then
                    MessageBox.Show("Alokasi Material " & objBarang.GoodNo & " Melebihi Batas.")
                    Return
                End If
            Next

            'inserting
            Dim RowNum As Integer = 1
            For Each ItemAlokasi As DataGridItem In dgAlokasi.Items
                Dim lblKodeDealer As Label = itemalokasi.FindControl("lblKodeDealer")
                Dim crtDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtDealer.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, lblKodeDealer.Text.Trim))
                Dim arlDealer As ArrayList = New DealerFacade(User).Retrieve(crtDealer)
                Dim objDealer As Dealer = arlDealer(0)

                For i = 2 To CInt(sHelper.GetSession("GoodCount")) + 1 'dgAlokasi.Columns.Count - 1
                    Dim crtBarangI As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtBarangI.opAnd(New Criteria(GetType(MaterialPromotion), "GoodNo", MatchType.Exact, dgAlokasi.Columns(i).HeaderText.Trim))
                    Dim arlBarangI As ArrayList = New MaterialPromotionFacade(User).Retrieve(crtBarangI)
                    Dim objBarangI As MaterialPromotion = arlBarangI(0)

                    Dim lblQtyI As Label = ItemAlokasi.FindControl("lblGoodNo" & (i - 1))

                    Dim objAlokasi As MaterialPromotionAllocation
                    objAlokasi = New MaterialPromotionAllocation
                    objAlokasi.MaterialPromotionPeriod = objPeriod
                    objAlokasi.Dealer = objDealer
                    objAlokasi.MaterialPromotion = objBarangI
                    objAlokasi.Qty = Val(lblQtyI.Text.ToString)

                    ArlToInsert.Add(objAlokasi)
                Next
                RowNum = RowNum + 1
            Next

            If ArlToInsert.Count > 0 Then
                Dim result As Integer = New MaterialPromotionAllocationFacade(User).InsertUpdateDeleteTransaction(ArlToInsert, True)
                If result = 1 Then
                    MessageBox.Show(SR.SaveSuccess)
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        End If

        'Old Script
        'If dgAlokasi.Items.Count > 0 Then
        '    Dim ArlToInsert As ArrayList = New ArrayList
        '    Dim QtyAlokasi As Integer = 0
        '    Dim objKodeBarang As String = String.Empty
        '    Dim objBarang As MaterialPromotion

        '    For Each itemAlokasi As DataGridItem In dgAlokasi.Items
        '        Dim lblDealer As Label = itemAlokasi.FindControl("lblDealer")
        '        Dim lblGoodsNo As Label = itemAlokasi.FindControl("lblGoodsNo")
        '        Dim lblStock As Label = itemAlokasi.FindControl("lblStock")
        '        Dim lblAllocation As Label = itemAlokasi.FindControl("lblAllocation")
        '        Dim lblPeriodx As Label = itemAlokasi.FindControl("lblPeriodx")

        '        Dim criteriaPeriod As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        criteriaPeriod.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "PeriodName", MatchType.Exact, lblPeriodx.Text))
        '        Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criteriaPeriod)
        '        Dim objPeriod As MaterialPromotionPeriod = arlPeriod(0)

        '        If objKodeBarang = "" Then
        '            objBarang = New MaterialPromotionFacade(User).Retrieve(lblGoodsNo.Text.ToString)
        '            objKodeBarang = lblGoodsNo.Text.ToString
        '        ElseIf objKodeBarang <> lblGoodsNo.Text.ToString Then
        '            MessageBox.Show("Upload hanya untuk satu jenis barang")
        '            Return
        '        End If

        '        If lblGoodsNo.Text = objBarang.GoodNo Then

        '            Dim Stock As Integer = objBarang.Stock
        '            Dim criteriasAlloc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", MatchType.Exact, lblDealer.Text))
        '            criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.GoodNo", MatchType.Exact, lblGoodsNo.Text))
        '            criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, objPeriod.ID))

        '            Dim arlAlloc As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criteriasAlloc)

        '            Dim objalokasi As MaterialPromotionAllocation

        '            If arlAlloc.Count > 0 Then
        '                objalokasi = arlAlloc(0)

        '                QtyAlokasi = QtyAlokasi + Val(lblAllocation.Text) - objalokasi.Qty
        '            Else
        '                objalokasi = New MaterialPromotionAllocation
        '                QtyAlokasi = QtyAlokasi + Val(lblAllocation.Text)
        '            End If

        '            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(lblDealer.Text)
        '            If Not IsNothing(objDealer) Then
        '                objalokasi.Dealer = objDealer
        '            End If
        '            objalokasi.MaterialPromotion = objBarang
        '            objalokasi.MaterialPromotionPeriod = objPeriod

        '            If QtyAlokasi > Stock Then
        '                MessageBox.Show("Alokasi Material " & objBarang.GoodNo & " Melebihi Batas.")
        '                Return
        '            End If

        '            'If objalokasi.Qty <> Val(lblAllocation.Text) Then
        '            objalokasi.Qty = Val(lblAllocation.Text)
        '            'End If
        '            ArlToInsert.Add(objalokasi)
        '        End If


        '    Next

        '    If ArlToInsert.Count > 0 Then
        '        Dim result As Integer = New MaterialPromotionAllocationFacade(User).InsertUpdateDeleteTransaction(ArlToInsert, True)
        '        If result = 1 Then
        '            MessageBox.Show(SR.SaveSuccess)
        '        Else
        '            MessageBox.Show(SR.SaveFail)
        '        End If
        '    End If

        'Else
        '    MessageBox.Show("Upload data terlebih dahulu")
        'End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'fileUpload.Value = String.Empty
        btnSave.Enabled = False
        lblPeriodeVal.Text = String.Empty
        lblStartMonthPeriodVal.Text = String.Empty
        lblEndMonthPeriodVal.Text = String.Empty
        lblYearPeriodVal.Text = String.Empty
        dgAlokasi.DataSource = Nothing
        dgAlokasi.DataBind()
    End Sub

    'Private Sub dgAlokasi_ItemDataBoundxxx(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    Private Sub dgAlokasi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objUpload As KTB.DNet.Parser.MaterialPromotionUpload = e.Item.DataItem
            Dim KodeDealer As String = ""
            KodeDealer = objUpload.KodeDealer.Trim
            If Not IsExistingDealerCode(KodeDealer) Then
                'e.Item.BackColor = System.Drawing.Color.Red
                e.Item.Cells(1).BackColor = System.Drawing.Color.Red
            End If
        End If

        Exit Sub
        '--Old Script
        'If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
        '    Dim objUpload As KTB.DNet.Parser.MaterialPromotionUpload = e.Item.DataItem
        '    If objUpload.ErrorMessage <> "" Then
        '        e.Item.BackColor = System.Drawing.Color.Red
        '        sHelper.SetSession("ItemError", CInt(sHelper.GetSession("ItemError")) + 1)
        '    End If

        '    Dim lblPeriodx As Label = CType(e.Item.FindControl("lblPeriodx"), Label)
        '    Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        '    Dim lblGoodsNo As Label = CType(e.Item.FindControl("lblGoodsNo"), Label)

        '    Dim lblAllocation As Label = CType(e.Item.FindControl("lblAllocation"), Label)
        '    Dim lblCurrentAllocation As Label = CType(e.Item.FindControl("lblCurrentAllocation"), Label)
        '    Dim lblDifferent As Label = CType(e.Item.FindControl("lblDifferent"), Label)

        '    Dim criteriaPeriod As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criteriaPeriod.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "PeriodName", MatchType.Exact, lblPeriodx.Text))
        '    Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criteriaPeriod)
        '    Dim objPeriod As MaterialPromotionPeriod

        '    If arlPeriod.Count > 0 Then
        '        objPeriod = arlPeriod(0)
        '    Else
        '        MessageBox.Show("Periode not found " + lblPeriodx.Text)
        '        Return
        '    End If

        '    Dim criteriasAlloc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", MatchType.Exact, lblDealer.Text))
        '    criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.GoodNo", MatchType.Exact, lblGoodsNo.Text))
        '    criteriasAlloc.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, objPeriod.ID))

        '    Dim arlAlloc As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criteriasAlloc)

        '    If arlAlloc.Count > 0 Then
        '        Dim objMaterialPromotionAllocation As MaterialPromotionAllocation = arlAlloc(0)
        '        lblCurrentAllocation.Text = objMaterialPromotionAllocation.Qty
        '        lblDifferent.Text = Val(lblAllocation.Text) - objMaterialPromotionAllocation.Qty
        '    Else
        '        lblCurrentAllocation.Text = 0
        '        lblDifferent.Text = lblAllocation.Text
        '    End If

        'End If

    End Sub

#End Region

#Region "Custom Method"

    Private Function IsFileExist(ByVal filename As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

        Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        sapImp.Start()

        Try
            '---Mode using transfer file---
            'Dim objUpload As TransferFile = New TransferFile(_user, _password, _sapServer)
            'objUpload.copyFile(filename, newFolder, False)

            '---Mode using sapImpersonate---
            Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)

            'Return fInfo.Exists
            If fInfo.Exists Then
                fInfo.Delete()
                Return False
            End If

        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(filename)))
        Finally
            sapImp.StopImpersonate()
        End Try
    End Function

    Private Sub SavingToFolder(ByVal targetFile As String, ByVal postedFile As HttpPostedFile)

        Try
            'Dim directoryFile As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(RootDestDir)
            'If Not directoryFile.Exists Then
            '    directoryFile.Create()
            'End If

            postedFile.SaveAs(targetFile)
            'fInfo.MoveTo(targetFile)

            Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
            If Not trgInfo.Exists Then
                Throw New IO.IOException("File gagal disimpan di Server. Harap hubungi Administrator")
            End If
        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(targetFile)))
        End Try
    End Sub


    Private Function uploadToServer() As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadAllocMatPromotion") & "\Upload" & srcfile   '-- Destination file
                Dim finfo As FileInfo = New FileInfo(DestFile)
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                fileUpload.PostedFile.SaveAs(DestFile)
            End If
            imp.StopImpersonate()
            imp = Nothing
            Return True
        Catch ex As Exception
            lblError.Text = "Gagal Impersonate"
            lblError.Visible = True
            Return False
        End Try


    End Function

    Private Sub parserData()

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionAllocationViewUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Upload Alokasi Material Promosi")
        End If
    End Sub

    Private Function CheckBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionAllocationUploadFile_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region




End Class
