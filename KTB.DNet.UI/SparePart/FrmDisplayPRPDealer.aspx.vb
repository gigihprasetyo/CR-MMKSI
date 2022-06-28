Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Parser.ExcelParser
Imports KTB.DNet.Parser.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Namespace KTB.DNet.UI.SparePart

    Public Class FrmDisplayPRPDealer
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents dtgExcel As System.Web.UI.WebControls.DataGrid
        Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
        Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
        Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label

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
        Dim sHPrp As SessionHelper = New SessionHelper
        Private RootDestDir As String = KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory")
        Private userInfo As Dealer
        Private objDealer As Dealer
        Private _totJan As Integer = 0
        Private _totFeb As Integer = 0
        Private _totMar As Integer = 0
        Private _totApr As Integer = 0
        Private _totMay As Integer = 0
        Private _totJun As Integer = 0
        Private _totJul As Integer = 0
        Private _totAug As Integer = 0
        Private _totSep As Integer = 0
        Private _totOct As Integer = 0
        Private _totNov As Integer = 0
        Private _totDec As Integer = 0
        Private _totTot As Integer = 0
#End Region

#Region "Event Method"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load            
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            objDealer = CType(sHPrp.GetSession("DEALER"), Dealer)
            If Not IsPostBack Then
                initiatePage()
                Try
                    btnDownload.Visible = False
                    bindDataGrid()
                    'Response.Write( _
                    '    New System.Text.StringBuilder("<script language='javascript'>"). _
                    '        Append("function downloadPRP()"). _
                    '        Append("{	window.open('./downloadPRP.aspx','_blank','fullscreen=no,menubar=yes,status=yes,titlebar=yes,toolbar=no,height=480,width=640,resizable=yes');	}"). _
                    '        Append("</script>").ToString _
                    '    )
                    btnDownload.Visible = True
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End Sub

        Private Sub dtgExcel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgExcel.ItemDataBound
            Const CellJan As Integer = 5
            Const CellFeb As Integer = 6
            Const CellMar As Integer = 7
            Const CellApr As Integer = 8
            Const CellMay As Integer = 9
            Const CellJun As Integer = 10
            Const CellJul As Integer = 11
            Const CellAug As Integer = 12
            Const CellSep As Integer = 13
            Const CellOct As Integer = 14
            Const CellNov As Integer = 15
            Const CellDec As Integer = 16
            Const CellTot As Integer = 17

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgExcel.CurrentPageIndex * dtgExcel.PageSize)
            ElseIf e.Item.ItemType = ListItemType.Footer Then
                SumData()
                e.Item.Cells(CellJan).Text = Format(_totJan, "#,###")
                e.Item.Cells(CellFeb).Text = Format(_totFeb, "#,###")
                e.Item.Cells(CellMar).Text = Format(_totMar, "#,###")
                e.Item.Cells(CellApr).Text = Format(_totApr, "#,###")
                e.Item.Cells(CellMay).Text = Format(_totMay, "#,###")
                e.Item.Cells(CellJun).Text = Format(_totJun, "#,###")
                e.Item.Cells(CellJul).Text = Format(_totJul, "#,###")
                e.Item.Cells(CellAug).Text = Format(_totAug, "#,###")
                e.Item.Cells(CellSep).Text = Format(_totSep, "#,###")
                e.Item.Cells(CellOct).Text = Format(_totOct, "#,###")
                e.Item.Cells(CellNov).Text = Format(_totNov, "#,###")
                e.Item.Cells(CellDec).Text = Format(_totDec, "#,###")
                e.Item.Cells(CellTot).Text = Format(_totTot, "#,###")
            End If
        End Sub

        Private Sub dtgExcel_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgExcel.PageIndexChanged
            dtgExcel.CurrentPageIndex = e.NewPageIndex
            Try
                bindDataGrid()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub dtgExcel_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgExcel.SortCommand
            If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)

                    Case Sort.SortDirection.ASC
                        ViewState("vsSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("vsSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("vsSortColumn") = e.SortExpression
                ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End If

            dtgExcel.SelectedIndex = -1
            dtgExcel.CurrentPageIndex = 0
            Try
                bindDataGrid()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Response.Redirect("FrmDisplayPRP.aspx")
        End Sub

        Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
            Response.Redirect("./DownloadPRP.aspx")
        End Sub
#End Region

#Region "General Method"
        Private Sub initiatePage()
            If Not objDealer Is Nothing Then
                lblKodeDealerValue.Text = objDealer.DealerCode
                lblNamaDealerValue.Text = objDealer.DealerName & " / " & objDealer.SearchTerm2
            End If
            sHPrp.SetSession("DisplayPRP", "PRPDealer")
            sHPrp.RemoveSession("excelDataTable")
            ViewState("vsSortColumn") = "PSCode"
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End Sub

        Private Sub bindDataGrid()
            Dim dtTable As DataTable = CType(sHPrp.GetSession("excelDataTable"), DataTable)
            Dim Data As ArrayList
            If IsNothing(dtTable) Then

                Dim filename As String = CStr(sHPrp.GetSession("dspFilename"))
                Dim filePath As String = RootDestDir & "\" & filename

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

                Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                sapImp.Start()

                Try
                    Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filePath)
                    If Not fInfo.Exists Then
                        MessageBox.Show("file " + filename + " tidak ditemukan")
                        Response.Redirect(Request.UrlReferrer.ToString)
                    End If

                    Dim excelParser As PRPExcelParser = New PRPExcelParser
                    Data = excelParser.ParseExcelNoTransactionPerDealer(filePath, "[PERDLR$]", User.Identity.Name, objDealer)
                    dtTable = ConvertToDataTable(Data)
                    sHPrp.SetSession("excelDataTable", dtTable)
                Catch
                    btnDownload.Visible = False
                    Throw New Exception("Data pada file excel tidak valid")
                Finally
                    sapImp.StopImpersonate()
                End Try

            End If

            dtTable.DefaultView.Sort = ViewState("vsSortColumn") + " " + CType(ViewState("vsSortDirect"), Sort.SortDirection).ToString
            dtgExcel.DataSource = dtTable.DefaultView
            dtgExcel.DataBind()
        End Sub

#End Region

#Region "Convert To DataTable"
        Private Function ConvertToDataTable(ByVal dtArray As ArrayList) As DataTable
            Dim dtTable As DataTable = New DataTable

            dtTable.Columns.AddRange(GenerateColumn)

            For rowIndex As Integer = 0 To dtArray.Count - 1
                Dim objPRP As PRPExcelPerDealer = CType(dtArray(rowIndex), PRPExcelPerDealer)
                dtTable.Rows.Add(toRow(dtTable, objPRP))
            Next

            Return dtTable
        End Function

        Private Function toRow(ByVal dtTable As DataTable, ByVal data As PRPExcelPerDealer) As DataRow
            Dim row As DataRow = dtTable.NewRow
            row("DealerCode") = data.DealerCode
            row("PSCode") = data.PsCode
            row("PartShopName") = data.PartShopName
            row("Kota") = data.Kota
            row("Jan") = data.Jan
            row("Feb") = data.Feb
            row("Mar") = data.Mar
            row("Apr") = data.Apr
            row("May") = data.May
            row("Jun") = data.Jun
            row("Jul") = data.Jul
            row("Aug") = data.Aug
            row("Sep") = data.Sep
            row("Oct") = data.Oct
            row("Nov") = data.Nov
            row("Dec") = data.Dec
            row("Total") = data.Total
            Return row
        End Function

        Private Sub SumADataRow(ByVal row As DataRow)
            _totJan += row("Jan")
            _totFeb += row("Feb")
            _totMar += row("Mar")
            _totApr += row("Apr")
            _totMay += row("May")
            _totJun += row("Jun")
            _totJul += row("Jul")
            _totAug += row("Aug")
            _totSep += row("Sep")
            _totOct += row("Oct")
            _totNov += row("Nov")
            _totDec += row("Dec")
            _totTot += row("Total")
        End Sub

        Private Sub SumData()
            Dim dt As DataTable = CType(sHPrp.GetSession("excelDataTable"), DataTable)
            For Each row As DataRow In dt.Rows
                SumADataRow(row)
            Next
        End Sub

        Private Function GenerateColumn() As DataColumn()
            Dim dtColColl(16) As DataColumn

            dtColColl(0) = New DataColumn("DealerCode", GetType(String))
            dtColColl(1) = New DataColumn("PSCode", GetType(String))
            dtColColl(2) = New DataColumn("PartShopName", GetType(String))
            dtColColl(3) = New DataColumn("Kota", GetType(String))
            dtColColl(4) = New DataColumn("Jan", GetType(Int32))
            dtColColl(5) = New DataColumn("Feb", GetType(Int32))
            dtColColl(6) = New DataColumn("Mar", GetType(Int32))
            dtColColl(7) = New DataColumn("Apr", GetType(Int32))
            dtColColl(8) = New DataColumn("May", GetType(Int32))
            dtColColl(9) = New DataColumn("Jun", GetType(Int32))
            dtColColl(10) = New DataColumn("Jul", GetType(Int32))
            dtColColl(11) = New DataColumn("Aug", GetType(Int32))
            dtColColl(12) = New DataColumn("Sep", GetType(Int32))
            dtColColl(13) = New DataColumn("Oct", GetType(Int32))
            dtColColl(14) = New DataColumn("Nov", GetType(Int32))
            dtColColl(15) = New DataColumn("Dec", GetType(Int32))
            dtColColl(16) = New DataColumn("Total", GetType(Int32))

            Return dtColColl
        End Function
#End Region

    End Class
End Namespace
