#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class FrmTrClassAllocationCancel
    Inherits System.Web.UI.Page
    Dim sHClass As SessionHelper = New SessionHelper
    Dim objv_trClass As v_trClass
    Dim objClass As TrClass
    Dim objClassAlloc As TrClassAllocation


    Protected WithEvents txtCapacity As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAllocated As System.Web.UI.WebControls.Label
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtAllocated As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblHeaderCaption As System.Web.UI.WebControls.Label
    Protected WithEvents lblReason As System.Web.UI.WebControls.Label
    Protected WithEvents txtReason As System.Web.UI.WebControls.TextBox

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents txtLokasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICTanggalMulai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICTanggalSelesai As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'clearData()
        getDataClass()
        If Not IsPostBack Then
            LoadData()
            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
            If Not objDealer Is Nothing Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    lblHeaderCaption.Text = "Training - Edit Allocation" '
                    lblReason.Text = "Alasan Perubahan Alokasi"
                Else
                    lblHeaderCaption.Text = "Training - Cancel Allocation"
                    lblReason.Text = "Alasan Pembatalan Training"
                End If
            End If
            'If Not Request.QueryString("Opener") Is Nothing Then
            '    If Request.QueryString("Opener").Trim = "FrmTrClassAllocation.aspx" Then
            '        lblHeaderCaption.Text = "Training - Edit Allocation"
            '    Else
            '        lblHeaderCaption.Text = "Training - Cancel Allocation"
            '    End If
            'End If
        End If
    End Sub
    Public Sub clearData()
        lblAllocated.Text = 0
        txtReason.Text = ""
        txtKodeKelas.Text = ""
        txtLokasi.Text = ""
        txtNamaKelas.Text = ""

        ICTanggalMulai.Value = Format(Now, "dd/MM/yyyy")
        ICTanggalSelesai.Value = Format(Now, "dd/MM/yyyy")
    End Sub

    Public Sub getDataClass()

        'if request.QueryString
        Dim strID As String = ""
        If Not Request.QueryString("ID") Is Nothing And Request.QueryString("ID").Trim <> "" Then
            strID = Request.QueryString("ID")

            'Dim crits As CriteriaComposite
            'crits = New CriteriaComposite(New Criteria(GetType(v_trClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crits.opAnd(New CriteriaComposite(New Criteria(GetType(v_trClass), "ID", MatchType.Exact, strID)))

            objv_trClass = New v_trClassFacade(User).Retrieve(CInt(strID))
            objClass = New TrClassFacade(User).Retrieve(objv_trClass.ClassID)
            objClassAlloc = New TrClassAllocationFacade(User).Retrieve(objv_trClass.ID)
            '
            'If Not Request.QueryString("Opener") Is Nothing Then
            '    If Request.QueryString("Opener").Trim = "FrmTrClassAllocation.aspx" Then
            '        lblHeaderCaption.Text = "Training - Edit Allocation"
            '    Else
            '        lblHeaderCaption.Text = "Training - Cancel Allocation"
            '    End If
            'End If
        Else
            sHClass.SetSession("backRes", 1)
            Dim StrUrl As String = "FrmTrClass.aspx"
            If Not Request.QueryString("Opener") Is Nothing Then
                StrUrl = Request.QueryString("Opener").Trim
            End If
            Response.Redirect(StrUrl)
        End If

    End Sub


    Private Sub LoadData()
        If Not IsNothing(objClassAlloc) Then
            lblAllocated.Text = objClassAlloc.Allocated
            txtAllocated.Text = objClassAlloc.Allocated

            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtAllocated.Enabled = True
            Else
                txtAllocated.Enabled = False
            End If

            txtReason.Text = objClassAlloc.CancelReason

        End If

        If Not IsNothing(objClass) Then
            txtKodeKelas.Text = objClass.ClassCode.Trim()
            txtNamaKelas.Text = objClass.ClassName.Trim()
            txtLokasi.Text = objClass.Location.Trim()
            txtCapacity.Text = objClass.Capacity
            ICTanggalMulai.Value = objClass.StartDate.ToString("dd/MM/yyyy").Trim()
            ICTanggalSelesai.Value = objClass.FinishDate.ToString("dd/MM/yyyy").Trim()
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim objTrClassAllocFacade As New TrClassAllocationFacade(User)
        Dim n As Integer
        Dim nResult = -1
        If txtReason.Text.Trim = "" Then
            MessageBox.Show("Silahkan mengisi Alasan Pembatalan terlebih dahulu")
            Exit Sub
        End If

        'tidak boleh kurang dari trainee yg terregistered untuk kelas tersebut
        If CInt(txtAllocated.Text) < objv_trClass.NumOfTrainee Then
            MessageBox.Show("Alokasi sudah terisi sejumlah " & objv_trClass.NumOfTrainee.ToString & "\n Lakukan pembatalan trainee terlebih dahulu")
            Exit Sub
        End If
        'jumlah alokasi tidak boleh lebih dari total alokasi

        If CInt(txtAllocated.Text) > objv_trClass.Capacity Then
            MessageBox.Show("Jumlah alokasi tidak boleh lebih dari total alokasi kelas (" & objv_trClass.Capacity.ToString & ")")
            Exit Sub
        End If

        If Not IsNothing(objClassAlloc) Then
            n = objClassAlloc.Allocated
            'error GetLatestVersion GetLatestVersion(objClassAlloc.CancelReason = txtReason.Text.Trim)
            'error GetLatestVersion GetLatestVersion(objClassAlloc.LastAllocated = n) '  objClassAlloc.Allocated
            objClassAlloc.LastAllocated = objClassAlloc.Allocated
            'objClassAlloc.Allocated = 0
            objClassAlloc.Allocated = CInt(txtAllocated.Text.Trim)
            objClassAlloc.CancelReason = txtReason.Text.Trim
            nResult = objTrClassAllocFacade.Update(objClassAlloc)
            If nResult <> -1 Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtReason.Text = ""
    End Sub
    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        sHClass.SetSession("backRes", 1)
        Dim StrUrl As String = "FrmTrClass.aspx"
        If Not Request.QueryString("Opener") Is Nothing Then
            StrUrl = Request.QueryString("Opener").Trim
        End If
        Response.Redirect(StrUrl)
        'Response.Redirect("FrmTrClass.aspx")
    End Sub


End Class