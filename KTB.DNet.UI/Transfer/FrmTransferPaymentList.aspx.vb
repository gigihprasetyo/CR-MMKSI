#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
#End Region



Public Class FrmTransferPaymentList
    Inherits System.Web.UI.Page

#Region "Variables"
    Private _sessHelper As New SessionHelper()

    'Private _sessTransferControls As String = "FrmTransferPaymentList._sessTransferControls"

    Private _vstCritProductCategory As String = "FrmTransferPaymentList._vstCritProductCategory"
    Private _vsCritSearch As String = "FrmTransferPaymentList._vsCritSearch"
    Private _vstCritDealerCode As String = "FrmTransferPaymentList._vstCritDealerCode"
    Private _vstCritChkPlanTransferDate As String = "FrmTransferPaymentList._vstCritChkPlanTransferDate"
    Private _vstCritPlanTransferDateStart As String = "FrmTransferPaymentList._vstCritPlanTransferDateStart"
    Private _vstCritPlanTransferDateEnd As String = "FrmTransferPaymentList._vstCritPlanTransferDateEnd"
    Private _vstCritChkDueDate As String = "FrmTransferPaymentList._vstCritChkDueDate"
    Private _vstCritDueDateStart As String = "FrmTransferPaymentList._vstCritDueDateStart"
    Private _vstCritDueDateEnd As String = "FrmTransferPaymentList._vstCritDueDateEnd"
    Private _vstCritPaymentPurpose As String = "FrmTransferPaymentList._vstCritPaymentPurpose"
    Private _vstCritRegNumber As String = "FrmTransferPaymentList._vstCritRegNumber"
    Private _vstCritSONumber As String = "FrmTransferPaymentList._vstCritSONumber"
    Private _vstCritStatus As String = "FrmTransferPaymentList._vstCritStatus"
    Private _vstCritIsNotOntime As String = "FrmTransferPaymentList._vstCritIsNotOntime"

    Private _sessData As String = "FrmTransferPaymentList._sessData"
    Private _lihat_daftar_pembayaran_transfer_Privilege As Boolean
#End Region

#Region "Custom"

    Private Sub InitPage()

        checkPrivilege()
        initControl()
        BindGrid()

    End Sub

    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")


        _lihat_daftar_pembayaran_transfer_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_pembayaran_transfer_Privilege)

        If Not _lihat_daftar_pembayaran_transfer_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
        End If
        If objDealer.Title = 1 Then '1=KTB

            Me.lblIsNotOntime.Visible = True
            Me.lblIsNotOntimeSeparator.Visible = True
            Me.ddlIsNotOntime.Visible = True
        Else
            Me.lblIsNotOntime.Visible = False
            Me.lblIsNotOntimeSeparator.Visible = False
            Me.ddlIsNotOntime.Visible = False
        End If
    End Sub
    Private Sub initControl()
        Dim dt As Date = DateSerial(Now.Year, Now.Month, 1)
        Dim aPPs As ArrayList
        Dim cPP As New CriteriaComposite(New Criteria(GetType(PaymentPurpose), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPPFac As New PaymentPurposeFacade(User)
        Dim sPP As New SortCollection()

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode:=companyCode)

        cPP.opAnd(New Criteria(GetType(PaymentPurpose), "ID", MatchType.LesserOrEqual, 3))
        cPP.opOr((New Criteria(GetType(PaymentPurpose), "PaymentPurposeCode", MatchType.Exact, "LC")))
        sPP.Add(New Sort(GetType(PaymentPurpose), "ID", Sort.SortDirection.DESC))
        aPPs = aPPFac.Retrieve(cPP, sPP)
        Me.ddlPaymentPurpose.Items.Clear()
        Me.ddlPaymentPurpose.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oPP As PaymentPurpose In aPPs
            Me.ddlPaymentPurpose.Items.Add(New ListItem(oPP.PaymentPurposeCode, oPP.ID))
        Next
        Me.chkPlanTransferDate.Checked = False
        Me.calPlanTransferDateStart.Value = Now
        Me.calPlanTransferDateEnd.Value = Now
        Me.chkPlanTransferDate.Checked = True
        Me.calDueDateStart.Value = Now
        Me.calDueDateEnd.Value = Now



        Me.ddlIsNotOntime.Items.Clear()
        Me.ddlIsNotOntime.Items.Add(New ListItem("Silahkan Pilih", -1))
        Me.ddlIsNotOntime.Items.Add(New ListItem("Tidak", 1))
        Me.ddlIsNotOntime.Items.Add(New ListItem("Ya", 0))

        Me.ddlStatus.Items.Clear()
        Me.ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        Me.ddlStatus.Items.Add(New ListItem(TransferPayment.EnumStatus.Baru.ToString(), TransferPayment.EnumStatus.Baru))
        Me.ddlStatus.Items.Add(New ListItem(TransferPayment.EnumStatus.Batal_Konfirmasi.ToString(), TransferPayment.EnumStatus.Batal_Konfirmasi))
        Me.ddlStatus.Items.Add(New ListItem(TransferPayment.EnumStatus.Konfirmasi.ToString(), TransferPayment.EnumStatus.Konfirmasi))
        Me.ddlStatus.Items.Add(New ListItem(TransferPayment.EnumStatus.Validasi.ToString(), TransferPayment.EnumStatus.Validasi))
        Me.ddlStatus.Items.Add(New ListItem(TransferPayment.EnumStatus.Selesai.ToString(), TransferPayment.EnumStatus.Selesai))
        Me.SaveCriteria()

        Me.btnCari.Visible = True
    End Sub

    Private Sub SaveCriteria()

        Me.ViewState.Add(Me._vstCritProductCategory, Me.ddlProductCategory.SelectedValue)
        Me.ViewState.Add(Me._vstCritDealerCode, Me.txtKodeDealer.Text.Trim)
        Me.ViewState.Add(Me._vstCritChkPlanTransferDate, Me.chkPlanTransferDate.Checked)
        Me.ViewState.Add(Me._vstCritPlanTransferDateStart, Me.calPlanTransferDateStart.Value)
        Me.ViewState.Add(Me._vstCritPlanTransferDateEnd, Me.calPlanTransferDateEnd.Value)
        Me.ViewState.Add(Me._vstCritChkDueDate, Me.chkDueDate.Checked)
        Me.ViewState.Add(Me._vstCritDueDateStart, Me.calDueDateStart.Value)
        Me.ViewState.Add(Me._vstCritDueDateEnd, Me.calDueDateEnd.Value)
        Me.ViewState.Add(Me._vstCritPaymentPurpose, Me.ddlPaymentPurpose.SelectedValue)
        Me.ViewState.Add(Me._vstCritRegNumber, Me.txtRegNumber.Text.Trim)
        Me.ViewState.Add(Me._vstCritSONumber, Me.txtSONumber.Text.Trim)
        Me.ViewState.Add(Me._vstCritStatus, Me.ddlStatus.SelectedValue)
        Me.ViewState.Add(Me._vstCritIsNotOntime, Me.ddlIsNotOntime.SelectedValue)
    End Sub

    Private Sub LoadCriteria()
        Me.ddlProductCategory.SelectedValue = Me.ViewState.Item(Me._vstCritProductCategory)
        Me.txtKodeDealer.Text = Me.ViewState.Item(Me._vstCritDealerCode)
        Me.chkPlanTransferDate.Checked = Me.ViewState.Item(Me._vstCritChkPlanTransferDate)
        Me.calPlanTransferDateStart.Value = Me.ViewState.Item(Me._vstCritPlanTransferDateStart)
        Me.calPlanTransferDateEnd.Value = Me.ViewState.Item(Me._vstCritPlanTransferDateEnd)
        Me.chkDueDate.Checked = Me.ViewState.Item(Me._vstCritChkDueDate)
        Me.calDueDateStart.Value = Me.ViewState.Item(Me._vstCritDueDateStart)
        Me.calDueDateEnd.Value = Me.ViewState.Item(Me._vstCritDueDateEnd)
        Me.ddlPaymentPurpose.SelectedValue = Me.ViewState.Item(Me._vstCritPaymentPurpose)
        Me.txtRegNumber.Text = Me.ViewState.Item(Me._vstCritRegNumber)
        Me.txtSONumber.Text = Me.ViewState.Item(Me._vstCritSONumber)
        Me.ddlStatus.SelectedValue = Me.ViewState.Item(Me._vstCritStatus)
        Me.ddlIsNotOntime.SelectedValue = Me.ViewState.Item(Me._vstCritIsNotOntime)

        Me.btnCari.Visible = True
        Me.dtgMain.Visible = True

    End Sub


    Private Function GetData() As ArrayList

        Dim oTPFac As New vw_TransferPaymentFacade(User)
        Dim aTPs As ArrayList
        Dim ProductCategoryID As Integer = 0
        Dim cTP As New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sTP As New SortCollection()

        sTP.Add(New Sort(GetType(vw_TransferPayment), "CreatedTime", Sort.SortDirection.DESC))

        If Me.ddlProductCategory.SelectedIndex > 0 Then
            ProductCategoryID = Me.ddlProductCategory.SelectedValue
        End If
        If Me.txtKodeDealer.Text.Trim() <> String.Empty Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If
        If Me.chkPlanTransferDate.Checked Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "PlanTransferDate", MatchType.GreaterOrEqual, Me.calPlanTransferDateStart.Value.ToString("yyyy.MM.dd")))
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "PlanTransferDate", MatchType.LesserOrEqual, Me.calPlanTransferDateEnd.Value.ToString("yyyy.MM.dd")))
        End If

        If txtRegDipercepat.Text.Trim <> "" Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "PaymentDiPercepat", MatchType.Partial, txtRegDipercepat.Text.Trim))
        End If

        If txtRegPemercepat.Text.Trim <> "" Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "PaymentPemercepat", MatchType.Partial, txtRegPemercepat.Text.Trim))
        End If
        If Me.chkDueDate.Checked Then
            'cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "DueDate", MatchType.GreaterOrEqual, Me.calDueDateStart.Value.ToString("yyyy.MM.dd")))
            'cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "DueDate", MatchType.LesserOrEqual, Me.calDueDateEnd.Value.ToString("yyyy.MM.dd")))

            Dim StrQSO As String = "(SELECT	a.TransferPaymentID FROM	dbo.TransferPaymentDetail a INNER JOIN dbo.SalesOrder b ON b.ID = a.SalesOrderID INNER JOIN dbo.POHeader c ON c.ID = b.POHeaderID WHERE	 c.ReqAllocationDateTime BETWEEN '{0}' AND  '{1}' AND a.RowStatus = 0 AND b.RowStatus = 0 AND c.RowStatus=0 GROUP BY a.TransferPaymentID)"

            StrQSO = String.Format(StrQSO, Me.calDueDateStart.Value.ToString("yyyy.MM.dd"), Me.calDueDateEnd.Value.AddDays(1).ToString("yyyy.MM.dd"))
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "ID", MatchType.InSet, StrQSO))

        End If
        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            Dim StrQSO As String = "(SELECT	a.TransferPaymentID FROM	dbo.TransferPaymentDetail a INNER JOIN dbo.SalesOrder b ON b.ID = a.SalesOrderID INNER JOIN dbo.POHeader c ON c.ID = b.POHeaderID INNER JOIN dbo.ContractHeader d ON d.ID = c.ContractHeaderID INNER JOIN dbo.Category e ON e.ID = d.CategoryID WHERE	e.ProductCategoryID = '{0}' AND a.RowStatus = 0 AND b.RowStatus = 0 AND c.RowStatus = 0 AND d.RowStatus = 0 GROUP BY a.TransferPaymentID)"

            StrQSO = String.Format(StrQSO, PCID)
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "ID", MatchType.InSet, StrQSO))
        End If
        If Me.ddlPaymentPurpose.SelectedIndex <> 0 Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "PaymentPurpose.ID", MatchType.Exact, CType(Me.ddlPaymentPurpose.SelectedValue, Integer)))
        End If

        If Me.txtDebitNumber.Text.Trim <> String.Empty Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "DebitNumber", MatchType.Exact, Me.txtDebitNumber.Text))
        End If

        If Me.txtRegNumber.Text.Trim <> String.Empty Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "RegNumber", MatchType.Exact, Me.txtRegNumber.Text))
        End If
        If Me.txtSONumber.Text.Trim <> String.Empty Then
            'cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "SalesOrder.SONumber", MatchType.Exact, Me.txtSONumber.Text))
            Dim StrQSO As String = "(SELECT a.TransferPaymentID FROM Dbo.TransferPaymentDetail a INNER JOIN dbo.SalesOrder b ON b.ID = a.SalesOrderID WHERE b.SONumber ='{0}' AND a.RowStatus=0 AND b.RowStatus=0 GROUP BY a.TransferPaymentID)"

            StrQSO = String.Format(StrQSO, Me.txtSONumber.Text.Trim)
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "ID", MatchType.InSet, StrQSO))
        End If
        If Me.ddlStatus.SelectedIndex <> 0 Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "Status", MatchType.Exact, CType(Me.ddlStatus.SelectedValue, Integer)))
        End If
        If Me.ddlIsNotOntime.Visible AndAlso Me.ddlIsNotOntime.SelectedIndex <> 0 Then
            cTP.opAnd(New Criteria(GetType(vw_TransferPayment), "IsNotOnTime", MatchType.Exact, CType(Me.ddlIsNotOntime.SelectedValue, Integer)))
        End If

        Dim objDealer As Dealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            cTP.opAnd(New Criteria(GetType(KTB.DNet.Domain.vw_TransferPayment), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If Not IsNothing(Me._sessHelper.GetSession(Me._vsCritSearch)) Then
            cTP = Me._sessHelper.GetSession(Me._vsCritSearch)
        End If
        'cTP.opAnd(New Criteria(GetType(vw_TransferPayment),""))
        Me._sessHelper.SetSession(Me._vsCritSearch, cTP)
        aTPs = oTPFac.Retrieve(cTP, sTP)


        Dim d As Double = 0
        'get Total Amount
        'FormatNumber(oTP.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        For Each dr As vw_TransferPayment In aTPs
            d = d + dr.Total
        Next
        lblTAmount.Text = FormatNumber(d, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Return aTPs
    End Function


    Private Sub BindGrid()
        Dim aTPs As ArrayList = Me.GetData()

        Me._sessHelper.SetSession(Me._sessData, aTPs)

        Me.dtgMain.DataSource = aTPs
        Me.dtgMain.DataBind()
        Me.dtgMain.Visible = True
    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function
#End Region

#Region "Handle"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        checkPrivilege()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack() Then

            If Not IsNothing(Request.QueryString("Mode")) AndAlso Request.QueryString("Mode").ToString().ToLower() = "new" Then
                Me._sessHelper.SetSession(Me._vsCritSearch, Nothing)
            End If
            InitPage()

        End If


    End Sub

    Private Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Select Case e.CommandName.Trim.ToLower()
            Case "Detail".ToLower()
                Dim aTPs As ArrayList = Me._sessHelper.GetSession(Me._sessData)
                Dim oTP As vw_TransferPayment = aTPs(e.Item.ItemIndex)

                Response.Redirect("FrmTransferPayment.aspx?ID=" & oTP.ID.ToString() & "&RequestPage=FrmTransferPaymentList")
            Case Else

        End Select
    End Sub


    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim aTPs As ArrayList = Me._sessHelper.GetSession(Me._sessData)
            Dim oTP As vw_TransferPayment = aTPs(e.Item.ItemIndex)

            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")
            Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
            Dim lblPlanTransferDate As Label = e.Item.FindControl("lblPlanTransferDate")
            Dim lblDueDate As Label = e.Item.FindControl("lblDueDate")
            Dim lblPaymentPurposeCode As Label = e.Item.FindControl("lblPaymentPurposeCode")
            Dim lblRegNumber As Label = e.Item.FindControl("lblRegNumber")
            Dim lblAmount As Label = e.Item.FindControl("lblAmount")
            Dim lblTransferAmount As Label = e.Item.FindControl("lblNilaiTransfer")
            Dim lblTransferDate As Label = e.Item.FindControl("lblTglTransfer")
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            Dim lblSelisih As Label = e.Item.FindControl("lblSelisih")
            Dim lblActAmount As Label = e.Item.FindControl("lblActAmount")
            Dim lblActTransfer As Label = e.Item.FindControl("lblActTransfer")


            lblNo.Text = (1 + e.Item.ItemIndex).ToString()
            lblProductCategory.Text = ""
            lblDealerCode.Text = oTP.Dealer.DealerCode
            lblPlanTransferDate.Text = oTP.PlanTransferDate.ToString("dd/MM/yyyy")
            lblDueDate.Text = oTP.DueDate.ToString("dd/MM/yyyy")
            lblPaymentPurposeCode.Text = oTP.PaymentPurpose.PaymentPurposeCode
            lblRegNumber.Text = oTP.RegNumber
            lblAmount.Text = FormatNumber(oTP.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblActAmount.Text = FormatNumber(oTP.TotalActualAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            If oTP.ActualTrfDate.Year <= 1900 Then
                lblActTransfer.Text = ""
            Else
                lblActTransfer.Text = oTP.ActualTrfDate.ToString("dd/MM/yyyy")
            End If

            lblTransferAmount.Text = FormatNumber(oTP.TransferAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            If oTP.TransferDate.Year <= 1900 Then
                lblTransferDate.Text = ""
            Else
                lblTransferDate.Text = oTP.TransferDate.ToString("dd/MM/yyyy")
            End If

            Dim lblRegNumberPemercepat As Label = e.Item.FindControl("lblRegNumberPemercepat")
            Dim lblRegNumberDipercepat As Label = e.Item.FindControl("lblRegNumberDipercepat")


            lblRegNumberPemercepat.Text = oTP.PaymentPemercepat
            lblRegNumberDipercepat.Text = oTP.PaymentDiPercepat
            lblStatus.Text = CType(oTP.Status, TransferPayment.EnumStatus).ToString()

            lblSelisih.Text = FormatNumber((oTP.TransferAmount - oTP.Total), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            If (oTP.TransferAmount - oTP.Total) < 0 Then

                e.Item.Cells(10).ForeColor = System.Drawing.Color.Red '"Selisih Transfer"
            End If
            Dim lbtnTransferactual As LinkButton = CType(e.Item.FindControl("lbtnTransferactual"), LinkButton)
            lbtnTransferactual.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Transfer/frmPopUpTransferActual.aspx?TransferPaymentID=" & oTP.ID.ToString(), "", 600, 600, "Detail")
        End If
    End Sub

#End Region

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Me._sessHelper.SetSession(Me._vsCritSearch, Nothing)
        Me.BindGrid()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim aTPs As New ArrayList
        Try
            If Not IsNothing(Me._sessHelper.GetSession(Me._sessData)) AndAlso CType(Me._sessHelper.GetSession(Me._sessData), ArrayList).Count > 0 Then
                aTPs = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)

                Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
                '-- Temp file must be a randomly named text file!
                Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\PaymentTransfer_list_" & sSuffix & ".xls"
                '-- Impersonation to manipulate file in server
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Try
                    If imp.Start() Then
                        Dim finfo As FileInfo = New FileInfo(InvoiceData)
                        If finfo.Exists Then
                            finfo.Delete()  '-- Delete temp file if exists
                        End If
                        '-- Create file stream
                        Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                        '-- Create stream writer
                        Dim sw As StreamWriter = New StreamWriter(fs)
                        '-- Write data to temp file
                        Download(sw, aTPs)
                        sw.Close()
                        fs.Close()
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                    '-- Download data to client!
                    Response.Redirect("../downloadLocal.aspx?file=DataTemp\PaymentTransfer_list_" & sSuffix & ".xls")
                Catch ex As Exception
                    MessageBox.Show("Download data gagal")
                End Try
            Else
                MessageBox.Show(SR.DataProcessNotFound("Payment", "Tidak Ada"))
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail("Report Payment"))
        End Try
    End Sub



    Private Sub Download(ByRef sw As StreamWriter, ByVal PaymentList As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim DataLine As StringBuilder = New StringBuilder

        Dim iNo As Integer = 1
        DataLine.Remove(0, DataLine.Length)  '-- Empty LKPP line
        DataLine.Append("No." & tab)
        DataLine.Append("Kode Dealer" & tab)
        DataLine.Append("Tgl Transfer" & tab)
        DataLine.Append("Tgl Jatuh Tempo" & tab)
        DataLine.Append("Tujuan Pembayaran" & tab)
        DataLine.Append("Nomor Reg" & tab)
        DataLine.Append("Total Amount" & tab)
        DataLine.Append("Total Aktual Amount" & tab)
        DataLine.Append("Tgl Aktual Transfer" & tab)
        DataLine.Append("SO Number" & tab)
        DataLine.Append("SO Amount" & tab)
        DataLine.Append("SO Actual Amount" & tab)
        DataLine.Append("Debit Number" & tab)
        sw.WriteLine(DataLine.ToString())  '-- Write LKPP line
        For Each objHeader As vw_TransferPayment In PaymentList



            DataLine.Remove(0, DataLine.Length)  '-- Empty LKPP line
            DataLine.Append(iNo.ToString() & tab)
            DataLine.Append(objHeader.Dealer.DealerCode & tab)
            DataLine.Append(Format(objHeader.PlanTransferDate, "dd/MM/yyyy") & tab)
            DataLine.Append(Format(objHeader.DueDate, "dd/MM/yyyy") & tab)
            DataLine.Append(objHeader.PaymentPurpose.PaymentPurposeCode & tab)
            DataLine.Append("=""" & objHeader.RegNumber & """" & tab)

            DataLine.Append(FormatNumber(objHeader.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)

            DataLine.Append(FormatNumber(objHeader.TotalActualAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)

            If objHeader.ActualTrfDate.Year <= 1900 Then
                DataLine.Append("" & tab)
            Else
                DataLine.Append(Format(objHeader.ActualTrfDate, "dd/MM/yyyy") & tab)
            End If

            DataLine.Append("" & tab)
            DataLine.Append("" & tab)
            DataLine.Append("" & tab)
            DataLine.Append("" & tab)
            sw.WriteLine(DataLine.ToString())  '-- Write LKPP line


            Dim intNoDetail As Integer = 1
            If objHeader.TransferPaymentDetails.Count > 0 Then
                For Each objDetail As TransferPaymentDetail In objHeader.TransferPaymentDetails
                    DataLine.Remove(0, DataLine.Length)  '-- Empty LKPP line
                    DataLine.Append(iNo.ToString() & "." & intNoDetail.ToString() & tab)
                    DataLine.Append(objHeader.Dealer.DealerCode & tab)
                    DataLine.Append(Format(objHeader.PlanTransferDate, "dd/MM/yyyy") & tab)
                    DataLine.Append(Format(objHeader.DueDate, "dd/MM/yyyy") & tab)
                    DataLine.Append(objHeader.PaymentPurpose.PaymentPurposeCode & tab)
                    DataLine.Append("=""" & objHeader.RegNumber & """" & tab)

                    DataLine.Append(FormatNumber(objHeader.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)

                    DataLine.Append(FormatNumber(objHeader.TotalActualAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)

                    If objHeader.ActualTrfDate.Year <= 1900 Then
                        DataLine.Append("" & tab)
                    Else
                        DataLine.Append(Format(objHeader.ActualTrfDate, "dd/MM/yyyy") & tab)
                    End If

                    DataLine.Append(objDetail.SalesOrder.SONumber & tab)
                    DataLine.Append(FormatNumber(objDetail.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
                    DataLine.Append(FormatNumber(objDetail.ActualAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & tab)
                    DataLine.Append(getDebitNumber(objDetail.SalesOrder.SONumber, objDetail) & tab)
                    sw.WriteLine(DataLine.ToString())
                    intNoDetail = intNoDetail + 1
                Next

            End If
            iNo = iNo + 1
        Next

    End Sub

    Private Function getDebitNumber(ByVal SONumber As String, Optional ByRef oTPD As TransferPaymentDetail = Nothing) As String
        Dim ListInvoice As ArrayList = New ArrayList

        Dim oLDN As LogisticDN = New LogisticDN
        Dim oInvoice As Invoice = New Invoice
        Dim oInvoiceFac As InvoiceFacade = New InvoiceFacade(User)
        Dim oLDNFac As LogisticDNFacade = New LogisticDNFacade(User)

        Dim oSO As SalesOrder = New SalesOrder
        oSO = New SalesOrderFacade(User).Retrieve(SONumber)

        'If ListInvoice.Count > 0 Then
        If oSO.ID > 0 Then
            'oInvoice = CType(ListInvoice(0), Invoice)
            'If Not IsNothing(oInvoice.LogisticDN) Then
            '    oLDN = oLDNFac.Retrieve(oInvoice.LogisticDN.ID)
            'End If


            If Not IsNothing(oTPD) Then
                If oSO.POHeader.IsTransfer = 1 Then
                    'If oInvoice.SalesOrder.POHeader.IsTransfer = 1 Then
                    oTPD.Factoring = "Tidak"
                Else
                    oTPD.Factoring = "Ya"
                End If

                'If Not IsNothing(oLDN) Then
                If Not IsNothing(oSO.LogisticDCHeader) Then
                    Return oSO.LogisticDCHeader.DebitChargeNo
                End If
            Else
                If Not IsNothing(oSO.LogisticDCHeader) Then
                    Return oSO.LogisticDCHeader.DebitChargeNo
                End If
            End If
        Else
            Return String.Empty
        End If
    End Function

End Class