Imports System.Web
Imports System.Web.UI
Imports System.Collections
Imports System.Data
Imports System.Linq


Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit


Public Class Information
    Inherits System.Web.UI.Page


    Private Sub initData()

        Try
            Dim strsp As String = "sp_GetSPKInfo '{0}' ;"
            Dim ds As DataSet
            ds = New SPKHeaderFacade(User).RetrieveDataset(String.Format(strsp, CType(Session("SPK"), SPKHeader).SPKNumber))

            ViewState("ds") = ds

            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then

                lblDealerName.Text = ds.Tables(0).Rows(0)("DealerName").ToString()
                lblOrderDate.Text = CDate(ds.Tables(0).Rows(0)("OrderDate")).ToString("dd-MM-yyyy")
                lblSPKNumber.Text = ds.Tables(0).Rows(0)("SPKNumber").ToString()
                lblDealerSPKNumber.Text = ds.Tables(0).Rows(0)("DealerSPKNumber").ToString()
                lblName.Text = ds.Tables(0).Rows(0)("Name1").ToString()
                lblHP.Text = ds.Tables(0).Rows(0)("Handphone").ToString()

                If ds.Tables(0).Rows(0)("Status").ToString().ToLower() <> "Batal".ToLower Then


                    If ds.Tables(1).Rows.Count > 0 Then
                        phVehcileInfo.Visible = True
                        gvVehicle.DataSource = ds.Tables(1)
                        gvVehicle.DataBind()


                    End If

                    If ds.Tables(2).Rows.Count > 0 Then
                        PHChassis.Visible = True
                        GVChassis.DataSource = ds.Tables(2)
                        GVChassis.DataBind()
                    End If

                Else
                    lblOK.Visible = False
                    lblFalse.Visible = True

                    Dim strCancel As String = lblFalse.Text

                    lblFalse.Text = String.Format(strCancel, CDate(ds.Tables(0).Rows(0)("CancelDate")).ToString("dd-MM-yyyy"), ds.Tables(0).Rows(0)("Reason").ToString())
                    If ds.Tables(1).Rows.Count > 0 Then
                        phVehcileInfo.Visible = True
                        gvVehicle.DataSource = ds.Tables(1)
                        gvVehicle.DataBind()


                    End If
                End If

            End If

            Me.Master.ShowLogin() = True
        Catch ex As Exception
            ErrorMessage.Visible = True
            FailureText.Text = ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("SPK")) Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            initData()
        End If
    End Sub

    Private Sub gvVehicle_DataBinding(sender As Object, e As EventArgs) Handles gvVehicle.DataBinding

    End Sub

    Private Sub gvVehicle_DataBound(sender As Object, e As EventArgs) Handles gvVehicle.DataBound
    End Sub

    Private Sub GVChassis_DataBound(sender As Object, e As EventArgs) Handles GVChassis.DataBound

    End Sub

    Private Sub GVChassis_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GVChassis.RowDataBound
        If Not IsNothing(e.Row.DataItem) Then
            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim dr As DataRowView = CType(e.Row.DataItem, DataRowView)
                    Dim txtHOD As Label = CType(e.Row.FindControl("txtHOD"), Label)
                    Dim dv As DataView = dr.DataView
                    Dim dvr As DataRow = dv.ToTable().Rows(e.Row.DataItemIndex)

                    If IsNothing(dvr("HandOverDate")) OrElse CType(dvr("HandOverDate"), DateTime).Year <= 1900 Then
                        txtHOD.Text = ""
                    Else
                        txtHOD.Text = CType(dvr("HandOverDate"), DateTime).ToString("dd-MM-yyyy")
                    End If
                End If
                
            Catch ex As Exception

            End Try
           
        End If
    End Sub

    Private Sub gvVehicle_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvVehicle.RowCommand

        If e.CommandName = "InfoChassis" Then

            Try

                If Not IsNothing(ViewState("ds")) Then
                    Dim ds As DataSet = CType(ViewState("ds"), DataSet)

                 


                    If ds.Tables(2).Rows.Count > 0 Then

                        Dim dt As DataTable = ds.Tables(2)
                        Dim objFilter = dt.Select("VechileTypeCode='" & e.CommandArgument.ToString() & "'")
                        If Not IsNothing(objFilter) AndAlso objFilter.Count > 0 Then
                            PHChassis.Visible = True
                            GVChassis.DataSource = objFilter.CopyToDataTable()
                            GVChassis.DataBind()
                        Else
                            PHChassis.Visible = False
                        End If

                    End If

                    If ds.Tables(1).Rows.Count > 0 Then
                        phVehcileInfo.Visible = True
                        gvVehicle.DataSource = ds.Tables(1)
                        gvVehicle.DataBind()


                    End If
                End If

            Catch ex As Exception

            End Try
           
        End If

    End Sub

    Private Sub gvVehicle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvVehicle.RowDataBound
        If Not IsNothing(e.Row.DataItem) Then
            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim dr As DataRowView = CType(e.Row.DataItem, DataRowView)
                    Dim lblType As Label = CType(e.Row.FindControl("lblType"), Label)
                    Dim dv As DataView = dr.DataView
                    Dim dvr As DataRow = dv.ToTable().Rows(e.Row.DataItemIndex)


                    lblType.Text = dvr("Tipe").ToString()

                End If

            Catch ex As Exception

            End Try

        End If
    End Sub
End Class