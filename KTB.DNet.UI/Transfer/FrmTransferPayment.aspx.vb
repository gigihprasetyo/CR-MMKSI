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
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.UI.Helper
Imports System.Web.Mail

#End Region

Public Class FrmTransferPayment
    Inherits System.Web.UI.Page

#Region "Variables"

    Private _sessHelper As New SessionHelper()

    Private _sessTransferPaymentDetails As String = "FrmTransferPayment._sessTransferPaymentDetails"
    Private _sessSOAmounts As String = "FrmTransferPayment._sessSOAmounts"
    Private _vstID As String = "_vstID"
    Private _vstcalDueDate As String = "_vstcalDueDate"
    Private _vstTotal As String = "_vstTotal"

    Private _input_pembayaran_transfer_Privilege As Boolean
    Private _validasi_daftar_pembayaran_transfer_Privilege As Boolean
    Private _konfirmasi_daftar_pembayaran_transfer_Privilege As Boolean
    Private _lihat_daftar_pembayaran_transfer_Privilege As Boolean

    Private _vsCritSearch As String = "FrmTransferPaymentList._vsCritSearch"
    Private _sessDebitNumber As String = "FrmTransferPayment.DebitNumber"
    Private _sessFactoring As String = "FrmTransferPayment.Factoring"
#End Region

#Region "Custom"
    Private Sub initPage()
        BindData()

        Dim ds As DataSet
        Dim oTCFac As New TransferCeilingFacade(User)

        ds = oTCFac.RetrieveCeilingStatus(1, "100001", 1, "2016.8.1", "2016.8.31", True)

        Dim i As Integer = 5

    End Sub

    Private Function GetData() As TransferPayment
        Dim ID As Integer = 0
        Dim oTP As TransferPayment
        Dim oTPFac As New TransferPaymentFacade(User)
        Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        Try
            ID = CType(Request.Item("ID"), Integer)
        Catch ex As Exception
            ID = 0
        End Try

        Me.ViewState.Item(Me._vstID) = ID
        If (ID = 0) Then
            oTP = New TransferPayment()
            Try
                Dim oDData As Dealer = New DealerFacade(User).Retrieve(oD.CreditAccount)
                oDData = oD
            Catch ex As Exception

            End Try
            oTP.Dealer = oD
            oTP.RegNumber = "[AUTO]"
            oTP.CreatedTime = DateTime.Now
            oTP.DueDate = DateTime.Now
            oTP.PlanTransferDate = DateTime.Now
            oTP.Status = TransferPayment.EnumStatus.Baru '.ToString()
        Else
            oTP = oTPFac.Retrieve(ID)
        End If

        Return oTP
    End Function

    Private Sub BindData()
        Dim oTP As TransferPayment = Me.GetData()
        Dim IsIntime As Boolean = Me.IsCreatingInTime()

        If IsIntime AndAlso oTP.ID = 0 Then
            Response.Redirect("../ErrorCode.aspx?error=Tidak Ditemukan Referensi Untuk Pembayaran Tidak Sesuai Tanggal Jatuh Tempo.")
        End If

        If oTP.ID = 0 Then
            Me.txtDealerID.Text = oTP.Dealer.ID
            Me.lblDealerCode.Text = oTP.Dealer.DealerCode & " / " & oTP.Dealer.DealerName
            Me.BindPaymentPurpose()
            Me.lblRegNumber.Text = oTP.RegNumber
            Me.lblCreatedDate.Text = oTP.CreatedTime.ToString("dd MMMM yyyy HH:mm:ss")
            Me.calDueDate.Value = oTP.DueDate
            Me.calPlanTransferDate.Value = oTP.PlanTransferDate
            Me.lblStatus.Text = CType(oTP.Status, TransferPayment.EnumStatus).ToString()
            Me.lblTotal.Text = FormatNumber(oTP.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            _sessHelper.SetSession(Me._sessTransferPaymentDetails, New ArrayList())

            'Me.btnSimpan.Visible = True
            'Me.btnValidasi.Visible = False
            'Me.btnPercepatan.Visible = False
        Else
            Me.txtDealerID.Text = oTP.Dealer.ID
            Me.lblDealerCode.Text = oTP.Dealer.DealerCode & " / " & oTP.Dealer.DealerName
            Me.BindPaymentPurpose()
            Me.lblRegNumber.Text = oTP.RegNumber
            Me.lblCreatedDate.Text = oTP.CreatedTime.ToString("dd MMMM yyyy HH:mm:ss")
            Me.calDueDate.Value = oTP.DueDate
            Me.calPlanTransferDate.Value = oTP.PlanTransferDate
            Me.lblStatus.Text = CType(oTP.Status, TransferPayment.EnumStatus).ToString()
            Me.lblTotal.Text = FormatNumber(oTP.Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.ddlPaymentPurpose.SelectedValue = oTP.PaymentPurpose.ID.ToString()
            _sessHelper.SetSession(Me._sessTransferPaymentDetails, oTP.TransferPaymentDetails)

            'Me.btnSimpan.Visible = True
            'Me.btnValidasi.Visible = True
            'Me.btnPercepatan.Visible = True
        End If

        Me.btnSimpan.Visible = False
        Me.btnValidasi.Visible = False
        Me.btnPercepatan.Visible = False
        Me.btnKonfirmasi.Visible = False
        Me.btnBatalKonfirmasi.Visible = False

        Me.calPlanTransferDate.Enabled = False

        _validasi_daftar_pembayaran_transfer_Privilege = SecurityProvider.Authorize(Context.User, SR.validasi_daftar_pembayaran_transfer_Privilege)
        _konfirmasi_daftar_pembayaran_transfer_Privilege = SecurityProvider.Authorize(Context.User, SR.konfirmasi_daftar_pembayaran_transfer_Privilege)

        Dim status As TransferPayment.EnumStatus = oTP.Status
        Dim oD As Dealer = Session("DEALER")
        If oD.Title = EnumDealerTittle.DealerTittle.KTB Then 'KTB
            If oTP.IsNotOnTime = 1 Then 'Percepatan/Perlambatan
                If status = TransferPayment.EnumStatus.Baru Then
                    Me.btnKonfirmasi.Visible = True
                    Me.btnBatalKonfirmasi.Visible = True
                    If Not _konfirmasi_daftar_pembayaran_transfer_Privilege Then
                        Me.btnKonfirmasi.Visible = False
                        Me.btnBatalKonfirmasi.Visible = False
                    End If
                End If

                If status = TransferPayment.EnumStatus.Konfirmasi Then
                    Me.btnValidasi.Visible = True
                    If Not _validasi_daftar_pembayaran_transfer_Privilege Then
                        Me.btnValidasi.Visible = False
                    End If
                End If
            Else
                'Nothing To Do, just wait the status become =SELESAI (from SAP)
            End If
        Else 'DEALER
            If IsIntime = False Then ' MODE Normal bukan percepatan
                If oTP.ID = 0 Then 'Baru
                    Me.btnSimpan.Visible = True
                Else 'Edit
                    If oTP.IsNotOnTime = 0 Then 'Normal 
                        If status = TransferPayment.EnumStatus.Baru Then
                            Me.btnSimpan.Visible = True
                            Me.btnValidasi.Visible = True
                            If Not _validasi_daftar_pembayaran_transfer_Privilege Then
                                Me.btnValidasi.Visible = False
                            End If
                        End If
                        If status <> TransferPayment.EnumStatus.Selesai Then
                            Me.btnPercepatan.Visible = True
                            If Not _validasi_daftar_pembayaran_transfer_Privilege Then
                                Me.btnPercepatan.Visible = False
                            End If
                        End If
                    Else ' Percepatan/Perlambatan
                        If status = TransferPayment.EnumStatus.Baru Then
                            Me.btnSimpan.Visible = True
                        ElseIf status = TransferPayment.EnumStatus.Konfirmasi Then
                            Me.btnValidasi.Visible = True
                            If Not _validasi_daftar_pembayaran_transfer_Privilege Then
                                Me.btnValidasi.Visible = False
                            End If
                        End If
                    End If
                End If
            Else 'pasti baru buat, querystring InTime hanya utk pertama buat percepatan/perlambatan SAJA
                Me.btnSimpan.Visible = True
                Me.calPlanTransferDate.Enabled = True
            End If

            If Not IsNothing(Request.QueryString("RequestPage")) Then
                If Request.QueryString("RequestPage").ToString = "FrmTransferPaymentList" Then
                    If oD.CreditAccount <> oTP.Dealer.CreditAccount Then
                        Me.btnSimpan.Visible = False
                        Me.btnValidasi.Visible = False
                        Me.btnPercepatan.Visible = False
                    End If
                End If
            End If
        End If

        'Dim ID As Integer = 0

        'Try
        '    ID = CType(Request.Item("ID"), Integer)
        'Catch ex As Exception
        '    ID = 0
        'End Try

        'Me.ViewState.Item(Me._vstID) = ID

        'If ID = 0 Then
        '    Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        '    Me.txtDealerID.Text = oD.ID
        '    Me.lblDealerCode.Text = oD.DealerCode & " / " & oD.DealerName
        '    Me.BindPaymentPurpose()
        '    Me.lblRegNumber.Text = "[AUTO]"
        '    Me.lblCreatedDate.Text = Now.ToString("dd MMMM yyyy HH:mm:ss")
        '    Me.calDueDate.Value = Now
        '    Me.calPlanTransferDate.Value = Now
        '    Me.lblStatus.Text = TransferPayment.EnumStatus.Baru.ToString()
        '    Me.lblTotal.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        '    _sessHelper.SetSession(Me._sessTransferPaymentDetails, New ArrayList())

        '    Me.btnValidasi.Visible = False
        '    Me.btnPercepatan.Visible = False
        'Else

        'End If
        Me.BindGrid()
        If oTP.ID > 0 Then
            calDueDate.Enabled = False
            If Not IsNothing(oTP.PaymentPurpose) AndAlso (oTP.PaymentPurpose.PaymentPurposeCode.ToUpper() <> "VH") Then
                btnPercepatan.Visible = False
            End If
        End If
    End Sub

    Private Sub calDueDate_Changed()
        Me.btnRefresSO_Click(Nothing, Nothing)
        Me.ViewState.Item(Me._vstcalDueDate) = Me.calDueDate.Value

    End Sub

    Private Sub BindGrid()
        Dim aRPDs As ArrayList

        aRPDs = _sessHelper.GetSession(Me._sessTransferPaymentDetails)

        Me.dtgMain.DataSource = aRPDs
        Me.dtgMain.DataBind()

        If aRPDs.Count > 0 Then
            Me.calDueDate.Enabled = False
            Me.ddlPaymentPurpose.Enabled = False
        End If
    End Sub

    Private Sub BindPaymentPurpose()
        Dim oPPFac As PaymentPurposeFacade = New PaymentPurposeFacade(User)
        Dim cPP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentPurpose), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sPP As New SortCollection()
        sPP.Add(New Sort(GetType(PaymentPurpose), "PaymentPurposeCode", Sort.SortDirection.DESC))

        Dim aPP As ArrayList = oPPFac.Retrieve(cPP, sPP)

        Me.ddlPaymentPurpose.Items.Clear()
        'Me.ddlPaymentPurpose.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oPP As PaymentPurpose In aPP
            If oPP.PaymentPurposeCode = "IT" OrElse oPP.PaymentPurposeCode = "PP" OrElse oPP.PaymentPurposeCode = "VH" OrElse oPP.PaymentPurposeCode = "LC" Then
                Me.ddlPaymentPurpose.Items.Add(New ListItem(oPP.PaymentPurposeCode, oPP.ID))
            End If
        Next
        'VH = objPOHeader.ReqAllocationDateTime.AddDays(objPOHeader.TermOfPayment.TermOfPaymentValue)
        'PP = End of Month  
        'IT = objPOHeader.ReqAllocationDateTime.AddDays(objPOHeader.TermOfPayment.TermOfPaymentValue)
    End Sub

    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")
        _input_pembayaran_transfer_Privilege = SecurityProvider.Authorize(Context.User, SR.input_pembayaran_transfer_Privilege)


        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then '1=KTB


        Else
            If Not _input_pembayaran_transfer_Privilege Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
            End If
        End If
    End Sub

    Private Function GetDueDateSO(ByVal DealerID As Integer, DueDate As DateTime, PaymentPurposeID As Integer, Optional ByVal TransferPaymentID As Integer = 0) As ArrayList
        Dim oSOLFac As New sp_SOListFacade(User)
        Dim strcompanyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Return oSOLFac.RetrieveFromSP(DealerID, DueDate, PaymentPurposeID, CompanyCode:=strcompanyCode)
    End Function

    Private Sub BindSOList(ByRef ddl As DropDownList, ByRef lblAmountF As Label)
        Dim DealerID As Integer = Me.txtDealerID.Text
        Dim DueDate As DateTime = Me.calDueDate.Value
        Dim PPID As Integer = Me.ddlPaymentPurpose.SelectedValue

        Dim aSOLs As ArrayList = GetDueDateSO(DealerID, DueDate, PPID)
        Dim aSOLFiltered As New ArrayList
        Dim aTPDs As ArrayList = Me._sessHelper.GetSession(Me._sessTransferPaymentDetails)
        Dim IsExist As Boolean
        Dim sName As String

        Me._sessHelper.SetSession(Me._sessSOAmounts, aSOLs)

        For Each oSOL As sp_SOList In aSOLs
            IsExist = False
            For Each oTPD As TransferPaymentDetail In aTPDs
                If oSOL.ID = oTPD.SalesOrder.ID Then
                    IsExist = True
                    Exit For
                End If
            Next
            If IsExist = False Then
                aSOLFiltered.Add(oSOL)
            End If
        Next

        lblAmountF.Text = "0"
        ddl.Items.Clear()
        For Each oSOL As sp_SOList In aSOLFiltered
            sName = oSOL.SONumber ' & " - Rp. " & FormatNumber(oSOL.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            ddl.Items.Add(New ListItem(sName, oSOL.ID))
        Next
        If ddl.Items.Count > 0 Then
            BindDebitNumber(ddl.SelectedItem.Text)
        End If
        If aSOLFiltered.Count > 0 Then
            lblAmountF.Text = FormatNumber(CType(aSOLFiltered(0), sp_SOList).Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub RefreshSOList()
        Dim aTPDs As ArrayList = _sessHelper.GetSession(_sessTransferPaymentDetails)

        If aTPDs.Count = 0 Then
            Me.BindGrid()
        End If
    End Sub

    Private Function IsCreatingInTime() As Boolean
        Dim IsIntime As Boolean = False
        If Not IsNothing(Me.Request.Item("InTime")) Then
            Try
                If Me.Request.Item("InTime") = "1" Then
                    IsIntime = True
                End If
            Catch ex As Exception
                IsIntime = False
            End Try
        End If
        Return IsIntime
    End Function

    Private Sub saveData()
        Dim aTPDs As ArrayList = _sessHelper.GetSession(_sessTransferPaymentDetails)

        If aTPDs.Count = 0 Then
            MessageBox.Show("Simpan Gagal. Tidak Ada SO Yang Dipilih")
            Exit Sub
        End If

        Dim IsIntime As Boolean = Me.IsCreatingInTime()
        Dim TransferDate As Date = Me.calPlanTransferDate.Value
        Dim NowDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim DueDate As Date = Me.calDueDate.Value

        Dim ID As Integer
        Dim oTPFac As New TransferPaymentFacade(User)
        Dim oTP As TransferPayment
        Dim oPP As PaymentPurpose

        oPP = New PaymentPurposeFacade(User).Retrieve(CType(Me.ddlPaymentPurpose.SelectedValue, Integer))

        'Tambahan SLA
        Dim objDailyPaymentFacade As TransferPaymentDetailFacade = New TransferPaymentDetailFacade(User)
        Dim SOinvalidLC As String = objDailyPaymentFacade.isLCExists(oPP.PaymentPurposeCode, aTPDs)
        If SOinvalidLC <> "" Then
            MessageBox.Show("Tujuan Pembayaran LC untuk SO Number " + SOinvalidLC + " belum diinput")
            Exit Sub
        End If
        'end Tambahan SLA

        If oPP.ID = 2 Then
        Else
            If IsIntime Then
                If TransferDate < NowDate OrElse TransferDate = DueDate Then
                    MessageBox.Show("Tanggal Transfer Tidak Valid")
                    Exit Sub
                End If
            Else ' Normal 
                If TransferDate < NowDate OrElse TransferDate <> DueDate Then
                    MessageBox.Show("Tanggal Transfer Tidak Valid")
                    Exit Sub
                End If
            End If
        End If

        Try
            ID = CType(Request.Item("ID"), Integer)
        Catch ex As Exception
            ID = 0
        End Try

        If ID = 0 Then
            Dim oD As Dealer = CType(Session("DEALER"), Dealer)
            oTP = New TransferPayment()

            oTP.RegNumber = Me.lblRegNumber.Text
            oTP.Dealer = New DealerFacade(User).Retrieve(oD.CreditAccount)
            oTP.DueDate = Me.calDueDate.Value
            oTP.PlanTransferDate = Me.calPlanTransferDate.Value
            oTP.Status = TransferPayment.EnumStatus.Baru
            oTP.PaymentPurpose = oPP

            For Each oTPD As TransferPaymentDetail In aTPDs
                oTP.TransferPaymentDetails.Add(oTPD)
            Next
            oTP.ID = oTPFac.Insert(oTP)
        Else
            oTP = oTPFac.Retrieve(ID)

            If Me.IsCreatingInTime() = False Then
                Dim aTPDInDBs As ArrayList = oTP.TransferPaymentDetails
                Dim IsExist As Boolean = False

                'Old
                'For Each oTPDInDB As TransferPaymentDetail In aTPDInDBs
                '    IsExist = False
                '    For Each oTPD As TransferPaymentDetail In aTPDs 'lihat yang ada di DB dengn session
                '        If oTPD.SalesOrder.ID = oTPDInDB.SalesOrder.ID Then
                '            IsExist = True
                '            Exit For
                '        End If
                '    Next
                '    If Not IsExist Then
                '        oTPDInDB.RowStatus = CType(DBRowStatus.Deleted, Short)
                '        oTP.TransferPaymentDetails.Add(oTPDInDB)
                '    End If
                'Next

                oTP.DueDate = Me.calDueDate.Value
                oTP.PlanTransferDate = Me.calPlanTransferDate.Value
                oTP.PaymentPurpose = oPP

                oTP.ID = oTPFac.Update(oTP, aTPDs, aTPDInDBs)
            Else 'Percepatan
                Dim oTPNew As New TransferPayment
                Dim oTPDNew As TransferPaymentDetail

                oTPNew.Dealer = oTP.Dealer
                oTPNew.PaymentPurpose = oPP
                oTPNew.RegNumber = ""
                oTPNew.DueDate = Me.calDueDate.Value
                oTPNew.PlanTransferDate = Me.calPlanTransferDate.Value
                oTPNew.IsNotOnTime = 1
                oTPNew.Status = TransferPayment.EnumStatus.Baru

                For Each oTPD As TransferPaymentDetail In aTPDs
                    oTPDNew = New TransferPaymentDetail()
                    oTPDNew.TransferPayment = oTPNew
                    oTPDNew.SalesOrder = oTPD.SalesOrder
                    oTPDNew.Amount = oTPD.Amount
                    oTPDNew.TransferPaymentNewID = oTPD.ID 'TODO :  salah nama kolom

                    oTPNew.TransferPaymentDetails.Add(oTPDNew)
                Next
                oTPNew.ID = oTPFac.Insert(oTPNew)
                If oTPNew.ID > 0 Then
                    oTP.Status = TransferPayment.EnumStatus.Batal
                    oTP.ID = oTPFac.UpdateSimple(oTP)
                End If

                If oTPNew.ID > 0 Then
                    'MessageBox.Show(SR.SaveSuccess())
                    oTP = oTPNew
                    Dim StrUrl As String = "FrmTransferPayment.aspx?ID=" & oTP.ID & "&msg=" & SR.SaveSuccess()
                    Response.Redirect(StrUrl)
                Else
                    MessageBox.Show(SR.SaveFail)
                    Exit Sub
                End If
            End If
        End If

        If oTP.ID > 0 Then
            'MessageBox.Show(SR.SaveSuccess())
            'oTP = oTPFac.Retrieve(oTP.ID)
            Response.Redirect("FrmTransferPayment.aspx?ID=" & oTP.ID & "&msg=" & SR.SaveSuccess())
        Else
            MessageBox.Show(SR.SaveFail())
        End If

    End Sub

    Private Sub TransferToSAP(ByVal oTP As TransferPayment)
        Dim oFH As New FileHelper()

        Dim str As FileInfo
        'Try
        str = oFH.TransferPaymentTransferToSAP(oTP)
        'MessageBox.Show(SR.UploadSucces(str.Name))
        'Catch ex As Exception
        '    '   MessageBox.Show(SR.UploadFail(str.Name))
        'End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(ViewState.Item(Me._vstcalDueDate)) Then
            If CType(ViewState.Item(Me._vstcalDueDate), Date).Day <> Me.calDueDate.Value.Day _
            AndAlso CType(ViewState.Item(Me._vstcalDueDate), Date).Month <> Me.calDueDate.Value.Month _
            AndAlso CType(ViewState.Item(Me._vstcalDueDate), Date).Year <> Me.calDueDate.Value.Year Then
                Me.calDueDate_Changed()
            End If
        Else
            Me.ViewState.Add(Me._vstcalDueDate, Me.calDueDate.Value)
        End If
        checkPrivilege()
        If Not IsPostBack Then
            If Not IsNothing(Me.Request.QueryString("msg")) Then
                MessageBox.Show(Me.Request.QueryString("msg"))
            End If

            If Not IsNothing(Me.Request.QueryString("Mode")) Then
                Me._sessHelper.SetSession(Me._vsCritSearch, Nothing)
            End If
            If Not IsNothing(Me._sessHelper.GetSession(Me._vsCritSearch)) Then
                BtnKembali.Visible = True
            End If

            initPage()
            Me.calDueDate.ScriptOnFocusOut = "calDueDate_LostFocus()"

            'btnSimpan.Attributes.Add("onclick", "this.disabled='true';")
            'btnSimpan.Attributes.Add("onload", "this.disabled='false';")
            btnSimpan.OnClientClick = "this.disabled=true;"
            btnSimpan.UseSubmitBehavior = False
        End If

        Dim ddlSOF As DropDownList
        Dim e1 As Control
        For Each e1 In Me.dtgMain.Controls
            For Each ct As Control In e1.Controls
                If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                    Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                    If di.ItemType = ListItemType.Footer Then
                        ddlSOF = di.FindControl("ddlSOF")
                    End If
                End If
            Next
        Next

        If ddlSOF.Items.Count > 0 Then
            BindDebitNumber(ddlSOF.SelectedItem.Text)
        End If

    End Sub

    Private Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Dim lblDebitNumberF As Label
        Dim lblFactoringF As Label

        Select Case e.CommandName.Trim().ToLower()
            Case "SOChangeF".ToLower()
                Dim ddlSOF As DropDownList = e.Item.FindControl("ddlSOF")
                Dim lblAmountF As Label = e.Item.FindControl("lblAmountF")
                Dim SOID As Integer = ddlSOF.SelectedValue
                Dim aSOLs As ArrayList = _sessHelper.GetSession(_sessSOAmounts)

                BindDebitNumber(ddlSOF.SelectedItem.Text)
                For Each oSOL As sp_SOList In aSOLs
                    If oSOL.ID = SOID Then
                        lblAmountF.Text = FormatNumber(oSOL.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    End If
                Next
            Case "Add".ToLower()
                Dim oTPD As New TransferPaymentDetail
                Dim ddlSOF As DropDownList = e.Item.FindControl("ddlSOF")
                Dim lblAmountF As Label = e.Item.FindControl("lblAmountF")
                lblDebitNumberF = e.Item.FindControl("lblDebitNumberF")
                lblFactoringF = e.Item.FindControl("lblFactoringF")

                oTPD.DebitNumber = lblDebitNumberF.Text
                oTPD.Factoring = lblFactoringF.Text
                Dim oSO As SalesOrder
                Try
                    oSO = New SalesOrderFacade(User).Retrieve(CType(ddlSOF.SelectedValue, Integer))
                Catch ex As Exception
                    Return
                End Try

                Dim aSOLs As ArrayList = _sessHelper.GetSession(_sessTransferPaymentDetails)

                'oTPD.SalesOrder.ID = ddlSOF.SelectedValue
                oTPD.SalesOrder = oSO
                oTPD.Amount = CType(lblAmountF.Text, Decimal)
                oTPD.RowStatus = CType(DBRowStatus.Active, Short)

                'Append
                Dim DealerID As Integer = Me.txtDealerID.Text
                Dim DueDate As DateTime = Me.calDueDate.Value
                Dim PPID As Integer = Me.ddlPaymentPurpose.SelectedValue

                Dim ObjLokal As ArrayList = GetDueDateSO(DealerID, DueDate, PPID)
                For Each oSOLL As sp_SOList In ObjLokal
                    If oSO.ID = oSOLL.ID Then
                        If oTPD.Amount = 0 OrElse oTPD.Amount <> oSOLL.Amount Then
                            oTPD.Amount = oSOLL.Amount
                        End If
                    End If
                Next

                Dim isNotExist As Boolean = True

                For Each orr As TransferPaymentDetail In aSOLs
                    If orr.SalesOrder.ID = oTPD.SalesOrder.ID Then
                        isNotExist = False
                    End If
                Next
                'end Of append
                If isNotExist Then
                    aSOLs.Add(oTPD)
                    _sessHelper.SetSession(_sessTransferPaymentDetails, aSOLs)
                    Me.BindGrid()
                Else
                    MessageBox.Show(SR.DataIsExist(oSO.SONumber))
                    Me.BindGrid()
                End If
        End Select
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        Dim Total As Decimal = 0

        If e.Item.ItemType = ListItemType.Header Then
            If IsNothing(Me.ViewState.Item(Me._vstTotal)) Then
                Me.ViewState.Add(Me._vstTotal, 0)
            Else
                Me.ViewState.Item(Me._vstTotal) = 0
            End If
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oTPD As TransferPaymentDetail = CType(_sessHelper.GetSession(Me._sessTransferPaymentDetails), ArrayList)(e.Item.ItemIndex)
            Dim lblSONumber As Label = e.Item.FindControl("lblSONumber")
            Dim lblAmount As Label = e.Item.FindControl("lblAmount")
            Dim lblDesc As Label = e.Item.FindControl("lblDesc")
            Dim lblDebitNumber As Label = e.Item.FindControl("lblDebitNumber")
            Dim lblFactoring As Label = e.Item.FindControl("lblFactoring")

            If oTPD.DebitNumber = String.Empty Then
                BindDebitNumber(oTPD.SalesOrder.SONumber, oTPD)
            End If
            lblDebitNumber.Text = oTPD.DebitNumber
            lblFactoring.Text = oTPD.Factoring

            lblSONumber.Text = oTPD.SalesOrder.SONumber
            lblAmount.Text = FormatNumber(oTPD.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblDesc.Text = ""

            Total = CType(Me.ViewState.Item(Me._vstTotal), Decimal)
            Total += oTPD.Amount

            Me.ViewState.Item(Me._vstTotal) = Total
        ElseIf e.Item.ItemType = ListItemType.EditItem Then

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim ddlSOF As DropDownList = e.Item.FindControl("ddlSOF")
            Dim lblAmountF As Label = e.Item.FindControl("lblAmountF")

            Me.BindSOList(ddlSOF, lblAmountF)

            Total = CType(Me.ViewState.Item(Me._vstTotal), Decimal)
            Me.lblTotal.Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub ddlPaymentPurpose_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentPurpose.SelectedIndexChanged
        Me.btnRefresSO_Click(Nothing, Nothing)
    End Sub

    Protected Sub btnRefresSO_Click(sender As Object, e As EventArgs) Handles btnRefresSO.Click
        If Me.calPlanTransferDate.Enabled = False Then
            Me.calPlanTransferDate.Value = Me.calDueDate.Value
        End If
        Me.RefreshSOList()
    End Sub

    Private Function IsValidCompany(ByVal OTP As TransferPayment) As Boolean
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        For Each td As TransferPaymentDetail In OTP.TransferPaymentDetails
            If td.SalesOrder.POHeader.ContractHeader.Category.ProductCategory.Code <> companyCode Then
                Return False
            End If
        Next
        Return True
    End Function

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        'Check current status


        Dim oTP As TransferPayment = Me.GetData()
        Dim status As TransferPayment.EnumStatus = oTP.Status
        Dim IsValidStatus As Boolean = False

        If oTP.IsNotOnTime = 1 Then
            If status = TransferPayment.EnumStatus.Konfirmasi Then
                IsValidStatus = True
            End If
        Else
            If status = TransferPayment.EnumStatus.Baru Then
                IsValidStatus = True
            End If
        End If

        If IsValidStatus = False Then
            MessageBox.Show("Status " & status.ToString() & " Tidak Bisa Divalidasi")
            Exit Sub
        End If
        'If Not (status = TransferPayment.EnumStatus.Baru OrElse status = TransferPayment.EnumStatus.Konfirmasi) Then
        '    MessageBox.Show("Status " & status.ToString() & " Tidak Bisa Divalidasi")
        '    Exit Sub
        'End If
        'Update status
        If Not IsValidCompany(oTP) Then
            MessageBox.Show("Transaksi beda company")
            Return
        End If

        Dim oTPFac As New TransferPaymentFacade(User)


        oTP.Status = TransferPayment.EnumStatus.Validasi

        Try
            Me.TransferToSAP(oTP)
        Catch ex As Exception
            SendErrEmail(oTP)
            MessageBox.Show("Update Status Gagal")
            Return
        End Try

        Try

            oTP.ValidatedTime = DateTime.Now
            oTP.ValidatedBy = User.Identity.Name
            oTP.ID = oTPFac.UpdateSimple(oTP)
            If oTP.ID > 0 Then
                Me.lblStatus.Text = TransferPayment.EnumStatus.Validasi.ToString()

            Else
                SendErrEmail(oTP)
                MessageBox.Show("Update Status Gagal")
                Return
            End If
        Catch ex As Exception
            SendErrEmail(oTP)
            MessageBox.Show("Update Status Gagal")
            Return
        End Try

        Try
            Response.Redirect("FrmTransferPayment.aspx?ID=" & oTP.ID.ToString() & "&msg=Update Status Berhasil")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SendErrEmail(ByVal ObjPT As TransferPayment)
        Dim sTO As String = KTB.DNet.Lib.WebConfig.GetValue("EmailPTRecipient")
        Dim sCC As String = ""
        Dim subject As String = "[MMKSI-DNet] Sales - Payment Transfer Notification - (Failed)"
        Dim Dir As String = Server.MapPath("") & "\..\DataFile\EmailTemplate\PaymentTransferFailed.htm"
        Try
            Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")
            Dim str = New StringBuilder()
            str.Append("<tr><td align='center'>1</td><td align='center'>" & ObjPT.RegNumber & "</td> </tr>")
            Dim sContents() As String = {objDealer.DealerCode, objDealer.DealerName, objDealer.City.CityName, Date.Now.ToString("dd MMMM yyyy"), str.ToString(), Now.ToString("dd MMMM yyyy")}

            Me.SendEmail(Dir, sTO, sCC, subject, sContents)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SendEmail(ByVal EmailFile As String, ByVal sTo As String, ByVal sCC As String, ByVal sSubject As String, ByVal sMessage() As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdmin")
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(EmailFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, sMessage)
        If Not IsNothing(sCC) AndAlso sCC.Trim() <> "" AndAlso sCC.EndsWith(";") = False AndAlso Not IsNothing(emailAdmin) AndAlso emailAdmin <> "" Then
            emailAdmin = ";" & emailAdmin
        End If

        'ObjEmail.sendMail(sTo, sCC & emailAdmin, emailFrom, sSubject, Mail.MailFormat.Html, szEmailContent)
        ObjEmail.sendMail(sTo, sCC, emailAdmin, emailFrom, sSubject, MailFormat.Html, szEmailContent)
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        btnSimpan.Enabled = True
        saveData()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim aIdxs As New ArrayList
        Dim aTPDs As ArrayList = CType(_sessHelper.GetSession(Me._sessTransferPaymentDetails), ArrayList)

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim chkSelect As CheckBox = di.FindControl("chkSelect")

            If Not IsNothing(chkSelect) AndAlso chkSelect.Checked Then
                aIdxs.Add(di.ItemIndex)
            End If
        Next

        Dim oSPSO As sp_SOList
        Dim oPP As PaymentPurpose = New PaymentPurposeFacade(User).Retrieve(CType(Me.ddlPaymentPurpose.SelectedValue, Integer))
        Dim aSOLs As ArrayList = Me._sessHelper.GetSession(_sessSOAmounts)

        For i As Integer = aIdxs.Count - 1 To 0 Step -1
            oSPSO = New sp_SOList()
            oSPSO.ID = CType(aTPDs(aIdxs(i)), TransferPaymentDetail).SalesOrder.ID
            oSPSO.SONumber = CType(aTPDs(aIdxs(i)), TransferPaymentDetail).SalesOrder.SONumber
            If oPP.PaymentPurposeCode = "VH" Then
                oSPSO.Amount = CType(aTPDs(aIdxs(i)), TransferPaymentDetail).SalesOrder.TotalVH
            ElseIf oPP.PaymentPurposeCode = "PP" Then
                oSPSO.Amount = CType(aTPDs(aIdxs(i)), TransferPaymentDetail).SalesOrder.TotalPP
            ElseIf oPP.PaymentPurposeCode = "IT" Then
                oSPSO.Amount = CType(aTPDs(aIdxs(i)), TransferPaymentDetail).SalesOrder.TotalIT
            End If
            aSOLs.Add(oSPSO)

            aTPDs.RemoveAt(aIdxs(i))
        Next
        _sessHelper.SetSession(_sessSOAmounts, aSOLs)
        _sessHelper.SetSession(_sessTransferPaymentDetails, aTPDs)
        Me.BindGrid()
    End Sub

    Protected Sub btnPercepatan_Click(sender As Object, e As EventArgs) Handles btnPercepatan.Click
        Dim oTP As TransferPayment = Me.GetData()
        If Not IsValidCompany(oTP) Then
            MessageBox.Show("Transaksi beda company")
            Return
        End If
        If Not IsNothing(oTP) AndAlso oTP.ID > 0 AndAlso oTP.Status <> TransferPayment.EnumStatus.Selesai Then
            Response.Redirect("FrmTransferPayment.aspx?ID=" & oTP.ID.ToString() & "&InTime=1")

        End If
    End Sub

    Protected Sub btnKonfirmasi_Click(sender As Object, e As EventArgs) Handles btnKonfirmasi.Click
        'Check current status


        Dim oTP As TransferPayment = Me.GetData()
        Dim status As TransferPayment.EnumStatus = oTP.Status
        Dim IsValidStatus As Boolean = False

        If oTP.IsNotOnTime = 1 Then
            If status = TransferPayment.EnumStatus.Baru Then
                IsValidStatus = True
            End If
        End If

        If IsValidStatus = False Then
            MessageBox.Show("Status " & status.ToString() & " Tidak Bisa Dikonfirmasi")
            Exit Sub
        End If
        'If Not (status = TransferPayment.EnumStatus.Baru OrElse status = TransferPayment.EnumStatus.Konfirmasi) Then
        '    MessageBox.Show("Status " & status.ToString() & " Tidak Bisa Divalidasi")
        '    Exit Sub
        'End If
        'Update status
        Dim oTPFac As New TransferPaymentFacade(User)


        oTP.Status = TransferPayment.EnumStatus.Konfirmasi
        oTP.ID = oTPFac.UpdateSimple(oTP)

        If oTP.ID > 0 Then
            Me.lblStatus.Text = TransferPayment.EnumStatus.Konfirmasi.ToString()
            Response.Redirect("FrmTransferPayment.aspx?id=" & oTP.ID.ToString() & "&msg=Update Status Berhasil")
            'MessageBox.Show("Update Status Berhasil")
        Else
            MessageBox.Show("Update Status Gagal")
        End If
    End Sub

    Private Sub BindDebitNumber(ByVal SONumber As String, Optional ByRef oTPD As TransferPaymentDetail = Nothing)
        Dim lblDebitNumberF As Label
        Dim lblFactoringF As Label

        Dim e1 As Control
        For Each e1 In Me.dtgMain.Controls
            For Each ct As Control In e1.Controls
                If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                    Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                    If di.ItemType = ListItemType.Footer Then
                        lblDebitNumberF = di.FindControl("lblDebitNumberF")
                        lblFactoringF = di.FindControl("lblFactoringF")
                    End If
                End If
            Next
        Next

        Dim ListInvoice As ArrayList = New ArrayList

        Dim oLDN As LogisticDN = New LogisticDN
        Dim oInvoice As Invoice = New Invoice
        Dim oInvoiceFac As InvoiceFacade = New InvoiceFacade(User)
        Dim oLDNFac As LogisticDNFacade = New LogisticDNFacade(User)

        'Dim cInvoice As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Invoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'cInvoice.opAnd(New Criteria(GetType(Invoice), "SalesOrder.SONumber", MatchType.Exact, SONumber))
        'ListInvoice = oInvoiceFac.Retrieve(cInvoice)

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
                    'If oLDN.ID <> 0 Then
                    'oTPD.DebitNumber = oLDN.LogisticDCHeader.DebitChargeNo
                    oTPD.DebitNumber = oSO.LogisticDCHeader.DebitChargeNo
                    'End If
                End If
            Else
                If oSO.POHeader.IsTransfer = 1 Then
                    'If oInvoice.SalesOrder.POHeader.IsTransfer = 1 Then
                    lblFactoringF.Text = "Tidak"
                Else
                    lblFactoringF.Text = "Ya"
                End If

                If Not IsNothing(oSO.LogisticDCHeader) Then
                    'If Not IsNothing(oLDN) Then
                    'If oLDN.ID <> 0 Then
                    '    lblDebitNumberF.Text = oLDN.LogisticDCHeader.DebitChargeNo
                    lblDebitNumberF.Text = oSO.LogisticDCHeader.DebitChargeNo
                    'End If
                End If
            End If
        Else
            lblFactoringF.Text = ""
            lblDebitNumberF.Text = ""
        End If
    End Sub
#End Region

    Protected Sub btnBatalKonfirmasi_Click(sender As Object, e As EventArgs) Handles btnBatalKonfirmasi.Click
        'Check current status


        Dim oTP As TransferPayment = Me.GetData()
        Dim status As TransferPayment.EnumStatus = oTP.Status
        Dim IsValidStatus As Boolean = False

        If oTP.IsNotOnTime = 1 Then
            If status = TransferPayment.EnumStatus.Baru Then
                IsValidStatus = True
            End If
        End If

        If IsValidStatus = False Then
            MessageBox.Show("Status " & status.ToString() & " Tidak Bisa Dikonfirmasi")
            Exit Sub
        End If
        'If Not (status = TransferPayment.EnumStatus.Baru OrElse status = TransferPayment.EnumStatus.Konfirmasi) Then
        '    MessageBox.Show("Status " & status.ToString() & " Tidak Bisa Divalidasi")
        '    Exit Sub
        'End If
        'Update status
        Dim oTPFac As New TransferPaymentFacade(User)


        oTP.Status = TransferPayment.EnumStatus.Batal_Konfirmasi
        oTP.ID = oTPFac.UpdateSimple(oTP)

        If oTP.ID > 0 Then
            Me.lblStatus.Text = TransferPayment.EnumStatus.Batal_Konfirmasi.ToString()
            MessageBox.Show("Update Status Berhasil")
        Else
            MessageBox.Show("Update Status Gagal")
        End If
    End Sub

    Protected Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnKembali.Click
        Response.Redirect("FrmTransferPaymentList.aspx")
    End Sub
End Class