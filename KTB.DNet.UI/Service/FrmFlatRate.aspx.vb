#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Reflection

#End Region
Public Class FrmFlatRate
    Inherits System.Web.UI.Page
#Region " Private fields "
    Private crit As CriteriaComposite
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
    Dim isDealerDMS As Boolean = False
#End Region
#Region "Custom Method"

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FlatRate_View_Privilage) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Flat Rate Master")
        End If
    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        Dim criterias As New CriteriaComposite(New Criteria(GetType(VW_FlatRateMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (ddlTipe.SelectedValue.ToString() <> "0") Then
            criterias.opAnd(New Criteria(GetType(VW_FlatRateMaster), "Type", MatchType.Exact, "'" & ddlTipe.SelectedValue.ToString() & "'"))
        End If

        If (txtVariant.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(VW_FlatRateMaster), "Varian", MatchType.Exact, "'" & txtVariant.Text & "'"))
        End If

        If (txtKode.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(VW_FlatRateMaster), "KindCode", MatchType.Exact, "'" & txtKode.Text & "'"))
        End If

        Dim arrFlatRate As ArrayList = New VW_FlatRateMasterFacade(User).Retrieve(criterias)


        '-- Store InvoiceReqList into session for later use
        sessHelp.SetSession("FlatRateList", arrFlatRate)
        sessHelp.SetSession("criteriadownload", criterias)
    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrFlatRate As ArrayList = CType(sessHelp.GetSession("FlatRateList"), ArrayList)
        Dim aStatus As New ArrayList
        If arrFlatRate.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrFlatRate, pageIndex, dtgFlatRate.PageSize)
            dtgFlatRate.DataSource = PagedList
            dtgFlatRate.VirtualItemCount = arrFlatRate.Count()
            dtgFlatRate.DataBind()
        Else
            dtgFlatRate.DataSource = New ArrayList
            dtgFlatRate.VirtualItemCount = 0
            dtgFlatRate.CurrentPageIndex = 0
            dtgFlatRate.DataBind()
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        sFileName = "FlatRate" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

        '-- Temp file must be a randomly named file!
        Dim SvcIncomingData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SvcIncomingData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SvcIncomingData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteFlatRate(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteFlatRate(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim header As String
        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("FlatRate")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Jenis Kendaraan" & tab)
            itemLine.Append("Type" & tab)
            itemLine.Append("Code" & tab)
            itemLine.Append("Flat Rate" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim intNo As Integer = 0
            Dim strFlatRate As String = ""
            Dim strKindCode As String = ""
            For Each item As VW_FlatRateMaster In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(intNo + 1 & tab)
                itemLine.Append(item.Varian & tab)
                itemLine.Append(item.Type & tab)
                'strKindCode = item.KindCode
                itemLine.Append(String.Format("'{0}", item.KindCode) & tab)
                'itemLine.Append(Format(item.KindCode, "0") & tab)
                If item.FlatRate.ToString().Contains(",") Then
                    strFlatRate = item.FlatRate.ToString().Replace(",", ".")
                End If
                itemLine.Append(strFlatRate & tab)
                sw.WriteLine(itemLine.ToString())
                intNo = intNo + 1
            Next
            Response.ContentType = "application/vnd.ms-excel"
        End If
    End Sub

#Region "download excel"

    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        If dtgFlatRate.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        'If Not IsNothing(sessHelp.GetSession("criteriadownload")) Then
        '    crits = CType(sessHelp.GetSession("criteriadownload"), CriteriaComposite)
        'End If
        ' mengambil data yang dibutuhkan
        Dim arrFlatRate As ArrayList = CType(sessHelp.GetSession("FlatRateList"), ArrayList)
        'Dim propertiesinfo As PropertyInfo() = arrFlatRate(0).GetType().GetProperties()

        'For Each pf As PropertyInfo In propertiesinfo
        '    Dim dc As DataColumn = New DataColumn(pf.Name)
        '    dc.DataType = pf.PropertyType
        '    arrData.Columns.Add(dc)
        'Next

        'For Each ar As Object In arrFlatRate
        '    Dim dr As DataRow = arrData.NewRow
        '    Dim pf As PropertyInfo() = ar.GetType().GetProperties()

        '    For Each prop As PropertyInfo In pf
        '        dr(prop.Name) = prop.GetValue(ar, Nothing)
        '    Next
        '    arrData.Rows.Add(dr)
        'Next

        'arrData = New VW_FlatRateMasterFacade(User).GetDownLoadExcel(crits.ToString())

        If arrFlatRate.Count > 0 Then
            CreateExcel("FlatRate", arrFlatRate)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As DataTable)
        Using pck As New ExcelPackage()
            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            'Create Header Column
            ws.Cells("A1").ValueBold(FileName)
            Dim rowIndex As Integer = 3
            Dim ColumnIndex As Integer = 1
            Dim lastColumn As Integer = 0
            ws.Cells(rowIndex, ColumnIndex).ValueBold("No")
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

            For Each dColumn As DataColumn In Data.Columns
                ColumnIndex += 1
                ws.Cells(rowIndex, ColumnIndex).ValueBold(dColumn.ColumnName)
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
            Next
            lastColumn = ColumnIndex

            'Create Data
            Dim noUrutan As Integer = 1
            For Each dRow As DataRow In Data.Rows
                rowIndex += 1
                ColumnIndex = 1
                ws.Cells(rowIndex, ColumnIndex).SetValue(noUrutan.ToString())
                For Each dColumn As DataColumn In Data.Columns
                    ColumnIndex += 1
                    ws.Cells(rowIndex, ColumnIndex).SetValue(dRow(dColumn.ColumnName).ToString())
                Next
                noUrutan += 1
            Next
            ws.Cells(3, 2, rowIndex, lastColumn).AutoFilter = True
            For colIdx As Integer = 1 To Data.Columns.Count + 1
                ws.Column(colIdx).AutoFit()
            Next
            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Jenis Kendaraan"
            ws.Cells("C3").Value = "Type"
            ws.Cells("D3").Value = "Code"
            ws.Cells("E3").Value = "Flat Rate"
            'Dim standardCodeStatusClaimList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusClaim").Cast(Of  _
            '                                    StandardCode).ToList()
            'Dim standardCodeStatusProsesReturList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur").Cast(Of  _
            'StandardCode).ToList()
            Dim strFlatRate As String = ""
            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As VW_FlatRateMaster = Data(i)
                
                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.Varian
                ws.Cells(idx + 4, 3).Value = item.Type
                ws.Cells(idx + 4, 4).Value = item.KindCode
                If item.FlatRate.ToString().Contains(",") Then
                    strFlatRate = item.FlatRate.ToString().Replace(",", ".")
                End If
                ws.Cells(idx + 4, 5).Value = strFlatRate
                idx = idx + 1
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}"";", fileName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

    'Private Sub bindddlTipe()
    '    Dim results As ArrayList

    '    ddlTipe.Items.Clear()
    '    crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))

    '    results = stdFacade.Retrieve(crit)

    '    With ddlJenisKegiatan.Items
    '        For Each obj As StandardCode In results
    '            .Add(New ListItem(obj.ValueDesc, obj.ValueId))
    '        Next
    '    End With

    '    ddlJenisKegiatan.Items.Insert(0, "Silahkan Pilih")
    '    '    Dim arrDDL As ArrayList = New ArrayList
    '    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(FlatRateMasterFS), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    '    'criterias.opAnd(New Criteria(GetType(FSKind), "ID", MatchType.Exact, "'" & ddlStatus.SelectedValue & "'"))
    '    '    Dim sortColl As SortCollection = New SortCollection
    '    '    sortColl.Add(New Sort(GetType(FlatRateMasterFS), "ID", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
    '    '    arrDDL = New FlatRateMasterFSFacade(User).Retrieve(criterias, sortColl)
    '    '    Dim lstFS As List(Of FSKind) = arrDDL.Cast(Of FlatRateMasterFS).Select(Function(x) x.FSKind).Distinct().ToList()
    '    '    Dim listCluster As List(Of String) = lstFS.Select(Function(x) x.KindCode & "-" & x.KindDescription).Distinct().ToList()

    '    '    'Dim olist As New List(Of String)  = lstFS.Distinct()
    '    '    'lstFS.DistinctBy(Function(x) x.).ToList()
    '    '    With ddlTipe.Items

    '    '        .Clear()

    '    '        'For Each obj As FSKind In lstFS.Distinct().ToList()
    '    '        '    .Add(New ListItem(obj.KindCode & " - " & obj.KindDescription, obj.ID))
    '    '        'Next
    '    '        For Each Items As String In listCluster
    '    '            .Add(New ListItem(Items).ToString())
    '    '        Next
    '    '        '.Items.Clear()
    '    '        '.DataSource = arrDDL
    '    '        '.DataTextField = "id" '"FSKind.ID"
    '    '        '.DataValueField = "id" '"FSKind.ID"
    '    '        '.DataBind()
    '    '    End With
    '    '    ddlTipe.Items.Insert(0, New ListItem("Silahkan Pilih ", -1))
    '    '    ddlTipe.SelectedIndex = 0
    'End Sub

#End Region

#Region "Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ActivateUserPrivilege()
            'bindddlTipe()
            ReadData()
            dtgFlatRate.CurrentPageIndex = 0
            BindPage(dtgFlatRate.CurrentPageIndex)
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        ReadData()
        dtgFlatRate.CurrentPageIndex = 0
        BindPage(dtgFlatRate.CurrentPageIndex)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim arrFlatRate As ArrayList = CType(sessHelp.GetSession("FlatRateList"), ArrayList)
        'Dim aStatus As New ArrayList
        If arrFlatRate.Count <> 0 Then
            SetDownload()
            'DoDownload(arrFlatRate)
        End If
    End Sub

    Private Sub dtgFlatRate_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFlatRate.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lbNo"), Label)
            Dim lbFlatRate As Label = CType(e.Item.FindControl("lbFlatRate"), Label)
            lblNo.Text = (dtgFlatRate.CurrentPageIndex * dtgFlatRate.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            If lbFlatRate.Text.Contains(",") Then
                lbFlatRate.Text = lbFlatRate.Text.Replace(",", ".")
            End If
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(FSKind), "KindDescription", MatchType.Exact, lbTipe.Text))
            'Dim sortColl As SortCollection = New SortCollection
            'sortColl.Add(New Sort(GetType(FSKind), "ID", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
            ''arrDDL = New FSKindFacade(User).Retrieve(criterias, sortColl)
            'Dim arrDDL As ArrayList = New FSKindFacade(User).Retrieve(criterias)
            'Dim objFSKind As New FSKind
            'If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            '    objFSKind = CType(arrDDL(0), FSKind)
            '    lbTipe.Text = objFSKind.KindCode & " - " & objFSKind.KindDescription
            'End If
        End If
    End Sub

    Private Sub dtgFlatRate_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFlatRate.PageIndexChanged
        '-- Change datagrid page

        dtgFlatRate.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub

#End Region



End Class