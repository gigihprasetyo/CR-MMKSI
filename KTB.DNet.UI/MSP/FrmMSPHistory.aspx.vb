Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

Public Class FrmMSPHistory
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper
    Private _strSessMSPRegistrationHistoryID As String = "MSPRegistrationHistoryID"
    Private objMSPRegistration As New MSPRegistration
    Private objMSPRegistrationHistory As New MSPRegistrationHistory
    Private arr As New ArrayList

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillForm()
        End If
    End Sub

    Private Sub FillForm()
        Dim mspRegistrationHistoryID As Integer = _sessHelper.GetSession(_strSessMSPRegistrationHistoryID)
        objMSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(mspRegistrationHistoryID)
        objMSPRegistration = objMSPRegistrationHistory.MSPRegistration

        lblMSPCode.Text = objMSPRegistration.MSPCode

        ' bind data grid
        dtgMSPHistory.DataSource = objMSPRegistration.MSPRegistrationHistorys
        dtgMSPHistory.DataBind()

    End Sub

    Private Sub dtgMSPHistory_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPHistory.ItemDataBound
        ' set lblNo
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPRegistrationHistory = CType(e.Item.DataItem, MSPRegistrationHistory)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                Dim itemIndex As Integer = e.Item.ItemIndex
                Dim lastRowValue As New MSPRegistrationHistory
                If itemIndex > 0 Then
                    lastRowValue = CType(arr(itemIndex - 1), MSPRegistrationHistory)
                End If

                ' no rangka
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = rowValue.MSPRegistration.ChassisMaster.ChassisNumber
                End If

                ' old category
                Dim lblOldCategory As Label = CType(e.Item.FindControl("lblOldCategory"), Label)
                If Not IsNothing(lblOldCategory) Then
                    Dim oldCategory As String = If(itemIndex = 0, If(rowValue.BenefitMasterHeaderID = 0, "Paid", "Promo"), If(lastRowValue.BenefitMasterHeaderID = 0, "Paid", "Promo"))
                    lblOldCategory.Text = oldCategory
                End If

                ' new category
                Dim lblNewCategory As Label = CType(e.Item.FindControl("lblNewCategory"), Label)
                If Not IsNothing(lblNewCategory) Then
                    Dim newCategory As String = If(itemIndex = 0, String.Empty, If(rowValue.BenefitMasterHeaderID = 0, "Paid", "Promo"))
                    lblNewCategory.Text = newCategory
                End If

                ' old request type
                Dim lblOldRequestType As Label = CType(e.Item.FindControl("lblOldRequestType"), Label)
                If Not IsNothing(lblOldRequestType) Then
                    Dim oldRequestType As String = If(itemIndex = 0, CType(rowValue.RequestType, EnumStatusMSP.StatusType).ToString, CType(lastRowValue.RequestType, EnumStatusMSP.StatusType).ToString)
                    lblOldRequestType.Text = oldRequestType
                End If

                ' new request type
                Dim lblNewRequestType As Label = CType(e.Item.FindControl("lblNewRequestType"), Label)
                If Not IsNothing(lblNewRequestType) Then
                    Dim newRequestType As String = If(itemIndex = 0, String.Empty, CType(rowValue.RequestType, EnumStatusMSP.StatusType).ToString)
                    lblNewRequestType.Text = newRequestType
                End If

                ' old MSP type
                Dim lblOldMSPType As Label = CType(e.Item.FindControl("lblOldMSPType"), Label)
                If Not IsNothing(lblOldMSPType) Then
                    Dim oldMSPType As String = If(itemIndex = 0, rowValue.MSPMaster.MSPType.Description, lastRowValue.MSPMaster.MSPType.Description)
                    lblOldMSPType.Text = oldMSPType
                End If

                ' new MSP type
                Dim lblNewMSPType As Label = CType(e.Item.FindControl("lblNewMSPType"), Label)
                If Not IsNothing(lblNewRequestType) Then
                    Dim newMSPType As String = If(itemIndex = 0, String.Empty, rowValue.MSPMaster.MSPType.Description)
                    lblNewMSPType.Text = newMSPType
                End If

                ' old duration
                Dim lblOldDuration As Label = CType(e.Item.FindControl("lblOldDuration"), Label)
                If Not IsNothing(lblOldDuration) Then
                    Dim oldDuration As String = If(itemIndex = 0, rowValue.MSPMaster.Duration, lastRowValue.MSPMaster.Duration)
                    lblOldDuration.Text = oldDuration
                End If

                ' new duration
                Dim lblNewDuration As Label = CType(e.Item.FindControl("lblNewDuration"), Label)
                If Not IsNothing(lblNewDuration) Then
                    Dim newDuration As String = If(itemIndex = 0, String.Empty, rowValue.MSPMaster.Duration)
                    lblNewDuration.Text = newDuration
                End If

                ' request date
                Dim lblRequestDate As Label = CType(e.Item.FindControl("lblRequestDate"), Label)
                If Not IsNothing(lblRequestDate) Then
                    lblRequestDate.Text = rowValue.RegistrationDate.ToString("dd/MM/yyyy")
                End If

                ' diproses oleh
                Dim lblCreatedBy As Label = CType(e.Item.FindControl("lblCreatedBy"), Label)
                If Not IsNothing(lblCreatedBy) Then
                    lblCreatedBy.Text = rowValue.CreatedBy
                End If

                ' diproses oleh
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text = CType(rowValue.Status, EnumStatusMSP.Status).ToString
                End If

                ' add item to array
                arr.Add(rowValue)
            End If
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmMSPRegistrationList.aspx")
    End Sub
End Class