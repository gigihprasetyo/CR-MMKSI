#Region " Summary "
'--------------------------------------------------------'
'-- Program Code : FrmInvoiceReqList.aspx              --'
'-- Program Name : Daftar Permohonan Faktur Kendaraan  --'
'-- Description  :                                     --'
'--------------------------------------------------------'
'-- Programmer   : Agus Pirnadi                        --'
'-- Start Date   : Nov 09 2005                         --'
'-- Update By    :                                     --'
'-- Last Update  : Jan 02 2005                         --'
'--------------------------------------------------------'
'-- Copyright ? 2005 by Intimedia                      --'
'--------------------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

Public Class FrmInvoiceReqList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesOrg As System.Web.UI.WebControls.Label
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesOrg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgInvoiceList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearch1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents cbDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icToday As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtColorRed As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkIsTemporary As CheckBox

    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private _sessCMStatus As String = "FrmInvoiceReqList._sessCMStatus"
#End Region

#Region " Custom Method "

    Private Sub DisplayDealer()
        '-- Display dealer info from login session "DEALER"

        If Not IsNothing(Session("DEALER")) Then
            Dim _Dealer As Dealer = CType(Session("DEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            lblSearch1.Text = _Dealer.SearchTerm1
            lblDealerName.Text = _Dealer.DealerName
            lblDealerCity.Text = _Dealer.City.CityName
        End If

    End Sub

    Private Sub BindDropdownList()

        '-- Bind Category dropdownlist
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))

        Dim cat As String = ""

        If _PCAccessAllowed Then
            cat = cat & "'PC',"
        End If
        If _CVAccessAllowed Then
            cat = cat & "'CV',"
        End If
        If _LCVAccessAllowed Then
            cat = cat & "'LCV',"
        End If

        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If

        ddlSalesOrg.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlSalesOrg.DataTextField = "CategoryCode"
        ddlSalesOrg.DataValueField = "CategoryCode"
        ddlSalesOrg.DataBind()
        ddlSalesOrg.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        '-- Bind Status dropdownlist
        Dim StatusItem0 As ListItem = New ListItem("Pilih", -1)
        Dim StatusItem1 As ListItem = New ListItem(EnumChassisMaster.FakturStatusDesc(EnumChassisMaster.FakturStatus.Baru), EnumChassisMaster.FakturStatus.Baru)
        Dim StatusItem2 As ListItem = New ListItem(EnumChassisMaster.FakturStatusDesc(EnumChassisMaster.FakturStatus.Validasi), EnumChassisMaster.FakturStatus.Validasi)

        ddlStatus.Items.Add(StatusItem0)
        ddlStatus.Items.Add(StatusItem1)
        ddlStatus.Items.Add(StatusItem2)
        ddlStatus.ClearSelection()
        ddlSalesOrg.ClearSelection()
    End Sub

    Private Function PopulateInvoice() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim _id As Integer

        For Each oDataGridItem In dgInvoiceList.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            Dim lblID As Label = CType(oDataGridItem.FindControl("lblID"), Label)
            If chkExport.Checked AndAlso oDataGridItem.BackColor.Equals(Me.txtColorRed.BackColor) = False Then
                _id = lblID.Text.Trim
                If _id > 0 Then
                    Dim _chsMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(_id)
                    'Dim arrListInvoice As ArrayList = CType(sessHelp.GetSession("InvoiceReqList"), ArrayList)
                    'oExArgs.Add(arrListInvoice(oDataGridItem.ItemIndex))
                    oExArgs.Add(_chsMaster)
                End If
            End If
        Next
        Return oExArgs
    End Function

    Private Sub WriteInvoiceData(ByRef sw As StreamWriter)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file

        'InvoiceLine.Remove(0, InvoiceLine.Length)
        InvoiceLine.Append("Chassis Number" & tab) 'ChassisNumber    
        InvoiceLine.Append("Tgl. Faktur" & tab) 'TglFaktur    
        InvoiceLine.Append("No. Rangka Pengganti" & tab) 'NoRangkaPengganti        
        InvoiceLine.Append("Nama_1" & tab) 'Nama_1    
        InvoiceLine.Append("Nama_2" & tab) 'Nama_2    
        InvoiceLine.Append("Nama_3" & tab) 'Nama_3    
        InvoiceLine.Append("Alamat" & tab) 'Alamat    
        InvoiceLine.Append("Kelurahan" & tab) 'Kelurahan    
        InvoiceLine.Append("Kecamatan" & tab) 'Kecamatan    
        InvoiceLine.Append("Kode Pos" & tab) 'KodePos    
        InvoiceLine.Append("Pre Area" & tab) 'PreArea    
        InvoiceLine.Append("Kota" & tab) 'Kota    
        InvoiceLine.Append("Propinsi" & tab) 'Propinsi    
        InvoiceLine.Append("Cetak" & tab) 'Cetak    
        InvoiceLine.Append("Email" & tab) 'Email    
        InvoiceLine.Append("Phone" & tab) 'Phone    
        InvoiceLine.Append("Tipe Pembayaran Plgrn Wil" & tab) 'TipePembayaran Plgrn Wil    
        InvoiceLine.Append("Jmlh Plgrn Wil   " & tab) 'Jmlh Plgrn Wil    
        InvoiceLine.Append("Nama Bank Plgrn Wil  " & tab) 'Nama Bank Plgrn Wil    
        InvoiceLine.Append("No.Gyro Plgrn Wil    " & tab) 'No.Gyro Plgrn Wil    
        InvoiceLine.Append("Tipe Pembayaran Penalti    " & tab) 'Tipe Pembayaran Penalti    
        InvoiceLine.Append("Jumlah Penalti" & tab) 'Jumlah Penalti    
        InvoiceLine.Append("Nama Bank Penalti" & tab) 'Nama Bank Penalti    
        InvoiceLine.Append("No. Gyro Penalti" & tab) 'No. Gyro Penalti    
        InvoiceLine.Append("No. Surat" & tab) 'No. Surat    
        InvoiceLine.Append("Dibuat Oleh" & tab) 'Dibuat Oleh    
        InvoiceLine.Append("Tgl Buat" & tab) 'Tgl Buat    
        InvoiceLine.Append("Divalidasi Oleh" & tab) 'Divalidasi Oleh    
        InvoiceLine.Append("Tgl Validasi" & tab) 'Tgl Validasi    
        InvoiceLine.Append("Nomor Faktur" & tab) 'Nomor Faktur    
        InvoiceLine.Append("Nomor SPK " & tab) 'Nomor SPK     
        InvoiceLine.Append("No.KTP/TDP" & tab) 'No.KTP/TDP     
        InvoiceLine.Append("Jenis" & tab) 'Jenis            
        InvoiceLine.Append("Model" & tab) 'Model   
        InvoiceLine.Append("SalesCode" & tab) 'SalesCode
        sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Dim InvoiceReqList As ArrayList = PopulateInvoice()

        For Each objInvoice As ChassisMaster In InvoiceReqList
            Try
                InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line

                InvoiceLine.Append(objInvoice.ChassisNumber & tab)  '-- Chassis number

                If Not IsNothing(objInvoice.EndCustomer) Then

                    InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "dd/MM/yyyy") & tab)  '-- Faktur date

                    Dim objRefChassisMaster As ChassisMaster
                    objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
                    If objRefChassisMaster Is Nothing Then
                        InvoiceLine.Append(tab)  '-- Empty column
                    Else
                        InvoiceLine.Append(objRefChassisMaster.ChassisNumber & tab)   '-- Ref chassis number
                    End If

                    'InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name1 & tab)    '-- Name1
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name2 & tab)   '-- Name2
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name3 & tab)   '-- Name3
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Alamat & tab)  '-- Alamat
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Kelurahan & tab)   '-- Kelurahan
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Kecamatan & tab)   '-- Kecamatan
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.PostalCode & tab)  '-- Kode Pos
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.PreArea & tab)  '-- Pre area

                    If Not IsNothing(objInvoice.EndCustomer.Customer.City) Then
                        InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.CityName & tab)  '-- City name

                        If Not IsNothing(objInvoice.EndCustomer.Customer.City.Province) Then
                            InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.Province.ProvinceName & tab)  '-- Province name
                        Else
                            InvoiceLine.Append(tab)
                        End If
                    Else
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                    End If

                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.PrintRegion & tab)  '-- Cetak mark?
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.Email & tab)  '-- Email
                    InvoiceLine.Append(objInvoice.EndCustomer.Customer.PhoneNo & tab)  '-- Phone

                    'If objInvoice.Category.CategoryCode = "CV" Or _
                    '   objInvoice.Category.CategoryCode = "LCV" Then

                    '    If Not IsNothing(objInvoice.EndCustomer.MainOperationArea) Then
                    '        InvoiceLine.Append(objInvoice.EndCustomer.MainOperationArea.Code & tab)  '-- Daerah utama operasi
                    '    Else
                    '        InvoiceLine.Append(tab)
                    '    End If
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If objInvoice.Category.CategoryCode = "PC" Then
                    '    If Not IsNothing(objInvoice.EndCustomer.MainUsage) Then
                    '        InvoiceLine.Append(objInvoice.EndCustomer.MainUsage.Code & tab)  '-- Penggunaan kendaraan
                    '    Else
                    '        InvoiceLine.Append(tab)
                    '    End If
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If Not IsNothing(objInvoice.EndCustomer.VehicleOwnership) Then
                    '    InvoiceLine.Append(objInvoice.EndCustomer.VehicleOwnership.Code & tab)  '-- Kepemilikan kendaraan
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If Not IsNothing(objInvoice.EndCustomer.VehiclePurpose) Then
                    '    InvoiceLine.Append(objInvoice.EndCustomer.VehiclePurpose.Code & tab)  '-- Kendaraan sebagai
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If objInvoice.Category.CategoryCode = "CV" Then  '-- (first column)
                    '    If Not IsNothing(objInvoice.EndCustomer.VehicleBodyShape) Then
                    '        InvoiceLine.Append(objInvoice.EndCustomer.VehicleBodyShape.Code & tab)  '-- Bentuk body CV
                    '    Else
                    '        InvoiceLine.Append(tab)
                    '    End If
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If objInvoice.Category.CategoryCode = "LCV" Then  '-- (Second column)
                    '    If Not IsNothing(objInvoice.EndCustomer.VehicleBodyShape) Then
                    '        InvoiceLine.Append(objInvoice.EndCustomer.VehicleBodyShape.Code & tab)  '-- Bentuk body LCV
                    '    Else
                    '        InvoiceLine.Append(tab)
                    '    End If
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If objInvoice.Category.CategoryCode = "CV" Or _
                    '   objInvoice.Category.CategoryCode = "LCV" Then
                    '    If Not IsNothing(objInvoice.EndCustomer.CustomerBusiness) Then
                    '        InvoiceLine.Append(objInvoice.EndCustomer.CustomerBusiness.Code & tab)  '-- Usaha konsumen
                    '    Else
                    '        InvoiceLine.Append(tab)
                    '    End If
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If Not IsNothing(objInvoice.EndCustomer.PaymentType) Then
                    '    InvoiceLine.Append(objInvoice.EndCustomer.PaymentType.Code & tab)  '-- Cara pembayaran
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    'If objInvoice.Category.CategoryCode = "PC" Then
                    '    If Not IsNothing(objInvoice.EndCustomer.OwnerAge) Then
                    '        InvoiceLine.Append(objInvoice.EndCustomer.OwnerAge.Code & tab)  '-- Usia pemilik
                    '    Else
                    '        InvoiceLine.Append(tab)
                    '    End If
                    'Else
                    '    InvoiceLine.Append(tab)
                    'End If

                    If UCase(objInvoice.EndCustomer.AreaViolationFlag) = "X" Then
                        Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                        Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(objInvoice.EndCustomer.AreaViolationPaymentMethodID)
                        If Not IsNothing(objAreaVioPatMeth) Then
                            InvoiceLine.Append(objAreaVioPatMeth.Code & tab)  '-- Wilayah TOP
                        Else
                            InvoiceLine.Append(tab)
                        End If
                        InvoiceLine.Append(Format(objInvoice.EndCustomer.AreaViolationyAmount, "0") & tab)  '-- Wilayah amount
                        InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationBankName & tab)  '-- Wilayah bank name
                        InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationGyroNumber & tab)  '-- Wilayah giro#
                    Else
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                    End If

                    If UCase(objInvoice.EndCustomer.PenaltyFlag) = "X" Then
                        Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                        Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(objInvoice.EndCustomer.PenaltyPaymentMethodID)
                        If Not IsNothing(objPenaltyPatMeth) Then
                            InvoiceLine.Append(objPenaltyPatMeth.Code & tab)  '-- Disc TOP
                        Else
                            InvoiceLine.Append(tab)
                        End If
                        InvoiceLine.Append(Format(objInvoice.EndCustomer.PenaltyAmount, "0") & tab) '-- Disc amount
                        InvoiceLine.Append(objInvoice.EndCustomer.PenaltyBankName & tab)  '-- Disc bank name
                        InvoiceLine.Append(objInvoice.EndCustomer.PenaltyGyroNumber & tab)  '-- Disc giro#
                    Else
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                        InvoiceLine.Append(tab)
                    End If

                    If UCase(objInvoice.EndCustomer.ReferenceLetterFlag) = "X" Then
                        InvoiceLine.Append(objInvoice.EndCustomer.ReferenceLetter & tab)  '-- Letter
                    Else
                        InvoiceLine.Append(tab)
                    End If

                    InvoiceLine.Append(IIf(objInvoice.EndCustomer.SaveBy <> "", UserInfo.Convert(objInvoice.EndCustomer.SaveBy), "") & tab)  '-- Dibuat oleh
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "dd/MM/yyyy") & tab)  '-- Tgl dibuat
                    InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy), "") & tab)  '-- Divalidasi oleh
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "dd/MM/yyyy") & tab)   '-- Tgl divalidasi
                    'added req by angga 20120627
                    InvoiceLine.Append(objInvoice.EndCustomer.FakturNumber & tab)  '-- No Faktur
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SPKNumber & tab) 'Nomor SPK   

                    Dim oCRP As CustomerRequestProfile = objInvoice.EndCustomer.Customer.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")
                    If Not IsNothing(oCRP) AndAlso oCRP.ID > 0 Then
                        InvoiceLine.Append(oCRP.ProfileValue.ToString & tab) 'No.KTP/TDP               
                    End If

                    Dim cmFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
                    Dim cmpJenis As ChassisMasterProfile = cmFacade.GetChassisMasterProfile(objInvoice.EndCustomer.ChassisMaster, 43) 'Jenis
                    Dim cmpModel As ChassisMasterProfile = cmFacade.GetChassisMasterProfile(objInvoice.EndCustomer.ChassisMaster, 44) 'Model

                    Dim vkgFacade As VehicleKindGroupFacade = New VehicleKindGroupFacade(User)
                    If Not IsNothing(cmpJenis) AndAlso cmpJenis.ID > 0 Then
                        Dim objVKGJenis As VehicleKindGroup = vkgFacade.RetrieveByCode(cmpJenis.ProfileValue)
                        If Not IsNothing(objVKGJenis) AndAlso objVKGJenis.ID > 0 Then
                            InvoiceLine.Append(objVKGJenis.Description.ToString & tab) 'Jenis         
                        Else
                            InvoiceLine.Append(String.Empty & tab) 'Jenis         
                        End If
                    End If
                    Dim vkFacade As VehicleKindFacade = New VehicleKindFacade(User)
                    If Not IsNothing(cmpModel) AndAlso cmpModel.ID > 0 Then
                        Dim objVKModel As VehicleKind = vkFacade.RetrieveByCode(cmpModel.ProfileValue)
                        If Not IsNothing(objVKModel) AndAlso objVKModel.ID > 0 Then
                            InvoiceLine.Append(objVKModel.Description.ToString & tab) 'Jenis         
                        Else
                            InvoiceLine.Append(String.Empty & tab) 'Jenis         
                        End If
                    End If
                    If Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) Then
                        InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.SalesmanCode & tab) 'SalesCode
                    Else
                        InvoiceLine.Append(String.Empty & tab) 'Jenis         
                    End If
                End If
                sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            End Try

        Next

    End Sub

#End Region

#Region " EventHandler "

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.FakturKendaraanViewList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR - Daftar Permohonan Faktur Kendaraan")
        End If
        Me.btnDnLoad.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanDownloadList_Privilege)
        Me.dgInvoiceList.Columns(9).Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanPangajuanBuat_Privilege)

        _PCAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege)
        _CVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege)
        _LCVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege)

        If (Not _PCAccessAllowed) And (Not _CVAccessAllowed) And (Not _LCVAccessAllowed) Then
            Me.btnSearch.Visible = False
            Me.ddlSalesOrg.Visible = False
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        btnDnLoad.Attributes.Add("onclick", "MakeValid();")
        btnSearch.Attributes.Add("onclick", "MakeValid();")

        If Not IsPostBack Then
            If Request.QueryString("Mode") = "New" Then
                Session("CriteriaFormInvoiceReqList") = Nothing
            End If
            DisplayDealer()  '-- Display dealer from login
            BindDropdownList()  '-- Init dropdownlist

            '-- Init sorting column and sort direction default
            ViewState("currSortColumn") = "ChassisNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            '-- Restore selection criteria
            Dim crit As Hashtable
            crit = CType(Session("CriteriaFormInvoiceReqList"), Hashtable)
            ddlStatus.ClearSelection()
            ddlSalesOrg.ClearSelection()
            If Not crit Is Nothing Then
                txtChassisNo.Text = CStr(crit.Item("ChassisNumber"))
                ddlStatus.SelectedValue = CStr(crit.Item("Status"))
                ddlSalesOrg.SelectedValue = CStr(crit.Item("Category"))
                cbDate.Checked = CBool(crit.Item("CBDate"))
                ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
                ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
                icToday.Value = CDate(crit.Item("icToday"))
                dgInvoiceList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
            End If

            txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            ReadData()   '-- Read all data matching criteria
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        '-- Search data based on criteria selected

        '-- Store selection criteria for later restore


        ReadData()
        dgInvoiceList.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("ChassisNumber", txtChassisNo.Text)     '-- Nomor chassis
        crit.Add("Status", ddlStatus.SelectedValue)      '-- Status
        crit.Add("Category", ddlSalesOrg.SelectedValue)  '-- Kategori
        crit.Add("PageIndex", dgInvoiceList.CurrentPageIndex)  '-- Kategori
        crit.Add("CBDate", cbDate.Checked)    '-- Kategori
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))
        crit.Add("icToday", icToday.Value)
        Session("CriteriaFormInvoiceReqList") = crit  '-- Store in session

    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Logged-in Dealer
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text))

        '-- Nomor chassis (LIKE '%%')
        If txtChassisNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.[Partial], txtChassisNo.Text.Trim()))
        End If

        '-- Sales org
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        If ddlSalesOrg.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.CategoryCode", MatchType.Exact, ddlSalesOrg.SelectedValue))
        Else
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        End If

        '-- Status
        If ddlStatus.SelectedValue = -1 Then  '-- Status: ''=Undefined '0'=Baru or '1'=Validasi
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.InSet, "('', '0','1')"))
        Else
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If cbDate.Checked Then
            Dim sDate As DateTime = New Date(icToday.Value.Year, icToday.Value.Month, icToday.Value.Day, 0, 0, 0)
            Dim eDate As DateTime = New Date(icToday.Value.Year, icToday.Value.Month, icToday.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.SaveTime", MatchType.GreaterOrEqual, sDate))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.SaveTime", MatchType.LesserOrEqual, eDate))
        End If


        If txtDealerBranchCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Replace(";", "','") & "')"))
        End If

        ' -- Temposrary Faktur
        If chkIsTemporary.Checked Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.IsTemporary", MatchType.Exact, CType(CInt(Int(chkIsTemporary.Checked)), Short)))
        End If

        'Validate : Cek apakah CM tidak diretur
        'criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Lesser, ChassisMaster.queryValidToCreateFaktur("ChassisMaster.ID")))

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(ChassisMaster), "ChassisNumber", Sort.SortDirection.ASC))  '-- Nomor chassis
        sortColl.Add(New Sort(GetType(ChassisMaster), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))  '-- Nomor chassis

        '-- Retrieve recordset
        Dim InvoiceReqList As ArrayList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, sortColl)


        '-- Store InvoiceReqList into session for later use
        sessHelp.SetSession("InvoiceReqList", InvoiceReqList)

        If InvoiceReqList.Count > 0 Then
            '-- Enable all buttons if any record exists
            btnDnLoad.Enabled = True
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If

            '-- Disable all buttons
            btnDnLoad.Enabled = False
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim InvoiceReqList As ArrayList = CType(sessHelp.GetSession("InvoiceReqList"), ArrayList)
        Dim aStatus As New ArrayList
        If InvoiceReqList.Count <> 0 Then
            SortListControl(InvoiceReqList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceReqList, pageIndex, dgInvoiceList.PageSize)
            For Each oCM As ChassisMaster In PagedList
                aStatus.Add(oCM.isValidToCreateFaktur())
            Next
            Me.sessHelp.SetSession(Me._sessCMStatus, aStatus)
            dgInvoiceList.DataSource = PagedList
            dgInvoiceList.VirtualItemCount = InvoiceReqList.Count()
            dgInvoiceList.DataBind()
        Else
            Me.sessHelp.SetSession(Me._sessCMStatus, aStatus)
            dgInvoiceList.DataSource = New ArrayList
            dgInvoiceList.VirtualItemCount = 0
            dgInvoiceList.CurrentPageIndex = 0
            dgInvoiceList.DataBind()
        End If
    End Sub

    Private Sub dgInvoiceList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceList.ItemCommand
        If e.CommandName = "lnkDetail" Then
            Dim lblID As Label = e.Item.FindControl("lblID")
            Dim ChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(lblID.Text, Integer))
            Dim objEndCust As EndCustomer = ChassisMaster.EndCustomer
            If objEndCust Is Nothing Then
                MessageBox.Show("Nomor Rangka belum ada pengajuan fakturnya")
                Return
            Else
                sessHelp.SetSession("ChassisMaster", ChassisMaster)
                sessHelp.SetSession("FrmEntryInvoice_CalledBy", "FrmInvoiceReqList.aspx")
                Server.Transfer("FrmEntryInvoice.aspx?ChassisMasterID=" & lblID.Text.Trim)
            End If
        End If
    End Sub

    Private Sub dgInvoiceList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim lblID As Label = e.Item.FindControl("lblID")
            Dim ChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(lblID.Text, Integer))
            Dim objEndCust As New EndCustomer
            objEndCust = ChassisMaster.EndCustomer
            Dim lnkDetail As LinkButton = e.Item.FindControl("lnkDetail")
            Dim isCMValid As Boolean = CType(Me.sessHelp.GetSession(Me._sessCMStatus), ArrayList)(e.Item.ItemIndex)

            If objEndCust Is Nothing Then
                lnkDetail.Visible = False
            Else
                If objEndCust.Customer Is Nothing Then
                    lnkDetail.Visible = False
                Else
                    lnkDetail.Visible = True
                End If
            End If


            Dim lblDealerBranch As Label = e.Item.FindControl("lblDealerBranch")

            If Not IsNothing(ChassisMaster) Then
                If Not IsNothing(ChassisMaster.EndCustomer) Then
                    If Not IsNothing(ChassisMaster.EndCustomer.SPKFaktur) Then
                        If Not IsNothing(ChassisMaster.EndCustomer.SPKFaktur.SPKHeader) Then

                            If Not IsNothing(ChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) Then
                                If Not IsNothing(ChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                                    lblDealerBranch.Text = ChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & " / " & ChassisMaster.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.Term1
                                End If
                            End If
                        End If

                    End If

                End If

            End If

            If Not IsNothing(objEndCust) AndAlso Not IsNothing(objEndCust.Customer) AndAlso Not IsNothing(objEndCust.Customer.MyCustomerRequest) AndAlso objEndCust.Customer.MyCustomerRequest.ID > 0 Then
                If (objEndCust.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP AndAlso objEndCust.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "CV") Then
                    e.Item.BackColor = System.Drawing.Color.Gainsboro
                End If

                'If (objEndCust.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP AndAlso objEndCust.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "CV") OrElse (objEndCust.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP AndAlso (objEndCust.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "PC" OrElse objEndCust.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "LCV")) Then
                '    e.Item.BackColor = System.Drawing.Color.Gainsboro
                'End If
            End If

            'LKPP

            If Not IsNothing(objEndCust) Then
                If (objEndCust.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "PC" OrElse objEndCust.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "LCV") AndAlso objEndCust.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
                    e.Item.BackColor = System.Drawing.Color.Gainsboro
                End If
            End If

            If isCMValid = False Then
                e.Item.BackColor = Me.txtColorRed.BackColor
            End If
        End If
    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
        '-- Sort arraylist

        'Dim IsAsc As Boolean = True
        'If SortDirection = Sort.SortDirection.ASC Then
        '    IsAsc = True
        'ElseIf SortDirection = Sort.SortDirection.DESC Then
        '    IsAsc = False
        'End If

        'Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        'pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub dgInvoiceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceList.SortCommand
        '-- Sort datagrid rows based on a column header clicked

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgInvoiceList.CurrentPageIndex = 0
        ReadData()
        BindPage(dgInvoiceList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub btnDnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDnLoad.Click
        '-- Download data in datagrid to text file

        '-- Generate random number [0..9999]
        Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        '-- Temp file must be a randomly named text file!
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\Permohonan_faktur_" & sSuffix & ".txt"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(InvoiceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteInvoiceData(sw)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\Permohonan_faktur_" & sSuffix & ".txt")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try

    End Sub

    Private Sub dgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        '-- Change datagrid page

        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

        StoreCriteria()
    End Sub

#End Region

End Class
