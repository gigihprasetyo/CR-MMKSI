#Region " Custom Namespace "
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

Public Class FrmViewHargaPasar
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgCompetitor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlClass As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMerk As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custome Variables Declaration"
    Dim sessHelp As SessionHelper = New SessionHelper
    Private bDownloadPriv As Boolean
#End Region


#Region "Custome Methods"
#Region "Check Privilage"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MarketPriceListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DEALER REPORT - Harga Pasar")
        End If

        bDownloadPriv = SecurityProvider.Authorize(context.User, SR.MarketPriceListDownload_Privilege)
    End Sub

#End Region

    Sub BindClass(ByVal ddl As DropDownList)
        Dim oClass As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VehicleClass), "Status", MatchType.Exact, 0))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(VehicleClass), "Description", Sort.SortDirection.ASC))
        oClass = New DealerReport.VehicleClassFacade(User).Retrieve(criterias, sortColl)
        ddl.DataTextField = "Description"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClass
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub BindMerk(ByVal ddl As DropDownList)
        Dim oMerk As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Status", MatchType.Exact, 0))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(CompetitorBrand), "Description", Sort.SortDirection.ASC))
        oMerk = New DealerReport.CompetitorBrandFacade(User).Retrieve(criterias, sortColl)
        ddl.DataTextField = "Description"
        ddl.DataValueField = "ID"
        ddl.DataSource = oMerk
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub BindArea(ByVal ddl As DropDownList)
        Dim oArea As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(Area1), "Description", Sort.SortDirection.ASC))
        oArea = New General.Area1Facade(User).Retrieve(criterias, sortColl)
        ddl.DataTextField = "Description"
        ddl.DataValueField = "ID"
        ddl.DataSource = oArea
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub BindGrid(ByVal index As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (ddlClass.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "CompetitorType.VehicleClass.ID", MatchType.Exact, ddlClass.SelectedValue))
        End If

        If (ddlMerk.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "CompetitorType.CompetitorBrand.ID", MatchType.Exact, ddlMerk.SelectedValue))
        End If

        If (ddlArea.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "Dealer.Area1.ID", MatchType.Exact, ddlArea.SelectedValue))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "ValidDate", MatchType.GreaterOrEqual, icDateFrom.Value))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "ValidDate", MatchType.Lesser, icDateUntil.Value.AddDays(1)))

        Dim arl As ArrayList = New ArrayList
        arl = New DealerReport.MarketPriceFacade(User).RetrieveByCriteria(criterias, index + 1, dgCompetitor.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        If (arl.Count > 0) Then

            If bDownloadPriv Then
                btnDownload.Visible = True
            Else
                btnDownload.Visible = bDownloadPriv
            End If

            dgCompetitor.Visible = True
            sessHelp.SetSession("DATADOWNLOAD", arl)
            dgCompetitor.DataSource = arl
            dgCompetitor.VirtualItemCount = totalRow
            dgCompetitor.DataBind()
        Else
            If bDownloadPriv Then
                btnDownload.Visible = False
            Else
                btnDownload.Visible = bDownloadPriv
            End If
            dgCompetitor.Visible = False
            MessageBox.Show(SR.DataNotFound("Harga pasar"))
        End If
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If (Not Page.IsPostBack) Then
            BindClass(ddlClass)
            BindMerk(ddlMerk)
            BindArea(ddlArea)
            ViewState("currSortColumn") = "CompetitorType.Description"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Private Sub dgCompetitor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCompetitor.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim lNum As LiteralControl = New LiteralControl(e.Item.ItemIndex + 1 + (dgCompetitor.CurrentPageIndex * dgCompetitor.PageSize))
            e.Item.Cells(0).Controls.Add(lNum)
            Dim oMP As MarketPrice = CType(e.Item.DataItem, MarketPrice)
            Dim lblOffTheRoad As Label = CType(e.Item.FindControl("lblOffTheRoad"), Label)
            Dim offRoad As Integer = oMP.OnTheRoadPrice - oMP.BBN
            If offRoad < 0 Then
                offRoad = 0
            End If
            lblOffTheRoad.Text = (offRoad).ToString("#,##0")
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim pengirim As String
            If oMP.CreatedBy.Length > 6 Then
                pengirim = oMP.CreatedBy.Remove(0, 6)
            End If

            lblDealer.Text = oMP.Dealer.DealerCode & "-" & pengirim
        End If
    End Sub

    Private Sub dgCompetitor_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCompetitor.PageIndexChanged
        dgCompetitor.CurrentPageIndex = e.NewPageIndex
        BindGrid(dgCompetitor.CurrentPageIndex)
    End Sub

    Private Sub dgCompetitor_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgCompetitor.SortCommand
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
        dgCompetitor.CurrentPageIndex = 0
        BindGrid(dgCompetitor.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgCompetitor.CurrentPageIndex = 0
        BindGrid(0)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
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
        sFileName = "DaftarHarga" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteTraineeData(sw, data)

                'Dim itemLine As StringBuilder = New StringBuilder
                'itemLine.Append("Test")
                'sw.WriteLine(itemLine.ToString())

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If



            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Dealer Report - Daftar Harga")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("Merk" & tab)
            itemLine.Append("Kelas" & tab)
            itemLine.Append("Tipe" & tab)
            ' Modified by Ikhsan, 20081204
            ' Requested by Rina as Part Of CR
            ' Start -------
            itemLine.Append("Periode" & tab)
            ' End ---------
            itemLine.Append("Tanggal Harga Berlaku" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Pengirim" & tab)
            itemLine.Append("InputDate" & tab)
            itemLine.Append("Area" & tab)
            itemLine.Append("Propinsi" & tab)
            itemLine.Append("Kota" & tab)
            itemLine.Append("Off The Road (Rp)" & tab)
            itemLine.Append("BBN (Rp)" & tab)
            itemLine.Append("On The Road (Rp)" & tab)
            itemLine.Append("Keterangan" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As MarketPrice In data

                itemLine.Remove(0, itemLine.Length)

                Dim strpengirim As String
                If item.CreatedBy.Length > 6 Then
                    strpengirim = item.Dealer.DealerCode & "-" & item.CreatedBy.Remove(0, 6)
                Else
                    strpengirim = item.Dealer.DealerCode & "-"
                End If

                itemLine.Append(item.CompetitorType.CompetitorBrand.Description.Trim & tab)
                If Not IsNothing(item.CompetitorType.VehicleClass) Then
                    itemLine.Append(item.CompetitorType.VehicleClass.Description.Trim & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                itemLine.Append(item.CompetitorType.Description.Trim & tab)
                'itemLine.Append("" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab & "" & tab)
                ' Modified by Ikhsan, 20081204
                ' Requested by Rina as Part Of CR
                ' Start -------
                Dim intDate As Integer = CInt(item.ValidDate.ToString("dd").Trim)

                If intDate <= 10 Then
                    itemLine.Append("Begin" & tab)
                ElseIf intDate > 10 And intDate <= 20 Then
                    itemLine.Append("Middle" & tab)
                ElseIf intDate > 20 And intDate <= 31 Then
                    itemLine.Append("end" & tab)
                End If

                ' End ---------
                itemLine.Append(item.ValidDate.ToString("dd/MM/yyyy").Trim & tab)
                itemLine.Append(item.Dealer.SearchTerm1.Trim & tab)
                itemLine.Append(strpengirim.Trim & tab)
                itemLine.Append(item.Dealer.CreatedTime.ToString("dd/MM/yyyy").Trim & tab)
                Try
                    itemLine.Append(item.Dealer.Area1.Description.Trim & tab)
                Catch ex As Exception
                    itemLine.Append("" & tab)
                End Try
                itemLine.Append(item.Dealer.Province.ProvinceName.Trim & tab)
                itemLine.Append(item.Dealer.City.CityName.Trim & tab)

                itemLine.Append(CLng(item.OnTheRoadPrice - item.BBN) & tab)
                itemLine.Append(CLng(item.BBN) & tab)
                itemLine.Append(CLng(item.OnTheRoadPrice) & tab)
                itemLine.Append(item.OtherInfo & tab)

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next


        End If
    End Sub

#End Region

End Class
