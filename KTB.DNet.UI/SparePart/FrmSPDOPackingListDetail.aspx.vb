#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmSPDOPackingListDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerAddr As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaraPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblSONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDONumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblLOT As System.Web.UI.WebControls.Label
    Protected WithEvents lblCase As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialPacking As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalItem As System.Web.UI.WebControls.Label
    Protected WithEvents lblQty As System.Web.UI.WebControls.Label
    Protected WithEvents lblWeight As System.Web.UI.WebControls.Label

    Protected WithEvents cmdPrint As System.Web.UI.WebControls.Button
    Protected WithEvents dgSPDOPackingDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
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
    Private nPLID As Integer = 0
    Private objPOHead As SparePartPO
    Private objSPDOPackingDetail As SparePartPackingDetail = New SparePartPackingDetail
    Private sessHelper As SessionHelper = New SessionHelper
    Private arrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
#End Region

#Region "Custom Method"

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub retrieveHeader()
        Dim crits As New CriteriaComposite(New Criteria(GetType(SparePartPackingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(SparePartPackingDetail), "SparePartPacking.ID", MatchType.Exact, nPLID))
        Dim arlList As ArrayList = New SparePartPackingDetailFacade(User).Retrieve(crits)

        'Dim arlList As ArrayList = New SparePartPackingDetailFacade(User).Retrieve(nPLID)
        If arlList.Count <> 0 Then
            objSPDOPackingDetail = arlList(0)
        Else
            objSPDOPackingDetail = Nothing
        End If

        If Not objSPDOPackingDetail Is Nothing Then
            lblDealerCode.Text = objSPDOPackingDetail.SparePartDO.Dealer.DealerCode
            lblDealerName.Text = objSPDOPackingDetail.SparePartDO.Dealer.DealerName
            lblDealerAddr.Text = objSPDOPackingDetail.SparePartDO.Dealer.Address
            If Not IsNothing(Request.QueryString("SONumber")) Then
                lblSONumber.Text = Request.QueryString("SONumber")
            End If

            If objSPDOPackingDetail.SparePartDO.SparePartDODetails.Count > 0 Then
                Dim doDetail As SparePartDODetail = CType(objSPDOPackingDetail.SparePartDO.SparePartDODetails(0), SparePartDODetail)

                If Not IsNothing(doDetail.SparePartPOEstimate.SparePartPO.TermOfPayment) Then
                    lblCaraPembayaran.Text = doDetail.SparePartPOEstimate.SparePartPO.TermOfPayment.Description
                End If
            End If


            lblDONumber.Text = objSPDOPackingDetail.SparePartDO.DONumber
            lblLOT.Text = objSPDOPackingDetail.SparePartPacking.LotCase
            'lblCase.Text = objSPDOPackingDetail.SparePartPacking.Casing
            lblMaterialPacking.Text = objSPDOPackingDetail.SparePartPacking.PackMaterial
            lblTotalItem.Text = objSPDOPackingDetail.SparePartPacking.TotalItem
            lblQty.Text = objSPDOPackingDetail.SparePartPacking.TotalQty
            lblWeight.Text = objSPDOPackingDetail.SparePartPacking.Weight
            sessHelper.SetSession("FrmSPDOPackingListDetail_Download", arlList)
        Else
            sessHelper.SetSession("FrmSPDOPackingListDetail_Download", Nothing)
        End If
    End Sub

    Private Sub retrieveDetails()

        If GetFromSession("FrmSPDOPackingListDetail_Download") Is Nothing Then
            arrList = New ArrayList
        Else
            arrList = sessHelper.GetSession("FrmSPDOPackingListDetail_Download")
        End If

    End Sub

    Private Sub BindDG()
        retrieveDetails()

        If arrList.Count > 0 Then
            dgSPDOPackingDetail.DataSource = arrList
            dgSPDOPackingDetail.VirtualItemCount = totalRow
            dgSPDOPackingDetail.DataBind()
        Else
            dgSPDOPackingDetail.DataSource = arrList
            dgSPDOPackingDetail.VirtualItemCount = 0
            dgSPDOPackingDetail.DataBind()
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub


    Private Sub WriteSPDOPackingData(ByRef sw As StreamWriter, ByVal objSPDOPacking As SparePartPacking)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read SPPOEstimate header

        If Not IsNothing(objSPDOPacking) Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer" & tab & ": ")
            itemLine.Append(lblDealerCode.Text)  '-- Kode dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Nomor Penjualan (SO MMKSI)" & tab & ": ")
            itemLine.Append(lblSONumber.Text)  '-- 
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nama Dealer" & tab & ": ")
            itemLine.Append(lblDealerName.Text)  '-- Nama dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Nomor DO MMKSI" & tab & ": ")
            itemLine.Append(lblDONumber.Text)  '-- 
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Alamat Dealer" & tab & ": ")
            itemLine.Append(lblDealerAddr.Text)  '-- Alamat dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("LOT" & tab & ": ")
            itemLine.Append(lblLOT.Text)  '-- 
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Cara Pembayaran" & tab & ": ")
            itemLine.Append(lblCaraPembayaran.Text)  '-- Cara Pembayaran
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Material Packing" & tab & ": ")
            itemLine.Append(lblMaterialPacking.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append(tab)  '-- tab space
            itemLine.Append("Total Item" & tab & ": ")
            itemLine.Append(lblTotalItem.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append(tab)  '-- tab space
            itemLine.Append("Total Qty" & tab & ": ")
            itemLine.Append(lblQty.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append(tab)  '-- tab space
            itemLine.Append("Total Berat (Kg)" & tab & ": ")
            itemLine.Append(lblWeight.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file

        End If

        '-- Read SPDOPacking detail

        Dim arrList As ArrayList = sessHelper.GetSession("FrmSPDOPackingListDetail_Download")

        If Not IsNothing(arrList) AndAlso arrList.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nomor Barang" & tab)  '-- Part number
            itemLine.Append("Nama Barang" & tab)   '-- Part name
            itemLine.Append("Qty" & tab)
            itemLine.Append("Unit" & tab)
            sw.WriteLine(itemLine.ToString())      '-- Write header


            For Each sppoLine As SparePartPackingDetail In arrList

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                Dim objSparePartMaster As SparePartMaster = New SparePartMasterFacade(User).Retrieve(sppoLine.SparePartMaster.ID)
                If Not objSparePartMaster Is Nothing Then
                    itemLine.Append(objSparePartMaster.PartNumber & tab)  '-- Part number
                    itemLine.Append(objSparePartMaster.PartName & tab)    '-- Part name
                End If


                itemLine.Append(FormatNumber(sppoLine.Qty, 0).ToString() & tab)                  '-- Jumlah Pesanan
                itemLine.Append(sppoLine.UoM & tab)

                sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
            Next

        End If

    End Sub


    Private Sub Download(ByVal intID As Integer)

        Dim objSPDOPacking As SparePartPacking = New SparePartPackingFacade(User).Retrieve(intID)

        Dim sFileName As String = "SPDOPacking" & "_" & Date.Now.ToString("yyyyMMddHHmmss")
        sFileName = sFileName & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
        '-- Temp file must be a randomly named file!
        Dim SPDOPackingData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPDOPackingData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPDOPackingData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPDOPackingData(sw, objSPDOPacking)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")


        Catch ex As Exception
            MessageBox.Show(ex.Message) '"Download data gagal")
        End Try
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "PartNumber")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            If Not IsNothing(Request.QueryString("PLID")) Then
                nPLID = CType(Request.QueryString("PLID"), Integer)
                retrieveHeader()
                BindDG()
            End If
        End If
    End Sub

    Private Sub dgSPDOPackingDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPDOPackingDetail.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgSPDOPackingDetail.PageSize * dgSPDOPackingDetail.CurrentPageIndex)).ToString

            Dim objPDOPackingDetail As SparePartPackingDetail
            objPDOPackingDetail = CType(arrList(e.Item.ItemIndex), SparePartPackingDetail)

            Dim objSparePartMaster As SparePartMaster = New SparePartMasterFacade(User).Retrieve(objPDOPackingDetail.SparePartMaster.ID)
            If Not objSparePartMaster Is Nothing Then
                e.Item.Cells(2).Text = objSparePartMaster.PartNumber
                e.Item.Cells(3).Text = objSparePartMaster.PartName
            End If
        End If
    End Sub


    'Private Sub dgSPDOPackingDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSPDOPackingDetail.PageIndexChanged
    '    dgSPDOPackingDetail.CurrentPageIndex = e.NewPageIndex
    '    BindDG(e.NewPageIndex + 1)
    'End Sub


    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Download(CType(Request.QueryString("PLID"), Integer))
    End Sub

    Private Sub dgSPDOPackingDetail_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgSPDOPackingDetail.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        'dgSPDOPackingDetail.SelectedIndex = -1
        'dgSPDOPackingDetail.CurrentPageIndex = 0
        'BindDG(dgSPDOPackingDetail.CurrentPageIndex)
        BindDG()
    End Sub

#End Region

End Class