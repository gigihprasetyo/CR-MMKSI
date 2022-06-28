Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.SparePart
Public Class frmDownloadPartIncidentalPOProcess
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPartList As System.Web.UI.WebControls.DataGrid

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
        'Put user code to initialize the page here
        Me.EnableViewState = False
        SetDownload()

        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Permintaan Khusus-Alokasi Pemesanan Khusus.xls").Append("""").ToString
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Function

    Private Sub BindGrid()

        dgPartList.DataSource = New SessionHelper().GetSession("arlDownloadAble")
        dgPartList.DataBind()
    End Sub

    Private Sub dgPartList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalPO = CType(e.Item.DataItem, PartIncidentalPO)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPartList.CurrentPageIndex * dgPartList.PageSize)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode
                Dim lblReqNo As Label = CType(e.Item.FindControl("lblReqNo"), Label)
                lblReqNo.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.RequestNumber
                Dim lblPKDate As Label = CType(e.Item.FindControl("lblPKDate"), Label)
                If RowValue.PartIncidentalDetail.PartIncidentalHeader.IncidentalDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPKDate.Text = ""
                Else
                    lblPKDate.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.IncidentalDate
                End If
                Dim lblPlanDate As Label = CType(e.Item.FindControl("lblPlanDate"), Label)

                If RowValue.PartIncidentalDetail.PlanDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPlanDate.Text = ""
                Else
                    lblPlanDate.Text = RowValue.PartIncidentalDetail.PlanDate
                End If

                Dim lblPartSubNo As Label = CType(e.Item.FindControl("lblPartSubNo"), Label)

                Dim lblPartName As Label = CType(e.Item.FindControl("lblPartName"), Label)
                Dim lblPartNo As Label = CType(e.Item.FindControl("lblPartNo"), Label)

                lblPartNo.Text = RowValue.PartIncidentalDetail.SparePartMaster.PartNumber
                lblPartName.Text = RowValue.PartIncidentalDetail.SparePartMaster.PartName

                If RowValue.PartIncidentalDetail.SparePartMasterSubstitutionID > 0 Then
                    Dim spMasterSub As SparePartMaster = getSparePartMaster(RowValue.PartIncidentalDetail.SparePartMasterSubstitutionID)
                    If spMasterSub.ID > 0 Then
                        'lblPartSubNo.Text = spMasterSub.PartNumber
                        lblPartNo.Text = spMasterSub.PartNumber
                        lblPartName.Text = spMasterSub.PartName
                        e.Item.BackColor = Color.Yellow
                    End If
                End If
                Dim lblOrgQty As Label = CType(e.Item.FindControl("lblOrgQty"), Label)
                lblOrgQty.Text = RowValue.PartIncidentalDetail.Quantity
                Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
                lblRemainQty.Text = RowValue.PartIncidentalDetail.RemainQuantity
                Dim cbSelect As CheckBox = CType(e.Item.FindControl("cbSelect"), CheckBox)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                Dim lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                Dim lblAlokasi As Label = CType(e.Item.FindControl("lblAlokasi"), Label)
                lblAlokasi.Text = RowValue.Alocation
                
            End If
        End If
    End Sub

    Private Function getSparePartMaster(ByVal id As Integer) As SparePartMaster
        Dim spMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
        Return spMasterFacade.Retrieve(id)
    End Function
End Class
