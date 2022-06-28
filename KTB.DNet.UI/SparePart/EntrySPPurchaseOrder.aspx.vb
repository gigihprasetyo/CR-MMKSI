Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.BusinessFacade.General
Imports Ktb.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility

'Imports Ktb.DNet.BusinessFacade.SparePart
Public Class EntrySPPurchaseOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode_Name As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents IntiCalendar1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents cmbOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPONo As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgSPOrderDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotHarga As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable"
    Private objSPHeader As Ktb.DNet.Domain.SparePartPO
    Dim objSPDetail As SparePartPODetail = New Ktb.DNet.Domain.SparePartPODetail
    Private nDealerID As Integer = 0
    Private sessHelper As SessionHelper = New SessionHelper
    Private SPPODetails As ArrayList = New ArrayList
    Private SPMasterFacade As KTB.DNet.BusinessFacade.SparePart.SparePartMasterFacade
    Private sp As SparePartMaster
#End Region

#Region "Custom Method"

    Private Function GetDate() As String
        Return CStr(Format(Date.Now(), "dd/MM/yyyy"))
    End Function
    Private Function SetDate() As Date
        Return Format(Date.Now(), "yyyy/MM/dd")
    End Function

    Private Sub RetrieveSPPOHeader()
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        lblDealerCode_Name.Text = objDealer.DealerCode + " / " + objDealer.DealerName
        cmbOrderType.DataSource = LookUp.ArraySPOrderType()
        cmbOrderType.DataBind()
        txtPONo.Text = "EAB0160123456"
        txtDate.Text = GetDate()
    End Sub
    Private Sub BindDG()
        dgSPOrderDetail.DataSource = CType(Session("sessPODetail"), ArrayList)
        dgSPOrderDetail.DataBind()
    End Sub
    Private Sub InitialzationDtg()
        sessHelper.SetSession("sessPODetail", SPPODetails)
    End Sub
    Private Sub GetDealerObject()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(nDealerID)
        sessHelper.SetSession("sesDealer", objDealer)
    End Sub
    Private Sub BindDataToPage()
        sessHelper.SetSession("sesPOHeader", objSPHeader)
        dgSPOrderDetail.DataSource = objSPHeader.SparePartPODetails
        dgSPOrderDetail.DataBind()
    End Sub
    Private Sub ClosePage()
        'ViewState.Clear()
        SessionHelper.RemoveAll()
        Response.Redirect("../default.aspx")
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            Dim dealerID As Integer = CInt(Request.QueryString("dealerid"))
            dealerID = 2
            If Not IsNothing(dealerID) Then
                nDealerID = CType(dealerID, Integer)
                GetDealerObject()
                RetrieveSPPOHeader()
                InitialzationDtg()
                BindDG()
            Else

                'MessageBox.Show("Tidak ada data dealer!")
                'ClosePage()
            End If

        End If
    End Sub


    Sub dgSPOrderDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "POFootNo"
                sp = New SparePartMasterFacade(User).Retrieve(1)
                Dim txt1 As TextBox = e.Item.FindControl("TextBox2")
                Dim txt2 As TextBox = e.Item.FindControl("txtFootPartName")
                Dim txt3 As TextBox = e.Item.FindControl("txtQty")
                Dim x As Decimal
                txt1.Text = sp.PartCode
                txt2.Text = sp.PartName
                e.Item.Cells(4).Text = sp.RetalPrice

            Case "btnAdd"
                objSPHeader = CType(Session("sesPOHeader"), SparePartPO)
                Dim txt As TextBox = e.Item.FindControl("txtQty")

                Dim CriteriaQty As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                CriteriaQty.opAnd(New Criteria(GetType(Ktb.DNet.Domain.SparePartMaster), "SparePartMaster.ID", MatchType.Exact, objSPDetail.SparePartMaster.ID))
                CriteriaQty.opAnd(New Criteria(GetType(Ktb.DNet.Domain.SparePartMaster), "SparePartMaster.ID", MatchType.Exact, sp.ID))
                Dim SPMasterArrayList As ArrayList = New SparePartMasterFacade(User).Retrieve(CriteriaQty)
                If (SPMasterArrayList.Count > 0 And sp.Stock > CInt(txt.Text)) Then
                    objSPDetail.Quantity = CInt(txt.Text)
                    objSPDetail.RetailPrice = sp.RetalPrice
                    objSPHeader.SparePartPODetails.Add(objSPDetail)
                    BindDataToPage()
                End If
        End Select
    End Sub


#End Region




  
   
End Class
