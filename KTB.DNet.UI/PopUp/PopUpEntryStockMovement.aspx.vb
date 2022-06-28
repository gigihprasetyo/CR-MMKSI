Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security

Public Class PopUpEntryStockMovement
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgStockMovement As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents lblStockDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamatStockDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtAllocateDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAlamatAllocateDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1AllocateDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchStockDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objChassis As ChassisMaster
    Dim sHelper As New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            objChassis = New ChassisMasterFacade(User).Retrieve(Integer.Parse(Request.QueryString("ID")))
            lblNoRangka.Text = objChassis.ChassisNumber
            If objChassis.StockDealer > 0 Then
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(objChassis.StockDealer)
                lblStockDealer.Text = objDealer.DealerCode + " - " + objDealer.SearchTerm1
            Else
                lblStockDealer.Text = objChassis.Dealer.DealerCode + " - " + objChassis.Dealer.SearchTerm1
            End If

            lblAlamatStockDealer.Text = objChassis.Dealer.Address
        End If
        lblSearchStockDealer.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpDealerSelectionOne.aspx','',500,760,DealerSelection);return false;")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        'insert new row into StockMovement
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtAllocateDealer.Text.Trim)
        If (objDealer.ID = 0) Then
            MessageBox.Show("Data Dealer Tidak Ditemukan")
            Return
        End If
        objChassis = New ChassisMasterFacade(User).Retrieve(Integer.Parse(Request.QueryString("ID")))
        If ValidateData() Then
            Dim _date As String = Date.Today.ToShortDateString
            Dim objStockMovement As New StockMovement
            objStockMovement.ChassisMaster = objChassis
            If objChassis.StockDealer > 0 Then
                objStockMovement.FromDealer = objChassis.StockDealer
            Else
                objStockMovement.FromDealer = objChassis.Dealer.ID
            End If
            objStockMovement.Dealer = objDealer
            objStockMovement.ProcessBy = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo).UserName
            objStockMovement.ProcessDate = CType(_date, DateTime)
            objChassis.StockDealer = objDealer.ID
            objChassis.StockDate = Now

            Dim n As Integer = New StockMovementFacade(User).Insert(objStockMovement)

            If n = 1 Then
                If New ChassisMasterFacade(User).Update(objChassis) <> -1 Then
                    MessageBox.Show("Data telah disimpan")
                    Response.Write("<script language='javascript'>{window.close();}</script>")
                Else
                    Return
                End If
            Else
                Return
            End If
        End If
    End Sub

    Private Function ValidateData() As Boolean

        Dim bcheck As Boolean = True
        If txtAllocateDealer.Text.Trim = String.Empty Then
            bcheck = False
            MessageBox.Show("Dealer belum dipilih")
        Else
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtAllocateDealer.Text.Trim)
            If objDealer Is Nothing Then
                bcheck = False
                MessageBox.Show("Dealer tidak terdaftar")
            Else
                Dim _objSessDealer As Dealer = CType(Session("Dealer"), Dealer)
                If _objSessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If objChassis.StockDealer <> _objSessDealer.ID Then
                        MessageBox.Show("No Rangka Bukan Stok dealer " & _objSessDealer.SearchTerm1)
                        bcheck = False
                    End If
                End If

                'Dim criterias2 As New CriteriaComposite(New Criteria(GetType(StockMovement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias2.opAnd(New Criteria(GetType(StockMovement), "Dealer.ID", MatchType.Exact, objDealer.ID))
                'criterias2.opAnd(New Criteria(GetType(StockMovement), "ChassisMaster.ID", MatchType.Exact, Integer.Parse(Request.QueryString("ID"))))
                'criterias2.opAnd(New Criteria(GetType(StockMovement), "ProcessDate", MatchType.GreaterOrEqual, Date.Today))

                'Dim temp As ArrayList = New StockMovementFacade(User).RetrieveByCriteria(criterias2)
                'If temp.Count > 0 Then
                '    bcheck = False
                '    MessageBox.Show("Transfer stok sudah pernah dilakukan ke dealer " + txtAllocateDealer.Text + " atau transfer stok untuk hari ini sudah dilakukan")
                'Else
                If objChassis.StockDealer = objDealer.ID Then
                    MessageBox.Show("Tujuan Dealer tidak boleh sama dengan Stock Dealer")
                    bcheck = False
                End If
                'End If
            End If
        End If
        Return bcheck
    End Function
End Class
