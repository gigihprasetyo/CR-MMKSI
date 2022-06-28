#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

#End Region

Public Class PopUpMSPExtendedRegistrationSelection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub dtgMSPRegistration_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPRegistration.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As MSPExRegistration = CType(e.Item.DataItem, MSPExRegistration)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub
End Class