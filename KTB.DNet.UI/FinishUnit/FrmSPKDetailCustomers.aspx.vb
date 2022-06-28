#Region "Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
#End Region


#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.SAP
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports KTB.DNet.UI.Helper


#End Region

Public Class FrmSPKDetailCustomers
    Inherits System.Web.UI.Page



#Region " Private Variables"

    Private sessionHelper As New SessionHelper
    Private objSPKHeader As SPKHeader
    Private objSPKCustomer As SPKCustomer
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private mode As enumMode.Mode
    Private _vstSPKCustomer As String = "_vstSPKCustomer"
    Private _vstSPKHeader As String = "_vstSPKHeader"
    Private _strPrevString As String = "_strPrevString"
    Private _isMMC As Boolean
    Private emailMandatory As Boolean = False

#End Region

    Private Sub loadSPK()
        ViewState("SPKDMS") = False

        If Not Request.Item("spkHeader") Is Nothing AndAlso Request.Item("spkHeader").ToString() <> "" Then
            If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                ViewState.Add(Me._vstSPKHeader, Request.Item("spkHeader"))
            Else
                ViewState.Item(Me._vstSPKHeader) = Request.Item("spkHeader")
            End If
        Else
            If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                ViewState.Add(Me._vstSPKHeader, "FrmSPKHeader.SPKCustomer" & Now.ToString("yyyyMMddhhmmssfff"))
            Else
                ViewState.Item(Me._vstSPKHeader) = "FrmSPKHeader.SPKHeader" & Now.ToString("yyyyMMddhhmmssfff")
            End If
        End If
        objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
        If IsNothing(objSPKHeader) AndAlso Not IsNothing(Request.QueryString("Id")) AndAlso Request.QueryString("Id") <> "" AndAlso Request.QueryString("Id") <> "0" Then
            objSPKHeader = New SPKHeaderFacade(User).Retrieve(CInt(Request.QueryString("Id")))
            sessionHelper.SetSession(Me.ViewState.Item(Me._vstSPKHeader), objSPKHeader)
        End If


        Dim strPrevString As String = ""
        For Each Str As String In Request.QueryString.AllKeys
            If Str.ToLower() = "prof" Then
                ViewState("Prof") = "1"
                Continue For
            End If
            If strPrevString = "" Then
                strPrevString = String.Format("{0}={1}", Str, Request.QueryString(Str))
            Else
                strPrevString = strPrevString & String.Format("&{0}={1}", Str, Request.QueryString(Str))
            End If
        Next
        ViewState("Mode") = CType(Request.QueryString("Mode"), enumMode.Mode)
        ViewState(_strPrevString) = strPrevString

        Dim idx As Integer = CInt(Request.QueryString("spkDetailIdx"))
        lblDealer.Text = objSPKHeader.Dealer.DealerCode & " / " & objSPKHeader.Dealer.DealerName
        lblSPKDate.Text = objSPKHeader.DealerSPKDate.ToString("dd/MM/yyyy")
        ViewState("SPKDetailID") = CInt(Request.QueryString("SPKDetailID"))

        Dim objSPKD As New SPKDetail

        If Not IsNothing(ViewState("SPKDetailID")) AndAlso ViewState("SPKDetailID").ToString() <> "" Then
            objSPKD = New SPKDetailFacade(User).Retrieve(CInt(ViewState("SPKDetailID")))
        Else
            If Not IsNothing(idx) AndAlso idx <> 0 Then
                objSPKD = New SPKDetailFacade(User).Retrieve(idx)
                ViewState("SPKDetailID") = idx
            End If
        End If

        lblQtyDetail.Text = objSPKD.Quantity.ToString()

        lblVehcile.Text = objSPKD.VechileColor.MaterialDescription
        lblRegSPK.Text = objSPKHeader.SPKNumber
        If Not IsNothing(objSPKHeader.SalesmanHeader) Then
            lblSalesman.Text = objSPKHeader.SalesmanHeader.SalesmanCode & " / " & objSPKHeader.SalesmanHeader.Name
        End If

        Dim arr As ArrayList
        arr = New ArrayList

        If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
            dtgConsumentFaktur.ShowFooter = False
        End If

        If (objSPKHeader.Status >= EnumStatusSPK.Status.Tunggu_Unit Or objSPKHeader.Status = EnumStatusSPK.Status.Selesai) Then
            btnJadiKonsumen.Visible = True
        End If

        If objSPKD.RejectedReason <> "" Then
            dtgConsumentFaktur.ShowFooter = False
        End If

        Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objSPKHeader.Dealer.ID))
        Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
        Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
        For Each objDealerSystem As DealerSystems In arlDealerSystem
            If objDealerSystem.isSPKDNET Then
            Else
                If Not objSPKHeader Is Nothing Then
                    If Not CType(objSPKHeader.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
                        ViewState("SPKDMS") = True
                        dtgConsumentFaktur.ShowFooter = False
                    End If
                End If
            End If
        Next


        If objSPKHeader.ID > 0 Then
            objSPKHeader = New SPKHeaderFacade(User).Retrieve(objSPKHeader.ID)
            sessionHelper.SetSession(Me.ViewState.Item(Me._vstSPKHeader), objSPKHeader)
        End If

        If objSPKD.SPKDetailCustomers.Count > 0 Then
            dtgConsumentFaktur.DataSource = objSPKD.SPKDetailCustomers
            dtgConsumentFaktur.DataBind()
        Else
            dtgConsumentFaktur.DataSource = arr
            dtgConsumentFaktur.DataBind()
        End If

        If ViewState("SPKDMS") = True Then
            dtgConsumentFaktur.ShowFooter = False
        End If


    End Sub

    Private Sub LoadDetail()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadSPK()
        End If
    End Sub

    Private Sub dtgConsumentFaktur_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgConsumentFaktur.ItemCommand
        Select Case (e.CommandName)
            Case "AddConsumentFaktur"
                Dim spkD As SPKDetail = New SPKDetailFacade(User).Retrieve(CInt(ViewState("SPKDetailID")))
                Dim SPKCQty As Integer = 0
                If spkD.SPKDetailCustomers.Count > 0 Then
                    For Each dr As SPKDetailCustomer In spkD.SPKDetailCustomers
                        SPKCQty = SPKCQty + dr.Quantity
                    Next
                    If Not (spkD.Quantity > SPKCQty) Then
                        MessageBox.Show("Quantity Sudah Habis Terpakai")
                        Return
                        Exit Sub
                    End If
                End If
                AddConsumentFaktur(e)
            Case "Detail"
                ViewConsumentFaktur(e)
            Case "Edit"
                EditConsumentFaktur(e)
            Case "Delete"
                Dim lShouldReturn As Boolean
                DeleteonsumentFaktur(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If
            Case "vProfile"
                VehicleProfileFaktur(e)
        End Select
    End Sub

    Private Sub dtgConsumentFaktur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgConsumentFaktur.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblConsumentName As Label = e.Item.FindControl("lblConsumentName")
            Dim lblConsumentAddress As Label = e.Item.FindControl("lblConsumentAddress")
            Dim lblQty As Label = e.Item.FindControl("lblQty")
            Dim chkKonsumen As CheckBox = e.Item.FindControl("chkKonsumen")
            Dim imgVProfile As HtmlImage = e.Item.FindControl("imgVProfile")

            If Not IsNothing(e.Item.DataItem) Then
                Dim obj As SPKDetailCustomer = e.Item.DataItem
                lblConsumentName.Text = obj.Name1
                lblConsumentAddress.Text = obj.Alamat
                lblQty.Text = obj.Quantity.ToString()
                If Not IsNothing(obj.CustomerRequest) AndAlso obj.CustomerRequest.ID > 0 Then
                    chkKonsumen.Checked = True
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False

                End If
                chkKonsumen.Enabled = False

                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")


                If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
                    imgVProfile.Src = "../images/detail.gif"
                    imgVProfile.Alt = "Daftar Profile Faktur Kendaraan"

                End If

                Dim arrKendaraan As New ArrayList

                Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, obj.ID))
                arrKendaraan = New SPKProfileFacade(User).Retrieve(criterias)


                If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode AndAlso arrKendaraan.Count > 0 Then
                    imgVProfile.Src = "../images/dok.gif"
                    imgVProfile.Alt = "Daftar Profile Faktur Kendaraan"
                End If


                If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode AndAlso arrKendaraan.Count = 0 Then
                    imgVProfile.Src = "../images/add.gif"
                    imgVProfile.Alt = "Tambah Profile Faktur Kendaraan"
                End If

                If CBool(ViewState("SPKDMS")) = True Then
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
                End If


            End If
        End If
    End Sub

    Protected Sub btnJadiKonsumen_Click(sender As Object, e As EventArgs) Handles btnJadiKonsumen.Click
        Dim oDataGridItem As DataGridItem
        Dim chkSelect As System.Web.UI.WebControls.CheckBox
        Dim chkKonsumen As System.Web.UI.WebControls.CheckBox
        Dim arrListTrue As New System.Collections.ArrayList
        Dim arlCustRequest As New System.Collections.ArrayList
        Dim arrListFalse As New System.Collections.ArrayList
        Dim objSPKHeaderFacade As New SPKHeaderFacade(User)
        Dim iFalse As Integer = 0
        Dim i As Integer = 0

        Dim AutoCustomerStatus As Boolean = False
        'cek transactionControl
        Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(sessionHelper.GetSession("DEALER").ID, CInt(EnumDealerTransType.DealerTransKind.AutoCustomer).ToString)
        If Not (objTransaction Is Nothing) Then
            If objTransaction.Status = 1 Then
                AutoCustomerStatus = True
            End If
        End If

        objUserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")

        Dim _spk As New SPKHeader
        _spk = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
        If IsNothing(objSPKHeader) AndAlso Not IsNothing(Request.QueryString("Id")) AndAlso Request.QueryString("Id") <> "" AndAlso Request.QueryString("Id") <> "0" Then
            _spk = New SPKHeaderFacade(User).Retrieve(CInt(Request.QueryString("Id")))
        End If
        '_spk.ID = oDataGridItem.Cells(1).Text
        For Each oDataGridItem In dtgConsumentFaktur.Items
            chkSelect = oDataGridItem.FindControl("chkSelect")
            Dim oSPKDetailCustomer As SPKDetailCustomer = New SPKDetailCustomer
            If chkSelect.Checked Then
                i = i + 1
                oSPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(CType(oDataGridItem.Cells(0).Text, Integer))
                Dim isSPKDValid As Boolean = False
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKProfile), "SPKDetailCustomer.ID", MatchType.Exact, oSPKDetailCustomer.ID))
                Dim arrSPKProfile As ArrayList = New SPKProfileFacade(User).Retrieve(criterias)

                If arrSPKProfile.Count = 0 Then
                    isSPKDValid = False
                End If
                If IsNothing(oSPKDetailCustomer.CustomerRequest) OrElse oSPKDetailCustomer.CustomerRequest.ID < 1 Then
                    isSPKDValid = True
                End If

                If (isSPKDValid) Then
                    '_spk = objSPKHeaderFacade.Retrieve(_spk.ID)
                    arrListTrue.Add(oSPKDetailCustomer)
                End If
            End If
        Next

        If arrListTrue.Count = 0 Then
            MessageBox.Show("Tidak ada konsumen faktur yang dipilih, Profile kendaraan belum diisi, atau semua konsumen faktur telah jadi konsumen")
            Exit Sub
        End If
        Dim msg As String = String.Empty
        msg = "- " & EnumStatusSPK.GetStringValueStatus(CInt(EnumStatusSPK.Status.Tunggu_Unit))
        If i = iFalse Then
            MessageBox.Show("Proses jadi konsumen gagal, syarat status jadi konsumen : \n " & msg & " \n & Mengisi Detail Konsumen Faktur")
            Exit Sub
        End If

        Dim UpdateMsg As String = String.Empty
        Dim _fileHelper As New FileHelper
        If arrListTrue.Count > 0 Then
            'For Each _header As SPKHeader In arrListTrue
            arlCustRequest = _fileHelper.SendToCustomerRequest(objDealer, objUserInfo, AutoCustomerStatus, UpdateMsg, arrListTrue)
            'Next

            If UpdateMsg <> String.Empty Then
                MessageBox.Show(UpdateMsg)
            End If

            '--------------------------------------------------------------------
            If arlCustRequest.Count > 0 Then
                'by pass to transfer process to SAP if status = validasi and transaction control = aktif
                If CType(sessionHelper.GetSession("AutoCustomerStatus"), Boolean) = True Then
                    _fileHelper.TransferProcess(arlCustRequest, objUserInfo, UpdateMsg)

                    If UpdateMsg <> String.Empty Then
                        MessageBox.Show(UpdateMsg)
                    End If
                End If
            End If
            '--------------------------------------------------------------------

            loadSPK()
        End If

        If iFalse > 0 And iFalse < i Then
            MessageBox.Show("Sebagian proses jadi konsumen gagal, syarat status jadi konsumen : \n " & msg)
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim strPrevString As String = ViewState(_strPrevString)
        If Not IsNothing(ViewState("Prof")) AndAlso ViewState("Prof").ToString() = "1" Then
            Response.Redirect("FrmSPKHeaderProfile.aspx?" & strPrevString & "&FromPage=FrmSPKDetailCustomers")
        Else
            Response.Redirect("FrmSPKHeader.aspx?" & strPrevString)
        End If
    End Sub

    Private Sub AddConsumentFaktur(ByVal e As DataGridCommandEventArgs)
        Dim _SpkH As SPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        If Not IsNothing(_SpkH) AndAlso Not IsNothing(_SpkH.SPKCustomer) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            'SalesmenInfo()
            mode = ViewState("Mode")
            Dim strPrevString As String = ViewState(_strPrevString)
            Dim strspkDetailCustomer = "spkDetailCustomer" & Guid.NewGuid().ToString()

          
          

            Dim objSPKDC As New SPKDetailCustomer

            If Not IsNothing(objSPKHeader) Then
                If objSPKHeader.SPKDetails.Count > 0 Then
                    Dim objSPKD As SPKDetail = New SPKDetailFacade(User).Retrieve(CInt(ViewState("SPKDetailID")))
                    objSPKDC.SPKDetail = objSPKD
                    sessionHelper.SetSession(strspkDetailCustomer, objSPKDC)

                    Dim varCustID As Integer = 0
                    If Not IsNothing(objSPKHeader.SPKCustomer) AndAlso Not IsNothing(objSPKHeader.SPKCustomer.SAPCustomer) Then
                        varCustID = objSPKHeader.SPKCustomer.SAPCustomer.ID
                    End If


                    If 1 = 1 Then
                        If varCustID > 0 Then
                            Response.Redirect("FrmSPKDetailCustomer.aspx?" & strPrevString & String.Format("&SPKDCID={0}&CustId={1}&spkDetailCustomer={2}", 0, varCustID.ToString(), strspkDetailCustomer))
                        Else
                            Response.Redirect("FrmSPKDetailCustomer.aspx?" & strPrevString & String.Format("&SPKDCID={0}&spkDetailCustomer={1}", 0, strspkDetailCustomer))
                        End If

                    End If
                Else
                    lblError.Text = "Tentukan kendaraan yang akan di pesan"
                End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan -"
            End If

        Else

            lblError.Text = "Error : SIlahkan Mengisi Data Konsumen & Kendaraan"
        End If
    End Sub

    Private Sub ViewConsumentFaktur(ByVal e As DataGridCommandEventArgs)
        Dim _SpkH As SPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        If Not IsNothing(_SpkH) AndAlso Not IsNothing(_SpkH.SPKCustomer) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            'SalesmenInfo()
            mode = ViewState("Mode")
            Dim strPrevString As String = ViewState(_strPrevString)
            Dim strspkDetailCustomer = "spkDetailCustomer" & Guid.NewGuid().ToString()

            Dim objSPKDC As New SPKDetailCustomer

            If Not IsNothing(objSPKHeader) Then
                If objSPKHeader.SPKDetails.Count > 0 Then
                    Dim objSPKD As SPKDetail = New SPKDetailFacade(User).Retrieve(CInt(ViewState("SPKDetailID")))
                    objSPKDC.SPKDetail = objSPKD

                    Dim objDC As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                    sessionHelper.SetSession(strspkDetailCustomer, objDC)
                    If 1 = 1 Then
                        Response.Redirect("FrmSPKDetailCustomer.aspx?" & strPrevString & String.Format("&SPKDCID={0}&spkDetailCustomer={1}", objDC.ID.ToString(), strspkDetailCustomer))
                    End If
                Else
                    lblError.Text = "Tentukan kendaraan yang akan di pesan"
                End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan -"
            End If

        Else

            lblError.Text = "Error : SIlahkan Mengisi Data Konsumen & Kendaraan"
        End If
    End Sub

    Private Sub EditConsumentFaktur(ByVal e As DataGridCommandEventArgs)
        Dim _SpkH As SPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        If Not IsNothing(_SpkH) AndAlso Not IsNothing(_SpkH.SPKCustomer) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            'SalesmenInfo()
            mode = ViewState("Mode")
            Dim strPrevString As String = ViewState(_strPrevString)
            Dim strspkDetailCustomer = "spkDetailCustomer" & Guid.NewGuid().ToString()

            Dim objSPKDC As New SPKDetailCustomer

            If Not IsNothing(objSPKHeader) Then
                If objSPKHeader.SPKDetails.Count > 0 Then
                    Dim objSPKD As SPKDetail = New SPKDetailFacade(User).Retrieve(CInt(ViewState("SPKDetailID")))
                    objSPKDC.SPKDetail = objSPKD
                    Dim objDC As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                    sessionHelper.SetSession(strspkDetailCustomer, objDC)
                    If 1 = 1 Then
                        Response.Redirect("FrmSPKDetailCustomer.aspx?" & strPrevString & String.Format("&SPKDCID={0}&spkDetailCustomer={1}", objDC.ID.ToString(), strspkDetailCustomer))
                    End If
                Else
                    lblError.Text = "Tentukan kendaraan yang akan di pesan"
                End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan -"
            End If

        Else

            lblError.Text = "Error : SIlahkan Mengisi Data Konsumen & Kendaraan"
        End If
    End Sub

    Private Sub DeleteonsumentFaktur(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False
        Try
            Dim varID As Integer = CInt(e.Item.Cells(0).Text)
            Dim varSPKDC As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(varID)
            If Not IsNothing(varSPKDC) AndAlso (IsNothing(varSPKDC.CustomerRequest) OrElse varSPKDC.CustomerRequest.ID = 0) Then
                Dim fa As SPKDetailCustomerFacade = New SPKDetailCustomerFacade(User)
                fa.Delete(varSPKDC)
                MessageBox.Show(SR.DataDeleted(varSPKDC.Name1))
                Response.Redirect("FrmSPKDetailCustomers.aspx?" & ViewState(_strPrevString).ToString() & "&Prof=1")

            End If
        Catch ex As Exception
            shouldReturn = True
            lblError.Text = ex.Message.ToString()
            Exit Sub
        End Try

    End Sub

    Private Sub VehicleProfileFaktur(ByVal e As DataGridCommandEventArgs)
        objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
        mode = ViewState("Mode")

        Dim varID As Integer = CInt(e.Item.Cells(0).Text)
        Dim varSPKDC As SPKDetailCustomer = New SPKDetailCustomerFacade(User).Retrieve(varID)
        If Not IsNothing(varSPKDC) Then
            Dim strPrevString As String = ViewState(_strPrevString)
            If Not IsNothing(objSPKHeader) Then
                If objSPKHeader.SPKDetails.Count > 0 Then
                    'If Mode = enumMode.Mode.NewItemMode Then
                    '    Response.Redirect("FrmSPKMasterProfile.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & viewstate.Item(Me._vstSPKHeader))
                    'Else
                    If strPrevString.ToLower().Contains("id=") Then
                        Response.Redirect("FrmSPKMasterProfile.aspx?" & strPrevString & String.Format("&SPKDCID={0}", varSPKDC.ID.ToString()) & "&FromPage=FrmSPKDetailCustomers")
                    Else
                        Response.Redirect("FrmSPKMasterProfile.aspx?" & strPrevString & String.Format("&SPKDCID={0}&Id={1}", varSPKDC.ID.ToString(), objSPKHeader.ID) & "&FromPage=FrmSPKDetailCustomers")
                    End If
                Else
                    lblError.Text = "Tentukan kendaraan yang akan di pesan"
                End If
            Else
                lblError.Text = "Tentukan kendaraan yang akan di pesan -"
            End If
        End If

    End Sub
End Class