#Region "Custom Namespace Imports"
Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.BusinessFacade.General
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
#End Region

Public Class GetSPPOCheckList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgCheckList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents ccPODateEnd As Intimedia.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ccPODateStart As Intimedia.WebCC.IntiCalendar
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents trDealerLabel1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trDealerLabel2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trDealerLabel3 As System.Web.UI.HtmlControls.HtmlTableRow
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

#Region "Custom Variable Declaration"
    Private sessHelper As SessionHelper = New SessionHelper
    Private arlDisplayedPO As ArrayList = New ArrayList
    Private totalRow As Integer = 0
    Private _sessArlDisplayedPO As String = "GetSPPOCheckList._sessArlDisplayedPO"
    Private _sessArlPOAll As String = "GetSPPOCheckList._sessArlPOAll"


#End Region

#Region "Custom Method"

    Private Sub Initialization()
        Dim objDealer As Dealer = Session.Item("DEALER")

        Me.lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"


        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.trDealerLabel1.Style.Add("display", "none")
            Me.trDealerLabel2.Style.Add("display", "none")
            Me.trDealerLabel3.Style.Remove("display")
            Me.dtgCheckList.Columns(2).Visible = True
            Me.dtgCheckList.Columns(3).Visible = True
        Else
            Me.trDealerLabel1.Style.Remove("display")
            Me.trDealerLabel2.Style.Remove("display")
            Me.trDealerLabel3.Style.Add("display", "none")
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerName.Text = objDealer.DealerName
            lblDealerTerm.Text = objDealer.SearchTerm2
            Me.dtgCheckList.Columns(2).Visible = False
            Me.dtgCheckList.Columns(3).Visible = False
        End If
    End Sub

    Private Function GetDealerObject() As Boolean
        If Not IsNothing(Session("DEALER")) Then
            sessHelper.SetSession("sesDealer", Session("DEALER"))
            Return True
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
        Return False
    End Function

    Private Sub RetrieveHeader()
        GetDealer()
        GetOrderType()
    End Sub

    Private Sub GetDealer()
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        lblDealerCode.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName
        lblDealerTerm.Text = objDealer.SearchTerm2

    End Sub

    Private Sub GetOrderType()
        ddlOrderType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer
            ddlOrderType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        ddlOrderType.DataBind()
    End Sub

    Private Sub FindData(ByVal pageIndex As Integer, Optional ByVal IsForDownload As Boolean = False)
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Me.txtKodeDealer.Text.Trim <> String.Empty Then
                Dim sDealerCodes As String = "'" & Me.txtKodeDealer.Text.Replace(";", "','") & "'"
                criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.DealerCode", MatchType.InSet, "(" & sDealerCodes & ")"))
            End If
        Else
            criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, objDealer.ID))
        End If
        If ddlOrderType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        If txtNoPO.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, txtNoPO.Text))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(ccPODateStart.Value, "yyyy/MM/dd")))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(ccPODateEnd.Value, "yyyy/MM/dd")))

        Dim Sql As String = String.Empty
        Sql &= " ( "
        Sql &= " select count(*) from SparePartPODetail sppod "
        Sql &= " where sppod.RowStatus=0 "
        Sql &= "    and sppod.CheckListStatus='0' "
        Sql &= "    and sppod.SparePartPOID=SparePartPO.ID "
        Sql &= " ) "
        criterias.opAnd(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Lesser, Sql))

        Dim oSPPOFac As New SparePartPOFacade(User)

        Dim arlPO As ArrayList '= New SparePartPOFacade(User).RetrieveByCriteria(criterias, pageIndex, dtgCheckList.PageSize, totalRow)
        Dim arlPOAll As ArrayList '= New SparePartPOFacade(User).Retrieve(criterias)
        Dim objPODetailFacade As SparePartPODetailFacade = New SparePartPODetailFacade(User)
        Dim i As Integer

        If IsForDownload = False Then
            arlPO = oSPPOFac.RetrieveByCriteria(criterias, pageIndex, dtgCheckList.PageSize, totalRow)
            arlDisplayedPO = arlPO
            Me.sessHelper.SetSession(Me._sessArlDisplayedPO, arlDisplayedPO)
        Else
            arlPOAll = oSPPOFac.Retrieve(criterias)
            'Next
            Me.sessHelper.SetSession(Me._sessArlPOAll, arlPOAll)
        End If


        'For i = 0 To arlPO.Count - 1
        '    If objPODetailFacade.ValidateCheckListStatus(CType(arlPO(i), SparePartPO).ID, "0") > 0 Then
        '        arlDisplayedPO.Add(CType(arlPO(i), SparePartPO))
        '    End If
    End Sub

    Private Sub RetrieveDetails(ByVal pageIndex As Integer, ByVal status As Integer)
        If ccPODateEnd.Value >= ccPODateStart.Value Then
            FindData(pageIndex)
            If arlDisplayedPO.Count > 0 Then
                dtgCheckList.DataSource = arlDisplayedPO
                dtgCheckList.VirtualItemCount = totalRow
                dtgCheckList.DataBind()
            Else
                dtgCheckList.DataSource = New ArrayList
                dtgCheckList.DataBind()
                If status = 1 Then MessageBox.Show("Data Tidak Ditemukan!")
            End If
        Else
            MessageBox.Show("Masukan Periode Tanggal Tidak Valid!")
        End If



    End Sub


    Private Sub DoDownload(ByRef arlData As ArrayList)
        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "BarangTidakTerpenuhi" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        fullFileName = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = ConfigurationSettings.AppSettings.Get("User")
        Dim _password As String = ConfigurationSettings.AppSettings.Get("Password")
        Dim _webServer As String = ConfigurationSettings.AppSettings.Get("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(fullFileName)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(fullFileName, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteData(sw, arlData)

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

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal arlData As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim oAS As AccessoriesSale
        Dim i As Integer = 1, j As Integer
        Dim sHeader As String

        If Not IsNothing(arlData) Then
            Try
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append("No" & tab)
                itemLine.Append("Kode Dealer" & tab)
                itemLine.Append("Nama Dealer" & tab)
                itemLine.Append("Kota" & tab)
                itemLine.Append("Tanggal Pesanan" & tab)
                itemLine.Append("Type" & tab)
                itemLine.Append("No Order" & tab)
                itemLine.Append("PartNumber" & tab)
                itemLine.Append("PartName" & tab)
                itemLine.Append("Model" & tab)
                itemLine.Append("Qty" & tab)
                itemLine.Append("RetailPrice" & tab)
                itemLine.Append("Status" & tab)

                sw.WriteLine(itemLine.ToString())
                i = 1
                For Each oSPPO As SparePartPO In arlData
                    For Each oSPPOD As SparePartPODetail In oSPPO.SparePartPODetails
                        If oSPPOD.CheckListStatus = 0 Then
                            itemLine.Remove(0, itemLine.Length)

                            itemLine.Append(i.ToString & tab)
                            itemLine.Append(oSPPO.Dealer.DealerCode & tab)
                            itemLine.Append(oSPPO.Dealer.DealerName & tab)
                            itemLine.Append(oSPPO.Dealer.City.CityName & tab)
                            itemLine.Append(oSPPO.PODate.ToString("yyyy/MM/dd") & tab)
                            itemLine.Append(oSPPO.OrderTypeDesc & tab)
                            itemLine.Append(oSPPO.PONumber & tab)
                            itemLine.Append(oSPPOD.SparePartMaster.PartNumber & tab)
                            itemLine.Append(oSPPOD.SparePartMaster.PartName & tab)
                            itemLine.Append(oSPPOD.SparePartMaster.ModelCode & tab)
                            itemLine.Append(oSPPOD.Quantity & tab)
                            itemLine.Append(oSPPOD.RetailPrice & tab)
                            itemLine.Append(EnumStopMark.GetStringValue(oSPPOD.StopMark) & tab)

                            sw.WriteLine(itemLine.ToString())

                            i += 1
                        End If
                    Next
                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


            'For Each di As DataGridItem In Me.dtgMain.Items
            '    oAS = CType(arlData(di.ItemIndex), AccessoriesSale)

            '    j = 1
            '    For Each oASD As AccessoriesSaleDetail In oAS.AccessoriesSaleDetails
            '        itemLine.Remove(0, itemLine.Length)

            '        itemLine.Append(IIf(j = 1, i.ToString, "") & tab)
            '        itemLine.Append(oAS.AccessoriesCategory.Name & tab)
            '        itemLine.Append(oAS.ReportNumber & tab)
            '        itemLine.Append(oAS.Dealer.DealerCode & tab)
            '        itemLine.Append(oAS.Dealer.DealerName & tab)
            '        itemLine.Append(oAS.Dealer.City.CityName & tab)
            '        itemLine.Append(oAS.SoldDate.ToString("dd/MMM/yyyy") & tab)
            '        itemLine.Append(oAS.CreatedTime.ToString("dd/MMM/yyyy") & tab)
            '        itemLine.Append(oAS.RefNumber & tab)
            '        itemLine.Append(oAS.ChassisMaster.ChassisNumber & tab)
            '        itemLine.Append(oAS.ChassisMaster.VechileColor.VechileType.Description & tab)
            '        itemLine.Append(oAS.ChassisMaster.VechileColor.VechileType.VechileTypeCode & tab)
            '        itemLine.Append(oAS.CustomerName & tab)
            '        itemLine.Append(oAS.CustomerPhone & tab)
            '        itemLine.Append(Me.GetValidComment(oAS.Comment) & tab)
            '        itemLine.Append(oASD.SparePartMaster.PartNumber & tab)
            '        itemLine.Append(oASD.SparePartMaster.PartName & tab)
            '        itemLine.Append(oASD.Jumlah & tab)

            '        sw.WriteLine(itemLine.ToString)
            '        j += 1
            '    Next
            '    i += 1
            'Next
        End If
    End Sub


#End Region

#Region "EventHandler"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ViewSPPO_CheckList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Barang yang Tidak Terpenuhi")
        End If
        '--exclude  this privilege from Asra (BA)
        'Me.btnSearch.Visible = SecurityProvider.Authorize(context.User, SR.SearchSPPO_CheckList_Privilege)
        Me.dtgCheckList.Columns(7).Visible = SecurityProvider.Authorize(context.User, SR.ViewSPPO_CheckListDetail_Privilege)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            Initialization()
            If GetDealerObject() Then
                RetrieveHeader()
                RetrieveDetails(1, 0)
            Else
                MessageBox.Show("Dealer Belum terdefinisi")
            End If
        End If
    End Sub

    Private Sub dtgCheckList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCheckList.ItemDataBound
        Dim objSPHeader As SparePartPO
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDGChecklistItem(e)
        End If
    End Sub

    Private Sub SetDGChecklistItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim oSPPO As SparePartPO = CType(Me.sessHelper.GetSession(Me._sessArlDisplayedPO), ArrayList)(e.Item.ItemIndex)

        CType(e.Item.FindControl("lblDetail"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference("../SparePart/FrmGetSPPOCheckListDetail.aspx?sppoID=" + e.Item.Cells(0).Text + "", "", 500, 700, "Checklist")
        CType(e.Item.FindControl("lblDCode"), Label).Text = oSPPO.Dealer.DealerCode
        CType(e.Item.FindControl("lblDName"), Label).Text = oSPPO.Dealer.DealerName
    End Sub

    Private Sub dtgCheckList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCheckList.PageIndexChanged
        dtgCheckList.CurrentPageIndex = e.NewPageIndex
        RetrieveDetails(e.NewPageIndex + 1, 1)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgCheckList.CurrentPageIndex = 0
        RetrieveDetails(1, 1)
    End Sub

    Private Sub ddlOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlOrderType.SelectedIndexChanged

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'Dim aDP As ArrayList = Me.sessHelper.GetSession(Me._sessArlDisplayedPO)
        If Me.ccPODateStart.Value > Me.ccPODateEnd.Value Then
            MessageBox.Show("Periode tidak valid.")
            Exit Sub
        End If
        If Math.Abs(DateDiff(DateInterval.Month, Me.ccPODateEnd.Value, Me.ccPODateStart.Value)) > 1 Then
            MessageBox.Show("Periode Download data maksimal 2 bulan. Mohon dicek kembali.")
            Exit Sub
        End If
        Me.FindData(0, True) 'retrieve all data to download
        Dim aDP As ArrayList = Me.sessHelper.GetSession(Me._sessArlPOAll)

        If aDP.Count > 0 Then
            DoDownload(aDP)
        Else
            MessageBox.Show("Tidak ada data yang didownload")
        End If
    End Sub

#End Region
End Class