#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Transfer
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser
Imports System.IO
#End Region

Public Class FrmTOPTransferControl
    Inherits System.Web.UI.Page



#Region "Custom CLasss"
    <Serializable>
    Private Class FormFilterTemplate
        Sub New()

        End Sub

        Public SortCol As String
        Public CurrentSortDirect As Int32

        Public Property ddlProductCategory As String
        Public Property txtCreditAccount As String

        Public Property ddlPaymentType As String
        Public Property ddlPaymentMethod As String
        Public Property chkvalidFrom As Boolean
        Public Property calValidFrom As DateTime


        Public Property chkValidTo As String
        Public Property calValidTo As DateTime
        Public Property chkValidityDate As Boolean
        Public Property calValidityDate As DateTime



    End Class
#End Region

#Region "Variables"

    Private _sessHelper As New SessionHelper()

    Private _sessTransferControls As String = "FrmTOPTransferControl._sessTransferControls"

    Private _vstCritProductCategory As String = "FrmTOPTransferControl._vstCritProductCategory"
    Private _vstCritCreditAccount As String = "FrmTOPTransferControl._vstCritCreditAccount"
    Private _vstCritValidFrom As String = "FrmTOPTransferControl._vstCritValidFrom"
    Private _vstCritPaymentType As String = "FrmTOPTransferControl._vstCritPaymentType"
    Private _vstCritPaymentMethod As String = "FrmTOPTransferControl._vstCritPaymentMethod"
    Private _edit_transaction_control_transfer_Privilege As Boolean


#End Region

#Region "Custom"
    Private Sub initPage()


        initControl()
        BindGrid()

    End Sub

    Private Sub initControl()
        Dim companyCode As String = KTB.DNET.Lib.WebConfig.GetValue("CompanyCode")

        Me.txtCreditAccount.Text = ""
        Me.calValidFrom.Value = DateSerial(2016, 1, 1)

        Me.ddlPaymentType.Items.Clear()
        Me.ddlPaymentType.Items.Add(New ListItem("Semua", -1))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.TOP.ToString(), enumPaymentType.PaymentType.TOP))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.COD.ToString(), enumPaymentType.PaymentType.COD))
        Me.ddlPaymentType.Items.Add(New ListItem(enumPaymentType.PaymentType.RTGS.ToString(), enumPaymentType.PaymentType.RTGS))
        Me.ddlPaymentType.SelectedValue = -1
        GeneralScript.BindPaymentMethod(ddlPaymentMethod, True)

        Me.SaveCriteria()


        Me.btnCari.Visible = True
    End Sub

    Private Sub SaveCriteria()
        Dim objFormFilterTemplate As New FormFilterTemplate()
        objFormFilterTemplate.calValidFrom = calValidFrom.Value
        objFormFilterTemplate.calValidityDate = calValidityDate.Value
        objFormFilterTemplate.calValidTo = calValidTo.Value
        objFormFilterTemplate.chkvalidFrom = chkvalidFrom.Checked
        objFormFilterTemplate.chkValidTo = chkValidTo.Checked
        objFormFilterTemplate.chkValidityDate = chkValidityDate.Checked
        objFormFilterTemplate.ddlPaymentMethod = ddlPaymentMethod.SelectedValue
        objFormFilterTemplate.ddlPaymentType = ddlPaymentType.SelectedValue
        objFormFilterTemplate.txtCreditAccount = txtCreditAccount.Text

        Me.ViewState("Filtering") = objFormFilterTemplate




        'Me.ViewState.Add(Me._vstCritProductCategory, Me.ddlProductCategory.SelectedValue)
        'Me.ViewState.Add(Me._vstCritCreditAccount, Me.txtCreditAccount.Text.Trim)
        'Me.ViewState.Add(Me._vstCritValidFrom, Me.calValidFrom.Value.ToString("yyyy.MM.dd"))
        'Me.ViewState.Add(Me._vstCritPaymentType, Me.ddlPaymentType.SelectedValue)
        'Me.ViewState.Add(Me._vstCritPaymentMethod, Me.ddlPaymentMethod.SelectedValue)

    End Sub

    Private Sub LoadCriteria()


        Dim objFormFilterTemplate As New FormFilterTemplate()

        objFormFilterTemplate = Me.ViewState("Filtering")
        calValidFrom.Value = objFormFilterTemplate.calValidFrom
        calValidityDate.Value = objFormFilterTemplate.calValidityDate
        calValidTo.Value = objFormFilterTemplate.calValidTo
        chkvalidFrom.Checked = objFormFilterTemplate.chkvalidFrom
        chkValidTo.Checked = objFormFilterTemplate.chkValidTo
        chkValidityDate.Checked = objFormFilterTemplate.chkValidityDate
        ddlPaymentMethod.SelectedValue = objFormFilterTemplate.ddlPaymentMethod
        ddlPaymentType.SelectedValue = objFormFilterTemplate.ddlPaymentType
        txtCreditAccount.Text = objFormFilterTemplate.txtCreditAccount

        Me.txtCreditAccount.Enabled = True
        Me.ddlPaymentType.Enabled = True

        Me.btnSimpan.Visible = True
        Me.btnBatal.Visible = True
        Me.btnCari.Visible = True
        Me.dtgMain.Visible = True


        ViewState("IsNew") = True
    End Sub

    Private Function GetData() As ArrayList
        Dim oTCFac As New TOPTransferControlFacade(User)
        Dim cTC As New CriteriaComposite(New Criteria(GetType(TOPTransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sTC As New SortCollection()
        Dim aTCs As ArrayList

        If Me.txtCreditAccount.Text.Trim <> "" Then
            Dim sCA As String = "'" & Me.txtCreditAccount.Text.Trim.Replace(";", "','") & "'"

            cTC.opAnd(New Criteria(GetType(TOPTransferControl), "CreditAccount", MatchType.InSet, "(" & sCA & ")"))
        End If

        If chkvalidFrom.Checked Then
            cTC.opAnd(New Criteria(GetType(TOPTransferControl), "ValidFrom", MatchType.GreaterOrEqual, Me.calValidFrom.Value.ToString("yyyy.MM.dd")))
        End If

        If chkValidTo.Checked Then
            cTC.opAnd(New Criteria(GetType(TOPTransferControl), "ValidTo", MatchType.GreaterOrEqual, Me.calValidTo.Value.ToString("yyyy.MM.dd")))
        End If

        If chkValidityDate.Checked Then
            cTC.opAnd(New Criteria(GetType(TOPTransferControl), "ValidityDate", MatchType.GreaterOrEqual, Me.calValidityDate.Value.ToString("yyyy.MM.dd")))
        End If

        If CType(Me.ddlPaymentType.SelectedValue, Short) <> -1 Then
            Dim pt As Short = CType(Me.ddlPaymentType.SelectedValue, Short)

            cTC.opAnd(New Criteria(GetType(TOPTransferControl), "PaymentType", MatchType.Exact, pt))


        End If
        If CType(Me.ddlPaymentMethod.SelectedValue, Short) <> -1 Then
            Dim state As Short = CType(Me.ddlPaymentMethod.SelectedValue, Short)

            cTC.opAnd(New Criteria(GetType(TOPTransferControl), "Status", MatchType.Exact, state))
        End If
        sTC.Add(New Sort(GetType(TOPTransferControl), "CreditAccount", SortDirection.Ascending))
        sTC.Add(New Sort(GetType(TOPTransferControl), "PaymentType", SortDirection.Ascending))
        sTC.Add(New Sort(GetType(TOPTransferControl), "ValidFrom", SortDirection.Descending))

        aTCs = oTCFac.Retrieve(cTC, sTC)

        _sessHelper.SetSession(Me._sessTransferControls, aTCs)
        Return aTCs
    End Function

    Private Sub BindData(ByVal oTC As TOPTransferControl)
        Me.txtID.Text = oTC.ID

        Me.txtCreditAccount.Text = oTC.CreditAccount
        Me.ddlPaymentType.SelectedValue = oTC.PaymentType
        Me.ddlPaymentMethod.SelectedValue = oTC.Status
        Me.calValidFrom.Value = oTC.ValidFrom

        Me.txtCreditAccount.Enabled = False
        Me.ddlPaymentType.Enabled = False

        Me.btnSimpan.Visible = True
        Me.btnBatal.Visible = True
        Me.btnCari.Visible = False
        'If (CType(oTC.PaymentType, enumPaymentType.PaymentType) = enumPaymentType.PaymentType.TOP) Then
        '    Me.calValidTo.Value = oTC.ValidTo
        '    Me.calValidTo.Enabled = True
        'Else
        '    Me.calValidTo.Enabled = False
        'End If

        Me.calValidFrom.Value = oTC.ValidFrom
        Me.calValidTo.Value = oTC.ValidTo
        Me.calValidityDate.Value = oTC.ValidityDate



    End Sub

    Private Sub BindGrid()
        Dim aTCs As ArrayList = Me.GetData()

        Me.dtgMain.DataSource = aTCs
        Me.dtgMain.DataBind()
        Me.dtgMain.Visible = True
    End Sub

    Private Sub checkPrivilege()
        Dim objDealer As Dealer = Me._sessHelper.GetSession("DEALER")
        Dim _lihat_transaction_control_transfer = SecurityProvider.Authorize(Context.User, SR.lihat_transaction_control_transfer_Privilege)
        Dim _edit_transaction_control_transfer_Privilege = SecurityProvider.Authorize(Context.User, SR.edit_transaction_control_transfer_Privilege)

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then '1=KTB
            If Not _lihat_transaction_control_transfer Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
            End If

            If Not _edit_transaction_control_transfer_Privilege Then
                Dim idCol As Integer = dtgMain.Columns.Count()
                idCol = idCol - 1
                dtgMain.Columns(idCol).Visible = False
                btnSimpan.Visible = False
                btnBatal.Visible = False

            Else


            End If

        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales - Transfer Payment")
        End If
    End Sub

    Private Sub SaveData()
        Dim oTC As New TOPTransferControl
        Dim oTCFac As New TOPTransferControlFacade(User)
        Dim oPC As ProductCategory
        Dim IsOk As Boolean = False
        Dim oVCA As v_CreditAccount
        Dim dtValid As Date

        If Me.txtCreditAccount.Text.Trim() = "" Then
            MessageBox.Show("Credit Account Harus Diisi!")
            Exit Sub
        End If


        oVCA = New v_CreditAccountFacade(User).Retrieve(Me.txtCreditAccount.Text)
        If IsNothing(oVCA) OrElse oVCA.ID < 1 Then
            MessageBox.Show("Credit Account Tidak Valid!")
            Exit Sub
        End If
        Try
            dtValid = Me.calValidFrom.Value
        Catch ex As Exception
            MessageBox.Show("Tanggal Berlaku Mulai Tidak Valid!")
            Exit Sub
        End Try


        If CType(Me.ddlPaymentMethod.SelectedValue, Short) = -1 Then
            MessageBox.Show("Metode Pembayaran Harus Diisi!")
            Exit Sub
        End If
        If CType(Me.ddlPaymentType.SelectedValue, Short) = -1 Then
            MessageBox.Show("Tipe Pembayaran Harus Diisi!")
            Exit Sub
        End If

        If dtValid.Day <> 1 Then
            MessageBox.Show("Tanggal Berlaku Tidak Valid. Harus Awal Bulan (Tanggal 1)")
            Exit Sub
        End If

        oTC.ID = Me.txtID.Text
        oTC.CreditAccount = oVCA.CreditAccount
        oTC.ValidFrom = dtValid
        oTC.PaymentType = Me.ddlPaymentType.SelectedValue
        oTC.Status = CType(Me.ddlPaymentMethod.SelectedValue, TransferControl.EnumPaymentScheme)
        If (CType(oTC.PaymentType, enumPaymentType.PaymentType) = enumPaymentType.PaymentType.TOP) Then
            oTC.ValidityDate = calValidTo.Value
        End If


        If 1 = 2 AndAlso oTC.ID < 1 Then 'never happend, always edit (have ID)
            oTC.ID = oTCFac.Insert(oTC)
        Else
            Dim oTCDB As TOPTransferControl = oTCFac.Retrieve(oTC.ID)

            If oTC.Status <> oTCDB.Status OrElse oTC.ValidFrom <> oTCDB.ValidFrom OrElse oTC.ValidityDate <> oTCDB.ValidityDate Then
                'validate date
                If oTC.ValidFrom <= DateSerial(Now.Year, Now.Month, Now.Day) Then
                    MessageBox.Show("Perubahan Tanggal Berlaku Tidak Valid! Harus Dilakukan Paling Lambat Di Bulan Sebelumnya.")
                    Exit Sub
                End If

                'validate to trans data
                'find POs with EffDate>=oTC.ValidFrom and POHeader.IsTransfer<>oTC.Status : jika ada maka JANGAN DISIMPAN
                Dim Sql As String = "select count(*) n from POHeader poh with (nolock) " & _
                    " where poh.RowStatus =0 and poh.Status in (0,2,4,6,8) " & _
                    " and poh.ReqAllocationDateTime>='" & oTC.ValidFrom.ToString("yyyy.MM.dd") & "' " & _
                    " and poh.IsTransfer<>" & oTC.Status.ToString()
                Sql = "(" & Sql & ")"

                Dim cD As New CriteriaComposite(New Criteria(GetType(Dealer), "ID", MatchType.Exact, 2))
                Dim aDs As ArrayList
                Dim oDFac As New DealerFacade(User)

                cD.opAnd(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, Sql))
                aDs = oDFac.Retrieve(cD)

                If aDs.Count = 0 Then
                    MessageBox.Show("Simpan Gagal. Sudah Ada Data PO di Periode Tersebut")
                    Exit Sub
                End If

                oTC.ID = 0
                oTC.ID = oTCFac.Insert(oTC)

                IsOk = oTC.ID > 0

                If IsOk Then
                    If oTC.ValidFrom = oTCDB.ValidFrom Then
                        'cancel the last status
                        oTCDB.RowStatus = -1
                        oTCDB.ID = oTCFac.Update(oTCDB)
                    End If

                    Me.calValidTo.Enabled = True
                    MessageBox.Show("Simpan Berhasil")

                    LoadCriteria()
                    BindGrid()
                Else
                    MessageBox.Show("Simpan Gagal")
                    LoadCriteria()
                End If
            Else ' No ChANGES
                LoadCriteria()
                BindGrid()
            End If

        End If

    End Sub


    Private Sub SaveDataNew()
        Dim oTC As New TOPTransferControl
        Dim oTCFac As New TOPTransferControlFacade(User)
        Dim oPC As ProductCategory
        Dim IsOk As Boolean = False
        Dim oVCA As v_CreditAccount
        Dim dtValid As Date

        If Me.txtCreditAccount.Text.Trim() = "" Then
            MessageBox.Show("Credit Account Harus Diisi!")
            Exit Sub
        End If


        oVCA = New v_CreditAccountFacade(User).Retrieve(Me.txtCreditAccount.Text)
        If IsNothing(oVCA) OrElse oVCA.ID < 1 Then
            MessageBox.Show("Credit Account Tidak Valid!")
            Exit Sub
        End If
        Try
            dtValid = Me.calValidFrom.Value
        Catch ex As Exception
            MessageBox.Show("Tanggal Berlaku Mulai Tidak Valid!")
            Exit Sub
        End Try


        If CType(Me.ddlPaymentMethod.SelectedValue, Short) = -1 Then
            MessageBox.Show("Metode Pembayaran Harus Diisi!")
            Exit Sub
        End If
        If CType(Me.ddlPaymentType.SelectedValue, Short) = -1 Then
            MessageBox.Show("Tipe Pembayaran Harus Diisi!")
            Exit Sub
        End If

        If dtValid.Day <> 1 Then
            MessageBox.Show("Tanggal Berlaku Tidak Valid. Harus Awal Bulan (Tanggal 1)")
            Exit Sub
        End If

        oTC.ID = Me.txtID.Text
        oTC.CreditAccount = oVCA.CreditAccount
        oTC.ValidFrom = dtValid
        oTC.PaymentType = Me.ddlPaymentType.SelectedValue
        oTC.Status = CType(Me.ddlPaymentMethod.SelectedValue, TransferControl.EnumPaymentScheme)
        oTC.ValidityDate = calValidityDate.Value
        'If (CType(oTC.PaymentType, enumPaymentType.PaymentType) = enumPaymentType.PaymentType.TOP) Then
        '    oTC.ValidityDate = calValidTo.Value
        'End If
        oTC.ValidTo = calValidTo.Value

        If 1 = 0 Then 'never happend, always edit (have ID)
            oTC.ID = oTCFac.Insert(oTC)
        Else

            If ViewState("IsNew") Then
                Dim strSQl As String = "()"


                Dim sTC As New SortCollection()
                Dim aTCs As New ArrayList

                Dim cTC As New CriteriaComposite(New Criteria(GetType(TOPTransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cTC.opAnd(New Criteria(GetType(TOPTransferControl), "CreditAccount", MatchType.Exact, oVCA.CreditAccount))
                cTC.opAnd(New Criteria(GetType(TOPTransferControl), "PaymentType", MatchType.Exact, ddlPaymentType.SelectedValue))

                aTCs = oTCFac.Retrieve(cTC)

                If Not IsNothing(aTCs) AndAlso aTCs.Count > 0 Then
                    MessageBox.Show("Data Sudah Ada ")
                    Return
                End If


                oTC.ID = oTCFac.Insert(oTC)

                MessageBox.Show("Simpan Berhasil")

                LoadCriteria()
                BindGrid()


            Else


                Dim sTC As New SortCollection()
                Dim aTCs As New ArrayList

                Dim cTC As New CriteriaComposite(New Criteria(GetType(TOPTransferControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cTC.opAnd(New Criteria(GetType(TOPTransferControl), "CreditAccount", MatchType.Exact, oVCA.CreditAccount))
                cTC.opAnd(New Criteria(GetType(TOPTransferControl), "PaymentType", MatchType.Exact, ddlPaymentType.SelectedValue))
                cTC.opAnd(New Criteria(GetType(TOPTransferControl), "ID", MatchType.No, txtID.Text))

                aTCs = oTCFac.Retrieve(cTC)

                If Not IsNothing(aTCs) AndAlso aTCs.Count > 0 Then
                    MessageBox.Show("Data Sudah Ada ")
                    Return

                End If


                oTC.ID = oTCFac.Update(oTC)

                MessageBox.Show("Simpan Berhasil")

                LoadCriteria()
                BindGrid()


            End If



            ViewState("IsNew") = True

            Exit Sub
            Dim oTCDB As TOPTransferControl = oTCFac.Retrieve(oTC.ID)

            If oTC.Status <> oTCDB.Status OrElse oTC.ValidFrom <> oTCDB.ValidFrom OrElse oTC.ValidityDate <> oTCDB.ValidityDate Then
                'validate date
                If oTC.ValidFrom <= DateSerial(Now.Year, Now.Month, Now.Day) Then
                    MessageBox.Show("Perubahan Tanggal Berlaku Tidak Valid! Harus Dilakukan Paling Lambat Di Bulan Sebelumnya.")
                    Exit Sub
                End If

                'validate to trans data
                'find POs with EffDate>=oTC.ValidFrom and POHeader.IsTransfer<>oTC.Status : jika ada maka JANGAN DISIMPAN
                Dim Sql As String = "select count(*) n from POHeader poh with (nolock) " & _
                    " where poh.RowStatus =0 and poh.Status in (0,2,4,6,8) " & _
                    " and poh.ReqAllocationDateTime>='" & oTC.ValidFrom.ToString("yyyy.MM.dd") & "' " & _
                    " and poh.IsTransfer<>" & oTC.Status.ToString()
                Sql = "(" & Sql & ")"

                Dim cD As New CriteriaComposite(New Criteria(GetType(Dealer), "ID", MatchType.Exact, 2))
                Dim aDs As ArrayList
                Dim oDFac As New DealerFacade(User)

                cD.opAnd(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, Sql))
                aDs = oDFac.Retrieve(cD)

                If aDs.Count = 0 Then
                    MessageBox.Show("Simpan Gagal. Sudah Ada Data PO di Periode Tersebut")
                    Exit Sub
                End If

                oTC.ID = 0
                oTC.ID = oTCFac.Insert(oTC)

                IsOk = oTC.ID > 0

                If IsOk Then
                    If oTC.ValidFrom = oTCDB.ValidFrom Then
                        'cancel the last status
                        oTCDB.RowStatus = -1
                        oTCDB.ID = oTCFac.Update(oTCDB)
                    End If

                    Me.calValidTo.Enabled = True
                    MessageBox.Show("Simpan Berhasil")

                    LoadCriteria()
                    BindGrid()
                Else
                    MessageBox.Show("Simpan Gagal")
                    LoadCriteria()
                End If
            Else ' No ChANGES
                LoadCriteria()
                BindGrid()
            End If

        End If

    End Sub
#End Region

#Region "Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ViewState("IsNew") = True
            checkPrivilege()
            initPage()
        End If
    End Sub

    Private Sub dtgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Select Case e.CommandName.Trim().ToLower()
            Case "Edit".ToLower()



                Dim lblID As Label = e.Item.FindControl("lblID")
                Dim ID As Integer = CType(lblID.Text, Integer)
                Dim oTC As TOPTransferControl = New TOPTransferControlFacade(User).Retrieve(ID)

                If Not IsNothing(oTC) AndAlso oTC.ID > 0 Then
                    ViewState("IsNew") = False
                    SaveCriteria()
                    Me.BindData(oTC)
                    Me.dtgMain.Visible = False
                End If
        End Select
    End Sub

    Private Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblID As Label = e.Item.FindControl("lblID")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblValidFrom As Label = e.Item.FindControl("lblValidFrom")
            Dim lblPaymentType As Label = e.Item.FindControl("lblPaymentType")
            Dim lblPaymentMethod As Label = e.Item.FindControl("lblPaymentMethod")
            Dim lblUpdatedTime As Label = e.Item.FindControl("lblUpdatedTime")
            Dim lblUpdatedBy As Label = e.Item.FindControl("lblUpdatedBy")
            Dim oTC As TOPTransferControl = e.Item.DataItem
            Dim lblValidTo As Label = e.Item.FindControl("lblValidTo")

            Dim lblDeskripsi As Label = e.Item.FindControl("lblDeskripsi")
            Dim lblValidity As Label = e.Item.FindControl("lblValidity")

            lblNo.Text = (e.Item.ItemIndex + 1)
            lblID.Text = oTC.ID

            lblCreditAccount.Text = oTC.CreditAccount
            lblPaymentType.Text = CType(oTC.PaymentType, enumPaymentType.PaymentType).ToString()

            lblDeskripsi.Text = oTC.CreditAccount & " - " & CType(oTC.PaymentType, enumPaymentType.PaymentType).ToString()

            lblPaymentMethod.Text = CType(oTC.Status, TransferControl.EnumPaymentScheme).ToString()
            lblValidFrom.Text = oTC.ValidFrom.ToString("dd MMM yyyy")

            If (CType(oTC.PaymentType, enumPaymentType.PaymentType) = enumPaymentType.PaymentType.TOP) Then
                If oTC.ValidityDate.Year > 2000 Then
                    lblValidity.Text = oTC.ValidityDate.ToString("dd MMM yyyy")
                Else
                    lblValidity.Visible = False
                End If
            Else
                lblValidity.Visible = False
            End If


            If oTC.ValidTo.Year > 2000 Then
                lblValidTo.Text = oTC.ValidTo.ToString("dd MMM yyyy")
            Else
                lblValidTo.Visible = False
            End If

            lblUpdatedTime.Text = oTC.LastUpdateTime.ToString("dd MMM yyyy HH:mm:ss")
            lblUpdatedBy.Text = oTC.LastUpdateBy

        End If
    End Sub


    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Me.BindGrid()
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        LoadCriteria()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        SaveDataNew()
    End Sub

#End Region
End Class