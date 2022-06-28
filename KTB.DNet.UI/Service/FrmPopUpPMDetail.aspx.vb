#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Drawing.Color
#End Region

Public Class FrmPopUpPMDetail
    Inherits System.Web.UI.Page

    Private objPMHeader As PMHeader
    Private objPMHeaderCollection As ArrayList
    Private objPMHeaderFacade As New PMHeaderFacade(User)
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents lblDealerVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblChassisNoVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblStandKMVal As System.Web.UI.WebControls.Label
    Protected WithEvents dgPMDetail As System.Web.UI.WebControls.DataGrid

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Custom Method"

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim pDealerCode As String
        'Dim pChassisNumber As String
        'Dim pServiceDate As DateTime
        Dim index As Integer
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Request.Params("index") = "-1" Then
            objPMHeader = objPMHeaderFacade.Retrieve(CInt(Request.Params("id")))
        Else
            'pDealerCode = Request.Params("dc")
            'pChassisNumber = Request.Params("cn")
            'pServiceDate = CType(Request.Params("dt"), DateTime)
            index = CType(Request.Params("index"), Integer)

            objPMHeaderCollection = CType(_sessHelper.GetSession("sessPMHeader"), ArrayList)
            objPMHeader = objPMHeaderCollection(index)
            'For Each Obj As PMHeader In objPMHeaderCollection
            '    If Not Obj.Dealer Is Nothing And Not Obj.ChassisMaster Is Nothing And Not Obj.ServiceDate < New DateTime(1900, 1, 1) Then
            '        If Obj.Dealer.DealerCode = pDealerCode And Obj.ChassisMaster.ChassisNumber = pChassisNumber And Obj.ServiceDate = pServiceDate Then
            '            objPMHeader = Obj
            '            Exit For
            '        End If
            '    End If
            'Next
        End If
        lblDealerVal.Text = objPMHeader.Dealer.DealerCode & "-" & objPMHeader.Dealer.DealerName
        lblChassisNoVal.Text = objPMHeader.ChassisMaster.ChassisNumber
        lblStandKMVal.Text = objPMHeader.StandKM.ToString

        dgPMDetail.DataSource = objPMHeader.PMDetails
        dgPMDetail.DataBind()

    End Sub

    Private Sub dgPMDetail_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPMDetail.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgPMDetail.CurrentPageIndex * dgPMDetail.PageSize)
            End If
        End If
    End Sub
#End Region

End Class
