Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.SparePart
Public Class frmDownloadPartIncidentalAllocation
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

    Private objPriorityDetailFacade As New PartIncidentalPriorityDetailFacade(User)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.EnableViewState = False
        SetDownload()

        If Not IsPostBack Then
            BindDataToGrid()
        End If
    End Sub

    Private Sub BindDataToGrid()
        dgPartList.DataSource = New SessionHelper().GetSession("arrDownloadAble")
        dgPartList.DataBind()
    End Sub

    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Permintaan Khusus-Alokasi Pemesanan Khusus.xls").Append("""").ToString
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Function

    Private Sub dgPartList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalDetail = CType(e.Item.DataItem, PartIncidentalDetail)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPartList.CurrentPageIndex * dgPartList.PageSize)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = RowValue.PartIncidentalHeader.Dealer.DealerCode
                Dim lblReqNo As Label = CType(e.Item.FindControl("lblReqNo"), Label)
                lblReqNo.Text = RowValue.PartIncidentalHeader.RequestNumber

                Dim lblDealerMailNumber As Label = CType(e.Item.FindControl("lblDealerMailNumber"), Label)
                lblDealerMailNumber.Text = RowValue.PartIncidentalHeader.DealerMailNumber
                Dim lblModelCode As Label = CType(e.Item.FindControl("lblModelCode"), Label)
                lblModelCode.Text = RowValue.SparePartMaster.ModelCode
                Dim lblSupplierCode As Label = CType(e.Item.FindControl("lblSupplierCode"), Label)
                lblSupplierCode.Text = RowValue.SparePartMaster.SupplierCode

                Dim lblPKDate As Label = CType(e.Item.FindControl("lblPKDate"), Label)
                If RowValue.PartIncidentalHeader.IncidentalDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPKDate.Text = ""
                Else
                    lblPKDate.Text = RowValue.PartIncidentalHeader.IncidentalDate

                End If
                Dim lblPlanDate As Label = CType(e.Item.FindControl("lblPlanDate"), Label)
                If RowValue.PlanDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPlanDate.Text = ""
                Else
                    lblPlanDate.Text = RowValue.PlanDate
                End If


                Dim lblPartSubNo As Label = CType(e.Item.FindControl("lblPartSubNo"), Label)
                Dim lblPartSubName As Label = CType(e.Item.FindControl("lblPartSubName"), Label)
                Dim lblPartNo As Label = CType(e.Item.FindControl("lblPartNo"), Label)
                Dim lblPartName As Label = CType(e.Item.FindControl("lblPartName"), Label)
                Dim lblTypeCode As Label = CType(e.Item.FindControl("lblTypeCode"), Label)
                Dim lblProdYear As Label = CType(e.Item.FindControl("lblProdYear"), Label)
                Dim lblPriority As Label = CType(e.Item.FindControl("lblPriority"), Label)

                lblPartName.Text = RowValue.SparePartMaster.PartName
                lblPartNo.Text = RowValue.SparePartMaster.PartNumber
                lblTypeCode.Text = Mid(RowValue.ChassisNumber, 4, 4)

                If Val(RowValue.AssemblyYear) = 1980 Or Val(RowValue.AssemblyYear) = 0 Then
                    lblProdYear.Text = "N/A"
                    lblPriority.Text = "Others"
                Else
                    lblProdYear.Text = Val(RowValue.AssemblyYear).ToString
                    Dim objPriorityDetail As PartIncidentalPriorityDetail = objPriorityDetailFacade.Retrieve(lblTypeCode.Text)
                    If objPriorityDetail.ID = 0 Then
                        lblPriority.Text = "Others"
                    Else
                        If objPriorityDetail.StartProdYear > Val(RowValue.AssemblyYear) Then
                            lblPriority.Text = "Others"
                        Else
                            lblPriority.Text = objPriorityDetail.PartIncidentalPriority.Priority
                        End If
                    End If
                End If


                If RowValue.SparePartMasterSubstitutionID > 0 Then
                    Dim spMasterSub As SparePartMaster = getSparePartMaster(RowValue.SparePartMasterSubstitutionID)
                    If spMasterSub.ID > 0 Then
                        'lblPartSubNo.Text = spMasterSub.PartNumber
                        'lblPartSubName.Text = spMasterSub.PartName
                        lblPartName.Text = String.Empty
                        lblPartNo.Text = String.Empty
                        lblPartName.Text = spMasterSub.PartNumber
                        lblPartNo.Text = spMasterSub.PartNumber
                        e.Item.BackColor = Color.Yellow
                    End If
                End If
                Dim lblOrgQty As Label = CType(e.Item.FindControl("lblOrgQty"), Label)
                lblOrgQty.Text = RowValue.Quantity
                Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
                lblRemainQty.Text = RowValue.RemainQuantity
                Dim txtAlokasi As Label = CType(e.Item.FindControl("txtAlokasi"), Label)
                txtAlokasi.Text = 0
            End If
        End If
    End Sub

    Private Function getSparePartMaster(ByVal id As Integer) As SparePartMaster
        Dim spMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
        Return spMasterFacade.Retrieve(id)
    End Function
End Class
