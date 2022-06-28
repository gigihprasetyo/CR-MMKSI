Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmEntryHargaPasar
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPerwakilan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgCompetitor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    'Protected WithEvents ddlPeriod1 As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"

    Private arl As ArrayList = New ArrayList
    Private objDealer As Dealer = New Dealer
    Dim sHelper As New SessionHelper
    Private bEditPriv As Boolean
#End Region

#Region "Custom Methods"

#Region "Check Privilage"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MarketPriceEntryView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Transaksi - Masukkan Harga")
        End If

        bEditPriv = SecurityProvider.Authorize(context.User, SR.MarketPriceEntrySave_Privilege)
        btnSave.Visible = bEditPriv
    End Sub

#End Region

    Sub BindData()
        arl = CType(sHelper.GetSession("Data"), ArrayList)
        dgCompetitor.DataSource = arl
        dgCompetitor.DataBind()
    End Sub

    Sub BindMarketCategory()
        Dim arlMC As New ArrayList
        Dim e As EnumMarketCategory = New EnumMarketCategory
        arlMC = e.RetrieveMarketCategory()
        ddlCategory.DataTextField = "NameStatus"
        ddlCategory.DataValueField = "ValStatus"
        ddlCategory.DataSource = arlMC
        ddlCategory.DataBind()
    End Sub

    Private Function IsExistMerkAndType(ByVal arlList As ArrayList, ByVal Merk As String, ByVal Type As String)
        For Each oMP As MarketPrice In arlList
            If (oMP.CompetitorType.Code.Trim.ToLower = Type.ToLower And oMP.CompetitorType.CompetitorBrand.Code.Trim.ToLower = Merk.ToLower) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub RenderPartItem(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, ByVal NamaKendaraan As String)
        Dim lblFName As Label = CType(e.Item.FindControl("lblFName"), Label)
        lblFName.Text = NamaKendaraan
    End Sub

    Private Function CekDependensiAll(ByVal e As DataGridCommandEventArgs) As Boolean

        'cek dependensi antara merk dan kategori
        Dim txtFMerk As TextBox = CType(e.Item.FindControl("txtFMerk"), TextBox)
        If ddlCategory.SelectedValue = 0 And txtFMerk.Text = "MITSUBISHI" Then
            Return True
        ElseIf ddlCategory.SelectedValue = 0 And txtFMerk.Text <> "MITSUBISHI" Then
            MessageBox.Show("Cek dependensi Merk dan Kategori")
            Return False
            Exit Function
        ElseIf ddlCategory.SelectedValue = 1 And txtFMerk.Text = "MITSUBISHI" Then
            MessageBox.Show("Cek dependensi Merk dan Kategori")
            Return False
            Exit Function
        ElseIf ddlCategory.SelectedValue = 1 And txtFMerk.Text <> "MITSUBISHI" Then
            Return True
        End If

        'cek dependensi antara merk dan tipe
        Dim txtFType As TextBox = CType(e.Item.FindControl("txtFType"), TextBox)
        Dim objCompetitorBrand As CompetitorBrand = New CompetitorBrandFacade(User).Retrieve(txtFMerk.Text)
        If objCompetitorBrand Is Nothing Then
            MessageBox.Show("Tipe tidak terdaftar dalam Merk yang bersangkutan")
            Return False
            Exit Function
        Else
            For Each item As CompetitorType In objCompetitorBrand.CompetitorTypes
                If item.Code.ToLower = txtFType.Text.ToLower Then
                    Return True
                    Exit Function
                Else
                    MessageBox.Show("Cek dependensi Merk dan Tipe")
                    Return False
                    Exit Function
                End If
            Next
        End If
    End Function

    Private Function CekTypeStatus(ByVal e As DataGridCommandEventArgs) As Boolean
        Dim txtFType As TextBox = CType(e.Item.FindControl("txtFType"), TextBox)

        'cek tipe dengan status aktif saja
        Dim objCompetitorType As CompetitorType = New CompetitorTypeFacade(User).Retrieve(txtFType.Text)
        If objCompetitorType.Status = EnumStatusSPL.StatusSPL.Aktif Then
            Return True
        Else
            MessageBox.Show("Tipe: " & txtFType.Text & " sudah tidak aktif!")
            txtFType.Text = ""
            Return False
        End If
    End Function

    Private Function CekMerkStatus(ByVal e As DataGridCommandEventArgs) As Boolean
        Dim txtFMerk As TextBox = CType(e.Item.FindControl("txtFMerk"), TextBox)

        'cek merk dengan status aktif saja
        Dim objCompetitorBrand As CompetitorBrand = New CompetitorBrandFacade(User).Retrieve(txtFMerk.Text)
        If objCompetitorBrand.Status = EnumStatusSPL.StatusSPL.Aktif Then
            Return True
        Else
            MessageBox.Show("Merk: " & txtFMerk.Text & " sudah tidak aktif!")
            txtFMerk.Text = ""
            Return False
        End If
    End Function

    Private Sub updateSession(ByVal arlist As ArrayList)
        Dim arlUpdate As ArrayList = New ArrayList
        For Each item As MarketPrice In arlist
            Dim objResult As MarketPrice = New MarketPrice

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MarketPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MarketPrice), "Dealer.ID", MatchType.Exact, item.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(MarketPrice), "ValidDate", MatchType.Exact, item.ValidDate))
            criterias.opAnd(New Criteria(GetType(MarketPrice), "CompetitorType.ID", MatchType.Exact, item.CompetitorType.ID))

            Dim arResult As ArrayList = New ArrayList
            arResult = New MarketPriceFacade(User).Retrieve(criterias)
            If arResult.Count > 0 Then
                arlUpdate.Add(CType(arResult(0), MarketPrice))
            End If
        Next
        'Todo session
        'Session("Data") = arlUpdate
        sHelper.SetSession("Data", arlUpdate)
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        objDealer = CType(sHelper.GetSession("DEALER"), Dealer)

        Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.HargaPasar).ToString)
        If Not objDealer Is Nothing Then
            If Not (objTransaction Is Nothing) Then
                If objTransaction.Status = 0 Then
                    'arrList.RemoveAt(arrList.Count - 1)
                    Response.Redirect("../frmAccessDenied.aspx?modulName=Harga Pasar")
                    Exit Sub
                End If
            End If
        End If


        InitiateAuthorization()
        If Not IsPostBack Then
            sHelper.SetSession("Data", Nothing)
            lblDealer.Text = String.Format("{0} / {1}", objDealer.DealerCode, objDealer.DealerName)
            If (Not objDealer.Area1 Is Nothing) Then
                lblPerwakilan.Text = objDealer.Area1.Description
            End If
            If IsNothing(sHelper.GetSession("Data")) Then
                'Todo session
                'Session("Data") = arl
                sHelper.SetSession("Data", arl)
            End If
            BindMarketCategory()
            BindData()
            Load_ddlPeriod()
            ddlPeriod.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        arl = CType(sHelper.GetSession("Data"), ArrayList)
        If (arl.Count > 0) Then
            If (New DealerReport.MarketPriceFacade(User).InsertUpdate(arl) = 1) Then
                updateSession(arl)
                dgCompetitor.DataSource = CType(sHelper.GetSession("Data"), ArrayList)
                dgCompetitor.DataBind()

                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Else
            MessageBox.Show("Data belum diisi")
        End If
    End Sub

    Private Sub dgCompetitor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCompetitor.ItemDataBound

        If e.Item.ItemType = ListItemType.Footer Then

            ' Modified by Ikhsan, 1 Dec 2008
            ' Requested by Rina as Part Of CR
            ' To modify the DropDownList
            ' Start --------------------------------------------------------------------


            Dim ddlDateF As DropDownList = CType(e.Item.FindControl("ddlDateF"), DropDownList)

            Dim ArrddlDateF As ArrayList = New ArrayList

            Dim SelectedItem As String
            If Not IsNothing(ddlPeriod) Then
                SelectedItem = CStr(viewstate("Period"))
            Else
                SelectedItem = "0"
            End If
            ArrddlDateF = Load_ddlDateF(SelectedItem)

            Dim i As Byte = 0

            If Not IsNothing(ArrddlDateF) Then
                If ddlPeriod.SelectedIndex = "1" Then
                    i = 1
                ElseIf ddlPeriod.SelectedIndex = "2" Then
                    i = 11
                ElseIf ddlPeriod.SelectedIndex = "3" Then
                    i = 21
                End If
                'Else
                '    i = 0
                '    ArrddlDateF.Add("Silahkan Pilih")
            End If

            For Each itemddlDateF As String In ArrddlDateF
                ddlDateF.Items.Add(New ListItem(itemddlDateF.ToString, i.ToString))
                i += 1
            Next

            ' End -------------------------------------------------------------------





            Dim lblFPopUpMerk As Label = CType(e.Item.FindControl("lblFPopUpMerk"), Label)
            Dim lblFType As Label = CType(e.Item.FindControl("lblFType"), Label)

            Dim txtFMerk As TextBox = CType(e.Item.FindControl("txtFMerk"), TextBox)
            Dim idKat As Integer = CInt(viewstate("Kategori"))
            If idKat = 0 Then
                txtFMerk.Text = "MITSUBISHI"
            Else
                txtFMerk.Text = ""
            End If
            If (IsPostBack) Then
                If (ddlCategory.SelectedItem.Text.ToUpper = "MITSUBISHI") Then
                    txtFMerk.Text = "MITSUBISHI"
                End If
            End If

            lblFPopUpMerk.Attributes("onclick") = "ShowPPMerk()"
            lblFType.Attributes("onclick") = "ShowPPType()"
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim ddlDate As DropDownList = CType(e.Item.FindControl("ddlDate"), DropDownList)

            Dim ArrddlDateF As ArrayList = New ArrayList

            Dim SelectedItem As String
            If Not IsNothing(ddlPeriod) Then
                SelectedItem = CStr(viewstate("Period"))
            Else
                SelectedItem = "0"
            End If
            ArrddlDateF = Load_ddlDateF(SelectedItem)

            Dim i As Byte = 0

            If Not IsNothing(ArrddlDateF) Then
                If ddlPeriod.SelectedIndex = "1" Then
                    i = 1
                ElseIf ddlPeriod.SelectedIndex = "2" Then
                    i = 11
                ElseIf ddlPeriod.SelectedIndex = "3" Then
                    i = 21
                End If
                'Else
                '    i = 0
                '    ArrddlDateF.Add("Silahkan Pilih")
            End If

            For Each itemddlDateF As String In ArrddlDateF
                ddlDate.Items.Add(New ListItem(itemddlDateF.ToString, i.ToString))
                i += 1
            Next


            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Clear()
            e.Item.Cells(0).Controls.Add(lNum)
            Dim lblEPopUpMerk As Label = CType(e.Item.FindControl("lblEPopUpMerk"), Label)
            Dim lblEType As Label = CType(e.Item.FindControl("lblEType"), Label)

            lblEPopUpMerk.Attributes("onclick") = "ShowPPMerk()"
            lblEType.Attributes("onclick") = "ShowPPType()"
        End If
    End Sub

    Private Sub dgCompetitor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCompetitor.ItemCommand
        arl = CType(sHelper.GetSession("Data"), ArrayList)
        Dim ddlDateF As DropDownList = CType(e.Item.FindControl("ddlDateF"), DropDownList)
        Dim ddlDate As DropDownList = CType(e.Item.FindControl("ddlDate"), DropDownList)
        Select Case e.CommandName
            Case "add"
                Dim arlListBrand As New ArrayList
                Dim arlListType As New ArrayList

                Dim txtFMerk As TextBox = CType(e.Item.FindControl("txtFMerk"), TextBox)
                Dim txtFType As TextBox = CType(e.Item.FindControl("txtFType"), TextBox)
                Dim txtFOnTheRoad As TextBox = CType(e.Item.FindControl("lblFOnTheRoad"), TextBox)
                Dim txtFBBN As TextBox = CType(e.Item.FindControl("lblFBBN"), TextBox)

                'cek dependensi
                If CekDependensiAll(e) = True Then
                    If txtFMerk.Text.Trim() <> String.Empty Then
                        If CekMerkStatus(e) = True Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CompetitorBrand), "Code", MatchType.Exact, txtFMerk.Text.Trim()))
                            arlListBrand = New DealerReport.CompetitorBrandFacade(User).Retrieve(criterias)
                            If (arlListBrand.Count = 0) Then
                                MessageBox.Show("Merk tidak terdaftar")
                                Return
                            End If
                        Else
                            Return
                        End If
                    Else
                        MessageBox.Show("Merk harus diisi")
                        Return
                    End If

                    If txtFType.Text.Trim() <> String.Empty Then
                        If CekTypeStatus(e) = True Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CompetitorType), "Code", MatchType.Exact, txtFType.Text.Trim()))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CompetitorType), "CompetitorBrand.Code", MatchType.Exact, CType(arlListBrand(0), CompetitorBrand).Code))

                            arlListType = New DealerReport.CompetitorTypeFacade(User).Retrieve(criterias)
                            If (arlListType.Count = 0) Then
                                MessageBox.Show("Tipe tidak terdaftar")
                                Return
                            End If
                        Else
                            Return
                        End If
                    Else
                        MessageBox.Show("Tipe harus diisi")
                        Return
                    End If

                    If (txtFOnTheRoad.Text.Trim() = String.Empty) Then
                        RenderPartItem(e, CType(arlListType(0), CompetitorType).Description)
                        MessageBox.Show("Harga on the road harus diisi")
                        Return
                    Else
                        If CDec(txtFOnTheRoad.Text) < 0 Then
                            MessageBox.Show("Harga on the road tidak boleh minus")
                            Return
                        ElseIf CDec(txtFOnTheRoad.Text) > 999999999999999999 Then
                            MessageBox.Show("Nilai harga on the road telah melebihi batas maksimal yg di perbolehkan")
                            Return
                        End If
                    End If

                    If (txtFBBN.Text.Trim() = String.Empty) Then
                        RenderPartItem(e, CType(arlListType(0), CompetitorType).Description)
                        MessageBox.Show("Harga BBN harus diisi")
                        Return
                    Else
                        If CDec(txtFBBN.Text) < 0 Then
                            MessageBox.Show("Harga BBN tidak boleh minus")
                            Return
                        ElseIf CDec(txtFBBN.Text) > 999999999999999999 Then
                            MessageBox.Show("Nilai harga BBN telah melebihi batas maksimal yg di perbolehkan")
                            Return
                        End If
                    End If


                    Dim txtFooterOtherInfo As TextBox = CType(e.Item.FindControl("txtFooterOtherInfo"), TextBox)

                    Dim oMP As MarketPrice = New MarketPrice
                    If (Not IsExistMerkAndType(arl, txtFMerk.Text.Trim(), txtFType.Text.Trim())) Then
                        Dim nresult As Integer = 0
                        nresult = GetID(arlListType, ConstructDate(ddlDateF.SelectedValue.ToString))
                        If nresult > 0 Then
                            oMP = New MarketPriceFacade(User).Retrieve(nresult)
                        End If
                        oMP.Dealer = objDealer
                        oMP.BBN = CDec(txtFBBN.Text.Trim())
                        oMP.CompetitorType = CType(arlListType(0), CompetitorType)
                        oMP.MarketCategory = ddlCategory.SelectedValue
                        oMP.OnTheRoadPrice = CDec(txtFOnTheRoad.Text.Trim())
                        oMP.PostingDate = Date.Now
                        ' modified by Ikhsan , 1 Des 2008
                        ' Requested by Rina as Part Of CR
                        ' To Change the input date from dropdownlist
                        ' oMP.ValidDate = icDate.Value
                        If Year(ConstructDate(ddlDateF.SelectedValue.ToString)) <> 1700 Then
                            oMP.ValidDate = ConstructDate(ddlDateF.SelectedValue.ToString)
                        Else
                            MessageBox.Show("Format Data Tidak Valid")
                        End If
                        oMP.OtherInfo = txtFooterOtherInfo.Text.Trim()

                        arl.Add(oMP)
                    Else
                        MessageBox.Show(SR.DataIsExist("Merk dan tipe"))
                    End If
                Else
                    Exit Select
                End If

            Case "save" 'Update this datagrid item   
                Dim arlListBrand As New ArrayList
                Dim arlListType As New ArrayList

                Dim txtEMerk As TextBox = CType(e.Item.FindControl("txtEMerk"), TextBox)
                Dim txtEType As TextBox = CType(e.Item.FindControl("txtEType"), TextBox)
                Dim txtEOnTheRoad As TextBox = CType(e.Item.FindControl("lblEOnTheRoad"), TextBox)
                Dim txtEBBN As TextBox = CType(e.Item.FindControl("lblEBBN"), TextBox)
                Dim lblEName As Label = CType(e.Item.FindControl("lblEName"), Label)

                If txtEMerk.Text.Trim() <> String.Empty Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CompetitorBrand), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CompetitorBrand), "Code", MatchType.Exact, txtEMerk.Text.Trim()))
                    arlListBrand = New DealerReport.CompetitorBrandFacade(User).Retrieve(criterias)
                    If (arlListBrand.Count = 0) Then
                        MessageBox.Show("Merk tidak terdaftar")
                        Return
                    End If
                Else
                    MessageBox.Show("Merk harus diisi")
                    Return
                End If

                If txtEType.Text.Trim() <> String.Empty Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CompetitorType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CompetitorType), "Code", MatchType.Exact, txtEType.Text.Trim()))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CompetitorType), "CompetitorBrand.Code", MatchType.Exact, CType(arlListBrand(0), CompetitorBrand).Code))

                    arlListType = New DealerReport.CompetitorTypeFacade(User).Retrieve(criterias)
                    If (arlListType.Count = 0) Then
                        MessageBox.Show("Tipe tidak terdaftar")
                        Return
                    End If
                Else
                    MessageBox.Show("Tipe harus diisi")
                    Return
                End If

                If (txtEOnTheRoad.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Harga on the road harus diisi")
                    Return
                Else
                    If CDec(txtEOnTheRoad.Text) < 0 Then
                        MessageBox.Show("Harga on the road tidak boleh minus")
                        Return
                    ElseIf CDec(txtEOnTheRoad.Text) > 999999999999999999 Then
                        MessageBox.Show("Nilai harga on the road telah melebihi batas maksimal yg di perbolehkan")
                        Return
                    End If
                End If

                If (txtEBBN.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Harga BBN harus diisi")
                    Return
                Else
                    If CDec(txtEBBN.Text) < 0 Then
                        MessageBox.Show("Harga BBN tidak boleh minus")
                        Return
                    ElseIf CDec(txtEBBN.Text) > 999999999999999999 Then
                        MessageBox.Show("Nilai harga BBN telah melebihi batas maksimal yg di perbolehkan")
                        Return
                    End If
                End If

                Dim txtEditOtherInfo As TextBox = CType(e.Item.FindControl("txtEditOtherInfo"), TextBox)

                Dim oMP As MarketPrice = CType(arl(e.Item.ItemIndex), MarketPrice)

                Dim StrDate As Date

                If Year(ConstructDate(ddlDate.SelectedValue.ToString)) <> 1700 Then
                    StrDate = ConstructDate(ddlDate.SelectedValue.ToString)
                Else
                    MessageBox.Show("Format Data Tidak Valid")
                    Return
                End If

                If (oMP.CompetitorType.CompetitorBrand.Code = txtEMerk.Text.Trim() And oMP.CompetitorType.Code = txtEType.Text.Trim() _
                    And oMp.ValidDate = StrDate) Then
                    oMP.BBN = txtEBBN.Text.Trim()
                    oMP.CompetitorType = CType(arlListType(0), CompetitorType)
                    oMP.MarketCategory = ddlCategory.SelectedValue
                    oMP.OnTheRoadPrice = txtEOnTheRoad.Text.Trim()
                    oMP.PostingDate = Date.Now
                    oMP.ValidDate = StrDate
                    omp.OtherInfo = txtEditOtherInfo.Text
                    dgCompetitor.EditItemIndex = -1
                    dgCompetitor.ShowFooter = True
                Else
                    If (Not IsExistMerkAndType(arl, txtEMerk.Text.Trim(), txtEType.Text.Trim())) Then
                        oMP = New MarketPrice
                        oMP.BBN = CDec(txtEBBN.Text.Trim())
                        oMP.CompetitorType = CType(arlListType(0), CompetitorType)
                        oMP.MarketCategory = ddlCategory.SelectedValue
                        oMP.OnTheRoadPrice = CDec(txtEOnTheRoad.Text.Trim())
                        oMP.PostingDate = Date.Now
                        'oMP.ValidDate = icDate.Value
                        oMP.ValidDate = StrDate
                        omp.OtherInfo = txtEditOtherInfo.Text
                        dgCompetitor.EditItemIndex = -1
                        dgCompetitor.ShowFooter = True
                    Else
                        MessageBox.Show(SR.DataIsExist("Merk dan tipe"))
                    End If
                End If
            Case "edit" 'Edit mode activated
                dgCompetitor.ShowFooter = False
                dgCompetitor.EditItemIndex = e.Item.ItemIndex
            Case "delete" 'Delete this datagrid item 
                Try
                    arl.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgCompetitor.EditItemIndex = -1
                dgCompetitor.ShowFooter = True
        End Select
        'Todo session
        'Session("Data") = arl
        sHelper.SetSession("Data", arl)
        BindData()
    End Sub

#End Region

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        Dim idKategori As Integer = ddlCategory.SelectedValue
        viewstate.Add("Kategori", idKategori)
        'For Each x As DataGridItem In dgCompetitor.Items.Count
        '    if x.
        'Next
        BindData()
    End Sub

    Private Function GetID(ByVal arrlst As ArrayList, ByVal VldDate As Date) As Integer
        ' Modified by Ikhsan, 20081202
        ' Requested by Rina as Part of CR
        ' Start ----------------------------------------
        Try
            Dim arrResult As ArrayList = New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "ValidDate", MatchType.Exact, VldDate))
            ' End ------------------------------------------
            Dim _User As String = String.Empty
            Dim objUser As UserInfo = New UserInfo
            If Not (IsNothing(sHelper.GetSession("LOGINUSERINFO"))) Then
                objUser = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
                _User = objUser.Dealer.ID.ToString.PadLeft(6, "0") & objUser.UserName
            End If
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "CreatedBy", MatchType.Exact, _User))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MarketPrice), "CompetitorType.ID", MatchType.Exact, CType(arrlst(0), CompetitorType).ID))
            arrlst = New MarketPriceFacade(User).Retrieve(criterias)
            If arrlst.Count > 0 Then
                Return CType(arrlst(0), MarketPrice).ID
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Private Sub Load_ddlPeriod()
        ddlPeriod.Items.Add(New ListItem("Silahkan Pilih", "0"))
        ddlPeriod.Items.Add(New ListItem("Begin", "1"))
        ddlPeriod.Items.Add(New ListItem("Middle", "2"))
        ddlPeriod.Items.Add(New ListItem("End", "3"))
    End Sub

    Private Function Load_ddlDateF(ByVal Periode As String) As ArrayList
        Dim ArrItem As ArrayList = New ArrayList

        If Periode = 0 Then
            ArrItem.Add("Silahkan Pilih")
        ElseIf Periode = 1 Then
            ArrItem.Add("1")
            ArrItem.Add("2")
            ArrItem.Add("3")
            ArrItem.Add("4")
            ArrItem.Add("5")
            ArrItem.Add("6")
            ArrItem.Add("7")
            ArrItem.Add("8")
            ArrItem.Add("9")
            ArrItem.Add("10")
        ElseIf Periode = 2 Then
            ArrItem.Add("11")
            ArrItem.Add("12")
            ArrItem.Add("13")
            ArrItem.Add("14")
            ArrItem.Add("15")
            ArrItem.Add("16")
            ArrItem.Add("17")
            ArrItem.Add("18")
            ArrItem.Add("19")
            ArrItem.Add("20")
        ElseIf Periode = 3 Then
            ArrItem.Add("21")
            ArrItem.Add("22")
            ArrItem.Add("23")
            ArrItem.Add("24")
            ArrItem.Add("25")
            ArrItem.Add("26")
            ArrItem.Add("27")
            ArrItem.Add("28")
            ArrItem.Add("29")
            ArrItem.Add("30")
            ArrItem.Add("31")
        End If

        Return ArrItem

    End Function



    Private Sub ddlPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriod.SelectedIndexChanged
        'dgCompetitor.DataBind()
        Dim idPeriod As Integer = ddlPeriod.SelectedIndex
        viewstate.Add("Period", idPeriod)
        BindData()
    End Sub

    Private Function ConstructDate(ByVal StrDay As String) As Date
        Dim StrDate As String
        If CInt(StrDay) < 10 Then
            StrDay = "0" + StrDay
        End If

        StrDate = StrDay + "/" + IIf(Date.Now.Month < 10, "0" + Date.Now.Month.ToString, Date.Now.Month.ToString + "/" + Date.Now.Year.ToString)

        If IsDate(CDate(StrDate)) Then
            Return CDate(StrDate)
        Else
            Return CDate("01/01/1700")
        End If


    End Function
End Class

