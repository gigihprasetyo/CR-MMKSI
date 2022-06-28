Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports Ktb.DNet.Security

Public Class FrmSalesmanTurnOverAnalisa
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAnalisa As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMonthStart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonthEnd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtYearStart As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtYearEnd As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanTurnOverAnalisaFacade As New SalesmanTurnOverAnalisaFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "PrivateCustomMethods"




#End Region

#Region "EventHandlers"

    
#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        If CheckValidation() = False Then
            Return
        End If

        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTurnOverAnalisa), "PeriodeStart", MatchType.Exact, txtYearStart.Text & "/" & ddlMonthStart.SelectedValue & "/01"))

        criterias.opAnd(New Criteria(GetType(SalesmanTurnOverAnalisa), "PeriodeEnd", MatchType.Exact, txtYearStart.Text & "/" & ddlMonthEnd.SelectedValue & "/01"))

        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanTurnOverAnalisa), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        End If

        arrList = _SalesmanTurnOverAnalisaFacade.RetrieveByCriteria(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


        If arrList.Count > 0 Then
            ' tdk bs disimpan
            MessageBox.Show("Data tdk bisa disimpan, sudah ada sebelumnya")
        Else
            Dim nResult As Integer = -1
            Dim objSalesmanTurnOverAnalisa As SalesmanTurnOverAnalisa
            objSalesmanTurnOverAnalisa = New SalesmanTurnOverAnalisa

            ' data akan disimpan
            Dim strPeriodeStart As String
            Dim strPeriodeEnd As String
            strPeriodeStart = txtYearStart.Text & "/" & ddlMonthStart.SelectedValue & "/1"
            strPeriodeEnd = txtYearEnd.Text & "/" & ddlMonthEnd.SelectedValue & "/1"

            Dim objDealerFacade As DealerFacade = New DealerFacade(User)
            objSalesmanTurnOverAnalisa.Dealer = objDealerFacade.Retrieve(txtDealerCode.Text)
            objSalesmanTurnOverAnalisa.PeriodeStart = Date.Parse(strPeriodeStart)
            objSalesmanTurnOverAnalisa.PeriodeEnd = Date.Parse(strPeriodeEnd)
            objSalesmanTurnOverAnalisa.Analisa = txtAnalisa.Text

            nResult = New SalesmanTurnOverAnalisaFacade(User).Insert(objSalesmanTurnOverAnalisa)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
#Region "Need To Add"
    ' untuk mengecek validasi data sebelum disimpan
    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean
        blnValid = True

        If txtYearEnd.Text < txtYearStart.Text Then
            blnValid = False
            MessageBox.Show("Tahun akhir tdk boleh lebih kecil daripada tahun awal")
            Return blnValid
        End If

        If txtYearStart.Text = "" Then
            blnValid = False
            MessageBox.Show("Tahun awal tdk boleh kosong")
            Return blnValid
        End If

        If txtYearEnd.Text = "" Then
            blnValid = False
            MessageBox.Show("Tahun akhir tdk boleh kosong")
            Return blnValid
        End If

        If Not IsNumeric(txtYearStart.Text) Then
            blnValid = False
            MessageBox.Show("Format tahun awal harus dalam bentuk numeric")
            Return blnValid
        End If

        If Not IsNumeric(txtYearEnd.Text) Then
            blnValid = False
            MessageBox.Show("Format tahun akhir harus dalam bentuk numeric")
            Return blnValid
        End If

        Return blnValid
    End Function

    ' penambahan untuk initialize data
    Private Sub ClearData()
        'txtDealerCode.Text = String.Empty
        ddlMonthStart.SelectedIndex = -1
        txtYearStart.Text = DateTime.Today.Year.ToString
        ddlMonthEnd.SelectedIndex = -1
        txtYearEnd.Text = DateTime.Today.Year.ToString
        txtAnalisa.Text = String.Empty
        btnSimpan.Enabled = True
    End Sub

    Private Sub Initialize()
        ClearData()
        BindDropDownLists()
        CheckQueryStr()
    End Sub

    Private Sub CheckQueryStr()
        If (Request.QueryString("DealerCode") <> "") Then
            txtDealerCode.Text = Request.QueryString("DealerCode")
        End If
    End Sub

    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindFromEnum("Month", ddlMonthStart, Me.User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Month", ddlMonthEnd, Me.User, False, "NameStatus", "ValStatus")
    End Sub

    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        'btnSearch.Visible = _view

    End Sub


#End Region

    
End Class
