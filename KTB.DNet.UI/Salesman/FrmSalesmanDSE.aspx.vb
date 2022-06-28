#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region


Public Class FrmSalesmanDSE
    Inherits System.Web.UI.Page

    Private ssH As New SessionHelper()
    Private _IsAccessView As Boolean = Nothing
    Private _IsAccessInput As Boolean = Nothing
    Private _IsCheckView As Boolean = False
    Private _IsCheckInput As Boolean = False

    Public ReadOnly Property IsAccessView() As Boolean
        Get
            If Not _IsCheckView Then
                _IsCheckView = True
                _IsAccessView = SecurityProvider.Authorize(Context.User, SR.SalesmanDSE_View_Privilege)
            End If
            Return _IsAccessView
        End Get
    End Property

    Public ReadOnly Property IsAccessInput() As Boolean
        Get
            If Not _IsCheckInput Then
                _IsCheckInput = True
                _IsAccessInput = SecurityProvider.Authorize(Context.User, SR.SalesmanDSE_Input_Privilege)
            End If
            Return _IsAccessInput
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsAccessView Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Salesman DSE")
        End If

        If Not IsPostBack Then
            InitialPage()
        End If
    End Sub

    Private Sub InitialPage()
        Dim dealer As Dealer = Me.GetDealer()
        lblPageTitle.Text = "Salesman DSE"
        If Me.IsKTB() Then
            lblDealerCode.Text = String.Empty
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True
            lblPopUpDealer.AddOnClick("ShowPPDealerSelection();")
        Else
            trDealername.Visible = False
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            lblDealerCode.Text = dealer.DealerCode + " - " + dealer.DealerName
            lblPopUpSalesman.AddOnClick("ShowPopUpSalesmanbyDealer('');")
            BindGridDealer()
        End If
        btnSimpan.Enabled = IsAccessInput
    End Sub

    Private Sub BindGridDealer()
        Dim func As New SalesmanDSEFacade(Me.User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, Me.GetDealer.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SalesmanDSE), "Status", Sort.SortDirection.DESC))
        sortColl.Add(New Sort(GetType(SalesmanDSE), "Priority", Sort.SortDirection.ASC))

        txtUrutan.Text = func.GetNewPriority(Me.GetDealer)
        Dim arrSalesmanDSE As ArrayList = func.RetrieveByCriteria(criterias, sortColl)
        Dim listSls As List(Of SalesmanDSE) = New List(Of SalesmanDSE)()
        If arrSalesmanDSE.Count > 0 Then
            listSls = arrSalesmanDSE.Cast(Of SalesmanDSE).ToList()
            ssH.SetSession("DataSalesman", listSls)

            dtgSalesmanDSE.DataSource = arrSalesmanDSE
            dtgSalesmanDSE.PageSize = arrSalesmanDSE.Count
            dtgSalesmanDSE.DataBind()
        End If

        Dim funcAll As New SalesmanDSEAllocationFacade(Me.User)
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSEAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(SalesmanDSEAllocation), "Dealer.ID", MatchType.Exact, Me.GetDealer.ID))

        Dim arrAll As ArrayList = funcAll.Retrieve(criterias2)
        If arrAll.Count > 0 Then
            Dim qouta As Integer = CType(arrAll(0), SalesmanDSEAllocation).Quota
            Dim jumlahAktif As Integer = listSls.Where(Function(x) x.Status = 1).Count
            Dim sisa As Integer = qouta - jumlahAktif

            lblSisa.Text = sisa.ToString()
        Else
            lblSisa.Text = "-"
        End If

    End Sub

    Private Sub BindGridMKS(ByVal objDealer As Dealer)
        Dim func As New SalesmanDSEFacade(Me.User)
        Dim listSls As List(Of SalesmanDSE) = New List(Of SalesmanDSE)()

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDealer.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SalesmanDSE), "Status", Sort.SortDirection.DESC))
        sortColl.Add(New Sort(GetType(SalesmanDSE), "Priority", Sort.SortDirection.ASC))


        txtUrutan.Text = func.GetNewPriority(objDealer)
        Dim arrSalesmanDSE As ArrayList = func.RetrieveByCriteria(criterias, sortColl)
        If arrSalesmanDSE.Count > 0 Then
            listSls = arrSalesmanDSE.Cast(Of SalesmanDSE).ToList()
            ssH.SetSession("DataSalesman", listSls)

            dtgSalesmanDSE.DataSource = arrSalesmanDSE
            dtgSalesmanDSE.PageSize = arrSalesmanDSE.Count
            dtgSalesmanDSE.DataBind()
        End If

        Dim funcAll As New SalesmanDSEAllocationFacade(Me.User)
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSEAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(SalesmanDSEAllocation), "Dealer.ID", MatchType.Exact, objDealer.ID))


        Dim arrAll As ArrayList = funcAll.Retrieve(criterias2)
        If arrAll.Count > 0 Then
            Dim qouta As Integer = CType(arrAll(0), SalesmanDSEAllocation).Quota
            Dim jumlahAktif As Integer = listSls.Where(Function(x) x.Status = 1).Count
            Dim sisa As Integer = qouta - jumlahAktif

            lblSisa.Text = sisa.ToString()
        Else
            lblSisa.Text = "-"
        End If
    End Sub

    Private Sub ClearPage()
        txtSalesmanCode.Text = String.Empty
        'txtUrutan.Text = String.Empty
        hdnSalesmanCode.Value = String.Empty
        lblNama.Text = String.Empty
        lblGrade.Text = String.Empty
        txtNoPhone.Text = String.Empty
        lblGrade.Text = String.Empty
        lblPosisi.Text = String.Empty
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            If String.IsNullorEmpty(hdnSalesmanCode.Value) Then
                MessageBox.Show("Kode salesman tidak boleh kosong.")
                Return
            End If

            If Not String.IsNullorEmpty(lblSisa.Text) AndAlso Not lblSisa.Text = "-" Then
                If CInt(lblSisa.Text) = 0 Then
                    MessageBox.Show("Kuota sudah habis.")
                    Return
                End If
            End If
            Dim func As New SalesmanDSEFacade(Me.User)

            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(SalesmanDSE), "SalesmanHeader.SalesmanCode", MatchType.Exact, hdnSalesmanCode.Value))
            criterias2.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, "1"))

            Dim arrSls As ArrayList = func.Retrieve(criterias2)
            If arrSls.Count > 0 Then
                Dim currSDE As SalesmanDSE = CType(arrSls(0), SalesmanDSE)
                If currSDE.Dealer.ID = currSDE.SalesmanHeader.Dealer.ID Then
                    MessageBox.Show(String.Format("Sudah terdaftar dan aktif di dealer {0}.", currSDE.Dealer.DealerCode))
                    Return
                Else
                    currSDE.RowStatus = -1
                    Me.NonAktif_Changes(currSDE)
                End If
            End If

            Dim objDSE As New SalesmanDSE
            If Me.IsDealer Then
                objDSE.Dealer = Me.GetDealer()
            Else
                objDSE.Dealer = New DealerFacade(Me.User).Retrieve(txtDealerCode.Text)
            End If
            objDSE.SalesmanHeader = New SalesmanHeaderFacade(Me.User).Retrieve(hdnSalesmanCode.Value)
            objDSE.PhoneNumber = txtNoPhone.Text
            objDSE.Priority = CInt(txtUrutan.Text)
            objDSE.Status = 1

            Dim rest As Integer = AddNew_Changes(objDSE)
            If rest > -1 Then
                MessageBox.Show(SR.SaveSuccess)
                ClearPage()
                If Me.IsDealer Then
                    BindGridDealer()
                Else
                    BindGridMKS(New DealerFacade(Me.User).Retrieve(txtDealerCode.Text))
                End If
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearPage()
    End Sub

    Private Sub dtgSalesmanDSE_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgSalesmanDSE.ItemCommand
        Dim func As New SalesmanDSEFacade(User)
        Dim dglblID As Label = e.Item.FindLabel("lblID")
        Dim objSalesmanDSE As SalesmanDSE = New SalesmanDSEFacade(Me.User).Retrieve(CInt(dglblID.Text))
        Select Case e.CommandName.ToLower()
            Case "delete"
                objSalesmanDSE.RowStatus = -1
                ssH.SetSession("salesmandse", objSalesmanDSE)
                NonAktif_Changes(objSalesmanDSE)
                MessageBox.Show(SR.DataDeleted("Salesman DSE"))
            Case "inaktif"
                objSalesmanDSE.Status = 0
                If objSalesmanDSE.Dealer.ID <> objSalesmanDSE.SalesmanHeader.Dealer.ID Then
                    objSalesmanDSE.RowStatus = -1
                    NonAktif_Changes(objSalesmanDSE)
                    MessageBox.Show(String.Format("Salesman atas nama {0} sudah tidak terdaftar di dealer {1}", _
                                                  objSalesmanDSE.SalesmanHeader.Name, objSalesmanDSE.Dealer.DealerCode))

                Else
                    NonAktif_Changes(objSalesmanDSE)
                    MessageBox.Show("Data Salesman DSE berhasil dinonaktifkan")
                End If

            Case "aktif"
                If Not String.IsNullorEmpty(lblSisa.Text) AndAlso Not lblSisa.Text = "-" Then
                    If CInt(lblSisa.Text) = 0 Then
                        MessageBox.Show("Kuota sudah habis.")
                        Return
                    End If
                End If
                If objSalesmanDSE.Dealer.ID <> objSalesmanDSE.SalesmanHeader.Dealer.ID Then
                    objSalesmanDSE.RowStatus = -1
                    func.Update(objSalesmanDSE)
                    MessageBox.Show(String.Format("Salesman atas nama {0} sudah tidak terdaftar di dealer {1}", _
                                                  objSalesmanDSE.SalesmanHeader.Name, objSalesmanDSE.Dealer.DealerCode))
                    Return
                End If

                Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(SalesmanDSE), "SalesmanHeader.SalesmanCode", MatchType.Exact, objSalesmanDSE.SalesmanHeader.ID))
                criterias2.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, "1"))

                Dim arrSls As ArrayList = func.Retrieve(criterias2)
                If arrSls.Count > 0 Then
                    Dim currSDE As SalesmanDSE = CType(arrSls(0), SalesmanDSE)
                    MessageBox.Show(String.Format("Sudah terdaftar dan aktif di dealer {0}.", currSDE.Dealer.DealerCode))
                    Return
                End If

                objSalesmanDSE.Status = 1
                Aktif_Changes(objSalesmanDSE)
                MessageBox.Show("Data Salesman DSE berhasil diaktifkan")
            Case "uplist"
                UpList_Changes(objSalesmanDSE)
            Case "downlist"
                DownList_Changes(objSalesmanDSE)
        End Select
        If Me.IsKTB Then
            BindGridMKS(New DealerFacade(Me.User).Retrieve(txtDealerCode.Text))
        Else
            BindGridDealer()
        End If
    End Sub

    Private Sub UpList_Changes(ByVal objDSE As SalesmanDSE)
        Dim func As New SalesmanDSEFacade(Me.User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDSE.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

        Dim listDSE As List(Of SalesmanDSE) = func.Retrieve(criterias).Cast(Of SalesmanDSE).ToList()

        'Get salesman DSE sama dengan urutan
        For Each iDSE As SalesmanDSE In listDSE.Where(Function(x) x.Priority = objDSE.Priority - 1)
            iDSE.Priority = objDSE.Priority
            func.Update(iDSE)
        Next
        objDSE.Priority = objDSE.Priority - 1
        func.Update(objDSE)
    End Sub

    Private Sub DownList_Changes(ByVal objDSE As SalesmanDSE)
        Dim func As New SalesmanDSEFacade(Me.User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDSE.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

        Dim listDSE As List(Of SalesmanDSE) = func.Retrieve(criterias).Cast(Of SalesmanDSE).ToList()

        'Get salesman DSE sama dengan atau lebih besar dari urutan
        For Each iDSE As SalesmanDSE In listDSE.Where(Function(x) x.Priority = objDSE.Priority + 1)
            iDSE.Priority = objDSE.Priority
            func.Update(iDSE)
        Next
        objDSE.Priority = objDSE.Priority + 1
        func.Update(objDSE)
    End Sub

    Private Function AddNew_Changes(ByVal objDSE As SalesmanDSE) As Integer
        Dim func As New SalesmanDSEFacade(Me.User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDSE.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

        Dim listDSE As List(Of SalesmanDSE) = func.Retrieve(criterias).Cast(Of SalesmanDSE).ToList()

        'Get salesman DSE sama dengan atau lebih besar dari urutan
        For Each iDSE As SalesmanDSE In listDSE.Where(Function(x) x.Priority >= objDSE.Priority)
            iDSE.Priority = iDSE.Priority + 1
            func.Update(iDSE)
        Next
        Return func.Insert(objDSE)
    End Function

    Private Function NonAktif_Changes(ByVal objDSE As SalesmanDSE) As Integer
        Dim func As New SalesmanDSEFacade(Me.User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDSE.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

        Dim listDSE As List(Of SalesmanDSE) = func.Retrieve(criterias).Cast(Of SalesmanDSE).ToList()
        If objDSE.Priority > 0 Then
            For Each iDSE As SalesmanDSE In listDSE.Where(Function(x) x.Priority > objDSE.Priority)
                iDSE.Priority = iDSE.Priority - 1
                func.Update(iDSE)
            Next
        End If
        'Get salesman DSE sama dengan atau lebih besar dari urutan
        objDSE.Priority = 0
        Return func.Update(objDSE)
    End Function

    Private Function Aktif_Changes(ByVal objDSE As SalesmanDSE) As Integer
        Dim func As New SalesmanDSEFacade(Me.User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDSE.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

        Dim listDSE As List(Of SalesmanDSE) = func.Retrieve(criterias).Cast(Of SalesmanDSE).ToList()
        If listDSE.Count > 0 Then
            objDSE.Priority = listDSE.Max(Function(x) x.Priority) + 1
        Else
            objDSE.Priority = 1
        End If

        Return func.Update(objDSE)
    End Function

    Private Sub dtgSalesmanDSE_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgSalesmanDSE.ItemDataBound
        If e.IsRowItems Then
            Dim dglblNo As Label = e.FindLabel("lblNo")
            Dim dglblKode As Label = e.FindLabel("lblSalesmanCode")
            Dim dglblNama As Label = e.FindLabel("lblNama")
            Dim dglblNoPhone As Label = e.FindLabel("lblNoPhone")
            Dim dglblPosisi As Label = e.FindLabel("lblPosisi")
            Dim dglblGrade As Label = e.FindLabel("lblGrade")
            Dim dglblScore As Label = e.FindLabel("lblScore")
            Dim dglblStatus As Label = e.FindLabel("lblStatus")
            Dim dglblID As Label = e.FindLabel("lblID")
            Dim lbtnUp As LinkButton = e.Item.FindControl("lbtnUp")
            Dim lbtnDown As LinkButton = e.Item.FindControl("lbtnDown")
            Dim lbtnAktif As LinkButton = e.Item.FindControl("lbtnAktif")
            Dim lbtnInAktif As LinkButton = e.Item.FindControl("lbtnInAktif")

            Dim funcGrade As New SalesmanGradeFacade(Me.User)
            Dim objData As SalesmanDSE = e.DataItem(Of SalesmanDSE)()
            Dim objSalesman As SalesmanHeader = objData.SalesmanHeader
            dglblID.Text = objData.ID.ToString()
            If objData.Status = 1 Then
                dglblNo.Text = objData.Priority.ToString()
            Else
                dglblNo.Text = "-"
            End If

            dglblKode.Text = objSalesman.SalesmanCode
            dglblNama.Text = objSalesman.Name
            dglblPosisi.Text = objSalesman.JobPosition.Description
            dglblNoPhone.Text = objData.PhoneNumber
            Dim lastGrade As SalesmanGrade = funcGrade.LastGrade(objSalesman.SalesmanCode)
            If Not IsNothing(lastGrade) Then
                dglblGrade.Text = ListGrade.FirstOrDefault(Function(z) z.ValueId = lastGrade.Grade).ValueDesc
                If lastGrade.Score > 0 Then
                    dglblScore.Text = lastGrade.Score.ToString()
                End If
            Else
                dglblGrade.Text = "-"
                dglblScore.Text = "-"
            End If
            dglblStatus.Text = IIf(objData.Status = 1, "Aktif", "Tidak Aktif")
            Dim firstPosition As Integer = 1
            Dim lastPosition As Integer = GetDataSalesmanDSE.Max(Function(p) p.Priority And p.Status = 1)

            lbtnUp.Visible = IsAccessInput
            lbtnDown.Visible = IsAccessInput
            lbtnAktif.Visible = IsAccessInput
            lbtnInAktif.Visible = IsAccessInput
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = IsAccessInput

            If objData.Priority = firstPosition Or objData.Status = 0 Then
                lbtnUp.Visible = False
            End If
            If objData.Priority = lastPosition Or objData.Status = 0 Then
                lbtnDown.Visible = False
            End If
            If objData.Status = 1 Then
                lbtnAktif.Visible = False
            Else
                lbtnInAktif.Visible = False
            End If

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            lbtnAktif.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            lbtnInAktif.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
        End If
    End Sub

    Private ReadOnly Property ListGrade As List(Of StandardCode)
        Get
            Return New StandardCodeFacade(Me.User).RetrieveByCategory("SalesmanGrade").Cast(Of StandardCode).ToList()
        End Get
    End Property

    Private Sub dtgSalesmanDSE_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgSalesmanDSE.PageIndexChanged

    End Sub

    Private Function GetDataSalesmanDSE() As List(Of SalesmanDSE)
        If Not IsNothing(ssH.GetSession("DataSalesman")) Then
            Return CType(ssH.GetSession("DataSalesman"), List(Of SalesmanDSE))
        End If
        Return New List(Of SalesmanDSE)
    End Function

    Private Sub btnTriggerSalesman_Click(sender As Object, e As EventArgs) Handles btnTriggerSalesman.Click
        Dim funcSales As New SalesmanHeaderFacade(Me.User)
        Dim funcGrade As New SalesmanGradeFacade(Me.User)
        Dim funcDSE As New SalesmanDSEFacade(Me.User)

        Dim objSales As SalesmanHeader = funcSales.Retrieve(hdnSalesmanCode.Value)
        lblNama.Text = objSales.Name
        lblPosisi.Text = objSales.JobPosition.Description


        txtNoPhone.Text = funcDSE.GetPhoneNumber(objSales)
        Dim lastGrade As SalesmanGrade = funcGrade.LastGrade(objSales.SalesmanCode)
        If Not IsNothing(lastGrade) Then
            lblGrade.Text = listGrade.FirstOrDefault(Function(z) z.ValueId = lastGrade.Grade).ValueDesc
        End If

    End Sub

    Private Sub btnTriggerDealer_Click(sender As Object, e As EventArgs) Handles btnTriggerDealer.Click
        Dim objDealer As Dealer = New DealerFacade(Me.User).Retrieve(txtDealerCode.Text)
        lblDealerName.Text = objDealer.DealerName
        lblPopUpSalesman.AddOnClick("ShowPopUpSalesmanbyDealer('" + objDealer.DealerCode + "');")
        BindGridMKS(objDealer)
    End Sub

End Class