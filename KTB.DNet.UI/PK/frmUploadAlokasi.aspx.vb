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
#End Region

Public Class frmUploadAlokasi
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPilihLokasiFile As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents dtgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

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
    Private ParseResultArrayList As ArrayList
    Private PkDetailArrList As ArrayList
    Private sessionHelper As New sessionHelper
#End Region

#Region "Custom Method"

    Private Sub ParseFile()
        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) Then
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
                Dim parser As IParser = New PKAlocationParser
                ParseResultArrayList = CType(parser.ParseNoTransaction(DestFile, User.Identity.Name.ToString), ArrayList)
                PkDetailArrList = ValidateAllocation(ParseResultArrayList)
                sessionHelper.SetSession("PKAllocation", PkDetailArrList)
            End If
        Else
            MessageBox.Show("Pilih Lokasi File")
        End If
    End Sub

    Private Function ValidateAllocation(ByVal arrList As ArrayList) As ArrayList
        Dim PkDetailArrayList As New ArrayList
        For Each objPKDetailArrList As ArrayList In arrList
            If objPKDetailArrList.Count > 0 Then
                Dim objPKDetail As PKDetail = CType(objPKDetailArrList(0), PKDetail)
                Dim SisaStok As Integer = 0
                Dim runningStock As Integer = 0
                For Each item As PKDetail In objPKDetailArrList
                    If objPKDetail.ID > 0 Then
                        SisaStok = GetSisaStokSetelahAlokasi(objPKDetailArrList, objPKDetail.PKHeader.RequestPeriodeMonth, objPKDetail.PKHeader.RequestPeriodeYear, objPKDetail.PKHeader.ProductionYear, objPKDetail.VechileColor.MaterialNumber)
                        runningStock = SisaStok
                        Exit For
                    End If
                Next

                For Each item As PKDetail In objPKDetailArrList
                    item.ResponseQty = item.AgreeQty
                    item.AgreeQty = SisaStok
                    runningStock -= item.ResponseQty
                    If runningStock < 0 Then
                        item.ErrorMessage = item.ErrorMessage & "Sisa Stok Tidak Mencukupi <br>"
                    End If
                    PkDetailArrayList.Add(item)
                Next
            End If
        Next
        Return PkDetailArrayList
    End Function

    Private Function GetSisaStokSetelahAlokasi(ByVal ArrList As ArrayList, ByVal periodeMonth As Integer, ByVal periodeYear As Integer, ByVal productionYear As Integer, ByVal materialNumber As String) As Integer
        Dim sisaStock As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKProductionPlan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "VechileColor.MaterialNumber", MatchType.Exact, materialNumber))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodMonth", MatchType.Exact, periodeMonth))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "PeriodYear", MatchType.Exact, periodeYear))
        criterias.opAnd(New Criteria(GetType(PKProductionPlan), "ProductionYear", MatchType.Exact, productionYear))
        Dim arlPKProductionPlan As ArrayList = New PKProductionPlanFacade(User).Retrieve(criterias)
        If arlPKProductionPlan.Count <> 0 Then
            sisaStock = CInt(CType(arlPKProductionPlan(0), PKProductionPlan).PlanQty) + CInt(CType(arlPKProductionPlan(0), PKProductionPlan).CarryOverPreviousQty) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).UnselledStock) - CInt(CType(arlPKProductionPlan(0), PKProductionPlan).TotalAlokasi)
        Else
            sisaStock = 0
        End If
        For Each item As PKDetail In ArrList
            If item.ID > 0 Then
                sisaStock += item.ResponseQty
            End If
        Next
        Return sisaStock
    End Function

    Private Sub BindToGrid()
        PkDetailArrList = sessionHelper.GetSession("PKAllocation")
        If Not ((PkDetailArrList Is Nothing) OrElse (PkDetailArrList.Count <= 0)) Then
            dtgAlokasi.DataSource = PkDetailArrList
            dtgAlokasi.DataBind()
            For Each item As PKDetail In PkDetailArrList
                If item.ErrorMessage <> String.Empty Then
                    btnSimpan.Enabled = False
                    Exit Sub
                End If
            Next
            btnSimpan.Enabled = True
        Else
            btnSimpan.Enabled = False
        End If
    End Sub

#End Region

#Region "EventHandler"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            'InitiatePage()

        End If
    End Sub

    'Private Sub InitiatePage()
    '    ViewState("CurrentSortColumn") = "VechileColor.MaterialNumber"
    '    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    'End Sub

    'Private Sub dtgProductionPlan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgProductionPlan.SortCommand
    '    If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
    '        Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

    '            Case Sort.SortDirection.ASC
    '                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

    '            Case Sort.SortDirection.DESC
    '                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    '        End Select
    '    Else
    '        ViewState("CurrentSortColumn") = e.SortExpression
    '        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    '    End If

    '    BindData()
    'End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            ParseFile()
            BindToGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Sub dtgAlokasi_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        PkDetailArrList = sessionHelper.GetSession("PKAllocation")
        If Not (PkDetailArrList.Count = 0 Or E.Item.ItemIndex = -1) Then
            Dim objPKDetail As PKDetail = PkDetailArrList(E.Item.ItemIndex)
            E.Item.Cells(1).Text = (E.Item.ItemIndex + 1 + (dtgAlokasi.PageSize * dtgAlokasi.CurrentPageIndex)).ToString
            If objPKDetail.ID > 0 Then
                E.Item.Cells(4).Text = objPKDetail.VechileColor.MaterialNumber
                E.Item.Cells(3).Text = objPKDetail.PKHeader.ProductionYear
                E.Item.Cells(2).Text = Format(New DateTime(objPKDetail.PKHeader.RequestPeriodeYear, objPKDetail.PKHeader.RequestPeriodeMonth, 1), "MMM yyyy")
                E.Item.Cells(5).Text = objPKDetail.PKHeader.PKNumber
                E.Item.Cells(6).Text = objPKDetail.PKHeader.Dealer.DealerCode & "/" & objPKDetail.PKHeader.Dealer.SearchTerm1
                E.Item.Cells(7).Text = objPKDetail.PKHeader.ProjectName
            Else
                E.Item.Cells(4).Text = objPKDetail.MaterialNumber
                E.Item.Cells(3).Text = objPKDetail.RowStatus
                E.Item.Cells(2).Text = Format(New DateTime(objPKDetail.LineItem, objPKDetail.TargetQty, 1), "MMM yyyy")
                E.Item.Cells(5).Text = objPKDetail.VehicleColorCode
                E.Item.Cells(6).Text = ""
                E.Item.Cells(7).Text = ""
            End If
            'E.Item.Cells(7).Text = GetSisaStok(objPKDetail.PKHeader.RequestPeriodeMonth, objPKDetail.PKHeader.RequestPeriodeYear, objPKDetail.PKHeader.ProductionYear, objPKDetail.VechileColor.MaterialNumber)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        PkDetailArrList = sessionHelper.GetSession("PKAllocation")
        Dim objPkDetailFacade As New PKDetailFacade(User)
        Try
            objPkDetailFacade.Update(PkDetailArrList)
            MessageBox.Show(SR.SaveSuccess)
            btnSimpan.Enabled = False
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../PK/DisplayDealerOrderQty.aspx")
    End Sub
End Class
