#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports KTB.DNet.BusinessFacade.Tools
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessFacade.MDP
#End Region

Public Class ConfirmDailyPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDuration As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAction As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CustomValidator1 As System.Web.UI.WebControls.CustomValidator
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTotalHargaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaTebusValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents btnTransferUlang As System.Web.UI.WebControls.Button
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents btnPassTOP As System.Web.UI.WebControls.Button
    Protected WithEvents CancelTOP As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPaymentType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPaymentScheme As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaTebusPP As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents Label24 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaTebusIT As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaTebusLC As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoRegPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNoMO As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoMO As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtColorGreen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtColor As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFreePPh As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBlock As System.Web.UI.WebControls.Button
    Protected WithEvents ddlFactoring As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPernahTahanDO As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTahanDO As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahanDOColon As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownloadGyro As System.Web.UI.WebControls.Button
    Protected WithEvents ddlRegion As System.Web.UI.WebControls.DropDownList

    Protected WithEvents btnTest As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransfer As System.Web.UI.WebControls.Button
    Protected WithEvents txtPass As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUser As System.Web.UI.WebControls.TextBox

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
    Private _ListPO As ArrayList
    Private _ListPOHeader As ArrayList
    Private objDealer As Dealer
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtKodeDealer.Text)
        objSSPO.Add(GetSelectedItem(lboxStatus))
        objSSPO.Add(calDari.Value)
        objSSPO.Add(calSampai.Value)
        objSSPO.Add(txtPONumber.Text)
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(dtgPO.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        objSSPO.Add(ddlFactoring.SelectedValue)
        objSSPO.Add(txtNoRegPO.Text)
        objSSPO.Add(txtNoMO.Text)
        objSSPO.Add(ddlFreePPh.SelectedValue)
        objSSPO.Add(ddlPaymentType.SelectedValue)


        sessionHelper.SetSession("SESSIONDAILYPOPO", objSSPO)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONDAILYPOPO")
        If Not objSSPO Is Nothing Then
            txtKodeDealer.Text = objSSPO.Item(0)
            Dim str() As String = objSSPO.Item(1).ToString().Split(",")
            For Each item As ListItem In lboxStatus.Items
                For i As Integer = 0 To str.Length - 1
                    If item.Value.ToString = str(i).ToString Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
            calDari.Value = objSSPO.Item(2)
            calSampai.Value = objSSPO.Item(3)
            txtPONumber.Text = objSSPO.Item(4)
            ddlCategory.SelectedIndex = objSSPO.Item(5)
            ddlOrderType.SelectedIndex = objSSPO.Item(6)
            dtgPO.CurrentPageIndex = objSSPO.Item(7)
            ViewState("CurrentSortColumn") = objSSPO.Item(8)
            ViewState("CurrentSortDirect") = objSSPO.Item(9)
            ddlFactoring.SelectedValue = objSSPO.Item(10)
            txtNoRegPO.Text = objSSPO.Item(11)
            txtNoMO.Text = objSSPO.Item(12)
            ddlFreePPh.SelectedValue = objSSPO.Item(13)
            ddlPaymentType.SelectedValue = objSSPO.Item(14)

            Return True
        End If
        Return False
    End Function

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Sub BindToddlCategory()
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
        ddlCategory.Items.Clear()
        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New ListItem("Silahkan Pilih", -1)
            ddlCategory.Items.Add(listitemBlank)
        End If

        Dim PCID As Short = GetProductCategoryID()
        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            End If
        Next
        'Dim listitemBlank As New listItem("Silahkan Pilih", -1)

        'ddlCategory.Items.Add(listitemBlank)
        'For Each item As Category In arrayListDealer
        '    Dim listItem As New listItem(item.CategoryCode, item.ID)
        '    ddlCategory.Items.Add(listItem)
        'Next

    End Sub

    Private Sub BindToddl()

        Dim listitemBlank As ListItem

        For Each item As ListItem In LookUp.ArrayStatusPO
            lboxStatus.Items.Add(item)
        Next
        lboxStatus.SelectedIndex = -1


        If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Semua) Then
            listitemBlank = New ListItem("Silahkan Pilih", -1)
            ddlOrderType.Items.Add(listitemBlank)
        End If

        For Each item As ListItem In LookUp.ArrayJenisPO
            If item.Text = "Harian" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) Then
                    ddlOrderType.Items.Add(item)
                End If
            ElseIf item.Text = "Tambahan" Then
                If SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) Then
                    ddlOrderType.Items.Add(item)
                End If
            End If
        Next

        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlOrderType.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArrayJenisPO
        '    ddlOrderType.Items.Add(item)
        'Next
        'ddlOrderType.SelectedValue = -1

        'listitemBlank = New ListItem("Silahkan Pilih", -1)
        'ddlStatus.Items.Add(listitemBlank)
        'For Each item As ListItem In LookUp.ArrayStatusPO
        '    ddlStatus.Items.Add(item)
        'Next
        'ddlStatus.SelectedValue = -1
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOBlock_Privilege) Then
            ddlAction.Items.RemoveAt(11)
            ddlAction.Items.RemoveAt(10)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPODisApprove_Privilege) Then
            ddlAction.Items.RemoveAt(9)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOApprove_Privilege) Then
            ddlAction.Items.RemoveAt(8)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOReleasa_Privilege) Then
            ddlAction.Items.RemoveAt(7)
            ddlAction.Items.RemoveAt(6)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOReject_Privilege) Then
            ddlAction.Items.RemoveAt(5)
            ddlAction.Items.RemoveAt(4)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOConfirm_Privilege) Then
            ddlAction.Items.RemoveAt(3)
            ddlAction.Items.RemoveAt(2)
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOCancel_Privilege) Then
            ddlAction.Items.RemoveAt(1)
        End If


        'start donas 20160916 Payment Transfer
        Me.ddlPaymentScheme.Items.Clear()
        Me.ddlPaymentScheme.Items.Add(New ListItem("Semua", -1))
        Me.ddlPaymentScheme.Items.Add(New ListItem(TransferControl.EnumPaymentScheme.Gyro.ToString(), TransferControl.EnumPaymentScheme.Gyro))
        Me.ddlPaymentScheme.Items.Add(New ListItem(TransferControl.EnumPaymentScheme.Transfer.ToString(), TransferControl.EnumPaymentScheme.Transfer))
        'end donas 20160916 Payment Transfer
    End Sub

    Private Sub BindDdlFreePPh()
        ddlFreePPh.Items.Clear()
        ddlFreePPh.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlFreePPh.Items.Add(New ListItem("Ya", 0))
        ddlFreePPh.Items.Add(New ListItem("Tidak", 1))
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindTodtgPO(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "BlockedStatus", MatchType.NotInSet, enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed & "," & enumPOBlockedStatus.POBlockedStatus.Blocked))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "POType", MatchType.Exact, ddlOrderType.SelectedValue))
        'If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.POHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        If txtPONumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "DealerPONumber", MatchType.StartsWith, txtPONumber.Text))

        If txtNoRegPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "PONumber", MatchType.Exact, txtNoRegPO.Text))
        End If

        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If

        If txtNoMO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.ContractNumber", MatchType.Exact, txtNoMO.Text))
        End If

        'Start  :DailyPO - add info about PPh Status
        If ddlFreePPh.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "FreePPh22Indicator", MatchType.Exact, ddlFreePPh.SelectedValue))
        End If
        'End    :DailyPO - add info about PPh Status
        If CType(ddlFactoring.SelectedValue, Short) <> 2 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "IsFactoring", MatchType.Exact, CType(ddlFactoring.SelectedValue, Short)))
        End If
        ' Modified by Ikhsan, 20081211
        ' Requested by Yurike as Part Of CR
        ' To add the selection criteria
        ' Start -----
        'If ddlPaymentType.SelectedIndex = 1 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.TermOfPaymentCode", MatchType.Exact, "Z000"))
        'ElseIf ddlPaymentType.SelectedIndex = 2 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.TermOfPaymentCode", MatchType.No, "Z000"))
        'End If
        ' End -------

        'Add RTGS
        If ddlPaymentType.SelectedValue > 0 Then
            If ddlPaymentType.SelectedItem.Text.Trim.ToUpper.EndsWith("TOP") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.TOP, Short)))
            ElseIf ddlPaymentType.SelectedItem.Text.Trim.ToUpper = "COD" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.COD, Short)))
            ElseIf ddlPaymentType.SelectedItem.Text.Trim.ToUpper = "RTGS" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.RTGS, Short)))
            End If
        End If
        'end Add RTGS
        Dim X As POHeader

        If Me.ddlPernahTahanDO.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "DOBlockHistory", MatchType.Exact, CType(Me.ddlPernahTahanDO.SelectedValue, Integer)))
        End If
        If Me.ddlRegion.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "PODestination.RegionDesc", MatchType.Exact, Me.ddlRegion.SelectedValue))
        End If
        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PCID))
        End If

        If Me.ddlPaymentScheme.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "IsTransfer", MatchType.Exact, CType(Me.ddlPaymentScheme.SelectedValue, Short)))
        End If

        Dim tglDari As DateTime = New DateTime(Me.calDari.Value.Year, Me.calDari.Value.Month, Me.calDari.Value.Day)
        Dim tglSampai As DateTime = New DateTime(Me.calSampai.Value.Year, Me.calSampai.Value.Month, Me.calSampai.Value.Day)

        If tglSampai >= tglDari Then
            Dim Time As TimeSpan = tglSampai.Subtract(tglDari)
            If Time.Days <= 65 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationYear", MatchType.Greater, CType(calDari.Value.Year.ToString, Short)), "(", True)
                criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationYear", MatchType.Exact, CType(calDari.Value.Year.ToString, Short)), "(", True)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationMonth", MatchType.Greater, CType(calDari.Value.Month.ToString, Short)), ")", False)
                criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationYear", MatchType.Exact, CType(calDari.Value.Year.ToString, Short)), "(", True)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationMonth", MatchType.Exact, CType(calDari.Value.Month.ToString, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationDate", MatchType.GreaterOrEqual, CType(calDari.Value.Day.ToString, Short)), "))", False)

                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationYear", MatchType.Lesser, CType(calSampai.Value.Year.ToString, Short)), "(", True)
                criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationYear", MatchType.Exact, CType(calSampai.Value.Year.ToString, Short)), "(", True)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationMonth", MatchType.Lesser, CType(calSampai.Value.Month.ToString, Short)), ")", False)
                criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationYear", MatchType.Exact, CType(calSampai.Value.Year.ToString, Short)), "(", True)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationMonth", MatchType.Exact, CType(calSampai.Value.Month.ToString, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationDate", MatchType.LesserOrEqual, CType(calSampai.Value.Day.ToString, Short)), "))", False)

                sessionHelper.SetSession("Criterias", criterias)

                _ListPO = New POHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgPO.PageSize, _
                               total, CType(ViewState("CurrentSortColumn"), String), _
                               CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


                _ListPOHeader = New POHeaderFacade(User).Retrieve(criterias)

                dtgPO.DataSource = _ListPO

                dtgPO.VirtualItemCount = total

                If _ListPO.Count > 0 Then
                    dtgPO.DataBind()
                Else
                    dtgPO.DataBind()
                    MessageBox.Show("Data Tidak Ditemukan")
                End If

            Else
                MessageBox.Show("Periode Melebihi 65 Hari")
            End If
        Else
            MessageBox.Show("Tanggal Permintaan Kirim Salah")
        End If

    End Sub

    Private Sub TotalHargaTebus()
        'Todo Make view PO
        If Not IsNothing(_ListPOHeader) Then
            Dim tot As Double = 0
            Dim totPP As Double = 0
            Dim totQty As Integer = 0
            Dim totIT As Double = 0
            Dim totLC As Double = 0

            'Start Modified by firman 17 Feb 15 (Reduce Roundtrip)

            'Hide
            'For Each item As POHeader In _ListPOHeader
            '    ' Modified by Ikhsan, 20081211
            '    ' Requested by Yurike as Part of CR
            '    ' To distribute the Harga tebus in to Tebus PP and VH
            '    ' Start -----
            '    tot += item.TotalHarga
            '    totPP += item.TotalHargaPP
            '    totQty += item.TotalQuantity
            '    totIT += item.TotalHargaIT
            'Next

            'Add

            Dim strID As String = ""
            For Each item As POHeader In _ListPOHeader
                strID &= item.ID.ToString & ","

            Next

            If strID <> "" Then
                strID = Left(strID, strID.Length - 1)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_RekapPO), "ID", MatchType.InSet, "(" & strID & ")"))
                Dim arl As ArrayList = New V_RekapPOFacade(User).Retrieve(criterias)

                For Each obj As V_RekapPO In arl
                    tot += obj.TotalHarga
                    totPP += obj.TotalHargaPP
                    totQty += obj.TotalQuantity
                    totIT += obj.TotalHargaIT
                    totLC += obj.TotalHargaLC
                Next
            End If

            'End Modified by firman 17 Feb 15 (Reduce Roundtrip)

            lblTotalHargaTebusValue.Text = FormatNumber(tot, 0, , , TriState.UseDefault)
            lblTotalHargaTebusPP.Text = FormatNumber(totPP, 0, , , TriState.UseDefault)
            lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
            lblTotalHargaTebusIT.Text = FormatNumber(totIT, 0, , , TriState.UseDefault)
            lblTotalHargaTebusLC.Text = FormatNumber(totLC, 0, , , TriState.UseDefault)
        End If
        ' End ------
    End Sub

    Private Function GetValueForExcel(ByVal strNumber As String) As String
        If strNumber.IndexOf(",") >= 0 Or strNumber.IndexOf(".") >= 0 Then
            Dim nFloating As Integer
            Dim strSubTotal As String
            Dim Idx As Integer
            Idx = strNumber.IndexOf(".")
            If Idx < 0 Then Idx = strNumber.IndexOf(",")
            strSubTotal = strNumber.Substring(0, Idx)
            nFloating = strSubTotal.Length
            nFloating = strNumber.Length - (nFloating + 1)
            strSubTotal = strNumber.Replace(",", "").Replace(".", "")
            Return "=" & Val(strSubTotal) & "/" & Math.Pow(10, nFloating)
        Else
            Return "=" & strNumber & ""
        End If
    End Function

    Private Function PopulatePassPO(ByVal tipe As String) As ArrayList
        Dim collPo As New ArrayList
        Dim po As POHeader
        Dim poFacade As POHeaderFacade = New POHeaderFacade(User)
        For Each item As DataGridItem In Me.dtgPO.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                po = poFacade.Retrieve(id)
                If po.TermOfPayment.TermOfPaymentValue > 0 Then
                    If po.Status = enumStatusPO.Status.Konfirmasi Then
                        If tipe = "PASS" Then
                            po.PassTOP = enumStatusPO.PassTOP.Pass
                        Else
                            po.PassTOP = enumStatusPO.PassTOP.NoPass
                        End If
                        collPo.Add(po)
                    End If
                End If
            End If
        Next
        Return collPo
    End Function

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Proses Status PO")
        End If
        btnTransfer.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOTransferSAP_Privilege)
        btnTransferUlang.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOTransferSAP_Privilege)

        ' Modified by Ikhsan, 20080109
        ' Requested by Peggy as Part of CR
        ' Adding Privilege
        ' Start --------
        Dim isPriceVHVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaVHTidakTampil_Privilege))
        Dim isPricePPVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaPPTidakTampil_Privilege))
        Dim isPriceITVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaITTidakTampil_Privilege))

        Label12.Visible = isPriceVHVisible
        lblTotalHargaTebusValue.Visible = isPriceVHVisible

        lblTotalHargaTebusPP.Visible = isPricePPVisible
        Label21.Visible = isPricePPVisible

        lblTotalHargaTebusIT.Visible = isPriceITVisible
        Label24.Visible = isPriceITVisible


        'Label5.Visible = isPriceVisible
        'lblTotalHargaTebus.Visible = isPriceVisible
        dtgPO.Columns(15).Visible = isPriceVHVisible
        dtgPO.Columns(16).Visible = isPricePPVisible
        dtgPO.Columns(17).Visible = isPriceITVisible
        ' End -----
        Dim IsTahanDO As Boolean = IsHavingTahanDOPriv()
        Me.lblTahanDO.Visible = IsTahanDO
        Me.lblTahanDOColon.Visible = IsTahanDO
        Me.ddlPernahTahanDO.Visible = IsTahanDO
        'Me.dtgPO.Columns(19).Visible = IsTahanDO ' Keterangan
    End Sub

    Private Function IsHavingTahanDOPriv() As Boolean
        Return SecurityProvider.Authorize(Context.User, SR.Po_tahan_do_privilege)
    End Function

    Private Sub BindDdlPaymentType()
        ddlPaymentType.Items.Clear()
        ddlPaymentType.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each li As ListItem In enumPaymentType.GetList
            ddlPaymentType.Items.Add(li)
        Next
    End Sub

    Private Sub BindDdlRegion()
        ddlRegion.Items.Clear()
        ddlRegion.Items.Add(New ListItem("Silahkan Pilih", 0))
        Dim PODestinationList As ArrayList = New PODestinationFacade(User).RetrieveActiveList()
        For Each objPODes As PODestination In PODestinationList
            'ddlRegion.Items.Add(objPODes.RegionCode)
            If (ddlRegion.Items.Contains(New ListItem(objPODes.RegionDesc))) Then
                'not implement
            Else
                ddlRegion.Items.Add(objPODes.RegionDesc)
            End If
        Next
    End Sub

    Private Sub BindDdlTahanDO()
        Me.ddlPernahTahanDO.Items.Clear()
        Me.ddlPernahTahanDO.Items.Add(New ListItem("Silahkan Pilih", -1))
        Me.ddlPernahTahanDO.Items.Add(New ListItem("Ya", 1))
        Me.ddlPernahTahanDO.Items.Add(New ListItem("Tidak", 0))
    End Sub

    Private Sub BindFactoring()
        Me.ddlFactoring.Items.Clear()
        Me.ddlFactoring.Items.Add(New ListItem("Silahkan Pilih", 2))
        Me.ddlFactoring.Items.Add(New ListItem("Factoring", 1))
        Me.ddlFactoring.Items.Add(New ListItem("Non Factoring", 0))

        Dim IsDSF As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.LEASING, String)) ' (CType(Session.Item("DEALER"), Dealer).DealerCode.Trim.ToUpper = "DSF")
        If IsDSF Then
            Me.ddlFactoring.SelectedValue = 1
            Me.ddlFactoring.Enabled = False
        Else
            Me.ddlFactoring.SelectedValue = 2
            Me.ddlFactoring.Enabled = True
        End If
    End Sub

    Private Sub DownloadStatusGyro()
        Dim i As Integer = 1
        Dim j As Integer, k As Integer
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim IsRightCD As Boolean
        Dim sw As StreamWriter
        Dim filename = String.Format("{0}{1}{2}", "StatusGyro", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename   '-- Destination file
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & filename    '-- Destination file

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If (Connect = False) Then
            imp = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        Dim strTab As String = Chr(9)
        Dim strBuffer As String
        Dim DataItem(22) As String


        strBuffer = "No" & _
        strTab & "Kode Dealer" & _
        strTab & "Nama Dealer" & _
        strTab & "Status PO" & _
        strTab & "Dealer PO Number" & _
        strTab & "No. SO" & _
        strTab & "Cara Pembayaran" & _
        strTab & "Factoring" & _
        strTab & "Status Gyro" & _
        strTab & "Pembayaran Untuk" & _
        strTab & "Tgl Permintaan Kirim" & _
        strTab & "Tanggal Gyro" & _
        strTab & "Nilai PO"
        sw.WriteLine(strBuffer)

        Dim criterias As CriteriaComposite
        Dim _ListPO As ArrayList
        Dim aPPs As New ArrayList
        Dim oDP As DailyPayment
        Dim NilaiPOs As New ArrayList

        criterias = CType(sessionHelper.GetSession("Criterias"), CriteriaComposite)
        _ListPO = New POHeaderFacade(User).Retrieve(criterias) ', "", Sort.SortDirection.ASC)
        i = 1
        For Each oPOH As POHeader In _ListPO
            aPPs = New ArrayList
            NilaiPOs = New ArrayList

            If oPOH.IsFactoring = 1 Then
                aPPs.Add("VH+PP+IT")
                '1 baris : VH+PP+IT
                NilaiPOs.Add(oPOH.TotalHarga + oPOH.TotalHargaPP + oPOH.TotalHargaIT)
            Else
                If oPOH.TermOfPayment.PaymentType = CType(enumPaymentType.PaymentType.RTGS, Short) Then
                    '2 baris : VH & PP (ada 2 kemungkinan yaitu 1 baris(vh+pp) atau 2 baris (vh,pp)
                    If oPOH.DailyPayments.Count > 0 AndAlso CType(oPOH.DailyPayments(0), DailyPayment).PaymentPurpose.PaymentPurposeCode = "VH+PP" Then
                        aPPs.Add("VH+PP")
                        NilaiPOs.Add(oPOH.TotalHarga + oPOH.TotalHargaPP)
                    Else
                        aPPs.Add("VH")
                        NilaiPOs.Add(oPOH.TotalHarga)
                        If oPOH.TotalHargaPP > 0 Then
                            aPPs.Add("PP")
                            NilaiPOs.Add(oPOH.TotalHargaPP)
                        End If
                    End If
                ElseIf oPOH.TermOfPayment.PaymentType = CType(enumPaymentType.PaymentType.COD, Short) Then
                    '2 baris : VH & PP(ada 2 kemungkinan yaitu 1 baris(vh+pp) atau 2 baris (vh,pp)
                    If oPOH.DailyPayments.Count > 0 AndAlso CType(oPOH.DailyPayments(0), DailyPayment).PaymentPurpose.PaymentPurposeCode = "VH+PP" Then
                        aPPs.Add("VH+PP")
                        NilaiPOs.Add(oPOH.TotalHarga + oPOH.TotalHargaPP)
                    Else
                        aPPs.Add("VH")
                        NilaiPOs.Add(oPOH.TotalHarga)
                        If oPOH.TotalHargaPP > 0 Then
                            aPPs.Add("PP")
                            NilaiPOs.Add(oPOH.TotalHargaPP)
                        End If
                    End If
                ElseIf oPOH.TermOfPayment.PaymentType = CType(enumPaymentType.PaymentType.TOP, Short) Then
                    '3 baris : VH , PP & IT
                    If oPOH.DailyPayments.Count > 0 AndAlso CType(oPOH.DailyPayments(0), DailyPayment).PaymentPurpose.PaymentPurposeCode = "VH+PP" Then
                        aPPs.Add("VH+PP")
                        NilaiPOs.Add(oPOH.TotalHarga + oPOH.TotalHargaPP)
                    Else
                        aPPs.Add("VH")
                        NilaiPOs.Add(oPOH.TotalHarga)
                        If oPOH.TotalHargaPP > 0 Then
                            aPPs.Add("PP")
                            NilaiPOs.Add(oPOH.TotalHargaPP)
                        End If
                    End If
                    If oPOH.TotalHargaIT > 0 Then
                        aPPs.Add("IT")
                        NilaiPOs.Add(oPOH.TotalHargaIT)
                    End If
                End If
            End If

            Dim Idx As Integer = 0
            For Each PP As String In aPPs
                Dim NilaiPO As Decimal = NilaiPOs(Idx)
                Dim Status As String
                Dim TanggalGyro As DateTime

                oDP = New DailyPayment
                For Each oGyro As DailyPayment In oPOH.DailyPayments
                    If oGyro.PaymentPurpose.PaymentPurposeCode = PP Then
                        oDP = oGyro
                        Exit For
                    End If
                Next
                Status = ""
                If Not IsNothing(oDP) AndAlso oDP.ID > 0 Then
                    Status = CType(oDP.Status, EnumPaymentStatus.PaymentStatus).ToString()
                    TanggalGyro = oDP.BaselineDateOri()
                Else
                    If PP = "PP" Then
                        TanggalGyro = New DateTime(oPOH.ReqAllocationDateTime.Year, oPOH.ReqAllocationDateTime.Month, 1).AddMonths(1).AddDays(-1)
                    ElseIf PP = "IT" Then
                        TanggalGyro = oPOH.ReqAllocationDateTime
                    Else
                        TanggalGyro = DateAdd(DateInterval.Day, oPOH.TermOfPayment.TermOfPaymentValue, oPOH.ReqAllocationDateTime)
                    End If
                End If

                strBuffer = i.ToString() & _
                strTab & oPOH.Dealer.DealerCode & _
                strTab & oPOH.Dealer.SearchTerm1 & _
                strTab & CType(oPOH.Status, enumStatusPO.Status).ToString() & _
                strTab & oPOH.DealerPONumber & _
                strTab & oPOH.SONumber & _
                strTab & oPOH.TermOfPayment.Description & _
                strTab & IIf(oPOH.IsFactoring = 1, "Ya", "Tidak") & _
                strTab & Status & _
                strTab & PP & _
                strTab & oPOH.ReqAllocationDateTime.ToString("yyyy/MM/dd") & _
                strTab & TanggalGyro.ToString("yyyy/MM/dd") & _
                strTab & GetValueForExcel(NilaiPO)
                sw.WriteLine(strBuffer)

                Idx += 1
                i = i + 1
            Next
        Next
        sw.WriteLine("")
        strBuffer = strBuffer
        If (success = True) Then
            sw.Close()
            Dim PathFile As String = filename ' KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile)
        Else
            MessageBox.Show("Download file Material Promotion Allocation gagal")
        End If

    End Sub

    ''' <summary>
    ''' New COde
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Private Sub DownloadFile(ByVal fileName As String)

        Dim path As System.IO.Path
        Dim fullpath As String = KTB.DNet.Lib.WebConfig.GetValue("DL_Folder")

        fullpath = fullpath & fileName
        Dim name = path.GetFileName(fullpath)
        Dim ext = path.GetExtension(fullpath)
        Dim type As String = ""
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("DL_User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("DL_Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("DL_Server")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            Dim FileCheck As FileInfo = New FileInfo(fullpath)
            If FileCheck.Exists = True Then

                Try
                    imp.StopImpersonate()
                    imp = Nothing
                    Response.Redirect("../Download.aspx?file=" & fullpath)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(fileName.ToString))
                End Try
            Else
                MessageBox.Show("File tidak tersedia")
            End If
        Else
            MessageBox.Show("File tidak dapat diakses")
        End If
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.Server.ScriptTimeout = 300
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlPaymentType()
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            BindToddlCategory()
            BindToddl()
            BindDdlFreePPh()
            BindDdlTahanDO()
            BindDdlRegion()
            InitiatePage()
            BindFactoring()

            If ddlCategory.Items.Count = 0 OrElse ddlOrderType.Items.Count = 0 OrElse lboxStatus.Items.Count = 0 Then
                btnFind.Enabled = False
            End If
            If GetSessionCriteria() Then
                BindGrid()
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnProses.Attributes.Add("OnClick", "return confirm('Yakin mau melakukan proses?');")

        ' add security, fix bug 1575
        btnPassTOP.Visible = False
        CancelTOP.Visible = False
        'pending implement credit control
        'If Not SecurityProvider.Authorize(Context.User, SR.POListStatusTOPPass_Privilege) Then
        '    btnPassTOP.Visible = False
        'Else
        '    btnPassTOP.Visible = True
        'End If

        'If Not SecurityProvider.Authorize(Context.User, SR.POListStatusTOPCancelPass_Privilege) Then
        '    CancelTOP.Visible = False
        'Else
        '    CancelTOP.Visible = True
        'End If

        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
        If IsImplementFactoring Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)

            If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
                IsImplementFactoring = False
            End If
        End If
        Me.lblFactoring.Visible = IsImplementFactoring
        Me.lblFactoringColon.Visible = IsImplementFactoring
        Me.ddlFactoring.Visible = IsImplementFactoring

        Dim ColIdx As Integer = CommonFunction.GetColumnIndexOfDTG(Me.dtgPO, "Factoring")
        If ColIdx >= 0 Then Me.dtgPO.Columns(ColIdx).Visible = IsImplementFactoring

    End Sub

    Sub dtgPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgPO.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PONumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Dim objDealer As Dealer = Session.Item("DEALER")
        CancelTOP.Visible = False
        btnPassTOP.Visible = False
        'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    CancelTOP.Visible = True
        '    btnPassTOP.Visible = True
        'End If
        btnBlock.Visible = False
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If SecurityProvider.Authorize(Context.User, SR.Sales_button_ceiling_block_privilege) Then
                btnBlock.Visible = True
            End If
        End If
    End Sub
    Private Sub dtgPO_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgPO.SelectedIndex = -1
        dtgPO.CurrentPageIndex = 0
        BindTodtgPO(dtgPO.CurrentPageIndex)

    End Sub
    Private Sub BindGrid()
        BindTodtgPO(dtgPO.CurrentPageIndex)
        TotalHargaTebus()
    End Sub
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        ViewState("StartTime") = DateTime.Now
        dtgPO.CurrentPageIndex = 0
        BindGrid()
        ViewState("EndTIme") = DateTime.Now
        lblDuration.Text = "<br/> Start :" & CType(ViewState("StartTime"), DateTime).ToString() & "<br/>" & "End :" & CType(ViewState("EndTIme"), DateTime).ToString() & "<br/>" & "Dif :" & DateDiff(DateInterval.Second, CType(ViewState("StartTime"), DateTime), CType(ViewState("EndTIme"), DateTime)).ToString()

    End Sub

    Private POSavePrivilege As Boolean
    Private POViewPrivilege As Boolean
    Sub dtgPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)

        If (e.Item.ItemIndex <> -1) Then
            If e.Item.ItemIndex = 0 Then
                POSavePrivilege = SecurityProvider.Authorize(Context.User, SR.PengajuanPO_Save_Privilege)
                POViewPrivilege = SecurityProvider.Authorize(Context.User, SR.PengajuanPOView_Detail)
            End If
            Dim objPOHeader As POHeader
            objPOHeader = CType(_ListPO(e.Item.ItemIndex), POHeader)
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgPO.PageSize * dtgPO.CurrentPageIndex)).ToString
            e.Item.Cells(3).Text = CType(objPOHeader.Status, enumStatusPO.Status).ToString
            Dim regdate As New DateTime(objPOHeader.ReqAllocationYear, objPOHeader.ReqAllocationMonth, objPOHeader.ReqAllocationDate, 0, 0, 0)
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            lblKodeDealer.Text = objPOHeader.ContractHeader.Dealer.DealerCode
            lblKodeDealer.ToolTip = objPOHeader.ContractHeader.Dealer.SearchTerm1
            e.Item.Cells(7).Text = Format(objPOHeader.CreatedTime, "dd/MM/yyyy")
            e.Item.Cells(8).Text = Format(objPOHeader.ReqAllocationDateTime, "dd/MM/yyyy")
            e.Item.Cells(9).Text = objPOHeader.ContractHeader.ProjectName
            e.Item.Cells(10).Text = objPOHeader.ContractHeader.Category.CategoryCode
            e.Item.Cells(11).Text = CType(objPOHeader.POType, LookUp.EnumJenisOrder).ToString
            e.Item.Cells(12).Text = objPOHeader.TermOfPayment.Description
            'Start  :DailyPO - add info about PPh Status
            If objPOHeader.FreePPh22Indicator = "0" Then
                e.Item.Cells(13).Text = "Ya"
            Else
                e.Item.Cells(13).Text = "Tidak"
            End If
            'End    :DailyPO - add info about PPh Status

            'Start  :CR:CashPayment;By:Doni;For:Yurike;date:20100223
            'Dim CashPayment As Decimal = 0
            'If objPOHeader.SalesOrders.Count > 0 Then
            '    CashPayment = CType(objPOHeader.SalesOrders(0), SalesOrder).CashPayment
            'End If
            'e.Item.Cells(14).Text = IIf(CashPayment > 0, "Ada", "Tidak")
            e.Item.Cells(14).Text = IIf(objPOHeader.TotalGuarantee() > 0, "Ada", "Tidak")
            'End    :CR:CashPayment;By:Doni;For:Yurike;date:20100223

            e.Item.Cells(15).Text = FormatNumber(objPOHeader.TotalHarga, 0, , , TriState.UseDefault)
            ' Modified by Ikhsan, 20081211
            ' Requested by Yurike as Part of CR
            ' To distribute the Harga tebus in to Tebus PP and VH
            ' Start -----
            e.Item.Cells(16).Text = FormatNumber(objPOHeader.TotalHargaPP, 0, , , TriState.UseDefault)
            If objPOHeader.Status = enumStatusPO.Status.DiBlok Then
                e.Item.Cells(17).Text = FormatNumber(0, 0, , , TriState.UseDefault)
            Else ': objPOHeader.TotalGuarantee()
                e.Item.Cells(17).Text = FormatNumber(objPOHeader.TotalHargaIT, 0, , , TriState.UseDefault)
            End If

            ' End -------
            e.Item.Cells(18).Text = FormatNumber(objPOHeader.TotalHargaLC, 0, , , TriState.UseDefault) '<--tambahan SLA
            e.Item.Cells(19).Text = IIf(objPOHeader.IsFactoring = 1, "Ya", "Tidak")
            Dim lblPaymentScheme As Label = e.Item.FindControl("lblPaymentScheme")

            lblPaymentScheme.Text = CType(objPOHeader.IsTransfer, TransferControl.EnumPaymentScheme).ToString()

            If objPOHeader.Status = enumStatusPO.Status.Rilis Then
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If

            'Start Remaining Module
            'LookUp.ArrayStatusPO() ' 2=Konfirmasi
            If objPOHeader.Status = 2 Then
                'e.Item.BackColor = txtColor.BackColor 'blue
                If objPOHeader.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Or objPOHeader.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then
                    'LookUp.ArrayStatusPO()
                    If objPOHeader.Status = 2 Then  'Konfirmasi
                        If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
                            Dim IsBlue As Boolean = True
                            If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD And objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
                                IsBlue = False
                            ElseIf objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS And objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
                                IsBlue = False
                            ElseIf objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP And objPOHeader.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
                                IsBlue = False
                            End If
                            If IsBlue Then
                                e.Item.BackColor = txtColor.BackColor 'blue
                            End If
                        Else
                            e.Item.BackColor = txtColor.BackColor 'blue
                        End If
                    Else
                        e.Item.BackColor = txtColor.BackColor 'blue
                    End If
                ElseIf objPOHeader.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                    If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.TahanDO Then
                        e.Item.BackColor = txtColorGreen.BackColor

                    Else
                        'e.Item.BackColor = txtColor.BackColor 'blue
                    End If
                End If
            End If
            If objPOHeader.Status = 6 Then
                If objPOHeader.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                    If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.TahanDO Then
                        e.Item.BackColor = txtColorGreen.BackColor

                    Else
                        'e.Item.BackColor = txtColor.BackColor 'blue
                    End If
                End If
            End If

            If objPOHeader.Status = 8 Then
                If objPOHeader.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                    If objPOHeader.RemarkStatus = enumPORemarkStatus.PORemarkStatus.TahanDO Then
                        e.Item.BackColor = txtColorGreen.BackColor

                    Else
                        'e.Item.BackColor = txtColor.BackColor 'blue
                    End If
                End If
            End If

            Dim lblRemark As Label = e.Item.FindControl("lblRemark")
            lblRemark.Text = (New enumPORemarkStatus).GetStringValue(objPOHeader.RemarkStatus)

            If objPOHeader.RemarkStatus < 1 Then 'If objPOHeader.RemarkStatus = 0 Then
                If objPOHeader.DOBlockHistory = 1 Then
                    If lblRemark.Text.Trim = "" Then
                        lblRemark.Text = "Pernah Tahan DO"
                    End If
                End If
            End If
            'End Remaining Module

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnOperator"), LinkButton)
            If (objPOHeader.Status = enumStatusPO.Status.Baru) AndAlso (Not objDealer Is Nothing) AndAlso (objPOHeader.ContractHeader.Dealer.ID = objDealer.ID) Then
                lbtnEdit.Text = "<img src=""../images/edit.gif"" alt=""Ubah"" border=""0"" style=""cursor:hand"">"
                lbtnEdit.CommandName = "Edit"
                lbtnEdit.Visible = POSavePrivilege
            Else
                lbtnEdit.Visible = POViewPrivilege
            End If


            'Remark by Andra AR - 21/11/2008
            'Dim lblGreen As Label = CType(e.Item.FindControl("lblGreen"), Label)
            'lblGreen.Visible = False
            'If (objPOHeader.TermOfPayment.TermOfPaymentValue <> 0) And (objPOHeader.PassTOP = enumStatusPO.PassTOP.Pass) Then
            'lblGreen.Visible = True
            'End If
            'End of Remark by Andra AR - 21/11/2008

            'Modified by Ikhsan, 01 Agustus 2008
            'Requested by Peggy as Part of D-Net CR 
            'Add label to call the Pop Up
            '-------------------------------------------------------------------------------------------
            Dim lblFlow As Label = CType(e.Item.FindControl("lblFlow"), Label)
            lblFlow.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDocumentFlow.aspx?flow=PO_" & objPOHeader.SONumber & "_" & objPOHeader.PONumber, "", 500, 450, "ViewDailyPKFlow")
            '-------------------------------------------------------------------------------------------

            'Add by Andra AR - 27/11/2008
            Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
            lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryFU.aspx?DocType=" & LookUp.DocumentType.PO_Harian & "&DocNumber=" & objPOHeader.PONumber, "", 400, 400, "DealerSelection")
            'End of Add by Andra AR - 27/11/2008

            e.Item.Cells(28).Text = objPOHeader.Status
        End If
    End Sub

    Private Sub RecordStatusChangeHistory(ByVal arrListPO As ArrayList)
        Dim oldStatus As Integer
        Select Case (ddlAction.SelectedValue)
            Case 0 'dihapus
                oldStatus = enumStatusPO.Status.Baru
            Case 1 'dikonfirmasi
                oldStatus = enumStatusPO.Status.Baru
            Case 2 'batal dikonfirmasi
                oldStatus = enumStatusPO.Status.Konfirmasi
            Case 3 'ditolak
                oldStatus = enumStatusPO.Status.Konfirmasi
            Case 4 'batal ditolak
                oldStatus = enumStatusPO.Status.Ditolak
            Case 5 'rilis
                oldStatus = enumStatusPO.Status.Konfirmasi
            Case 6 'batal rilis
                oldStatus = enumStatusPO.Status.Rilis
            Case 7 'disetujui
                oldStatus = enumStatusPO.Status.Rilis
            Case 8 'tidak disetujui
                oldStatus = enumStatusPO.Status.Rilis
            Case 9 'diblok
                oldStatus = enumStatusPO.Status.Rilis
            Case 10 'batal diblok
                oldStatus = enumStatusPO.Status.DiBlok
            Case 11 'SAP Update
                oldStatus = enumStatusPO.Status.Setuju
        End Select
        For Each item As POHeader In arrListPO
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.PO_Harian), item.PONumber, oldStatus, item.Status)
        Next
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        If Me.ddlAction.SelectedIndex <> -1 Then

            'Validasi Apply on BatalKonfirmasi Or BatalTolak
            'If (ddlAction.SelectedValue = "2") Then
            '    If Not IsSisaOCValid() Then Exit Sub
            'ElseIf (ddlAction.SelectedValue = "4") Then
            '    If Not IsSisaOCValid() Then Exit Sub
            'ElseIf (ddlAction.SelectedValue = "6") Then
            '    If Not IsSisaOCValid() Then Exit Sub
            'ElseIf (ddlAction.SelectedValue = "8") Then
            '    If Not IsSisaOCValid() Then Exit Sub
            'ElseIf (ddlAction.SelectedValue = "10") Then
            '    If Not IsSisaOCValid() Then Exit Sub
            'End If
            If Not IsAllowToChange() Then Exit Sub
            Dim al As ArrayList
            al = PopulatePO(Me.ddlAction.SelectedValue)
            If al.Count > 0 Then
                'Dim _status As enumStatusPO = New enumStatusPO
                If ddlAction.SelectedValue = "7" Then 'Disetujui
                    If IsTransferValidPaymentScheme(al, False) Then
                        If CType(al(0), POHeader).IsTransfer = TransferControl.EnumPaymentScheme.Gyro Then
                            TransferPOs(al)
                        End If
                    End If

                    If "Payment" = "Transfer" Then
                        Dim _fileHelper As New FileHelper
                        Dim str As FileInfo
                        Try
                            str = _fileHelper.TransferPOtoSAP(al)
                            MessageBox.Show(SR.SaveSuccess)
                        Catch ex As Exception
                            MessageBox.Show(SR.SaveFail)
                        End Try
                    End If

                ElseIf ddlAction.SelectedValue = "5" Then
                    Dim arrlist As New ArrayList
                    For Each objPO As POHeader In al
                        If objPO.Status = enumStatusPO.Status.Setuju Then
                            arrlist.Add(objPO)
                        End If
                    Next
                    If IsTransferValidPaymentScheme(arrlist, False) Then
                        If CType(arrlist(0), POHeader).IsTransfer = TransferControl.EnumPaymentScheme.Gyro Then
                            TransferPOs(arrlist)
                        End If
                    End If


                    If "payment" = "transfer" Then
                        Dim _fileHelper As New FileHelper
                        Dim str As FileInfo
                        Try
                            str = _fileHelper.TransferPOtoSAP(arrlist)
                            MessageBox.Show(SR.SaveSuccess)
                        Catch ex As Exception
                            MessageBox.Show(SR.SaveFail)
                        End Try
                    End If
                End If
                Dim i As Integer = New POHeaderFacade(User).UpdateTransaction(al)

                For Each POData As POHeader In al
                    Dim PODCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    PODCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "POHeader.ID", MatchType.Exact, POData.ID))
                    Dim arrPODraftHeaderStock As ArrayList = New PODraftHeaderFacade(User).Retrieve(PODCriteria)

                    Dim PODraftHeaderList As ArrayList = New PODraftHeaderFacade(User).Retrieve(PODCriteria)
                    If PODraftHeaderList.Count > 0 Then
                        Dim objPODraftHeader As PODraftHeader = PODraftHeaderList(0)
                        If POData.Status = enumStatusPO.Status.Batal OrElse POData.Status = enumStatusPO.Status.Ditolak _
                            OrElse POData.Status = enumStatusPO.Status.DiBlok OrElse POData.Status = enumStatusPO.Status.Tidak_Setuju Then
                            objPODraftHeader.Status = CType(enumStatusPO.StatusDraftPO.Batal, Integer)
                        ElseIf POData.Status = enumStatusPO.Status.Konfirmasi Then
                            objPODraftHeader.Status = CType(enumStatusPO.StatusDraftPO.SubmitPO, Integer)
                        End If
                        Dim UpdatePODraft As Integer = New PODraftHeaderFacade(User).Update(objPODraftHeader)
                    End If
                Next
                ' Modified by ikhsan, 17 Juli 2008
                ' Untuk memastikan bahwa History akan diisi jika update PO berhasil.
                If i > 0 Then
                    RecordStatusChangeHistory(al)
                    BindTodtgPO(dtgPO.CurrentPageIndex)
                Else
                    MessageBox.Show("Proses Update Status PO Gagal, silahkan diulang.")
                End If
            Else
                MessageBox.Show("Tidak ada PO yang diproses " & Me.ddlAction.SelectedItem.Text)
            End If
        End If
    End Sub

    Private Function IsSisaOCValid() As Boolean 'Remaining Module
        Dim _retVal As Boolean = False
        Dim objPOH As POHeader
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim nStatus As Integer = ddlAction.SelectedValue

        If nStatus = -1 Then Return False
        For Each dgItem As DataGridItem In dtgPO.Items
            If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                objPOH = objPOHFac.Retrieve(CType(dgItem.Cells(0).Text, Integer))
                'Validasi Perubahan
                If (Not objPOH Is Nothing) Then
                    If DoValidateSisaOC(objPOH, objPOH.Status, nStatus) Then
                        _retVal = True
                    Else
                        _retVal = False
                    End If
                End If
            End If
        Next
        Return _retVal
    End Function

    Private Function IsAllowToChange() As Boolean 'Remaining Module
        Dim objPOH As POHeader
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim nStatus As Integer = ddlAction.SelectedValue
        Dim strMessage As String = ""
        Dim strErrorSisaOCRecap As String = ""
        If nStatus = -1 Then Return False
        For Each dgItem As DataGridItem In dtgPO.Items
            If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                objPOH = objPOHFac.Retrieve(CType(dgItem.Cells(0).Text, Integer))

                'Validasi Perubahan

                If nStatus > 4 Then   'Rilis and above
                    If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Or objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then 'Or objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                        strMessage &= IIf(strMessage.Trim = "", "", "; ") & objPOH.PONumber
                    End If
                ElseIf nStatus = 2 Then 'Batal Konfirmasi
                    If objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked Or objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed Then 'Or objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed
                        If objPOH.RemarkStatus <= 0 Then
                            strMessage &= IIf(strMessage.Trim = "", "", "; ") & objPOH.PONumber
                            'Return False
                        End If
                    End If
                End If

            End If
        Next

        If strMessage.Trim <> "" Then
            'MessageBox.Show("Perubahan status untuk PO " & strMessage & "(yang diblock) tidak bisa")
            MessageBox.Show("PO Sedang divalidasi oleh NCD")
            Return False
        Else
            Return True
        End If




    End Function

    Private Function PopulatePO(ByVal type As Integer) As ArrayList
        Dim item As DataGridItem
        Dim collPo As New ArrayList
        Dim po As POHeader
        Dim strMsg As String
        Dim _status As enumStatusPO = New enumStatusPO
        Dim poFacade As POHeaderFacade = New POHeaderFacade(User)
        Dim POItem As DataGridItem

        For Each item In Me.dtgPO.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                po = poFacade.Retrieve(id)
                Select Case (type)
                    Case 0 'dihapus
                        If po.Status = enumStatusPO.Status.Baru Then
                            Dim HapusIsValid As Boolean = True
                            objDealer = sessionHelper.GetSession("DEALER")
                            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                                Dim isMDPCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                isMDPCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "POHeader.ID", MatchType.Exact, po.ID))

                                Dim arrPODraftHeaderStock As ArrayList = New PODraftHeaderFacade(User).Retrieve(isMDPCriteria)
                                If arrPODraftHeaderStock.Count > 0 Then
                                    MessageBox.Show("Proses Batal PO MDP silahkan melakukan pengajuan ke MKS")
                                    HapusIsValid = False
                                End If
                            End If
                            If HapusIsValid Then
                            po.Status = _status.Delete
                            collPo.Add(po)
                        End If
                        End If
                    Case 1 'dikonfirmasi
                        If po.Status = enumStatusPO.Status.Baru Then
                            po.Status = _status.Confirm
                            collPo.Add(po)
                        End If
                    Case 2 'batal dikonfirmasi
                        If po.Status = enumStatusPO.Status.Konfirmasi Then
                            po.Status = _status.UnConfirm
                            For Each items As PODetail In po.PODetails
                                'items.AllocQty = 0
                                'Start  : Remaining Module
                                If po.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or po.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or po.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
                                Else
                                    items.AllocQty = 0
                                End If
                                'End    : Remaining Module
                            Next
                            collPo.Add(po)
                        End If
                    Case 3 'ditolak
                        If po.Status = enumStatusPO.Status.Konfirmasi Then
                            po.Status = _status.Reject
                            collPo.Add(po)
                        End If
                    Case 4 'batal ditolak
                        If po.Status = enumStatusPO.Status.Ditolak Then
                            Dim BatalIsValid As Boolean = True
                            'Start Bugfix jika data di grid yang dicentang lebih dari satu dan memiliki ContractDetail yang sama
                            Dim TempArray As ArrayList = New ArrayList
                            Dim StringID As String = String.Empty
                            Dim ArrPOHeaderID As ArrayList = New ArrayList
                            Dim ArrSelectedPOHeaderID As ArrayList = New ArrayList
                            Dim ArrPoDetail As ArrayList = New ArrayList

                            For Each PODetailData As PODetail In po.PODetails
                                Dim PODetailCriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                PODetailCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.ID", MatchType.Exact, PODetailData.ContractDetailID))
                                ArrPoDetail = New PODetailFacade(User).Retrieve(PODetailCriterias)
                            Next

                            For Each PODetailItem As PODetail In ArrPoDetail
                                Dim SesPOID = sessionHelper.GetSession("S" + po.ContractHeader.ContractNumber)
                                If Not IsNothing(SesPOID) Then
                                    If PODetailItem.POHeaderID <> SesPOID Then
                                ArrPOHeaderID.Add(PODetailItem.POHeaderID)
                                    End If
                                End If
                            Next

                            For Each POItem In Me.dtgPO.Items
                                If CType(POItem.FindControl("chkSelect"), CheckBox).Checked AndAlso ArrPOHeaderID.IndexOf(Convert.ToInt32(POItem.Cells(0).Text)) > -1 AndAlso POItem.Cells(28).Text = enumStatusPO.Status.Ditolak Then
                                    StringID += POItem.Cells(0).Text + ","
                                    ArrSelectedPOHeaderID.Add(POItem.Cells(0).Text)
                                End If
                            Next
                            If StringID.Length > 0 Then
                                StringID = StringID.Substring(0, StringID.Length - 1)
                                TempArray = GetPODetailData(StringID)
                            End If

                            Dim IsLastSameData = False

                            If ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) = ArrSelectedPOHeaderID.Count - 1 Then
                                IsLastSameData = True
                            End If
                            'end
                            For Each itemPODetail As PODetail In po.PODetails
                                If IsLastSameData Then
                                    Dim DataPos As Integer = ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) + 1
                                    For i As Integer = DataPos To ArrSelectedPOHeaderID.Count - 1
                                        ArrSelectedPOHeaderID.RemoveAt(DataPos)
                                    Next
                                    Dim DoneProses As Boolean = ProsesSisaUnitPO(ArrSelectedPOHeaderID, itemPODetail.ContractDetail.SisaUnit, itemPODetail.ContractDetailID, po.SONumber)
                                    If DoneProses Then
                                        BatalIsValid = False
                                        MessageBox.Show("Jumlah Unit di PO Lebih besar dari sisa stok di O/C, PO tidak dapat di Batal Blok")
                                        Exit For
                                    End If
                                Else
                                    Dim DataPos As Integer = ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) + 1
                                    For i As Integer = DataPos To ArrSelectedPOHeaderID.Count - 1
                                        ArrSelectedPOHeaderID.RemoveAt(DataPos)
                                    Next
                                    Dim DoneProses As Boolean = ProsesSisaUnitPO(ArrSelectedPOHeaderID, itemPODetail.ContractDetail.SisaUnit, itemPODetail.ContractDetailID, po.SONumber)
                                    If DoneProses Then
                                        sessionHelper.SetSession("S" + po.ContractHeader.ContractNumber, po.ID)
                                        BatalIsValid = False
                                        MessageBox.Show("Jumlah Unit di PO Lebih besar dari sisa stok di O/C, PO tidak dapat di Batal Tolak")
                                        Exit For
                                    End If
                                End If
                            Next

                            If BatalIsValid Then
                                For Each itemPOD As PODetail In po.PODetails
                                    Dim PODraftCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    PODraftCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "ContractDetail.ID", MatchType.Exact, itemPOD.ContractDetail.ID))
                                    PODraftCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Status", MatchType.No, CType(enumStatusPO.StatusDraftPO.Batal, Integer)))

                                    Dim arrPODraftStock As ArrayList = New PODraftDetailFacade(User).Retrieve(PODraftCriteria)
                                    If arrPODraftStock.Count > 0 Then
                                        For Each objPODraftDetail As PODraftDetail In arrPODraftStock
                                            If objPODraftDetail.ReqQty > itemPOD.ContractDetail.SisaUnit Then
                                                BatalIsValid = False
                                            End If
                                        Next
                                    End If
                                Next
                                
                                If BatalIsValid Then
                                po.Status = _status.UnReject
                                For Each items As PODetail In po.PODetails
                                    items.AllocQty = 0
                                Next
                                collPo.Add(po)
                            End If
                        End If
                        End If
                    Case 5 'rilis
                        If po.Status = enumStatusPO.Status.Konfirmasi Then
                            Dim IsAllNullAllocQty As Boolean = True
                            For Each pODetail As PODetail In po.PODetails
                                If pODetail.AllocQty <> 0 Then
                                    IsAllNullAllocQty = False
                                    Exit For
                                End If
                            Next
                            If IsAllNullAllocQty Then
                                po.Status = _status.Block
                            Else
                                If po.POType = LookUp.EnumJenisOrder.Harian Then
                                    Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(po.ContractHeader.Dealer.ID, EnumDealerTransType.DealerTransKind.POBulanan)
                                    If Not objTransaction Is Nothing AndAlso objTransaction.Status = 0 Then
                                        po.Status = _status.Status.Setuju
                                    Else
                                        po.Status = _status.Release
                                    End If
                                    po.ReleaseDate = Now.Day
                                    po.ReleaseMonth = Now.Month
                                    po.ReleaseYear = Now.Year
                                ElseIf po.POType = LookUp.EnumJenisOrder.Tambahan Then
                                    po.Status = _status.Agree
                                    Dim nationalHolidayFacade As New NationalHolidayFacade(User)
                                    Dim ReleaseTime As Date = nationalHolidayFacade.RetrieveDayBefore(DateTime.Now)
                                    po.ReleaseDate = ReleaseTime.Day
                                    po.ReleaseMonth = ReleaseTime.Month
                                    po.ReleaseYear = ReleaseTime.Year
                                End If
                                
                            End If
                            'If po.TermOfPayment.TermOfPaymentValue > 0 Then
                            '    If po.PassTOP = enumStatusPO.PassTOP.Pass Then
                            '        collPo.Add(po)
                            '    End If
                            'Else

                            ''Start  : RemainModule Clear RemarkStatus & BlockedStatus
                            'If po.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeCOD Or po.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeRTGS Or po.RemarkStatus = enumPORemarkStatus.PORemarkStatus.GantiKeTOP Then
                            '    po.RemarkStatus = 0 'Nothing
                            '    po.BlockedStatus = 0 'Nothing
                            'End If
                            ''End    : RemainModule Clear RemarkStatus & BlockedStatus

                            collPo.Add(po)
                            'End If
                        End If
                    Case 6 'batal rilis
                        If po.Status = enumStatusPO.Status.Rilis Then
                            Dim BatalIsValid As Boolean = True
                            For Each itemPODetail As PODetail In po.PODetails
                                If itemPODetail.ReqQty > itemPODetail.ContractDetail.SisaUnit + itemPODetail.AllocQty Then
                                    BatalIsValid = False
                                    Exit For
                                End If
                            Next
                            If BatalIsValid Then
                                po.Status = _status.UnRelease
                                For Each items As PODetail In po.PODetails
                                    items.AllocQty = 0
                                Next
                                collPo.Add(po)
                            End If
                        End If
                    Case 7 'disetujui
                        If po.Status = enumStatusPO.Status.Rilis Then
                            po.Status = _status.Agree
                            collPo.Add(po)
                        End If
                    Case 8 'tidak disetujui
                        If po.Status = enumStatusPO.Status.Rilis Then
                            po.Status = _status.Disagree
                            collPo.Add(po)
                        End If
                    Case 9 'diblok
                        If po.Status = enumStatusPO.Status.Rilis OrElse po.Status = enumStatusPO.Status.Setuju Then
                            po.Status = _status.Block
                            collPo.Add(po)
                        End If
                    Case 10 'batal diblok
                        If po.Status = enumStatusPO.Status.DiBlok Then
                            'po = poFacade.Retrieve(id)
                            Dim BatalIsValid As Boolean = True
                            'Start Bugfix jika data di grid yang dicentang lebih dari satu dan memiliki contractdetail yang sama
                            Dim TempArray As ArrayList = New ArrayList
                            Dim StringID As String = String.Empty
                            Dim ArrPOHeaderID As ArrayList = New ArrayList
                            Dim ArrSelectedPOHeaderID As ArrayList = New ArrayList
                            Dim ArrPoDetail As ArrayList = New ArrayList

                            For Each PODetailData As PODetail In po.PODetails
                                Dim PODetailCriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                PODetailCriterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.ID", MatchType.Exact, PODetailData.ContractDetailID))
                                ArrPoDetail = New PODetailFacade(User).Retrieve(PODetailCriterias)
                            Next

                            For Each PODetailItem As PODetail In ArrPoDetail
                                Dim SesPOID = sessionHelper.GetSession("S" + po.ContractHeader.ContractNumber)
                                If Not IsNothing(SesPOID) Then
                                    If PODetailItem.POHeaderID <> SesPOID Then
                                ArrPOHeaderID.Add(PODetailItem.POHeaderID)
                                    End If
                                End If
                            Next

                            For Each POItem In Me.dtgPO.Items
                                If CType(POItem.FindControl("chkSelect"), CheckBox).Checked AndAlso ArrPOHeaderID.IndexOf(Convert.ToInt32(POItem.Cells(0).Text)) > -1 AndAlso POItem.Cells(28).Text = enumStatusPO.Status.DiBlok Then
                                    StringID += POItem.Cells(0).Text + ","
                                    ArrSelectedPOHeaderID.Add(POItem.Cells(0).Text)
                                End If
                            Next
                            If StringID.Length > 0 Then
                                StringID = StringID.Substring(0, StringID.Length - 1)
                                TempArray = GetPODetailData(StringID)
                            End If

                            Dim IsLastSameData = False

                            If ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) = ArrSelectedPOHeaderID.Count - 1 Then
                                IsLastSameData = True
                            End If
                            'end
                            If po.SONumber <> String.Empty Then
                                For Each itemPODetail As PODetail In po.PODetails
                                    If IsLastSameData Then
                                        Dim AllocQTY As Integer = 0
                                        For Each PODetailItem As PODetail In TempArray
                                            If PODetailItem.ContractDetailID = itemPODetail.ContractDetailID Then
                                                AllocQTY += PODetailItem.AllocQty
                                            End If
                                        Next
                                        If AllocQTY > itemPODetail.ContractDetail.SisaUnit Then
                                            BatalIsValid = False
                                            MessageBox.Show("Jumlah Unit di PO Lebih besar dari sisa stok di O/C, PO tidak dapat di Batal Blok")
                                            Exit For
                                        End If
                                    Else
                                        Dim DataPos As Integer = ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) + 1
                                        For i As Integer = DataPos To ArrSelectedPOHeaderID.Count - 1
                                            ArrSelectedPOHeaderID.RemoveAt(DataPos)
                                        Next
                                        Dim DoneProses As Boolean = ProsesSisaUnitPO(ArrSelectedPOHeaderID, itemPODetail.ContractDetail.SisaUnit, itemPODetail.ContractDetailID, po.SONumber)
                                        If DoneProses Then
                                            BatalIsValid = False
                                            MessageBox.Show("Jumlah Unit di PO Lebih besar dari sisa stok di O/C, PO tidak dapat di Batal Blok")
                                            Exit For
                                        End If
                                    End If
                                Next
                            Else
                                'Start Bugfix jika data di grid yang dicentang lebih dari satu dan memiliki ContractDetail yang sama
                                For Each itemPODetail As PODetail In po.PODetails
                                    If IsLastSameData Then
                                        Dim DataPos As Integer = ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) + 1
                                        For i As Integer = DataPos To ArrSelectedPOHeaderID.Count - 1
                                            ArrSelectedPOHeaderID.RemoveAt(DataPos)
                                        Next
                                        Dim DoneProses As Boolean = ProsesSisaUnitPO(ArrSelectedPOHeaderID, itemPODetail.ContractDetail.SisaUnit, itemPODetail.ContractDetailID, po.SONumber)
                                        If DoneProses Then
                                            BatalIsValid = False
                                            MessageBox.Show("Jumlah Unit di PO Lebih besar dari sisa stok di O/C, PO tidak dapat di Batal Blok")
                                            Exit For
                                        End If
                                    Else
                                        Dim DataPos As Integer = ArrSelectedPOHeaderID.IndexOf(po.ID.ToString()) + 1
                                        For i As Integer = DataPos To ArrSelectedPOHeaderID.Count - 1
                                            ArrSelectedPOHeaderID.RemoveAt(DataPos)
                                        Next
                                        Dim DoneProses As Boolean = ProsesSisaUnitPO(ArrSelectedPOHeaderID, itemPODetail.ContractDetail.SisaUnit, itemPODetail.ContractDetailID, po.SONumber)
                                        If DoneProses Then
                                            sessionHelper.SetSession("S" + po.ContractHeader.ContractNumber, po.ID)
                                            BatalIsValid = False
                                            MessageBox.Show("Jumlah Unit di PO Lebih besar dari sisa stok di O/C, PO tidak dapat di Batal Blok")
                                            Exit For
                                        End If
                                    End If
                                Next
                                'end
                            End If

                            If BatalIsValid Then
                                For Each itemPOD As PODetail In po.PODetails
                                    Dim PODraftCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    PODraftCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "ContractDetail.ID", MatchType.Exact, itemPOD.ContractDetail.ID))
                                    PODraftCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftDetail), "PODraftHeader.Status", MatchType.No, CType(enumStatusPO.StatusDraftPO.Batal, Integer)))

                                    Dim arrPODraftStock As ArrayList = New PODraftDetailFacade(User).Retrieve(PODraftCriteria)
                                    If arrPODraftStock.Count > 0 Then
                                        For Each objPODraftDetail As PODraftDetail In arrPODraftStock
                                            If objPODraftDetail.ReqQty > itemPOD.ContractDetail.SisaUnit Then
                                                BatalIsValid = False
                                            End If
                                        Next
                                    End If
                                Next

                                If BatalIsValid Then
                                If po.SONumber <> String.Empty Then
                                    po.Status = _status.Status.Selesai
                                Else
                                    po.Status = _status.UnBlock
                                    For Each items As PODetail In po.PODetails
                                        items.AllocQty = 0
                                    Next
                                End If
                                collPo.Add(po)
                            End If
                        End If
                        End If
                    Case 11 'SAP Update
                        If po.Status = enumStatusPO.Status.Setuju Then
                            po = poFacade.Retrieve(id)
                            po.Status = _status.SAPUpdate
                            collPo.Add(po)
                        End If
                    Case 12 'Transfer Ulang
                        If po.Status = enumStatusPO.Status.Setuju Then
                            'po.Status = _status.SAPUpdate
                            collPo.Add(po)
                        End If
                    Case 13 ' Transfer for POHeader.IsTransfer=1
                        If po.IsTransfer = 1 AndAlso po.Status = enumStatusPO.Status.Setuju Then
                            collPo.Add(po)
                        End If
                End Select

            End If
        Next
        Return collPo
    End Function

    Private Function IsTransferValidPaymentScheme(al As ArrayList, Optional ByVal IsShowErrMsg As Boolean = True) As Boolean
        If al.Count < 1 Then Return False

        Dim oPOH As POHeader
        Dim IsTransfer As Short = CType(al(0), POHeader).IsTransfer

        For i As Integer = 1 To al.Count - 1
            oPOH = al(i)
            If oPOH.IsTransfer <> IsTransfer Then
                If IsShowErrMsg Then MessageBox.Show("Transfer Gagal. Skema Pembayaran PO Yang Akan Ditransfer Harus Sama.")
                Return False
            End If
        Next
        Return True
    End Function

    Private Function IsValidPasswordForTransferPayment() As Boolean
        Return False
    End Function

    Private Function TransferPOs(al As ArrayList)
        If al.Count > 0 Then
            If IsTransferValidPaymentScheme(al) = False Then Return False

            Dim IsTransfer As Short = CType(al(0), POHeader).IsTransfer

            If IsTransfer = TransferControl.EnumPaymentScheme.Transfer Then

                If Me.txtPass.Text = String.Empty Then
                    'MessageBox.Show("Silahkan Masukkan Password SAP Anda!")
                    RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
                    Return False
                End If

                Dim UserName As String
                Dim Password As String
                Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") ' User "SAPConnectionString" and prompt user to enter password first
                Dim oSAPDnet As SAPDNet
                Dim SONumber As String = "", Msg As String = ""
                Dim aErrors As New ArrayList
                Dim oUI As UserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
                Dim oPOH As POHeader

                Try
                    UserName = Me.txtUser.Text
                    Password = Me.txtPass.Text
                    oSAPDnet = New SAPDNet(sapConStr, UserName, Password)
                    'oSAPDnet = New SAPDNet(sapConStr)
                    For i As Integer = 0 To al.Count - 1
                        oPOH = CType(al(i), POHeader)

                        SONumber = ""
                        Msg = ""
                        Dim arrPKDtl As ArrayList
                        Dim objPKHeader As PKHeader = New PKHeaderFacade(User).Retrieve(oPOH.ContractHeader.PKNumber)
                        arrPKDtl = objPKHeader.PKDetails
                        Dim isTransControlPO As Boolean = CommonFunction.CheckTransactionControlPO(oPOH.Dealer)
                        Dim ddt As DateTime = DateTime.Now
                        oSAPDnet.SendPOViaRFC(oPOH, SONumber, Msg, isTransControlPO, arrPKDtl)

                        Try
                            Dim bp As New BapiLog()
                            bp.UserName = UserName

                            Dim Data = New With {Key .PONumber = oPOH.PONumber}
                            bp.Body = Data.ToString()
                            bp.SubmitDate = ddt

                            bp.KindLog = 0
                            bp.Status = IIf(String.IsNullorEmpty(SONumber), 0, 1)
                            Dim REspon = New With {Key .Msg = Msg, Key .SONumber = SONumber}
                            bp.ResponseLog = REspon.ToString()
                            Dim bfp = New BapiLogFacade(User)
                            Dim fa = bfp.Insert(bp)
                        Catch ex As Exception
                            Dim k = ex
                        End Try
                       

                        'oSAPDnet.SendPOViaRFC(oPOH, SONumber, Msg)
                        If SONumber.Trim = String.Empty Then
                            aErrors.Add("Error PO : " & oPOH.PONumber & ". " & Msg)
                            'MessageBox.Show(SONumber)
                        End If
                    Next
                    If aErrors.Count > 0 Then
                        Msg = ""
                        For Each erm As String In aErrors
                            Msg = Msg & erm & ";"
                        Next
                        MessageBox.Show("Transfer Gagal. " & Msg)
                        Return False
                    Else
                        MessageBox.Show("Transfer Berhasil.")
                        Return True
                    End If
                Catch ex As Exception
                    MessageBox.Show("Transfer Gagal. " & ex.Message)
                    Return False
                End Try
            Else
                Dim _fileHelper As New FileHelper
                Dim str As FileInfo
                Try
                    str = _fileHelper.TransferPOtoSAP(al)
                    MessageBox.Show(SR.UploadSucces(str.Name))
                    Return True
                Catch ex As Exception
                    MessageBox.Show(SR.UploadFail(str.Name))
                    Return False
                End Try

            End If
        Else
            MessageBox.Show("Tidak ada data PO untuk Proses Transfer Ulang")
            Return False
        End If
    End Function

    Private Sub btnTransferUlang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferUlang.Click
        Dim al As ArrayList
        al = PopulatePO(12)

        TransferPOs(al)

        Exit Sub
        If al.Count > 0 Then
            Dim _fileHelper As New FileHelper
            Dim str As FileInfo
            Try
                str = _fileHelper.TransferPOtoSAP(al)
                MessageBox.Show(SR.UploadSucces(str.Name))
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail(str.Name))
            End Try
        Else
            MessageBox.Show("Tidak ada data PO untuk Proses Transfer Ulang")
        End If
    End Sub

    'Private Sub dtgPO_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
    Sub dtgPO_ItemCommand(ByVal source As System.Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "Detail"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PO/PODetails.aspx?id=" & e.Item.Cells(0).Text)
            Case "Edit"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PO/EditPO.aspx?id=" & e.Item.Cells(0).Text & "&count=0" & "&src=list")
            Case "Download"
                Dim _Label As Label = e.Item.FindControl("lblString")
                Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SOFileDirectory").ToString & "\" & _Label.Text)

                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim destFilePath As String = fileInfo1.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("SODestFileDirectory").ToString & "\" & _Label.Text
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False

                Try
                    success = imp.Start()
                    If success Then
                        If (fileInfo.Exists) Then
                            Dim destinationFileInfo As New FileInfo(destFilePath)
                            If Not destinationFileInfo.Directory.Exists Then
                                destinationFileInfo.Directory.Create()
                            End If
                            fileInfo.CopyTo(destFilePath, True)
                            imp.StopImpersonate()
                            imp = Nothing
                        Else
                            MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                        End If
                    End If
                    Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("SODestFileDirectory").ToString & "\" & _Label.Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_Label.Text))
                End Try
        End Select
    End Sub

#End Region

    Private Sub CancelPassTOP()
        Dim al As ArrayList = PopulatePassPO("CANCEL")
        If al.Count > 0 Then
            Dim objPOHeaderFacade As POHeaderFacade = New POHeaderFacade(User)
            objPOHeaderFacade.UpdatePassTop(al)
        End If
    End Sub


    Private Function PassTOP() As Boolean
        Dim al As ArrayList = PopulatePassPO("PASS")
        If al.Count > 0 Then
            Dim objPOHeaderFacade As POHeaderFacade = New POHeaderFacade(User)
            objPOHeaderFacade.UpdatePassTop(al)
            Return True
        Else
            MessageBox.Show("Tidak ada status yang konfirmasi/TOP belum di Pass.")
            Return False
        End If
    End Function

    Private Sub btnPassTOP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPassTOP.Click
        Try
            If PassTOP() Then
                BindTodtgPO(dtgPO.CurrentPageIndex)
                MessageBox.Show("Proses Berhasil")
            End If

        Catch ex As Exception
            MessageBox.Show("Proses Tidak Berhasil")
        End Try
    End Sub

    Private Sub CancelTOP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelTOP.Click
        Try
            CancelPassTOP()
            BindTodtgPO(dtgPO.CurrentPageIndex)
            MessageBox.Show("Proses Berhasil")
        Catch ex As Exception
            MessageBox.Show("Proses Tidak Berhasil")
        End Try
    End Sub

    Private Sub dtgPO_SortCommand1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPO.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgPO.SelectedIndex = -1
        dtgPO.CurrentPageIndex = 0
        BindTodtgPO(dtgPO.CurrentPageIndex)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click

        Dim i As Integer = 1
        Dim j As Integer, k As Integer
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim IsRightCD As Boolean
        Dim sw As StreamWriter
        Dim filename = String.Format("{0}{1}{2}", "ProsesStatusPO", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename   '-- Destination file
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & filename    '-- Destination file




        Dim criteriasN As CriteriaComposite
        Dim _ListPON As ArrayList


        criteriasN = CType(sessionHelper.GetSession("Criterias"), CriteriaComposite)
        _ListPON = New POHeaderFacade(User).Retrieve(criteriasN) ', "", Sort.SortDirection.ASC)

        '''Changed By Ali. 20150224
        ''' download via bcp
        ''' 
        Dim _POID As String = ""
        filename = String.Format("{0}{1}", "ProsesStatusPO", Date.Now.ToString("ddMMyyyyHHmmss"))
        For Each item As POHeader In _ListPON
            _POID = _POID + "," + item.ID.ToString()
        Next

        If (_ListPON.Count = 0) Then
            _POID = "-1"
        End If


        Dim strQuery As String = String.Empty
        Dim strWhere As String = String.Empty
        Dim strOrder As String = String.Empty


        Dim oBCPQueryFacade As BCPQueryFacade = New BCPQueryFacade(User)
        strQuery = "V_BCP_PODownload"

        Try
            btnDownload.Enabled = False
            Dim objReportList As ArrayList = oBCPQueryFacade.RetrieveFromSP(strQuery, _POID, filename)
            If objReportList.Count > 0 Then
                Dim objReport As BCPQuery = CType(objReportList(0), BCPQuery)
                If objReport.FlName <> String.Empty Then
                    btnDownload.Enabled = True
                    DownloadFile(objReport.FlName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Download Gagal")
        Finally
            btnDownload.Enabled = True
        End Try





        Exit Sub
        '''End Of Changed By Ali. 20150224
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If (Connect = False) Then
            imp = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        Dim strTab As String = Chr(9)
        Dim strBuffer As String
        Dim DataItem(22) As String


        strBuffer = "Kode Dealer" & _
        strTab & "Nama Dealer" & _
        strTab & "Kota" & _
        strTab & "PO Number" & _
        strTab & "Status" & _
        strTab & "Nomor PO Dealer" & _
        strTab & "Tanggal Pengajuan" & _
        strTab & "Tanggal Permintaan Kirim" & _
        strTab & "Cara Pembayaran" & _
        strTab & "Nomor OC" & _
        strTab & "Jenis OC" & _
        strTab & "Kode Kategori" & _
        strTab & "Tahun Perakitan" & _
        strTab & "Nama Pesanan Khusus" & _
        strTab & "Jenis Order" & _
        strTab & "Material" & _
        strTab & "Description" & _
        strTab & "Order" & _
        strTab & "Alokasi" & _
        strTab & "Amount" & _
        strTab & "PPh22" & _
        strTab & "Interest" & _
        strTab & "%Int" & _
        strTab & "%Int Factoring" & _
        strTab & "Sub Total" & _
        strTab & "Tanggal SO" & _
        strTab & "SO Number" & _
        strTab & "Jatuh Tempo" & _
        strTab & "%Disc Reward" & _
        strTab & "Amount Reward" & _
        strTab & "Sub Total II"

        sw.WriteLine(strBuffer)

        Dim criterias As CriteriaComposite
        Dim _ListPO As ArrayList

        criterias = CType(sessionHelper.GetSession("Criterias"), CriteriaComposite)
        _ListPO = New POHeaderFacade(User).Retrieve(criterias) ', "", Sort.SortDirection.ASC)
        i = 0
        For Each Item As POHeader In _ListPO
            'Start->Detail
            For j = 0 To Item.ContractHeader.ContractDetails.Count - 1
                Dim objCD As New ContractDetail
                objCD = CType(Item.ContractHeader.ContractDetails(j), ContractDetail)

                Dim ObjPOD As New PODetail

                IsRightCD = False
                For k = 0 To Item.PODetails.Count - 1
                    Dim objPODTmp As New PODetail
                    objPODTmp = CType(Item.PODetails(k), PODetail)

                    If objCD.ID = objPODTmp.ContractDetailID Then
                        IsRightCD = True
                        ObjPOD = objPODTmp
                        Exit For
                    End If
                Next

                If IsRightCD Then
                    strBuffer = ""
                    strBuffer = strBuffer & CStr(Item.ContractHeader.Dealer.DealerCode)
                    strBuffer = strBuffer & strTab & CStr(Item.ContractHeader.Dealer.DealerName)
                    strBuffer = strBuffer & strTab & CStr(Item.ContractHeader.Dealer.City.CityName)
                    strBuffer = strBuffer & strTab & CStr(Item.PONumber)
                    Select Case CStr(Item.Status)
                        Case "0"
                            strBuffer = strBuffer & strTab & "Baru"
                        Case "1"
                            strBuffer = strBuffer & strTab & "Batal"
                        Case "2"
                            strBuffer = strBuffer & strTab & "Konfirmasi"
                        Case "3"
                            strBuffer = strBuffer & strTab & "Ditolak"
                        Case "4"
                            strBuffer = strBuffer & strTab & "Rilis"
                        Case "5"
                            strBuffer = strBuffer & strTab & "DiBlok"
                        Case "6"
                            strBuffer = strBuffer & strTab & "Setuju"
                        Case "7"
                            strBuffer = strBuffer & strTab & "Tidak Setuju"
                        Case "8"
                            strBuffer = strBuffer & strTab & "Selesai"
                    End Select
                    strBuffer = strBuffer & strTab & CStr(Item.DealerPONumber)
                    strBuffer = strBuffer & strTab & CStr(Format(Item.CreatedTime, "dd/MM/yyyy"))
                    strBuffer = strBuffer & strTab & CStr(Item.ReqAllocationDateTime)
                    strBuffer = strBuffer & strTab & CStr(Item.TermOfPayment.Description)
                    strBuffer = strBuffer & strTab & CStr(Item.ContractHeader.ContractNumber)
                    If CStr(Item.ContractHeader.ContractType) = "0" Then
                        strBuffer = strBuffer & strTab & "Bulanan"
                    ElseIf CStr(Item.ContractHeader.ContractType) = "1" Then
                        strBuffer = strBuffer & strTab & "Tambahan"
                    End If
                    strBuffer = strBuffer & strTab & CStr(Item.ContractHeader.Category.CategoryCode)
                    strBuffer = strBuffer & strTab & CStr(Item.ContractHeader.ProductionYear)
                    strBuffer = strBuffer & strTab & CStr(Item.ContractHeader.ProjectName)
                    If CStr(Item.POType) = "0" Then
                        strBuffer = strBuffer & strTab & "Harian"
                    ElseIf CStr(Item.POType) = "1" Then
                        strBuffer = strBuffer & strTab & "Tambahan"
                    End If

                    strBuffer = strBuffer & strTab & CStr(objCD.VechileColor.VechileType.VechileTypeCode & objCD.VechileColor.ColorCode)     'Material
                    strBuffer = strBuffer & strTab & CStr(objCD.VechileColor.VechileType.Description)  'Description 

                    strBuffer = strBuffer & strTab & CStr(ObjPOD.ReqQty)  'Order
                    strBuffer = strBuffer & strTab & CStr(ObjPOD.AllocQty)  ' alokasi


                    strBuffer = strBuffer & strTab & GetValueForExcel(Math.Round(ObjPOD.Price, 0)) ' amount
                    strBuffer = strBuffer & strTab & GetValueForExcel(Math.Round(ObjPOD.PPh22, 0))  ' PPh22
                    strBuffer = strBuffer & strTab & GetValueForExcel(Math.Round(ObjPOD.Interest, 0))  ' Interest
                    Dim oPrice As Price = ObjPOD.getRefPrice()
                    If IsNothing(oPrice) = False AndAlso oPrice.ID > 0 Then
                        strBuffer = strBuffer & strTab & GetValueForExcel(oPrice.Interest)  ' %Interest
                        strBuffer = strBuffer & strTab & GetValueForExcel(oPrice.FactoringInt)  ' %InterestFactoring
                    Else
                        strBuffer = strBuffer & strTab & GetValueForExcel(0)  ' %Interest
                        strBuffer = strBuffer & strTab & GetValueForExcel(0)  ' %InterestFactoring
                    End If
                    Dim subTotal As Double
                    Dim tmpNumber As Double
                    'subTotal = CType(Item.TotalHarga, Double) + CType(Item.TotalHargaPP, Double) + CType(Item.TotalHargaIT, Double)



                    tmpNumber = Math.Round(objCD.Amount, 0)
                    subTotal = Val(tmpNumber.ToString.Replace(",", "."))
                    tmpNumber = Math.Round(objCD.PPh22, 0)
                    subTotal = subTotal + Val(tmpNumber.ToString.Replace(",", "."))
                    'subTotal = subTotal + Val(objCD.Discount.ToString.Replace(",", "."))
                    tmpNumber = Math.Round(ObjPOD.Interest, 0)
                    subTotal = subTotal + Val(tmpNumber.ToString.Replace(",", "."))

                    subTotal = subTotal * Val(ObjPOD.AllocQty.ToString.Replace(",", "."))
                    subTotal = Val(subTotal)
                    subTotal = Math.Round(subTotal, 0)

                    ''' tamabahan ali buat khsusus yang Factoring

                    If Item.IsFactoring() = 1 Then
                        tmpNumber = Math.Round(ObjPOD.Price(), 0)
                        subTotal = Val(tmpNumber.ToString.Replace(",", "."))
                        tmpNumber = Math.Round(ObjPOD.PPh22, 0)
                        subTotal = subTotal + Val(tmpNumber.ToString.Replace(",", "."))
                        'subTotal = subTotal + Val(objCD.Discount.ToString.Replace(",", "."))
                        tmpNumber = Math.Round(ObjPOD.Interest, 0)
                        subTotal = subTotal + Val(tmpNumber.ToString.Replace(",", "."))

                        subTotal = subTotal * Val(ObjPOD.AllocQty.ToString.Replace(",", "."))
                        subTotal = Val(subTotal)
                        subTotal = Math.Round(subTotal, 0)
                    End If
                    ''' END tamabahan ali

                    'GetValueForExcel
                    strBuffer = strBuffer & strTab & GetValueForExcel(subTotal) & ""
                    'If subTotal.ToString.IndexOf(",") >= 0 Or subTotal.ToString.IndexOf(".") >= 0 Then
                    '    Dim nFloating As Integer
                    '    Dim strSubTotal As String
                    '    Dim Idx As Integer
                    '    Idx = subTotal.ToString.IndexOf(".")
                    '    If Idx < 0 Then Idx = subTotal.ToString.IndexOf(",")
                    '    strSubTotal = subTotal.ToString.Substring(0, Idx)
                    '    nFloating = strSubTotal.Length
                    '    nFloating = subTotal.ToString.Length - (nFloating + 1)
                    '    strSubTotal = subTotal.ToString.Replace(",", "").Replace(".", "")
                    '    strBuffer = strBuffer & strTab & "=" & Val(strSubTotal) & "/" & Math.Pow(10, nFloating)
                    'Else
                    '    strBuffer = strBuffer & strTab & "=" & subTotal & ""
                    'End If

                    'End->Detail

                    Dim objSO As New SalesOrder
                    If Item.SalesOrders.Count > 0 Then
                        objSO = CType(Item.SalesOrders(0), SalesOrder)
                        strBuffer = strBuffer & strTab & CStr(objSO.SODate) 'Tanggal SO
                    Else
                        strBuffer = strBuffer & strTab & CStr("")
                    End If

                    strBuffer = strBuffer & strTab & CStr(Item.SONumber) 'SO Number
                    strBuffer = strBuffer & strTab & Format(Item.ReqAllocationDateTime.AddDays(Item.TermOfPayment.TermOfPaymentValue), "dd/MM/yyyy")    'Jatuh Tempo

                    strBuffer = strBuffer & strTab & GetValueForExcel(ObjPOD.DiscountReward)   ' Discount Reward
                    strBuffer = strBuffer & strTab & GetValueForExcel(Math.Round(ObjPOD.AmountReward + ObjPOD.AmountRewardDepA, 0)) ' amount Reward


                    ''' tamabahan ali buat khsusus yang Factoring
                    Dim _Interest As Double = 0
                    Dim ItemDeposit As Double = 0
                    Dim NTop As Integer = 0
                    Dim NMonth As Integer = 0
                    If Item.TermOfPayment.TermOfPaymentValue > 0 Then
                        ' ItemDeposit = GetItemDeposit(objCD, Item, Item.Dealer.DealerCode)
                        NTop = Item.TermOfPayment.TermOfPaymentValue
                        NMonth = DateTime.DaysInMonth(Item.ContractHeader.ContractPeriodYear, Item.ContractHeader.ContractPeriodMonth)
                        If (Item.IsFactoring = 1) Then
                            _Interest = Item.ContractHeader.FreeIntIndicator * Calculation.CountInterest(NTop, NMonth, oPrice.FactoringInt, objCD.Amount - objCD.GuaranteeAmount, oPrice.PPh23)

                        Else
                            _Interest = Item.ContractHeader.FreeIntIndicator * Calculation.CountInterest(NTop, NMonth, oPrice.Interest, objCD.Amount - objCD.GuaranteeAmount, oPrice.PPh23)

                        End If

                    Else
                        _Interest = 0
                    End If
                    Dim TP As Double = Math.Round(objCD.Amount, 0) + Math.Round(objCD.PPh22) + Math.Round(_Interest)
                    TP = Val(TP.ToString.Replace(",", "."))
                    TP = TP * Val(ObjPOD.AllocQty.ToString.Replace(",", "."))

                    strBuffer = strBuffer & strTab & GetValueForExcel(TP) & ""
                    '''End tambahan

                    'lblJatuhTempo.Text = Format(objPOHeader.ReqAllocationDateTime.AddDays(objPOHeader.TermOfPayment.TermOfPaymentValue), "dd/MM/yyyy")
                    'Add by anh 20110826
                    If Item.RemarkStatus = 1 Then
                        strBuffer = strBuffer & strTab & "Sedang Tahan DO"
                    Else
                        If Item.DOBlockHistory = 1 Then
                            strBuffer = strBuffer & strTab & "Pernah Tahan DO"
                        Else
                            strBuffer = strBuffer & strTab & CStr("")
                        End If
                    End If
                    'end added by anh
                    sw.WriteLine(strBuffer)
                End If
            Next


            i = i + 1
        Next
        sw.WriteLine("")
        strBuffer = strBuffer
        If (success = True) Then
            sw.Close()
            Dim PathFile As String = filename ' KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile)
        Else
            MessageBox.Show("Download file Material Promotion Allocation gagal")
        End If

    End Sub

#Region "Validate O/C Remaining"


    'Check StatusEnum
    Private Function DoValidateSisaOC(ByVal _poHeader As POHeader, ByVal _statusAwal As String, ByVal _statusAkhir As String) As Boolean
        Dim _retval As Boolean = False
        If (_statusAwal = enumStatusPO.Status.Batal) Or (_statusAwal = enumStatusPO.Status.Ditolak) Or (_statusAwal = enumStatusPO.Status.DiBlok) Or (_statusAwal = enumStatusPO.Status.Tidak_Setuju) Then
            If (_statusAkhir = enumStatusPO.Status.Baru) Or (_statusAkhir = enumStatusPO.Status.Konfirmasi) Or (_statusAkhir = enumStatusPO.Status.Rilis) Or (_statusAkhir = enumStatusPO.Status.Setuju) Or (_statusAkhir = enumStatusPO.Status.Selesai) Then
                ErrSisaOc(_poHeader)
            End If
        End If
        Return _retval
    End Function



    'Hitung Sisa Qty Per PoDetail

    'Do Validation for each POHeader
    Private Function ErrSisaOc(ByVal _poHeader As POHeader) As Boolean

        Dim _retVal As StringBuilder = New StringBuilder
        'Get PoDetailByPoHeaderId
        Dim _lstPoDetail As New ArrayList
        Dim _poDetailFacade As New PODetailFacade(User)

        Dim _objPodetail As New PODetail
        Dim crtParam As CriteriaComposite
        Try
            crtParam = New CriteriaComposite(New Criteria(GetType(PODetail), "PoHeader", MatchType.Exact, _poHeader.ID))
            _lstPoDetail = _poDetailFacade.Retrieve(crtParam)

            Dim _qty As Integer = 0
            For Each _objPodetail In _lstPoDetail

                Select Case _poHeader.Status
                    Case enumStatusPO.Status.Baru
                        _qty = _objPodetail.ReqQty
                    Case enumStatusPO.Status.Konfirmasi
                        _qty = _objPodetail.ReqQty
                    Case enumStatusPO.Status.Rilis
                        _qty = _objPodetail.AllocQty
                    Case enumStatusPO.Status.Setuju
                        _qty = _objPodetail.AllocQty
                    Case enumStatusPO.Status.Selesai
                        _qty = _objPodetail.AllocQty
                End Select

                If _qty < _objPodetail.ContractDetail.SisaUnit Then

                Else
                    _retVal.Append("PO No.")
                    _retVal.Append(_poHeader.DealerPONumber)

                    _retVal.Append(" tidak dapat diubah statusnya <br/>karena sisa O/C ")
                    _retVal.Append(" tidak mencukupi untuk kode type ")
                    _retVal.Append(_objPodetail.ContractDetail.VechileColor.MaterialDescription)

                    MessageBox.Show(_retVal.ToString())
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Ubah Status Gagal karena terjadi Kesalahan Pada Sistem. ")
        End Try

        If (_retVal.ToString <> "") Then
            Return False
        Else
            Return True
        End If



    End Function

#End Region



    Private Sub btnBlock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBlock.Click
        Dim oPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim oPOH As POHeader

        For Each di As DataGridItem In Me.dtgPO.Items
            Dim ID As Integer = CType(di.Cells(0).Text, Integer)
            Dim chkSelect As CheckBox = di.FindControl("chkSelect")

            If chkSelect.Checked Then
                oPOH = oPOHFac.Retrieve(ID)
                If Not IsNothing(oPOH) AndAlso oPOH.ID > 0 Then
                    arlPOH.Add(oPOH)
                End If
            End If
        Next

        If arlPOH.Count > 0 Then
            Dim nUpdated As Integer = 0
            Dim strError As String = ""
            For Each objPOH As POHeader In arlPOH
                objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked
                Try
                    oPOHFac.Update(objPOH)
                    nUpdated += 1
                Catch ex As Exception
                End Try
            Next
            If nUpdated = 0 Then
                strError = "Ceiling Block gagal"
            ElseIf nUpdated < arlPOH.Count Then
                strError = (arlPOH.Count - nUpdated) & " dari " & arlPOH.Count & " PO gagal diblok"
            End If
            If strError <> "" Then
                MessageBox.Show(strError)
            End If
        Else
            MessageBox.Show("Tidak ada data yang akan diblok")
        End If
    End Sub

    Private Sub ddlProductCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        Me.BindToddlCategory()
    End Sub

    Private Sub btnDownloadGyro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadGyro.Click
        DownloadStatusGyro()
    End Sub

    Protected Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim oPOH As New POHeader
        Dim oPOHFac As New POHeaderFacade(User)
        Dim aPOHs As New ArrayList

        oPOH = oPOHFac.Retrieve(534893)

        aPOHs.Add(oPOH)
        TransferPOs(aPOHs)
    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim al As ArrayList
        al = PopulatePO(13)
        If (al.Count = 0) Then
            MessageBox.Show("Transfer ke SAP Gagal. Tidak ada Data PO dengan Pembayaran Transfer")
            Exit Sub
        End If
        TransferPOs(al)

    End Sub

    Private Function GetPODetailData(ByVal ID As String) As ArrayList
        'method ini digunakan untuk mengambil data - data podetail dari ponumber yang sedang diproses
        Dim PODetailArray As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ID", MatchType.InSet, "(" & ID & ")"))
        PODetailArray = New PODetailFacade(User).Retrieve(criterias)
        Return PODetailArray
    End Function

    Private Function ProsesSisaUnitPO(ByVal POHeaderID As ArrayList, ByVal SisaUnit As Integer, ByVal ContractDetailID As Integer, ByVal SONumber As String) As Boolean
        'method ini digunakan untuk menghitung sisa unit dari contractdetail, dan jika sisa unit sudah kurang dari 0 maka po tersebut tidak bisa di batal tolak / batal blok
        Dim RemainingID As String = String.Empty
        Dim RemainingArr As ArrayList = New ArrayList
        Dim DoneProses As Boolean = False

        For Each Remain As String In POHeaderID
            RemainingID += Remain + ","
        Next
        If RemainingID.Length > 0 Then
            RemainingID = RemainingID.Substring(0, RemainingID.Length - 1)
            RemainingArr = GetPODetailData(RemainingID)
        End If
        For Each PODetailRemain As PODetail In RemainingArr
            If PODetailRemain.ContractDetailID = ContractDetailID Then
                If SONumber <> String.Empty Then
                    SisaUnit -= PODetailRemain.AllocQty
                Else
                    SisaUnit -= PODetailRemain.ReqQty
                End If

                If SisaUnit < 0 Then
                    DoneProses = True
                End If
            End If
        Next
        Return DoneProses
    End Function

End Class