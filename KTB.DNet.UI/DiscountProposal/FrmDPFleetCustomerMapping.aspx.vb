Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.Service

Public Class FrmDPFleetCustomerMapping
    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private _sessData As String = "FrmDPFleetCustomerMapping._sessData"
    Private _sessDelData As String = "FrmDPFleetCustomerMapping._sessDelData"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If IsNothing(ViewState("FleetHeaderID")) Then
                ViewState("FleetHeaderID") = CInt(Request.QueryString("FleetHeaderID"))
            End If

            ViewState.Add("SortColFrmDPFleetCustomerDetail", "ID")
            ViewState.Add("SortDirFrmDPFleetCustomerDetail", Sort.SortDirection.DESC)
            sessHelper.SetSession(_sessDelData, New ArrayList)
            LoadData()
            BindGrid(0)
        End If
    End Sub

    Private Sub LoadData()
        Dim FleetHeader As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(ViewState("FleetHeaderID")))
        lblKodeFleet.Text = FleetHeader.FleetCode
        Dim stdCode As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(FleetHeader.FleetCustomerType, "EnumDiscountProposal.CustomerType")
        If stdCode.Count > 0 Then
            Dim vData As StandardCode = CType(stdCode(0), StandardCode)
            lblTipe.Text = vData.ValueDesc
        Else
            lblTipe.Text = ""
        End If

        stdCode = New StandardCodeFacade(User).RetrieveByValueId(FleetHeader.FleetCompanyCategory, "EnumDiscountProposal.FleetCategory")
        If stdCode.Count > 0 Then
            Dim vData As StandardCode = CType(stdCode(0), StandardCode)
            lblKategori.Text = vData.ValueDesc
        Else
            lblKategori.Text = ""
        End If

        lblNama.Text = FleetHeader.FleetCustomerName
        lblFleetGroup.Text = FleetHeader.FleetCustomerGroupCompany
        lblJenisUsaha.Text = FleetHeader.BusinessSectorDetail.BusinessSectorHeader.BusinessSectorName & " - " & FleetHeader.BusinessSectorDetail.BusinessDomain
        lblTglPengajualFleet.Text = FleetHeader.CreatedTime.ToString("yyyy/MM/dd")

    End Sub

    Private Sub BindGrid(ByVal index As Integer)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader.ID", MatchType.Exact, CInt(ViewState("FleetHeaderID"))))
        Dim totalrow As Integer = 0
        Dim arlFFleetCustomerDetailMapping As ArrayList = New FleetCustomerDetailMappingFacade(User).RetrieveActiveList(crit, index + 1,
                               dtgMain.PageSize,
                               totalrow,
                               CType(ViewState("SortColFrmDPFleetCustomerMapping"), String),
                               CType(ViewState("SortDirFrmDPFleetCustomerMapping"), Sort.SortDirection))

        sessHelper.SetSession(_sessData, arlFFleetCustomerDetailMapping)
        dtgMain.DataSource = arlFFleetCustomerDetailMapping
        dtgMain.VirtualItemCount = totalrow
        dtgMain.DataBind()
    End Sub

    Protected Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType.AlternatingItem Then
            Dim lblGridNoIdentitas As Label = CType(e.Item.FindControl("lblGridNoIdentitas"), Label)
            Dim objFleetCustomerDetailMapping As FleetCustomerDetailMapping = CType(e.Item.DataItem, FleetCustomerDetailMapping)
            If Not IsNothing(objFleetCustomerDetailMapping) AndAlso
                Not IsNothing(objFleetCustomerDetailMapping.Customer) AndAlso
                Not IsNothing(objFleetCustomerDetailMapping.Customer.CustomerRequest) Then

                lblGridNoIdentitas.Text = From oCustomerRequestProfile As CustomerRequestProfile In objFleetCustomerDetailMapping.Customer.CustomerRequest.CustomerRequestProfiles
                                          Where oCustomerRequestProfile.ProfileHeader.ID = 29
                                          Select oCustomerRequestProfile.ProfileValue
            End If
        End If
    End Sub

    Protected Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Select Case e.CommandName
            Case "Delete"
                Dim arlMapping As ArrayList = sessHelper.GetSession(_sessData)
                Dim arlDel As ArrayList = CType(sessHelper.GetSession(_sessDelData), ArrayList)
                arlDel.Add(arlMapping(e.Item.ItemIndex))
                arlMapping.RemoveAt(e.Item.ItemIndex)
                sessHelper.SetSession(_sessData, arlMapping)
                sessHelper.SetSession(_sessDelData, arlDel)
                dtgMain.DataSource = arlMapping
                dtgMain.DataBind()
        End Select
    End Sub

    Protected Sub hdnFleetCustomerID_ValueChanged(sender As Object, e As EventArgs) Handles hdnButton.Click
        Dim FleetHeader As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(ViewState("FleetHeaderID")))
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(Customer), "Code", MatchType.InSet, "('" & hdnFleetCustomerID.Value.Replace(";", "','") & "')"))
        Dim arlCust As ArrayList = New CustomerFacade(User).Retrieve(crit)
        Dim arlMapping As ArrayList = sessHelper.GetSession(_sessData)

        Dim crit2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit2.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader.ID", MatchType.Exact, CInt(ViewState("FleetHeaderID"))))
        crit2.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "Customer.Code", MatchType.InSet, "('" & hdnFleetCustomerID.Value.Replace(";", "','") & "')"))
        Dim arl As ArrayList = New FleetCustomerDetailMappingFacade(User).Retrieve(crit2)
        If arl.Count > 0 Then
            Dim dbMess As String = String.Empty
            For Each item As FleetCustomerDetailMapping In arl
                If dbMess.Trim.Length = 0 Then
                    dbMess = "Terdapat data customer yang sama pada : " & item.Customer.Name1
                Else
                    dbMess = dbMess & ", " & item.Customer.Name1
                End If
            Next
            MessageBox.Show(dbMess)
            Exit Sub
        End If


        Dim mappingList As List(Of FleetCustomerDetailMapping) = arlMapping.Cast(Of FleetCustomerDetailMapping).ToList()
        Dim custCode As String() = hdnFleetCustomerID.Value.Split(";")
        Dim arlMess As String = String.Empty
        For Each varStr As String In custCode
            Dim q As FleetCustomerDetailMapping = mappingList.Where(Function(i) i.Customer.Code = varStr).FirstOrDefault()
            If Not IsNothing(q) Then
                If arlMess.Trim.Length = 0 Then
                    arlMess = "Terdapat data customer yang sama pada : " & q.Customer.Name1
                Else
                    arlMess = arlMess & ", " & q.Customer.Name1
                End If
            End If
        Next
        If arlMess.Trim.Length > 0 Then
            MessageBox.Show(arlMess)
            Exit Sub
        End If

        For Each cust As Customer In arlCust
            Dim oMapping As New FleetCustomerDetailMapping
            oMapping.Customer = cust
            oMapping.FleetCustomerHeader = FleetHeader
            arlMapping.Add(oMapping)
        Next
        sessHelper.SetSession(_sessData, arlMapping)
        dtgMain.DataSource = arlMapping
        dtgMain.DataBind()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Dim arlData As ArrayList = sessHelper.GetSession(_sessData)

        Dim arlMapping = (From ObjES As FleetCustomerDetailMapping In arlData
            Where ObjES.Customer.Code = txtKodeCustomer.Text.Trim OrElse ObjES.Customer.Name1 = txtNamaCustomer.Text.Trim
            Order By ObjES.ID
            Select ObjES).ToList()
        dtgMain.DataSource = arlMapping
        dtgMain.DataBind()

    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim arlAdd As ArrayList = CType(sessHelper.GetSession(_sessData), ArrayList)
        Dim arlDel As ArrayList = CType(sessHelper.GetSession(_sessDelData), ArrayList)
        Dim custCode As String = String.Empty
        Dim nResult As Integer
        If arlDel.Count > 0 AndAlso custCode.Trim.Length = 0 Then
            Try
                For Each item As FleetCustomerDetailMapping In arlDel
                    If Not IsNothing(item.FleetCustomerHeader) Then
                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader.ID", MatchType.Exact, item.FleetCustomerHeader.ID))
                        crit.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "Customer.Code", MatchType.Exact, item.Customer.Code))
                        Dim arl As ArrayList = New FleetCustomerDetailMappingFacade(User).Retrieve(crit)
                        If arl.Count > 0 Then
                            Dim oFCDM As FleetCustomerDetailMapping = arl(0)
                            oFCDM.RowStatus = CType(DBRowStatus.Deleted, Short)
                            nResult = New FleetCustomerDetailMappingFacade(User).Update(oFCDM)

                            If nResult <= 0 Then
                                custCode = "Delete " & item.Customer.Code
                                Exit For
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show("Error " & SR.SaveFail)
            End Try
        End If

        If arlAdd.Count > 0 AndAlso custCode.Trim.Length = 0 Then
            For Each item As FleetCustomerDetailMapping In arlAdd
                Try
                    If Not IsNothing(item.FleetCustomerHeader) Then
                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader.ID", MatchType.Exact, item.FleetCustomerHeader.ID))
                        crit.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "Customer.Code", MatchType.Exact, item.Customer.Code))
                        Dim arl As ArrayList = New FleetCustomerDetailMappingFacade(User).Retrieve(crit)
                        If arl.Count > 0 Then
                            Dim oFCDM As FleetCustomerDetailMapping = arl(0)
                            oFCDM.FleetCustomerHeader = item.FleetCustomerHeader
                            oFCDM.Customer = item.Customer
                            nResult = New FleetCustomerDetailMappingFacade(User).Update(oFCDM)

                            If nResult <= 0 Then
                                custCode = "Add " & item.Customer.Code
                                Exit For
                            End If
                        Else
                            nResult = New FleetCustomerDetailMappingFacade(User).Insert(item)

                            If nResult <= 0 Then
                                custCode = "Add " & item.Customer.Code
                                Exit For
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error " & SR.SaveFail)
                End Try
            Next
        End If

        If nResult <= 0 Then
            If custCode.Trim.Length > 0 Then
                MessageBox.Show(SR.SaveFail & " pada customer code " & custCode)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Else
            sessHelper.SetSession(_sessData, New ArrayList)
            sessHelper.SetSession(_sessDelData, New ArrayList)
            MessageBox.Show(SR.SaveSuccess)
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmDPFleetCustomerList.aspx?IsBack=true")
    End Sub

    Protected Sub dtgMain_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        dtgMain.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub
End Class