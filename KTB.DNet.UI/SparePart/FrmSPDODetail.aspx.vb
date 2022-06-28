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

Public Class FrmSPDODetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaraPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerTerm As System.Web.UI.WebControls.Label
    Protected WithEvents dgSPDO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgPackingList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblETD As System.Web.UI.WebControls.Label
    Protected WithEvents lblPickingDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderType As System.Web.UI.WebControls.Label
    Protected WithEvents lblPackingDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblDO As System.Web.UI.WebControls.Label
    Protected WithEvents lblGoodIssueDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblExpClaimDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPaymentDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblReadyForDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents txtExpeditionNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPartNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtUrlToBack As System.Web.UI.WebControls.TextBox

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
    'Private DONumber As String = ""
    Private DOID As Integer
    Private POID As Integer
    Private objSPDO As SparePartDO = New SparePartDO
    Private objFlow As V_SparePartFlow
    Private sessHelper As SessionHelper = New SessionHelper
    Private arrList As ArrayList = New ArrayList

    Private _sessDataDO As String = "FrmSPDODetail._sessDataDO"
    Private _sessDataExpedition As String = "FrmSPDODetail._sessDataExpedition"
    Private _sessDataPacking As String = "FrmSPDODetail._sessDataPacking"
    Private _sessDataPackingDownload As String = "FrmSPDODetail._sessDataPackingDownload"
    Private _iLeadTime As Integer = 0
#End Region

#Region "Custom Method"

    'Private Function GetFromSession(ByVal sObject As String) As Object
    '    If Session("DEALER") Is Nothing Then
    '        Response.Redirect("../SessionExpired.htm")
    '    Else
    '        Return Session(sObject)
    '    End If
    'End Function

    Private Sub BindHeader()
        Dim crits As New CriteriaComposite(New Criteria(GetType(SparePartDO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(SparePartDO), "ID", MatchType.Exact, DOID))
        Dim arlList As ArrayList = New SparePartDOFacade(User).Retrieve(crits)
        If arlList.Count <> 0 Then
            objSPDO = arlList(0)
        Else
            objSPDO = Nothing
        End If

        Dim objPO As SparePartPO = New SparePartPO
        Dim _poID = CType(Request.QueryString("POID"), Integer)
        Dim critPO As New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critPO.opAnd(New Criteria(GetType(SparePartPO), "ID", MatchType.Exact, _poID))
        Dim arlPOList As ArrayList = New SparePartPOFacade(User).Retrieve(critPO)
        If arlPOList.Count <> 0 Then
            objPO = CType(arlPOList(0), SparePartPO)
            If Not IsNothing(objPO) AndAlso objPO.ID > 0 Then
                lblOrderType.Text = objPO.OrderTypeDesc

                If Not IsNothing(objPO.TermOfPayment) Then
                    lblCaraPembayaran.Text = objPO.TermOfPayment.Description
                End If
            End If

        Else
            objPO = Nothing
        End If




        If Not objSPDO Is Nothing Then


            lblDealerCode.Text = objSPDO.Dealer.DealerCode
            lblDealerName.Text = objSPDO.Dealer.DealerName
            lblDealerTerm.Text = objSPDO.Dealer.SearchTerm2
            lblDO.Text = objSPDO.DONumber + " - " + objSPDO.DoDate
            If objSPDO.EstmationDeliveryDate.Year > 1901 Then
                If objFlow.POID > 0 Then
                    If objFlow.OrderType = "R" Then
                        lblETD.Text = Format(objSPDO.EstmationDeliveryDate, "dd/MM/yyyy")
                    Else
                        lblETD.Text = Format(objSPDO.EstmationDeliveryDate.AddHours(2), "dd/MM/yyyy HH:mm")
                    End If
                End If

            End If
            If objSPDO.PickingDate.Year > 1901 Then
                lblPickingDate.Text = Format(objSPDO.PickingDate, "dd/MM/yyyy HH:mm")
            End If
            If objSPDO.PackingDate.Year > 1901 Then
                lblPackingDate.Text = Format(objSPDO.PackingDate, "dd/MM/yyyy HH:mm")
            End If
            If objSPDO.GoodIssueDate.Year > 1901 Then
                lblGoodIssueDate.Text = Format(objSPDO.GoodIssueDate, "dd/MM/yyyy HH:mm")
            End If
            If objSPDO.PaymentDate.Year > 1901 Then
                lblPaymentDate.Text = Format(objSPDO.PaymentDate, "dd/MM/yyyy HH:mm")
            End If
            If objSPDO.ReadyForDeliveryDate.Year > 1901 Then
                lblReadyForDeliveryDate.Text = Format(objSPDO.ReadyForDeliveryDate, "dd/MM/yyyy HH:mm")
            End If

            lblExpClaimDate.Text = ""
            sessHelper.SetSession(_sessDataDO, objSPDO)
        Else
            sessHelper.SetSession(_sessDataDO, Nothing)
        End If


    End Sub

    Private Sub BindExpedition(ByVal pageIndex As Integer)
        Dim totalRow As Integer = 0

        sessHelper.SetSession(Me._sessDataExpedition, Nothing)

        Dim _doID As Integer = CType(Request.QueryString("DOID"), Integer)
        Dim strSQL As String = "select SparePartDOExpeditionID from SparePartPacking h join SparePartPackingDetail d on d.SparePartPackingID = h.ID where d.SparePartDOID = " & _doID
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDOExpedition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartDOExpedition), "ID", MatchType.InSet, "(" & strSQL & ")"))
        arrList = New SparePartDOExpeditionFacade(User).RetrieveByCriteria(criterias, pageIndex, dgSPDO.PageSize, totalRow)

        sessHelper.SetSession(Me._sessDataExpedition, arrList)

        If arrList.Count > 0 Then
            dgSPDO.DataSource = arrList
            dgSPDO.VirtualItemCount = totalRow
            dgSPDO.DataBind()
        Else
            dgSPDO.DataSource = arrList
            dgSPDO.VirtualItemCount = 0
            dgSPDO.DataBind()
        End If
    End Sub


    Private Sub WriteSPDOPackingData(ByRef sw As StreamWriter, ByVal objSPDO As SparePartDO)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        '-- Read SPPOEstimate header
        If Not IsNothing(objSPDO) Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer" & tab & ": ")
            itemLine.Append(lblDealerCode.Text)  '-- Kode dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("ETD (Estimation Time Delivery)" & tab & ": ")
            itemLine.Append(lblETD.Text)  '-- 
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Nama Dealer" & tab & ": ")
            itemLine.Append(lblDealerName.Text)  '-- Nama dealer
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Picking Date" & tab & ": ")
            itemLine.Append(lblPickingDate.Text)  '-- 
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("Tipe Order" & tab & ": ")
            itemLine.Append(lblOrderType.Text)  '-- Tipe order
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Packing Date" & tab & ": ")
            itemLine.Append(lblPackingDate.Text)  '-- 
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nomor DO MMKSI - Tanggal" & tab & ": ")
            itemLine.Append(lblDO.Text)  '-- DO number
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Goods Issue Date" & tab & ": ")
            itemLine.Append(lblGoodIssueDate.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            'itemLine.Append("Expired Claim Date" & tab & ": ")
            'itemLine.Append(lblExpClaimDate.Text)  '-- 
            itemLine.Append("" & tab & ": ")
            itemLine.Append("")  '-- 
            itemLine.Append(tab & tab & tab)  '-- tab space
            itemLine.Append("Payment Date" & tab & ": ")
            itemLine.Append(lblPaymentDate.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file


            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append(tab & tab & tab & tab)
            itemLine.Append("Ready For Delivery Date" & tab & ": ")
            itemLine.Append(lblReadyForDeliveryDate.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append(tab & tab & tab & tab)
            itemLine.Append("Cara Pembayaran" & tab & ": ")
            itemLine.Append(lblCaraPembayaran.Text)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file

        End If

        '-- Read SPDO detail
        Dim arrList As ArrayList = sessHelper.GetSession(Me._sessDataPacking)

        If Not IsNothing(arrList) AndAlso arrList.Count <> 0 Then

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Surat Jalan" & tab)  '-- Part number
            itemLine.Append("Ekpedisi" & tab)   '-- Part name
            itemLine.Append("ATD" & tab)
            itemLine.Append("ETA" & tab)
            itemLine.Append("ATA" & tab)
            itemLine.Append("LOT / CASE" & tab)
            itemLine.Append("Material Packing" & tab)
            itemLine.Append("Description" & tab)
            itemLine.Append("QTY" & tab)
            itemLine.Append("KG" & tab)
            itemLine.Append("M3" & tab)
            sw.WriteLine(itemLine.ToString())      '-- Write header


            For Each sppoLine As SparePartPacking In arrList

                itemLine.Remove(0, itemLine.Length)  '-- Empty line

                If Not IsNothing(sppoLine.SparePartDOExpedition) AndAlso sppoLine.SparePartDOExpedition.ID > 0 Then
                    itemLine.Append(sppoLine.SparePartDOExpedition.ExpeditionNo & tab)
                    itemLine.Append(sppoLine.SparePartDOExpedition.ExpeditionName & tab)
                    itemLine.Append(Format(sppoLine.SparePartDOExpedition.ATD, "dd/MM/yyyy hh:mm") & tab)
                    itemLine.Append(Format(sppoLine.SparePartDOExpedition.ATD.AddDays(_iLeadTime), "dd/MM/yyyy hh:mm") & tab)
                    If sppoLine.SparePartDOExpedition.ATA.Year > 1900 Then
                        itemLine.Append(Format(sppoLine.SparePartDOExpedition.ATA, "dd/MM/yyyy hh:mm") & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                Else
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                End If



                itemLine.Append(sppoLine.LotCase & tab)
                itemLine.Append(sppoLine.PackMaterial & tab)
                itemLine.Append(sppoLine.PackMaterialDesc & tab)
                itemLine.Append(FormatNumber(sppoLine.TotalQty, 0).ToString() & tab)
                itemLine.Append(FormatNumber(sppoLine.Weight, 0).ToString() & tab)
                itemLine.Append(FormatNumber(sppoLine.Volume, 0).ToString() & tab)

                sw.WriteLine(itemLine.ToString())  '-- Write Deposit line
            Next

        End If

    End Sub


    Private Sub Download(ByVal doID As Integer)

        Dim objSPDO As SparePartDO = sessHelper.GetSession(_sessDataDO)

        Dim sFileName As String  '-- File name
        If Not IsNothing(objSPDO) Then
            sFileName = "SPDO" & doID.ToString   '-- Set file name as PO number.xls
        Else
            sFileName = "SPDO"  '-- Dummy file name
        End If
        sFileName = sFileName & "_" & Date.Now.ToString("yyyyMMddHHmmss")

        '-- Temp file must be a randomly named file!
        Dim SPDOData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPDOData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPDOData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteSPDOPackingData(sw, objSPDO)

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


    Private Sub BindPackingList(ByVal SparePartDOID As Integer, ByVal ExpID As Integer)
        Dim totalRow As Integer = 0

        Dim suratJalan As String = txtExpeditionNo.Text.Trim
        Dim partNumber As String = txtPartNumber.Text.Trim

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ExpID > 0 Then
            criterias.opAnd(New Criteria(GetType(SparePartPacking), "SparePartDOExpedition.ID", MatchType.Exact, ExpID))
        Else
            Dim strSql As String = ""

            strSql += " Select p.id"
            strSql += " from SparePartPacking p  "
            strSql += " left join SparePartPackingDetail pdet on p.ID = pdet.SparePartPackingID"
            strSql += " left join SparePartMaster part on pdet.SparePartMasterID = part.ID"
            strSql += " left join SparePartDOExpedition expd on expd.ID = p.SparePartDOExpeditionID"
            strSql += " where 1 = 1"
            If SparePartDOID > 0 Then
                strSql += " and pdet.SparePartDOID = " & SparePartDOID & ""
            End If
            If suratJalan <> "" Then
                strSql += " and expd.ExpeditionNo like '%" & suratJalan & "%'"
            End If
            If partNumber <> "" Then
                strSql += " and part.PartNumber like '%" & partNumber & "%'"
            End If
            criterias.opAnd(New Criteria(GetType(SparePartPacking), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        'Dim arrListTmp As ArrayList = New SparePartPackingFacade(User).RetrieveByCriteria(criterias, pageIndex + 1, dgSPDO.PageSize, totalRow)
        'sessHelper.SetSession(_sessDataPacking, arrListTmp)

        Dim arrListTmp As ArrayList = New SparePartPackingFacade(User).Retrieve(criterias)
        sessHelper.SetSession(_sessDataPacking, arrListTmp)


        If arrListTmp.Count > 0 Then
            dtgPackingList.DataSource = arrListTmp
            'dtgPackingList.VirtualItemCount = totalRow
            dtgPackingList.DataBind()
        Else
            dtgPackingList.DataSource = New ArrayList
            'dtgPackingList.VirtualItemCount = 0
            dtgPackingList.DataBind()
        End If

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        _userLogin = sessHelper.GetSession("DEALER")
        If Me.sessHelper.GetSession("frmPOStatus.PrevPage") Is Nothing Then
            txtUrlToBack.Text = ""
        Else
            txtUrlToBack.Text = sessHelper.GetSession("frmPOStatus.PrevPage")
        End If
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "ExpeditionNo")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            If Not IsNothing(Request.QueryString("DOID")) Then
                DOID = CType(Request.QueryString("DOID"), Integer)
            End If

            If DOID > 0 Then
                GetDataFlow()
                _iLeadTime = GetLeadTime()
                BindHeader()
                BindPackingList(DOID, 0)
                BindExpedition(1)
            End If
        Else
            If Not IsNothing(Request.QueryString("DOID")) Then
                DOID = CType(Request.QueryString("DOID"), Integer)
            End If
        End If
    End Sub

    Private Sub GetDataFlow()
        If Not IsNothing(Request.QueryString("DOID")) Then
            DOID = CType(Request.QueryString("DOID"), Integer)
        End If
        Dim _poID As Integer
        If Not IsNothing(Request.QueryString("POID")) Then
            _poID = CType(Request.QueryString("POID"), Integer)
        End If
        objFlow = New V_SparePartFlow
        Dim objFlowFacade As V_SparePartFlowFacade = New V_SparePartFlowFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_SparePartFlow), "POID", MatchType.GreaterOrEqual, 1))
        criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "POID", MatchType.Exact, _poID))
        criterias.opAnd(New Criteria(GetType(V_SparePartFlow), "DOID", MatchType.Exact, DOID))

        Dim arlFlow As ArrayList ' = New V_SparePartFlowFacade(User).Retrieve(criterias)

        Dim Crt As String = criterias.ToString()
        ViewState("currSortTable") = GetType(V_SparePartFlow)
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        ViewState("currSortColumn") = "POID"
        Dim totalRow As Integer = 0


        arlFlow = New V_SparePartFlowFacade(User).RetrieveCustomPagingBySP(criterias, 0 + 1, 50, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortTable"), System.Type), CType(ViewState("currSortDirection"), Sort.SortDirection))


        If arlFlow.Count > 0 Then
            objFlow = CType(arlFlow(0), V_SparePartFlow)
        End If
    End Sub

    Private Sub dgSPDO_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgSPDO.ItemCommand
        If e.CommandName = "BindPackingList" Then
            If DOID > 0 Then
                'GetDataFlow()
                BindPackingList(DOID, CType(e.Item.Cells(0).Text, Integer))
            End If
        End If
        If e.CommandName = "Save" Then
            Dim calATADate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calATADate")
            Dim expID As Integer = CType(e.Item.Cells(0).Text, Integer)
            Dim oCMFac As New SparePartDOExpeditionFacade(User)
            Dim oSC As SparePartDOExpedition = oCMFac.Retrieve(expID)
            If Not IsNothing(oSC) AndAlso oSC.ID > 0 Then
                'oSC = oCMFac.Retrieve(oSC.ID)
                'If Not IsNothing(oSC) Then
                If calATADate.Value > Date.Now Then
                    MessageBox.Show("Tanggal ATA tidak boleh lebih besar dari tanggal hari ini!")
                    Exit Sub
                End If
                Dim calATADateTime As DateTime = New DateTime(calATADate.Value.Year, calATADate.Value.Month, calATADate.Value.Day, Now.Hour, Now.Minute, Now.Second)
                Dim calATA As DateTime = New DateTime(calATADate.Value.Year, calATADate.Value.Month, calATADate.Value.Day)
                Dim calATD As DateTime = New DateTime(oSC.ATD.Year, oSC.ATD.Month, oSC.ATD.Day)

                If calATA >= calATD Then
                    oSC.ATA = calATADateTime
                    If oCMFac.Update(oSC) = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        BindExpedition(dgSPDO.CurrentPageIndex)
                    End If
                Else
                    MessageBox.Show("Tanggal ATA tidak boleh lebih kecil dari tanggal ATD!")
                    Exit Sub
                End If
                'End If
            End If
        End If
    End Sub

    Private Function GetLeadTime() As Integer
        Dim ireturn As Integer = 0
        Dim transType As Integer = 0
        Dim objDealerLeadTime As DealerLeadTime = New DealerLeadTime()
        Dim objDealerLeadTimeFac As DealerLeadTimeFacade = New DealerLeadTimeFacade(User)
        If (objFlow.OrderType.Trim = "R" Or objFlow.OrderType.Trim = "Z") Then
            transType = 0
        Else
            transType = 1
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerLeadTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerLeadTime), "Dealer.ID", MatchType.Exact, objFlow.DealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerLeadTime), "TransactionType", MatchType.Exact, transType))
        Dim arlLeadTime As ArrayList = objDealerLeadTimeFac.Retrieve(criterias)
        If arlLeadTime.Count > 0 Then
            objDealerLeadTime = CType(arlLeadTime(0), DealerLeadTime)
            ireturn = objDealerLeadTime.Value
        End If
        Return ireturn
    End Function

    Private _userLogin As Dealer

    Private Sub dgSPDO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPDO.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            Dim RowValue As SparePartDOExpedition = CType(e.Item.DataItem, SparePartDOExpedition)

            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgSPDO.PageSize * dgSPDO.CurrentPageIndex)).ToString
            Dim etaDate As DateTime = RowValue.ATD.AddDays(_iLeadTime)

            Dim lblETADate As Label = e.Item.FindControl("lblETADate")
            lblETADate.Text = etaDate.ToString("dd/MM/yyyy")

            Dim calATADate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calATADate")
            If RowValue.ATA.Year < 1900 Then
                calATADate.Value = Date.Now
                If Date.Now <= etaDate Then
                    e.Item.BackColor = Color.White
                End If
                If Date.Now > etaDate Then
                    e.Item.BackColor = Color.Red
                End If
            Else
                'lnkSave
                calATADate.Value = RowValue.ATA
                e.Item.BackColor = Color.LightGreen
            End If

        End If
    End Sub

    Private Sub dgSPDO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSPDO.PageIndexChanged
        dgSPDO.CurrentPageIndex = e.NewPageIndex
        BindExpedition(e.NewPageIndex + 1)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Download(CType(Request.QueryString("DOID"), Integer))
    End Sub

    Private Sub dgSPDO_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgSPDO.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)
        dgSPDO.SelectedIndex = -1
        dgSPDO.CurrentPageIndex = 0
        BindExpedition(dgSPDO.CurrentPageIndex)
    End Sub

    'Private Sub dtgPackingList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgPackingList.ItemCommand
    '    If e.CommandName = "PackingDetail" Then
    '        Response.Redirect("FrmSPDOPackingListDetail.aspx?DOID=" & e.Item.Cells(15).Text & "&POID=" & e.Item.Cells(0).Text)
    '    End If
    'End Sub

    Private Sub dtgPackingList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPackingList.ItemDataBound
        If e.Item.ItemIndex > -1 Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgPackingList.PageSize * dtgPackingList.CurrentPageIndex)).ToString

            Dim objSparePartPacking As SparePartPacking
            objSparePartPacking = CType(sessHelper.GetSession(_sessDataPacking)(e.Item.ItemIndex), SparePartPacking)
            If Not IsNothing(objSparePartPacking) Then
                If Not IsNothing(objSparePartPacking.SparePartDOExpedition) Then
                    e.Item.Cells(2).Text = objSparePartPacking.SparePartDOExpedition.ExpeditionNo
                End If
                Dim lblDetail As LinkButton = e.Item.FindControl("lblDetail")
                If IsNothing(objFlow) Then
                    GetDataFlow()
                End If
                If objSparePartPacking.ID > 0 Then
                    lblDetail.Visible = True
                    lblDetail.Text = "Detail"
                    lblDetail.Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                            "../SparePart/FrmSPDOPackingListDetail.aspx?PLID=" & objSparePartPacking.ID.ToString() & "&SONumber=" & objFlow.SONumber.ToString & "", "", 600, 800, "null")
                End If
            End If
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindPackingList(DOID, 0)
    End Sub

#End Region


End Class