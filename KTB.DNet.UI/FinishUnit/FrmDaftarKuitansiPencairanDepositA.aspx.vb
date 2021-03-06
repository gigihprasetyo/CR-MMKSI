Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.IO
Imports System.Text

Public Class FrmDaftarKuitansiPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents rbNoKuitansi As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtNoKuitansi As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbPeriodeKuitansi As System.Web.UI.WebControls.RadioButton
    Protected WithEvents icPeriodeFromKuitansi As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeToKuitansi As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents rbNoPengajuan As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbPeriodePengajuan As System.Web.UI.WebControls.RadioButton
    Protected WithEvents icPeriodeFromPengajuan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeToPengajuan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dgDaftarPengajuanKuitansiDepositA As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnTransferSAP As System.Web.UI.WebControls.Button
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents ddlAction As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblUbahStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkTglPencairan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTglPencairan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoReg As System.Web.UI.WebControls.TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private arlDepositAKuitansiPencairan As ArrayList = New ArrayList
    Private arlDepositAKuitansiPencairanFilter As ArrayList = New ArrayList

    Private _DepositAKuitansiPencairanFacade As New FinishUnit.DepositAKuitansiPencairanFacade(User)

    Dim sHelper As New SessionHelper
    Dim objUserInfo As UserInfo
    Dim IsDealer As Boolean = True
    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2

    Dim NoKuitansi As String
    Dim PeriodeFromKuitansi As String
    Dim PeriodeToKuitansi As String
    Dim NoPengajuan As String
    Dim PeriodeFromPengajuan As String
    Dim PeriodeToPengajuan As String
#End Region

#Region "Custom Method"

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String, ByVal icstart As KTB.DNet.WebCC.IntiCalendar, ByVal icEnd As KTB.DNet.WebCC.IntiCalendar)
        Dim dtStart As DateTime = New DateTime(icstart.Value.Year, icstart.Value.Month, _
                                    icstart.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icEnd.Value.Year, icEnd.Value.Month, _
                                    icEnd.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), ColumnName, MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), ColumnName, MatchType.Lesser, dtEnd))
    End Sub

    ''Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
    ''    Dim bResult As Boolean = False
    ''    For Each item As DepositAKuitansiPencairan In arl
    ''        If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
    ''            bResult = True
    ''            Exit For
    ''        End If
    ''    Next
    ''    Return bResult
    ''End Function

    Sub BindDatagridDaftarPencairan(ByVal idxPage As Integer)

        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim = String.Empty Then
                MessageBox.Show("Tentukan kode dealer terlebih dahulu")
                Exit Sub
            End If
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        If txtNoKuitansi.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "ReceiptNumber", MatchType.[Partial], txtNoKuitansi.Text.Trim))
        End If

        If txtNoReg.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "NoReg", MatchType.[Partial], txtNoReg.Text.Trim()))
        End If

        If rbPeriodeKuitansi.Checked Then
            AddPeriodCriteria(criterias, "RequestedTime", icPeriodeFromKuitansi, icPeriodeToKuitansi)
        End If

        If rbPeriodePengajuan.Checked Then
            AddPeriodCriteria(criterias, "RequestedTime", icPeriodeFromPengajuan, icPeriodeToPengajuan)
        End If

        If txtNoPengajuan.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "NoSurat", MatchType.[Partial], txtNoPengajuan.Text.Trim))
        End If

        If ddlJenisStatus.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Status", MatchType.Exact, ddlJenisStatus.SelectedValue))
        End If

        If ddlTipePengajuan.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Type", MatchType.Exact, ddlTipePengajuan.SelectedValue))
        End If

        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        If chkTglPencairan.Checked = True Then
            Dim TglPencairan As New DateTime(CInt(icTglPencairan.Value.Year), CInt(icTglPencairan.Value.Month), CInt(icTglPencairan.Value.Day), 0, 0, 0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "TglPencairan", MatchType.Exact, Format(TglPencairan, "yyyy-MM-dd HH:mm:ss")))
        End If

        If IsDealer Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Status", MatchType.InSet, GetStatusKuitansiDealerEnumValues()))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "Status", MatchType.InSet, GetStatusKuitansiKTBEnumValues()))
        End If

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAKuitansiPencairan), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        Dim totalRow As Integer
        arlDepositAKuitansiPencairan = New FinishUnit.DepositAKuitansiPencairanFacade(User).RetrieveActiveList(criterias, idxPage + 1, dgDaftarPengajuanKuitansiDepositA.PageSize, totalRow, ViewState("currSortColumn"), ViewState("currSortDirection"))
        'For Each item As DepositAKuitansiPencairan In arlDepositAKuitansiPencairan
        '    'If (Not IsExist(item.Dealer.DealerCode, arlDepositAKuitansiPencairanFilter)) Then
        '    arlDepositAKuitansiPencairanFilter.Add(item)
        '    'End If
        'Next
        sHelper.SetSession("VDepositAkuntansi", arlDepositAKuitansiPencairan)

        Dim arlDepositAKuitansiPencairanDL As ArrayList = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(criterias)
        sHelper.SetSession("VDepositAkuntansiDL", arlDepositAKuitansiPencairanDL)

        If (arlDepositAKuitansiPencairan.Count > 0) Then
            dgDaftarPengajuanKuitansiDepositA.Visible = True
            btnDownload.Enabled = True
        Else
            dgDaftarPengajuanKuitansiDepositA.Visible = False
            btnDownload.Enabled = False
            MessageBox.Show("Data tidak ditemukan")
        End If
        dgDaftarPengajuanKuitansiDepositA.DataSource = arlDepositAKuitansiPencairan
        dgDaftarPengajuanKuitansiDepositA.VirtualItemCount = totalRow
        dgDaftarPengajuanKuitansiDepositA.DataBind()

    End Sub

    Private Sub InitNonMandatoryObject()
        rbNoKuitansi.Checked = True
        txtNoKuitansi.Enabled = True
        rbPeriodeKuitansi.Checked = False
        icPeriodeFromKuitansi.Enabled = False
        icPeriodeToKuitansi.Enabled = False
        rbNoPengajuan.Checked = False
        txtNoPengajuan.Enabled = False
        rbPeriodePengajuan.Checked = False
        icPeriodeFromPengajuan.Enabled = False
        icPeriodeToPengajuan.Enabled = False
    End Sub

    Private Sub BindStatus()
        ddlAction.Items.Clear()
        ddlAction.Items.Add(New ListItem("Silahkan Pilih", ""))
        If IsDealer Then
            ddlAction.Items.Add(New ListItem("Baru", "Baru"))
            ddlAction.Items.Add(New ListItem("Validasi", "Validasi"))
            ddlAction.Items.Add(New ListItem("Batal Validasi", "BatalValidasi"))
            ddlAction.Items.Add(New ListItem("Hapus", "Hapus"))
        Else
            ddlAction.Items.Add(New ListItem("Konfirmasi", "Konfirmasi"))
            ddlAction.Items.Add(New ListItem("Batal Konfirmasi", "BatalKonfirmasi"))
        End If
    End Sub

    'Sub BindStatusKuitansiDealer(ByVal ddl As DropDownList)
    '    ddl.DataSource = [Enum].GetNames(GetType(EnumDepositA.StatusKuitansiDealer))
    '    ddl.DataBind()
    'End Sub

    'Sub BindStatusKuitansiKTB(ByVal ddl As DropDownList)
    '    ddl.DataSource = [Enum].GetNames(GetType(EnumDepositA.StatusKuitansiKTB))
    '    ddl.DataBind()
    'End Sub

    Private Function GetStatusKuitansiKTBEnumValues() As String
        Dim strResult As String = " ("
        Dim item As String = String.Empty

        For Each item In [Enum].GetValues(GetType(EnumDepositA.StatusKuitansiKTB))
            strResult = strResult & item.ToString & ","
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Function GetStatusKuitansiDealerEnumValues() As String
        Dim strResult As String = " ("
        Dim item As String = String.Empty

        For Each item In [Enum].GetValues(GetType(EnumDepositA.StatusKuitansiDealer))
            strResult = strResult & item.ToString & ","
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
        Dim objHistoryDepositAKuitansiPencairan As DepositAStatusHistory = New DepositAStatusHistory
        objHistoryDepositAKuitansiPencairan.DocNumber = NoSurat
        objHistoryDepositAKuitansiPencairan.OldStatus = OldStatus
        objHistoryDepositAKuitansiPencairan.NewStatus = NewStatus
        objHistoryDepositAKuitansiPencairan.DocType = DocType
        Dim nResult As Integer = -1
        nResult = New FinishUnit.DepositAStatusHistoryFacade(User).Insert(objHistoryDepositAKuitansiPencairan)
        Return nResult
    End Function

    Private Sub Upload2SAP(ByVal strKuitansiIDs As String)
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim sb2 As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "jvoffsetdepoA", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim filename2 = String.Format("{0}{1}{2}", "jvpencairandepoA", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")

        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename

        Dim DestFile2 As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\" & filename2  '-- Destination file
        Dim LocalDest2 As String = Server.MapPath("") & "\..\DataTemp\" & filename2
        Dim tmp As Integer = 0

        Dim DecInterest As Decimal = 0
        Dim DecTax As Decimal = 0
        Dim DecNetto As Decimal = 0
        Dim DecApprovalAmount As Decimal = 0
        Dim objCulture As Globalization.CultureInfo = New Globalization.CultureInfo("id-ID")
        'MessageBox.Show(x.ToString("#,#.#0", objCulture))

        Dim arlDepositAKuitansiPencairan As ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositAKuitansiPencairan), "ID", MatchType.InSet, strKuitansiIDs))
        arlDepositAKuitansiPencairan = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(criterias)

        Dim arlOffset As New ArrayList
        Dim arlOther As New ArrayList

        For Each item As DepositAKuitansiPencairan In arlDepositAKuitansiPencairan
            If item.NoJV <> String.Empty And item.Status <> EnumDepositA.StatusKuitansiDealer.CancelJV Then
                MessageBox.Show("Sudah ada No JVnya, tidak bisa transfer SAP")
                Exit Sub
            End If
            If item.Status = EnumDepositA.StatusKuitansiDealer.Konfirmasi Then
                If item.Type = 1 Then
                    arlOffset.Add(item)
                Else
                    arlOther.Add(item)
                End If
            Else
                MessageBox.Show("Transfer Gagal. Status data harus konfirmasi")
                Exit Sub
            End If
        Next

        Dim strBankAccount As String = String.Empty
        For Each item As DepositAKuitansiPencairan In arlOffset
            Dim strAssignment As String
            If item.AssignmentNumber = String.Empty Then
                If item.DNNumber.Substring(0, 2) = "00" Then
                    strAssignment = item.DNNumber.Substring(2, item.DNNumber.Length - 2)
                Else
                    strAssignment = item.DNNumber
                End If
            Else
                strAssignment = item.AssignmentNumber
            End If

            Dim objDepositPencairan As New DepositAPencairanH
            objDepositPencairan = New DepositAPencairanHFacade(User).Retrieve(item.NoReg)

            If Not IsNothing(objDepositPencairan.DealerBankAccount) Then
                strBankAccount = objDepositPencairan.DealerBankAccount.BankAccount
            Else
                strBankAccount = String.Empty
            End If
            sb.Append(DateTime.Now.ToString("yyyyMMdd") & ";" & item.NoReg & ";" & item.ReceiptNumber & ";" & IIf(item.Type = 4, item.ProductCategory.AccountCode.Replace("210910", "210919"), item.ProductCategory.AccountCode) & ";" & item.CreatedTime.ToString("yyyyMMdd") & ";" & item.Dealer.DealerCode & ";" & item.TotalAmount.ToString("0") & ";" & icTglPencairan.Value.ToString("yyyyMMdd") & ";" & strAssignment & ";" & "ZC05" & ";" & item.Description & "|#|" & "" & "|#|" & "" & "|#|" & "" & "|#|Rp " & objDepositPencairan.ApprovalAmount.ToString("#,#.#0", objCulture) & ";" & strBankAccount)
            sb.Append(vbNewLine)
        Next

        strBankAccount = String.Empty
        For Each item As DepositAKuitansiPencairan In arlOther
            Dim objDepositPencairan As New DepositAPencairanH
            objDepositPencairan = New DepositAPencairanHFacade(User).Retrieve(item.NoReg)

            If Not IsNothing(objDepositPencairan.DealerBankAccount) Then
                strBankAccount = objDepositPencairan.DealerBankAccount.BankAccount
            Else
                strBankAccount = String.Empty
            End If

            If item.Type = 2 OrElse item.Type = 3 Then
                sb2.Append(DateTime.Now.ToString("yyyyMMdd") & ";" & item.NoReg & ";" & item.ReceiptNumber & ";" & IIf(item.Type = 4, item.ProductCategory.AccountCode.Replace("210910", "210919"), item.ProductCategory.AccountCode) & ";" & item.CreatedTime.ToString("yyyyMMdd") & ";" & item.Dealer.DealerCode & ";" & item.TotalAmount.ToString("0") & ";" & icTglPencairan.Value.ToString("yyyyMMdd") & ";" & item.Dealer.DealerCode & ";" & String.Empty & ";" & item.Description & "|#|" & "" & "|#|" & "" & "|#|" & "" & "|#|Rp " & objDepositPencairan.ApprovalAmount.ToString("#,#.#0", objCulture) & ";" & strBankAccount)
            Else
                Dim objDepositInterest As DepositAInterestH = objDepositPencairan.DepositAInterestH
                sb2.Append(DateTime.Now.ToString("yyyyMMdd") & ";" & item.NoReg & ";" & item.ReceiptNumber & ";" & IIf(item.Description.Substring(0, 31) = "Pencairan bunga saldo Deposit A", item.ProductCategory.AccountCode.Replace("210910", "210919"), item.ProductCategory.AccountCode) & ";" & item.CreatedTime.ToString("yyyyMMdd") & ";" & item.Dealer.DealerCode & ";" & item.TotalAmount.ToString("0") & ";" & icTglPencairan.Value.ToString("yyyyMMdd") & ";" & item.Dealer.DealerCode & ";" & String.Empty & ";" & item.Description & "|#|Rp " & objDepositInterest.InterestAmount.ToString("#,#.#0", objCulture) & "|#|Rp " & objDepositInterest.TaxAmount.ToString("#,#.#0", objCulture) & "|#|Rp " & objDepositInterest.NettoAmount.ToString("#,#.#0", objCulture) & "|#|" & "" & ";" & strBankAccount)
            End If
            sb2.Append(vbNewLine)
        Next

        If (sb.Length > 0) Then
            If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                Dim intResult = -1
                Dim InitialStatus As Integer
                For Each item As DepositAKuitansiPencairan In arlOffset
                    InitialStatus = item.Status
                    'Remarks by ANH 20110808
                    'item.Status = EnumDepositA.StatusKuitansiDealer.Selesai
                    'belum naik
                    item.Status = EnumDepositA.StatusKuitansiDealer.Proses
                    'End Remarks by ANH 20110808

                    item.TglPencairan = icTglPencairan.Value
                    item.IsTransfer = 1
                    intResult = New DepositAKuitansiPencairanFacade(User).Update(item)
                    intResult = InsertHistory(item.NoSurat, InitialStatus, item.Status, 2)

                    If intResult < 0 Then
                        MessageBox.Show("Data gagal di update")
                        Exit Sub
                    End If
                Next
            Else
                MessageBox.Show("Download data gagal")
                Exit Sub
            End If
        End If

        If (sb2.Length > 0) Then
            If Transfer(DestFile2, sb2.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                Dim intResult = -1
                Dim InitialStatus As Integer
                For Each item As DepositAKuitansiPencairan In arlOther
                    InitialStatus = item.Status
                    'Remarks by ANH 20110808
                    'item.Status = EnumDepositA.StatusKuitansiDealer.Selesai
                    item.Status = EnumDepositA.StatusKuitansiDealer.Proses
                    'End Remarks by ANH 20110808

                    item.TglPencairan = icTglPencairan.Value
                    item.IsTransfer = 1
                    intResult = New DepositAKuitansiPencairanFacade(User).Update(item)
                    intResult = InsertHistory(item.NoSurat, InitialStatus, item.Status, 2)
                    If intResult < 0 Then
                        MessageBox.Show("Data gagal di update")
                        Exit Sub
                    End If
                Next
            Else
                MessageBox.Show("Download data gagal")
                Exit Sub
            End If
        End If

        MessageBox.Show("Data berhasil di upload ke SAP")
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
    End Sub

    Private Sub Upload3SAP(ByVal strKuitansiIDs As String)
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "jvpencairandepoA", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim tmp As Integer = 0

        Dim arlDepositAKuitansiPencairan As ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DepositAKuitansiPencairan), "ID", MatchType.InSet, strKuitansiIDs))
        arlDepositAKuitansiPencairan = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(criterias)

        For Each item As DepositAKuitansiPencairan In arlDepositAKuitansiPencairan
            sb.Append(DateTime.Now.ToString("yyyyMMdd") & ";" & item.ReceiptNumber & ";" & item.CreatedTime.ToString("yyyyMMdd") & ";" & item.Dealer.DealerCode & ";" & item.TotalAmount.ToString("0") & ";" & DateTime.Now.ToString("yyyyMM") & "25" & ";" & item.Dealer.DealerCode & ";" & "ZC05" & ";" & item.Description)
            sb.Append(vbNewLine)
        Next

        If (sb.Length > 0) Then
            If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                Dim intResult = -1
                For Each item As DepositAKuitansiPencairan In arlDepositAKuitansiPencairan
                    item.Status = EnumDepositA.StatusKuitansiDealer.Selesai
                    intResult = New DepositAKuitansiPencairanFacade(User).Update(item)
                    If intResult < 0 Then
                        MessageBox.Show("Data gagal di update")
                        Exit Sub
                    End If
                Next
                MessageBox.Show("Data berhasil di upload ke SAP")
                BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
            Else
                MessageBox.Show("Download data gagal")
            End If
        End If
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                sw = New StreamWriter(DestFile)
                sw.Write(Val)
                sw.Close()
                success = True
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        Finally
            imp.StopImpersonate()
        End Try
        Return success
    End Function

    Private Function GetSelectedKuitansis(ByVal dg As DataGrid) As String
        Dim i As Integer = 0
        Dim strResult As String = "("

        For Each item As DataGridItem In dgDaftarPengajuanKuitansiDepositA.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                strResult = strResult & dgDaftarPengajuanKuitansiDepositA.DataKeys().Item(i) & ","
            End If
            i = i + 1
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)


        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            IsDealer = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnTransferSAP.Visible = True
        Else
            IsDealer = True
            'lblSearchDealer.Visible = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            'txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            btnTransferSAP.Visible = False
        End If

        InitiateAuthorization()
        'btnTransferSAP.Attributes("onclick") = "GetSelectedKuitansi();"

        If Not IsPostBack Then
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            btnDownload.Enabled = False
            btnTransferSAP.Attributes.Add("onclick", "return confirm('Anda yakin transfer SAP dengan tgl pencairan yang telah ditentukan?');")
            BindStatus()
            BindTipePengajuan()
            BindJenisStatus()
            InitNonMandatoryObject()
            If Session("IsBindDataGrid") Then
                If Request.QueryString("NoKuitansi") Is Nothing = False Then
                    If Request.QueryString("NoKuitansi").Length > 0 Then
                        rbNoKuitansi.Checked = True
                        txtNoKuitansi.Enabled = True
                        txtNoKuitansi.Text = Request.QueryString("NoKuitansi")
                    End If
                End If
                If Request.QueryString("PeriodeFromKuitansi") Is Nothing = False Then
                    If Request.QueryString("PeriodeFromKuitansi").Length > 0 Then
                        icPeriodeFromKuitansi.Value = Format(CDate(Request.QueryString("PeriodeFromKuitansi")), "dd/MM/yyyy")
                        If icPeriodeFromKuitansi.Value <> Now.Date Then
                            rbPeriodeKuitansi.Checked = True
                            icPeriodeFromKuitansi.Enabled = True
                            txtNoPengajuan.Enabled = False
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                If Request.QueryString("PeriodeToKuitansi") Is Nothing = False Then
                    If Request.QueryString("PeriodeToKuitansi").Length > 0 Then
                        icPeriodeToKuitansi.Value = Format(CDate(Request.QueryString("PeriodeToKuitansi")), "dd/MM/yyyy")
                        If icPeriodeToKuitansi.Value <> Now.Date Then
                            rbPeriodeKuitansi.Checked = True
                            icPeriodeToKuitansi.Enabled = True
                            txtNoPengajuan.Enabled = False
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                If Request.QueryString("NoPengajuan") Is Nothing = False Then
                    If Request.QueryString("NoPengajuan").Length > 0 Then
                        rbNoPengajuan.Checked = True
                        txtNoPengajuan.Enabled = True
                        txtNoPengajuan.Text = Request.QueryString("NoPengajuan")
                    End If
                End If
                If Request.QueryString("PeriodeFromPengajuan") Is Nothing = False Then
                    If Request.QueryString("PeriodeFromPengajuan").Length > 0 Then
                        icPeriodeFromPengajuan.Value = Format(CDate(Request.QueryString("PeriodeFromPengajuan")), "dd/MM/yyyy")
                        If icPeriodeFromPengajuan.Value <> Now.Date Then
                            rbPeriodePengajuan.Checked = True
                            icPeriodeFromPengajuan.Enabled = True
                            txtNoPengajuan.Enabled = False
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                If Request.QueryString("PeriodeToPengajuan") Is Nothing = False Then
                    If Request.QueryString("PeriodeToPengajuan").Length > 0 Then
                        icPeriodeToPengajuan.Value = Format(CDate(Request.QueryString("PeriodeToPengajuan")), "dd/MM/yyyy")
                        If icPeriodeToPengajuan.Value <> Now.Date Then
                            rbPeriodePengajuan.Checked = True
                            icPeriodeToPengajuan.Enabled = True
                            txtNoPengajuan.Enabled = False
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                Session("IsBindDataGrid") = False
                BindDatagridDaftarPencairan(0)
            End If
        Else
        End If
    End Sub

    Sub BindTipePengajuan()
        ddlTipePengajuan.Items.Add(New ListItem("Silahkan Pilih", 0))
        ddlTipePengajuan.Items.Add(New ListItem("Offset", EnumDepositA.TipePengajuan.Offset))
        ddlTipePengajuan.Items.Add(New ListItem("Cash Tahunan", EnumDepositA.TipePengajuan.CashAnnual))
        ddlTipePengajuan.Items.Add(New ListItem("Cash Incidental", EnumDepositA.TipePengajuan.CashIncidental))
        ddlTipePengajuan.Items.Add(New ListItem("Cash Interest", EnumDepositA.TipePengajuan.CashInterest))
    End Sub

    Sub BindJenisStatus()
        ddlJenisStatus.Items.Clear()
        ddlJenisStatus.Items.Add(New ListItem("Silahkan Pilih", -1))


        'New Function CR Deposit A, by ALI
        'If IsDealer Then
        ddlJenisStatus.Items.Add(New ListItem("Baru", EnumDepositA.StatusKuitansiDealer.Baru))
        'End If
        ddlJenisStatus.Items.Add(New ListItem("Validasi", EnumDepositA.StatusKuitansiDealer.Validasi))
        ddlJenisStatus.Items.Add(New ListItem("Konfirmasi", EnumDepositA.StatusKuitansiDealer.Konfirmasi))
        ddlJenisStatus.Items.Add(New ListItem("Selesai", EnumDepositA.StatusKuitansiDealer.Selesai))
        ddlJenisStatus.Items.Add(New ListItem("Proses", EnumDepositA.StatusKuitansiDealer.Proses))
        ddlJenisStatus.Items.Add(New ListItem("Cancel JV", EnumDepositA.StatusKuitansiDealer.CancelJV))
        ddlJenisStatus.Items.Add(New ListItem("Cair", EnumDepositA.StatusKuitansiDealer.Cair))
        'End of CR  Deposit A, by ALI

        Return

        ''remark by anh 20120501, req from angga
        ''If IsDealer Then
        ddlJenisStatus.Items.Add(New ListItem("Baru", EnumDepositA.StatusKuitansiDealer.Baru))
        'End If
        ddlJenisStatus.Items.Add(New ListItem("Validasi", EnumDepositA.StatusKuitansiDealer.Validasi))
        ddlJenisStatus.Items.Add(New ListItem("Konfirmasi", EnumDepositA.StatusKuitansiDealer.Konfirmasi))
        ddlJenisStatus.Items.Add(New ListItem("Selesai", EnumDepositA.StatusKuitansiDealer.Selesai))
        ddlJenisStatus.Items.Add(New ListItem("Proses", EnumDepositA.StatusKuitansiDealer.Proses))
        ddlJenisStatus.Items.Add(New ListItem("Cancel JV", EnumDepositA.StatusKuitansiDealer.CancelJV))
        ddlJenisStatus.Items.Add(New ListItem("Cair", EnumDepositA.StatusKuitansiDealer.Cair))



    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        'If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    If txtKodeDealer.Text.Trim = String.Empty Then
        dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex = 0
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
        '    End If
        'End If

    End Sub

    Private Sub dgDaftarPengajuanKuitansiDepositA_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarPengajuanKuitansiDepositA.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex * dgDaftarPengajuanKuitansiDepositA.PageSize)

            Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = CType(e.Item.DataItem, DepositAKuitansiPencairan)

            If objDepositAKuitansiPencairan.IsTransfer = 1 Then
                e.Item.BackColor = Color.FromArgb(232, 232, 232)
            End If

            Dim intStatus As Integer = CInt(objDepositAKuitansiPencairan.Status)

            '   Dim btnValidasi As Button = CType(e.Item.FindControl("btnValidasi"), Button)
            '  Dim btnBatalValidasi As Button = CType(e.Item.FindControl("btnBatalValidasi"), Button)
            '  Dim btnKonfirmasi As Button = CType(e.Item.FindControl("btnKonfirmasi"), Button)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)



            If IsDealer Then
                'Dim selectedStatusDealer As EnumDepositA.StatusKuitansiDealer = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansiDealer), intStatus), EnumDepositA.StatusKuitansiDealer)
                'Dim SelectedStatusDealerName As String = selectedStatusDealer.GetName(GetType(EnumDepositA.StatusKuitansiDealer), selectedStatusDealer)

                'CR Deposit A , By Ali
                Dim selectedStatusDealer As EnumDepositA.StatusKuitansiDealer = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansiDealer), intStatus), EnumDepositA.StatusKuitansiDealer)
                Dim SelectedStatusDealerName As String = selectedStatusDealer.GetName(GetType(EnumDepositA.StatusKuitansiDealer), selectedStatusDealer)

                lblStatus.Text = SelectedStatusDealerName

                'btnKonfirmasi.Visible = False
                'Select Case intStatus
                '    Case 0
                '        btnValidasi.Enabled = True
                '        btnBatalValidasi.Enabled = False
                '    Case 1
                '        btnValidasi.Enabled = False
                '        btnBatalValidasi.Enabled = True
                '    Case Else 'mungkin tidak diperlukan karena filter berdasarkan enum dealer
                '        btnValidasi.Enabled = False
                '        btnBatalValidasi.Enabled = False
                'End Select
            Else
                'Dim selectedStatusKTB As EnumDepositA.StatusKuitansiKTB = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansiKTB), intStatus), EnumDepositA.StatusKuitansiKTB)
                'Dim SelectedStatusKTBName As String = selectedStatusKTB.GetName(GetType(EnumDepositA.StatusKuitansiKTB), selectedStatusKTB)

                'CR Deposit A , By Ali
                Dim selectedStatusKTB As EnumDepositA.StatusKuitansiKTB = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansiKTB), intStatus), EnumDepositA.StatusKuitansiKTB)
                Dim SelectedStatusKTBName As String = selectedStatusKTB.GetName(GetType(EnumDepositA.StatusKuitansiKTB), selectedStatusKTB)

                lblStatus.Text = SelectedStatusKTBName
                'btnValidasi.Visible = False
                'btnBatalValidasi.Visible = False
                'Select Case intStatus
                '    Case 1
                '        btnKonfirmasi.Enabled = True
                '    Case 10, 11
                '        btnKonfirmasi.Enabled = False
                'End Select
            End If

            ''Dim lbViewDetail As LinkButton = CType(e.Item.FindControl("lbViewDetail"), LinkButton)
            '''lbViewDetail.Attributes("OnClick") = "showPopUp('../PopUp/PopUpPengajuanPencairanDepositAViewEdit.aspx?id=" & objDepositAKuitansiPencairan.ID & " ','',500,760,'');"
            '''Server.Transfer()
            ''lbViewDetail.Attributes("OnClick") = "ShowDetailKuitansi('../FinishUnit/FrmBuatKuitansiPencairanDepositA.aspx?id=" & objDepositAKuitansiPencairan.ID & " ')"

            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)

            If objDepositAKuitansiPencairan.Type = EnumDepositA.TipePengajuan.Offset Then
                lblTipe.Text = "Offset"
            ElseIf objDepositAKuitansiPencairan.Type = EnumDepositA.TipePengajuan.CashAnnual Then
                lblTipe.Text = "Cash Tahunan"
            ElseIf objDepositAKuitansiPencairan.Type = EnumDepositA.TipePengajuan.CashInterest Then
                lblTipe.Text = "Cash Interest"
            ElseIf objDepositAKuitansiPencairan.Type = EnumDepositA.TipePengajuan.CashIncidental Then
                lblTipe.Text = "Cash Incidental"
            End If

            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            If Not IsNothing(objDepositAKuitansiPencairan.NoReg) Then
                lblNoReg.Text = objDepositAKuitansiPencairan.NoReg
            Else
                lblNoReg.Text = String.Empty
            End If

            Dim lbViewFlow As LinkButton = CType(e.Item.FindControl("lbViewFlow"), LinkButton)
            lbViewFlow.Attributes("OnClick") = "showPopUp('../PopUp/PopUpFlowPencairanDepositA.aspx?DealerID=" & objDepositAKuitansiPencairan.Dealer.ID & "&NoReg=" & objDepositAKuitansiPencairan.NoReg.ToString & "&NoSurat=" & objDepositAKuitansiPencairan.NoSurat.ToString & " ','',500,760,'');"

            Dim lbViewStatusHistory As LinkButton = CType(e.Item.FindControl("lbViewStatus"), LinkButton)
            lbViewStatusHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpHistoryKuitansiPencairanDepositA.aspx?DealerID=" & objDepositAKuitansiPencairan.Dealer.ID & "&NoReg=" & objDepositAKuitansiPencairan.NoReg.ToString & "&NoSurat=" & objDepositAKuitansiPencairan.NoSurat.ToString & "','',500,760,'');"

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
        End If
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        If ddlAction.SelectedValue.ToString() <> "" AndAlso ddlAction.SelectedValue.ToString() <> "-1" Then
            UpdateStatus(ddlAction.SelectedValue)
        End If
    End Sub

    Private Sub dgDaftarPengajuanKuitansiDepositA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarPengajuanKuitansiDepositA.PageIndexChanged
        dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex = e.NewPageIndex
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanKuitansiDepositA_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDaftarPengajuanKuitansiDepositA.ItemCommand
        Dim nResult As Integer = -1

        Select Case e.CommandName
            Case "Validasi"
                Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = _DepositAKuitansiPencairanFacade.Retrieve(CInt(e.CommandArgument))
                'insert history
                nResult = InsertHistory(objDepositAKuitansiPencairan.NoSurat, objDepositAKuitansiPencairan.Status, EnumDepositA.StatusKuitansiDealer.Validasi, DocTypeKuitansi)
                If nResult > -1 Then
                    objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Validasi
                    nResult = _DepositAKuitansiPencairanFacade.Update(objDepositAKuitansiPencairan)
                    If nResult <> -1 Then
                        'MessageBox.Show("Berhasil update menjadi " & objDepositAKuitansiPencairan.Status.ToString)
                        MessageBox.Show("Ubah Status berhasil")
                    Else
                        MessageBox.Show(SR.UpdateFail())
                    End If
                End If
                BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
            Case "BatalValidasi"
                Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = _DepositAKuitansiPencairanFacade.Retrieve(CInt(e.CommandArgument))
                nResult = InsertHistory(objDepositAKuitansiPencairan.NoSurat, objDepositAKuitansiPencairan.Status, EnumDepositA.StatusKuitansiDealer.Baru, DocTypeKuitansi)
                If nResult > -1 Then
                    objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Baru
                    nResult = _DepositAKuitansiPencairanFacade.Update(objDepositAKuitansiPencairan)
                    If nResult <> -1 Then
                        'MessageBox.Show("Berhasil update menjadi " & objDepositAKuitansiPencairan.Status.ToString)
                        MessageBox.Show("Ubah Status berhasil")
                    Else
                        MessageBox.Show(SR.UpdateFail())
                    End If
                End If
                BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
            Case "Konfirmasi"
                Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = _DepositAKuitansiPencairanFacade.Retrieve(CInt(e.CommandArgument))
                nResult = InsertHistory(objDepositAKuitansiPencairan.NoSurat, objDepositAKuitansiPencairan.Status, EnumDepositA.StatusKuitansiKTB.Konfirmasi, DocTypeKuitansi)
                If nResult > -1 Then
                    'Save Header
                    objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiKTB.Konfirmasi
                    nResult = _DepositAKuitansiPencairanFacade.Update(objDepositAKuitansiPencairan)
                    If nResult <> -1 Then
                        'MessageBox.Show("Berhasil update menjadi " & objDepositAKuitansiPencairan.Status.ToString)
                        MessageBox.Show("Ubah Status berhasil")
                    Else
                        MessageBox.Show(SR.UpdateFail())
                    End If
                End If
                BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
            Case "ViewDetail"
                NoKuitansi = txtNoKuitansi.Text
                PeriodeFromKuitansi = Format(icPeriodeFromKuitansi.Value, "dd-MM-yyyy").ToString
                'PeriodeFromKuitansi = PeriodeFromKuitansi.Replace("/", "%2F")
                'PeriodeFromKuitansi = PeriodeFromKuitansi.Replace("/", "")
                PeriodeToKuitansi = Format(icPeriodeToKuitansi.Value, "dd-MM-yyyy").ToString
                'PeriodeToKuitansi = PeriodeToKuitansi.Replace("/", "%2F")
                'PeriodeToKuitansi = PeriodeToKuitansi.Replace("/", "")
                NoPengajuan = txtNoPengajuan.Text
                PeriodeFromPengajuan = Format(icPeriodeFromPengajuan.Value, "dd-MM-yyyy").ToString
                'PeriodeFromPengajuan = PeriodeFromPengajuan.Replace("/", "%2F")
                'PeriodeFromPengajuan = PeriodeFromPengajuan.Replace("/", "")
                PeriodeToPengajuan = Format(icPeriodeToPengajuan.Value, "dd-MM-yyyy").ToString
                'PeriodeToPengajuan = PeriodeToPengajuan.Replace("/", "%2F")
                'PeriodeToPengajuan = PeriodeToPengajuan.Replace("/", "")

                Dim strKwitansi As String = "../FinishUnit/FrmBuatKuitansiPencairanDepositA.aspx?id=" & e.CommandArgument & "&NoKuitansi=" & NoKuitansi & "&PeriodeFromKuitansi=" & PeriodeFromKuitansi & "&PeriodeToKuitansi=" & PeriodeToKuitansi & "&NoPengajuan=" & NoPengajuan & "&PeriodeFromPengajuan=" & PeriodeFromPengajuan & "&PeriodeToPengajuan=" & PeriodeToPengajuan
                sHelper.SetSession("BackKwitansi", strKwitansi)
                Server.Transfer("../FinishUnit/FrmBuatKuitansiPencairanDepositA.aspx?id=" & e.CommandArgument & "&NoKuitansi=" & NoKuitansi & "&PeriodeFromKuitansi=" & PeriodeFromKuitansi & "&PeriodeToKuitansi=" & PeriodeToKuitansi & "&NoPengajuan=" & NoPengajuan & "&PeriodeFromPengajuan=" & PeriodeFromPengajuan & "&PeriodeToPengajuan=" & PeriodeToPengajuan)
        End Select
    End Sub

    Private Sub rbNoKuitansi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNoKuitansi.CheckedChanged
        If rbNoKuitansi.Checked Then
            txtNoKuitansi.Enabled = True
            icPeriodeFromKuitansi.Enabled = False
            icPeriodeToKuitansi.Enabled = False
            txtNoPengajuan.Enabled = False
            txtNoPengajuan.Text = ""
            icPeriodeFromPengajuan.Enabled = False
            icPeriodeToPengajuan.Enabled = False
        End If
    End Sub

    Private Sub rbNoPengajuan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNoPengajuan.CheckedChanged
        If rbNoPengajuan.Checked Then
            txtNoKuitansi.Enabled = False
            'txtNoKuitansi.Text = False
            icPeriodeFromKuitansi.Enabled = False
            icPeriodeToKuitansi.Enabled = False
            txtNoPengajuan.Enabled = True
            icPeriodeFromPengajuan.Enabled = False
            icPeriodeToPengajuan.Enabled = False
        End If
    End Sub

    Private Sub rbPeriodeKuitansi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPeriodeKuitansi.CheckedChanged
        If rbPeriodeKuitansi.Checked Then
            txtNoKuitansi.Enabled = False
            txtNoKuitansi.Text = ""
            icPeriodeFromKuitansi.Enabled = True
            icPeriodeToKuitansi.Enabled = True
            txtNoPengajuan.Enabled = False
            txtNoPengajuan.Text = ""
            icPeriodeFromPengajuan.Enabled = False
            icPeriodeToPengajuan.Enabled = False
        End If
    End Sub

    Private Sub rbPeriodePengajuan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPeriodePengajuan.CheckedChanged
        If rbPeriodePengajuan.Checked Then
            txtNoKuitansi.Enabled = False
            txtNoKuitansi.Text = ""
            icPeriodeFromKuitansi.Enabled = False
            icPeriodeToKuitansi.Enabled = False
            txtNoPengajuan.Enabled = False
            txtNoPengajuan.Text = ""
            icPeriodeFromPengajuan.Enabled = True
            icPeriodeToPengajuan.Enabled = True
        End If
    End Sub

    Private Sub btnTransferSAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferSAP.Click
        'Dim arlKuitansiID As ArrayList = GetSelectedRows(dgDaftarPengajuanKuitansiDepositA)
        'If ddlTipePengajuan.SelectedIndex > 0 Then
        Dim strKuitansiIDs As String = GetSelectedKuitansis(dgDaftarPengajuanKuitansiDepositA)
        If strKuitansiIDs = ")" Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Exit Sub
        End If

        If chkTglPencairan.Checked = False Then
            MessageBox.Show("Tgl Pencairan belum dipilih")
            Exit Sub
        End If

        If strKuitansiIDs.Length > 0 Then
            'If ddlTipePengajuan.SelectedValue = EnumDepositA.TipePengajuan.Offset Then
            '    Upload2SAP(strKuitansiIDs)
            'Else
            '    Upload3SAP(strKuitansiIDs)
            'End If
            Upload2SAP(strKuitansiIDs)
        Else
            MessageBox.Show("Pilih kuitansi terlebih dahulu!")
        End If

        'Else
        'MessageBox.Show("Pilih Tipe Pengajuan terlebih dahulu!")
        '    End If

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlPF As New ArrayList
        Dim strKuitansiIDs As String = GetSelectedKuitansis(dgDaftarPengajuanKuitansiDepositA)
        If strKuitansiIDs = ")" Then
            'MessageBox.Show("Tidak ada data yang dipilih")
            'Exit Sub
            arlPF = CType(sHelper.GetSession("VDepositAkuntansiDL"), ArrayList)
        End If

        'If chkTglPencairan.Checked = False Then
        '    MessageBox.Show("Tgl Pencairan belum dipilih")
        '    Exit Sub
        'End If

        If strKuitansiIDs.Length > 1 Then
            Dim arlDepositAKuitansiPencairan As ArrayList

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositAKuitansiPencairan), "ID", MatchType.InSet, strKuitansiIDs))
            arlDepositAKuitansiPencairan = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(criterias)

            DoDownload(arlDepositAKuitansiPencairan)
        Else
            DoDownload(arlPF)
            'MessageBox.Show("Pilih kuitansi terlebih dahulu!")
        End If
    End Sub

    Private Sub DoDownload(ByVal arlDPK As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Kuitansi Pencairan [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim oFile As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(oFile)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(oFile, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDataToExcell(sw, arlDPK)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByVal arlDPK As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DAFTAR STATUS PENCAIRAN")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        If (arlDPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("N0" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("NO. REG" & tab)
            itemLine.Append("NO. KUITANSI" & tab)
            itemLine.Append("NO. REF PENGAJUAN" & tab)
            itemLine.Append("TGL. PENCAIRAN (DD/MM/YYYY)" & tab)
            itemLine.Append("NO. JV" & tab)
            itemLine.Append("NO. DEBIT NOTE" & tab)
            itemLine.Append("NO. SO" & tab)
            itemLine.Append("SEJUMLAH" & tab)
            itemLine.Append("UNTUK PEMBAYARAN" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As DepositAKuitansiPencairan In arlDPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    If IsDealer Then
                        Dim selectedStatusDealer As EnumDepositA.StatusKuitansiDealer = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansiDealer), CInt(item.Status)), EnumDepositA.StatusKuitansiDealer)
                        Dim SelectedStatusDealerName As String = selectedStatusDealer.GetName(GetType(EnumDepositA.StatusKuitansiDealer), selectedStatusDealer)
                        itemLine.Append(SelectedStatusDealerName & tab)
                    Else
                        Dim selectedStatusKTB As EnumDepositA.StatusKuitansiKTB = CType([Enum].Parse(GetType(EnumDepositA.StatusKuitansiKTB), CInt(item.Status)), EnumDepositA.StatusKuitansiKTB)
                        Dim SelectedStatusKTBName As String = selectedStatusKTB.GetName(GetType(EnumDepositA.StatusKuitansiKTB), selectedStatusKTB)
                        itemLine.Append(SelectedStatusKTBName & tab)
                    End If
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append(item.ProductCategory.Code & tab)
                    If Not IsNothing(item.NoReg) Then
                        itemLine.Append(item.NoReg.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.ReceiptNumber) Then
                        itemLine.Append(item.ReceiptNumber.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.NoSurat) Then
                        itemLine.Append(item.NoSurat.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.TglPencairan) Then
                        itemLine.Append(item.TglPencairan.ToString("dd/MM/yyyy") & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.NoJV) Then
                        itemLine.Append(item.NoJV.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.DNNumber) Then
                        itemLine.Append(item.DNNumber.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.AssignmentNumber) Then
                        itemLine.Append(item.AssignmentNumber.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.TotalAmount) Then
                        itemLine.Append(Decimal.Round(item.TotalAmount, 0) & tab)
                    Else
                        itemLine.Append("0" & tab)
                    End If
                    If Not IsNothing(item.Description) Then
                        itemLine.Append(item.Description.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub UpdateStatus(ByVal JenisStatus As String)
        Dim nResult As Integer = -1
        Dim intStatus As Integer
        Dim iSuccess As Integer = 0

        Select Case JenisStatus
            Case "Validasi"
                intStatus = EnumDepositA.StatusKuitansiDealer.Validasi
            Case "BatalValidasi"
                intStatus = EnumDepositA.StatusKuitansiDealer.Baru
            Case "Konfirmasi"
                intStatus = EnumDepositA.StatusKuitansiKTB.Konfirmasi
            Case "BatalKonfirmasi"
                intStatus = EnumDepositA.StatusKuitansiKTB.Validasi
            Case "Hapus"
                intStatus = EnumDepositA.StatusKuitansiDealer.Hapus
        End Select

        For Each item As DataGridItem In dgDaftarPengajuanKuitansiDepositA.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chkCek.Checked Then
                Dim ArrPengajuan = New ArrayList
                If Not sHelper.GetSession("VDepositAkuntansi") Is Nothing Then
                    ArrPengajuan = sHelper.GetSession("VDepositAkuntansi")
                    Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = CType(ArrPengajuan(item.ItemIndex), DepositAKuitansiPencairan)
                    Dim Valid As Boolean = True
                    If objDepositAKuitansiPencairan.Status <> EnumDepositA.StatusKuitansiDealer.Selesai AndAlso objDepositAKuitansiPencairan.Status <> EnumDepositA.StatusKuitansiDealer.Cair Then
                        Select Case intStatus
                            Case EnumDepositA.StatusKuitansiDealer.Hapus
                                If objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Baru Then
                                    objDepositAKuitansiPencairan.Status = intStatus
                                    objDepositAKuitansiPencairan.RowStatus = -1
                                End If
                            Case EnumDepositA.StatusKuitansiDealer.Validasi
                                If objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Baru Or objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Konfirmasi Then
                                    objDepositAKuitansiPencairan.Status = intStatus
                                End If
                            Case EnumDepositA.StatusKuitansiDealer.Baru
                                If objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Validasi Then
                                    objDepositAKuitansiPencairan.Status = intStatus
                                End If
                            Case EnumDepositA.StatusKuitansiDealer.Konfirmasi
                                If objDepositAKuitansiPencairan.Status = EnumDepositA.StatusKuitansiDealer.Validasi Then
                                    objDepositAKuitansiPencairan.Status = intStatus
                                End If
                            Case Else

                                Valid = False
                        End Select
                        If Valid Then
                            nResult = _DepositAKuitansiPencairanFacade.Update(objDepositAKuitansiPencairan)
                        End If

                        If nResult <> -1 AndAlso Valid Then
                            'khusus untuk yg hapus, ubah status di DepositAPencairanHeader menjadi setuju(11) untuk diajukan kembali
                            If objDepositAKuitansiPencairan.RowStatus = -1 Then
                                Dim objDepositAPencairanHFacade As DepositAPencairanHFacade = New DepositAPencairanHFacade(User)
                                Dim objDepositAPencairanH As DepositAPencairanH = objDepositAPencairanHFacade.Retrieve(objDepositAKuitansiPencairan.NoReg)
                                Dim iUpdate As Integer
                                objDepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Setuju ' 11 ' Status setuju
                                iUpdate = objDepositAPencairanHFacade.Update(objDepositAPencairanH)
                            End If


                            'insert history              
                            nResult = InsertHistory(objDepositAKuitansiPencairan.NoSurat, objDepositAKuitansiPencairan.Status, intStatus, DocTypeKuitansi)
                            If nResult > -1 Then
                                'MessageBox.Show("Berhasil update menjadi " & objDepositAKuitansiPencairan.Status.ToString)
                                'MessageBox.Show("Ubah Status berhasil")
                            End If
                        Else
                            MessageBox.Show(SR.UpdateFail())
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Next
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositA.CurrentPageIndex)
        'For Each item As DataGridItem In dgDaftarPengajuanKuitansiDepositA.Items
        '    Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
        '    If chkCek.Checked Then
        '        Dim ArrPengajuan = New ArrayList
        '        If Not sHelper.GetSession("VDepositAkuntansi") Is Nothing Then
        '            ArrPengajuan = sHelper.GetSession("VDepositAkuntansi")
        '            Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = CType(ArrPengajuan(item.ItemIndex), DepositAKuitansiPencairan)
        '            If objDepositAKuitansiPencairan.Status <> EnumDepositA.StatusKuitansiDealer.Selesai Then
        '                'insert history              
        '                nResult = InsertHistory(objDepositAKuitansiPencairan.NoSurat, objDepositAKuitansiPencairan.Status, intStatus, DocTypeKuitansi)
        '                If nResult > -1 Then
        '                    If intStatus = EnumDepositA.StatusKuitansiDealer.Hapus Then
        '                        objDepositAKuitansiPencairan.RowStatus = -1
        '                        objDepositAKuitansiPencairan.Status = intStatus
        '                    Else
        '                        objDepositAKuitansiPencairan.Status = intStatus
        '                    End If
        '                    nResult = _DepositAKuitansiPencairanFacade.Update(objDepositAKuitansiPencairan)
        '                    If nResult <> -1 Then
        '                        'MessageBox.Show("Berhasil update menjadi " & objDepositAKuitansiPencairan.Status.ToString)
        '                        'MessageBox.Show("Ubah Status berhasil")
        '                    Else
        '                        MessageBox.Show(SR.UpdateFail())
        '                        Exit Sub
        '                    End If
        '                End If
        '            End If

        '        End If
        '    End If
        'Next

        'If nResult > -1 Then
        '    MessageBox.Show("Ubah Status berhasil")
        '    BindDatagridDaftarPencairan()
        'Else
        '    MessageBox.Show("Data belum dipilih")
        'End If


    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.DepositA_daftar_kuitansi_pencairan_depositA_lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Lihat Daftar Kuitansi Pencairan Deposit A")
            Me.btnProses.Visible = False
            Me.btnTransferSAP.Visible = False
        End If

        If IsDealer Then
            If Not SecurityProvider.Authorize(context.User, SR.DepositA_daftar_kuitansi_pencairan_depositA_lihat_dealer_Privilege) Then
                Me.btnProses.Visible = False
            End If
        End If


        If Not SecurityProvider.Authorize(context.User, SR.DepositA_daftar_kuitansi_pencairan_depositA_lihat_detail_Privilege) Then
            Me.dgDaftarPengajuanKuitansiDepositA.Columns(13).Visible = False
        End If


    End Sub
#End Region

#Region "Internal Enum"
    'Private Enum TipePengajuan
    '    Offset = 1
    '    CashAnnual = 2
    '    CashIncidental = 3
    '    CashInterest = 4
    'End Enum

    'Private Enum StatusKuitansiDealer
    '    Baru = 0
    '    Validasi = 1
    '    Konfirmasi = 10
    '    Printed = 11
    '    Selesai = 12
    '    Hapus = 13
    '    'belum naik
    '    Proses = 14
    '    CancelJV = 15
    '    Cair = 16
    'End Enum

    'Private Enum StatusKuitansiKTB
    '    Validasi = 1
    '    Konfirmasi = 10
    '    Printed = 11
    '    Selesai = 12
    '    'belum naik
    '    Proses = 14
    '    CancelJV = 15
    '    Cair = 16
    'End Enum
#End Region



End Class

