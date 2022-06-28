#Region " Summary "
'----------------------------------------------------'
'-- Program Code : FrmInvoiceResultList.aspx       --'
'-- Program Name : Daftar Status Faktur Kendaraan  --'
'-- Description  :                                 --'
'----------------------------------------------------'
'-- Programmer   : Agus Pirnadi                    --'
'-- Start Date   : Nov 01 2005                     --'
'-- Update By    :                                 --'
'-- Last Update  : Jan 02 2005                     --'
'----------------------------------------------------'
'-- Copyright © 2005 by Intimedia                  --'
'----------------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports KTB.DNet.BusinessValidation

#End Region

Public Class FrmInvoiceResultList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgInvoiceList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnConfirm As System.Web.UI.WebControls.Button
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvoiceNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtInvoiceNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents icStartConfirm As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndConfirm As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents icStartValid As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndValid As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkValidPeriod As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkConfirmPeriod As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnTransfer As System.Web.UI.WebControls.Button
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadFaktur As System.Web.UI.WebControls.Button
    Protected WithEvents btnRetransfer As System.Web.UI.WebControls.Button
    Protected WithEvents txtPendingReason As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSavePendingReason As System.Web.UI.WebControls.Button
    Protected WithEvents lblPendingReason As System.Web.UI.WebControls.Label
    Protected WithEvents lblttkduaPendingReason As System.Web.UI.WebControls.Label
    Protected WithEvents lblJumRecord As System.Web.UI.WebControls.Label
    Protected WithEvents txtMCPNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNoFleetReq As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoFleetReq As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLKPPNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents icHandoverDateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icHandoverDateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkHandoverDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnReSendPKT As System.Web.UI.WebControls.Button
    'Protected WithEvents ICHandoverDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkIsTemporary As CheckBox

    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents ddlSubCategory As DropDownList

    Protected WithEvents CBTglPembuatan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ICTglPembuatanStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICTglPembuatanEnd As KTB.DNet.WebCC.IntiCalendar
    '
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        objDealer = CType(sessHelp.GetSession("DEALER"), Dealer)
    End Sub

#End Region

#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        '-- Items selected in listbox

        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindListBoxList()
        lboxStatus.DataSource = New EnumDNET().RetrieveStatusFakturKendaraan
        lboxStatus.DataTextField = "NameType"
        lboxStatus.DataValueField = "ValType"
        lboxStatus.DataBind()
    End Sub

    Private Sub BindDropdownList()

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

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

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        lboxStatus.SelectedIndex = -1  '-- Clear status selection
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Sub WriteInvoiceData(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        For Each objInvoice As ChassisMaster In InvoiceList
            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
            InvoiceLine.Append(objInvoice.ChassisNumber.Replace(tab, " ") & tab) '-- Chassis number
            If Not IsNothing(objInvoice.EndCustomer) Then
                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "ddMMyyyy") & tab)  '-- Faktur date
                Dim objRefChassisMaster As ChassisMaster
                objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
                If objRefChassisMaster Is Nothing Then
                    InvoiceLine.Append(tab)  '-- Empty column
                Else
                    InvoiceLine.Append(objRefChassisMaster.ChassisNumber.Replace(tab, " ") & tab)  '-- Ref chassis number
                End If
                ''Change Status
                InvoiceLine.Append(" " & tab)   '-- Code

                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code.Replace(tab, " ") & tab)   '-- Code
                If UCase(objInvoice.EndCustomer.AreaViolationFlag) = "X" Then
                    Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(objInvoice.EndCustomer.AreaViolationPaymentMethodID)
                    InvoiceLine.Append(objAreaVioPatMeth.Code.Replace(tab, " ") & tab)   '-- Wilayah TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.AreaViolationyAmount, "0") & tab)  '-- Wilayah amount
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationBankName.Replace(tab, " ") & tab)  '-- Wilayah bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationGyroNumber.Replace(tab, " ") & tab)  '-- Wilayah giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                If UCase(objInvoice.EndCustomer.PenaltyFlag) = "X" Then
                    Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(objInvoice.EndCustomer.PenaltyPaymentMethodID)
                    InvoiceLine.Append(objPenaltyPatMeth.Code.Replace(tab, " ") & tab)  '-- Disc TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.PenaltyAmount, "0") & tab)  '-- Disc amount
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyBankName.Replace(tab, " ") & tab)  '-- Disc bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyGyroNumber.Replace(tab, " ") & tab)  '-- Disc giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If

                If UCase(objInvoice.EndCustomer.ReferenceLetterFlag) = "X" Then
                    InvoiceLine.Append(objInvoice.EndCustomer.ReferenceLetter.Replace(tab, " ") & tab)  '-- Letter
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.SaveBy <> "", UserInfo.Convert(objInvoice.EndCustomer.SaveBy.Replace(tab, " ")), "") & tab)  '-- Dibuat oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "ddMMyyyy") & tab)  '-- Tgl dibuat
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy.Replace(tab, " ")), "") & tab)  '-- Divalidasi oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "ddMMyyyy") & tab)
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.PrintRegion.Replace(tab, " ") & tab)

                'Start  :Add MCP Flag;by:dna;on:20110623;for:rina;
                If Not IsNothing(objInvoice.EndCustomer.Customer.MyCustomerRequest) AndAlso objInvoice.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                    If objInvoice.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                        InvoiceLine.Append("X" & tab)
                    Else
                        InvoiceLine.Append("" & tab)
                    End If
                Else
                    InvoiceLine.Append("" & tab)
                End If

                'If Not IsNothing(objInvoice.EndCustomer.Customer.MyCustomerRequest) AndAlso objInvoice.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                '    If objInvoice.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
                '        InvoiceLine.Append("X" & tab)
                '    Else
                '        InvoiceLine.Append("" & tab)
                '    End If
                'Else
                '    InvoiceLine.Append("" & tab)
                'End If

                'End    :Add MCP Flag;by:dna;on:20110623;for:rina;
                '============
                'Start  :Add MCP Number by anh 20150623 for yurike
                If Not IsNothing(objInvoice.EndCustomer.LKPPHeader) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.ReferenceNumber & tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.LetterDate.ToString("ddMMyyyy") & tab)
                    'If Not IsNothing(objInvoice.EndCustomer.MCPHeader) Then
                    '    InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.ReferenceNumber & tab)
                    '    InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.LetterDate.ToString("ddMMyyyy") & tab)
                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append("" & tab)
                End If

                '' AdddSpkNumber
                If Not IsNothing(objInvoice.EndCustomer) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SPKNumber & tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.CreatedTime.ToString("ddMMyyyy") & tab)
                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append("" & tab)
                End If

                '--No Fleet Request, add by wdi 20161013 for isye
                If Not IsNothing(objInvoice.FleetFaktur) AndAlso Not IsNothing(objInvoice.FleetFaktur.FleetRequest) Then
                    InvoiceLine.Append(objInvoice.FleetFaktur.FleetRequest.NoRegRequest & tab)
                Else
                    InvoiceLine.Append("" & tab)
                End If


                If Not IsNothing(objInvoice.EndCustomer) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & tab)
                Else
                    InvoiceLine.Append("" & tab)
                End If



                'If Not IsNothing(objInvoice.EndCustomer.LKPPHeader) Then
                '    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.ReferenceNumber & tab)
                '    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.LetterDate.ToString("ddMMyyyy") & tab)
                'Else
                '    InvoiceLine.Append("" & tab)
                '    InvoiceLine.Append("" & tab)
                'End If
                'End    :Add MCP Number by anh 20150623 for yurike

                If objInvoice.EndCustomer.IsTemporary = CType(EnumEndCustomer.TemporaryFaktur.Temporary, Short) Then
                    InvoiceLine.Append("X" & tab) '-- if it is temporary faktur
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
            End If
            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next
    End Sub

    Private Sub WriteInvoiceDataOld(ByRef sw As StreamWriter)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file

        For Each objInvoice As ChassisMaster In InvoiceList

            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line

            InvoiceLine.Append(objInvoice.ChassisNumber & tab)  '-- Chassis number

            If Not IsNothing(objInvoice.EndCustomer) Then

                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "ddMMyyyy") & tab)  '-- Faktur date

                Dim objRefChassisMaster As ChassisMaster
                objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
                If objRefChassisMaster Is Nothing Then
                    InvoiceLine.Append(tab)  '-- Empty column
                Else
                    InvoiceLine.Append(objRefChassisMaster.ChassisNumber & tab)  '-- Ref chassis number
                End If
                'Change Status
                InvoiceLine.Append(" " & tab)   '-- Code
                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code & tab)   '-- Code
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name1 & tab)   '-- Name1
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name2 & tab)   '-- Name2
                ' InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name3 & tab)   '-- Name3
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.Alamat & tab)  '-- Alamat
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.Kelurahan & tab)   '-- Kelurahan
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.Kecamatan & tab)   '-- Kecamatan
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.PostalCode & tab)  '-- Kode Pos
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.PreArea & tab)  '-- Pre area
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.CityCode & tab)  '-- City code
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.Province.ProvinceCode & tab)  '-- Province code
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.PrintRegion & tab)  '-- Cetak mark?
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.Email & tab)  '-- Email
                'InvoiceLine.Append(objInvoice.EndCustomer.Customer.PhoneNo & tab)    '-- Phone

                'If objInvoice.Category.CategoryCode = "CV" Or _
                '    objInvoice.Category.CategoryCode = "LCV" Then
                '    InvoiceLine.Append(objInvoice.EndCustomer.MainOperationArea.Code & tab)  '-- Daerah utama operasi
                'Else
                '    InvoiceLine.Append(tab)  '-- Empty column
                'End If

                'If objInvoice.Category.CategoryCode = "PC" Then
                '    InvoiceLine.Append(objInvoice.EndCustomer.MainUsage.Code & tab)  '-- Penggunaan kendaraan
                'Else
                '    InvoiceLine.Append(tab)  '-- Empty column
                'End If

                'InvoiceLine.Append(objInvoice.EndCustomer.VehicleOwnership.Code & tab)  '-- Kepemilikan kendaraan
                'InvoiceLine.Append(objInvoice.EndCustomer.VehiclePurpose.Code & tab)  '-- Kendaraan sebagai

                'If objInvoice.Category.CategoryCode = "CV" Then  '-- (first column)
                '    InvoiceLine.Append(objInvoice.EndCustomer.VehicleBodyShape.Code & tab)  '-- Bentuk body CV
                'Else
                '    InvoiceLine.Append(tab)  '-- Empty column
                'End If

                'If objInvoice.Category.CategoryCode = "LCV" Then  '-- (Second column)
                '    InvoiceLine.Append(objInvoice.EndCustomer.VehicleBodyShape.Code & tab)  '-- Bentuk body LCV
                'Else
                '    InvoiceLine.Append(tab)  '-- Empty column
                'End If

                'If objInvoice.Category.CategoryCode = "CV" Or _
                '    objInvoice.Category.CategoryCode = "LCV" Then
                '    InvoiceLine.Append(objInvoice.EndCustomer.CustomerBusiness.Code & tab)  '-- Usaha konsumen
                'Else
                '    InvoiceLine.Append(tab)  '-- Empty column
                'End If

                'InvoiceLine.Append(objInvoice.EndCustomer.PaymentType.Code & tab)  '-- Cara pembayaran

                'If objInvoice.Category.CategoryCode = "PC" Then
                '    InvoiceLine.Append(objInvoice.EndCustomer.OwnerAge.Code & tab)  '-- Usia pemilik
                'Else
                '    InvoiceLine.Append(tab)  '-- Empty column
                'End If

                If UCase(objInvoice.EndCustomer.AreaViolationFlag) = "X" Then
                    Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(objInvoice.EndCustomer.AreaViolationPaymentMethodID)
                    InvoiceLine.Append(objAreaVioPatMeth.Code & tab)   '-- Wilayah TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.AreaViolationyAmount, "0") & tab)  '-- Wilayah amount
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationBankName & tab)  '-- Wilayah bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationGyroNumber & tab)  '-- Wilayah giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If

                If UCase(objInvoice.EndCustomer.PenaltyFlag) = "X" Then
                    Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(objInvoice.EndCustomer.PenaltyPaymentMethodID)
                    InvoiceLine.Append(objPenaltyPatMeth.Code & tab)  '-- Disc TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.PenaltyAmount, "0") & tab)  '-- Disc amount
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyBankName & tab)  '-- Disc bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyGyroNumber & tab)  '-- Disc giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If

                If UCase(objInvoice.EndCustomer.ReferenceLetterFlag) = "X" Then
                    InvoiceLine.Append(objInvoice.EndCustomer.ReferenceLetter & tab)  '-- Letter
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                End If

                InvoiceLine.Append(IIf(objInvoice.EndCustomer.SaveBy <> "", UserInfo.Convert(objInvoice.EndCustomer.SaveBy), "") & tab)  '-- Dibuat oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "ddMMyyyy") & tab)  '-- Tgl dibuat
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy), "") & tab)  '-- Divalidasi oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "ddMMyyyy"))  '-- Tgl divalidasi
            End If

            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next

    End Sub
#End Region

#Region " EventHandler "

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.FakturKendaraanStatusView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR - Daftar Status Faktur Kendaraan")
        End If
        Me.btnConfirm.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanStatusConfirmation_Privilege)
        Me.btnCancel.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanStatusCancelConfirmation_Privilege)
        Me.btnTransfer.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanStatusDownload_Privilege)
        Me.dgInvoiceList.Columns(9).Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanPangajuanBuat_Privilege)
        Me.btnRetransfer.Visible = SecurityProvider.Authorize(Context.User, SR.ENHFakturKendaraanReTransfer_Privilege)
        Me.btnReSendPKT.Visible = SecurityProvider.Authorize(Context.User, SR.FakturKendaraanDaftarStatusKirimUlangPKT_Privilege)
        _PCAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege)
        _CVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege)
        _LCVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege)
        EnablingPendingReaoson()
        If (Not _PCAccessAllowed) And (Not _CVAccessAllowed) And (Not _LCVAccessAllowed) Then
            Me.btnSearch.Visible = False
            Me.ddlCategory.Visible = False
        End If
        ViewState("PKT") = SecurityProvider.Authorize(Context.User, SR.InputPKTDate_Privilege)
    End Sub

    Private Sub EnablingPendingReaoson()
        Dim valid As Boolean = SecurityProvider.Authorize(Context.User, SR.ENHFakturPendingReason_Privilege)
        If valid Then
            btnSavePendingReason.Visible = True
            lblPendingReason.Visible = True
            txtPendingReason.Visible = True
            lblttkduaPendingReason.Visible = True
        End If
    End Sub

    Private Function CheckInputPKTPrivilege() As Boolean
        Return CBool(ViewState("PKT"))
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        btnTransfer.Attributes.Add("onclick", "return IsChecked();")
        btnConfirm.Attributes.Add("onclick", "return IsChecked();")
        btnCancel.Attributes.Add("onclick", "return IsChecked();")
        btnTransfer.Attributes.Add("onclick", "MakeValid();")
        btnSearch.Attributes.Add("onclick", "MakeValid();")
        btnDnLoad.Attributes.Add("onclick", "MakeValid();")
        btnDownloadFaktur.Attributes.Add("onclick", "MakeValid();")
        btnRetransfer.Attributes.Add("onclick", "MakeValid();")
        btnSavePendingReason.Attributes.Add("onclick", "MakeValid();")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblNoFleetReq.Attributes("onClick") = "ShowFleetReqSelection();"
        InitiateAuthorization()
        If Not IsPostBack Then
            BindDropdownList()  '-- Bind dropdownlist
            BindListBoxList()
            ViewState("currSortColumn") = "ChassisNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            ViewState("IsDealerPilotting") = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingPKT))
            Dim crit As Hashtable = New Hashtable
            crit = CType(Session("CriteriaFormInvoiceResultList"), Hashtable)
            If Not crit Is Nothing Then
                txtChassisNo.Text = CStr(crit.Item("ChassisNumber"))
                txtInvoiceNo.Text = CStr(crit.Item("InvoiceNumber"))
                ddlCategory.SelectedValue = CStr(crit.Item("Category"))
                lboxStatus.Items(0).Selected = CType(crit("Validasi"), Boolean)
                lboxStatus.Items(1).Selected = CType(crit("Konfirmasi"), Boolean)
                lboxStatus.Items(2).Selected = CType(crit("Proses"), Boolean)
                lboxStatus.Items(3).Selected = CType(crit("Selesai"), Boolean)
                icStartValid.Value = CType(crit("StartValid"), Date)
                icEndValid.Value = CType(crit("EndValid"), Date)
                icStartConfirm.Value = CType(crit("StartConfirm"), Date)
                icEndConfirm.Value = CType(crit("EndConfirm"), Date)
                txtKodeDealer.Text = CStr(crit.Item("Dealer"))
                chkValidPeriod.Checked = CType(crit("chkValidPeriod"), Boolean)
                chkConfirmPeriod.Checked = CType(crit("chkConfirmPeriod"), Boolean)
                dgInvoiceList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
                icHandoverDateStart.Value = CType(crit("icHandoverDateStart"), Date)
                icHandoverDateEnd.Value = CType(crit("icHandoverDateEnd"), Date)
                chkHandoverDate.Checked = CType(crit("chkHandoverDate"), Boolean)
                Try
                    CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
                    ddlSubCategory.SelectedValue = CStr(crit.Item("ddlSubCategory"))
                Catch ex As Exception

                End Try
            End If
            txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            ReadData()   '-- Read all data matching criteria
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        storeCriteria()
        txtPendingReason.Text = String.Empty
        ReadData()   '-- Read all data matching criteria
        dgInvoiceList.CurrentPageIndex = 0
        BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Private Sub storeCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("ChassisNumber", txtChassisNo.Text)
        crit.Add("InvoiceNumber", txtInvoiceNo.Text)
        crit.Add("Category", ddlCategory.SelectedValue)
        crit.Add("Validasi", lboxStatus.Items(0).Selected)
        crit.Add("Konfirmasi", lboxStatus.Items(1).Selected)
        crit.Add("Proses", lboxStatus.Items(2).Selected)
        crit.Add("Selesai", lboxStatus.Items(3).Selected)
        crit.Add("StartValid", icStartValid.Value)
        crit.Add("EndValid", icEndValid.Value)
        crit.Add("StartConfirm", icStartConfirm.Value)
        crit.Add("EndConfirm", icEndConfirm.Value)
        crit.Add("Dealer", txtKodeDealer.Text)
        crit.Add("PageIndex", dgInvoiceList.CurrentPageIndex)
        crit.Add("chkValidPeriod", chkValidPeriod.Checked)
        crit.Add("chkConfirmPeriod", chkConfirmPeriod.Checked)
        crit.Add("txtDealerBranchCode", txtDealerBranchCode.Text)
        sessHelp.SetSession("CriteriaFormInvoiceResultList", crit)
        crit.Add("icHandoverDateStart", icHandoverDateStart.Value)
        crit.Add("icHandoverDateEnd", icHandoverDateEnd.Value)
        crit.Add("chkHandoverDate", chkHandoverDate.Checked)

        crit.Add("ddlSubCategory", ddlSubCategory.SelectedValue)
    End Sub

    Private Sub ReadData()
        '-- Read all data selected

        '-- Date validation
        If Not chkValidPeriod.Checked And Not chkConfirmPeriod.Checked And Not chkHandoverDate.Checked Then
            '-- At least one date range is set: Periode Validasi or Periode Konfirmasi
            MessageBox.Show("Periode Validasi, Periode Konfirmasi atau Periode PKT harus diisi")
            Exit Sub  '-- Directly exits
        End If

        '-- Validation date
        If chkValidPeriod.Checked Then
            If icStartValid.Value > icEndValid.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal validasi tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icEndValid.Value.Subtract(icStartValid.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal validasi harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If

        '-- Confirmation date
        If chkConfirmPeriod.Checked Then
            If icStartConfirm.Value > icEndConfirm.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal konfirmasi tidak valid")
                Exit Sub  '-- Directly exits
            Else
                If icEndConfirm.Value.Subtract(icStartConfirm.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal konfirmasi harus <= 65 hari")
                    Exit Sub  '-- Directly exits
                End If
            End If
        End If

        '-- Handover date
        If chkHandoverDate.Checked Then
            If icHandoverDateStart.Value > icHandoverDateEnd.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal PKT tidak valid")
                Exit Sub  '-- Directly exits
            End If
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Nomor chassis
        If txtChassisNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.[Partial], txtChassisNo.Text.Trim()))
        End If

        '-- Nomor faktur
        If txtInvoiceNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.FakturNumber", MatchType.[Partial], txtInvoiceNo.Text.Trim()))
        End If

        '-- Category
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        '-- Status
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        Else
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.InSet, "('1','2','3','4')"))
        End If



        If txtDealerBranchCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Replace(";", "','") & "')"))
        End If

        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ValidateTime", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ValidateTime", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkConfirmPeriod.Checked Then
            '-- Periode Konfirmasi
            Dim StartConfirm As New DateTime(CInt(icStartConfirm.Value.Year), CInt(icStartConfirm.Value.Month), CInt(icStartConfirm.Value.Day), 0, 0, 0)
            Dim EndConfirm As New DateTime(CInt(icEndConfirm.Value.Year), CInt(icEndConfirm.Value.Month), CInt(icEndConfirm.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ConfirmTime", MatchType.GreaterOrEqual, Format(StartConfirm, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ConfirmTime", MatchType.LesserOrEqual, Format(EndConfirm, "yyyy-MM-dd HH:mm:ss")))
        End If

        If chkHandoverDate.Checked Then
            '-- Periode HandoverDate
            Dim HandoverDateStart As New DateTime(CInt(icHandoverDateStart.Value.Year), CInt(icHandoverDateStart.Value.Month), CInt(icHandoverDateStart.Value.Day), 0, 0, 0)
            Dim HandoverDateEnd As New DateTime(CInt(icHandoverDateEnd.Value.Year), CInt(icHandoverDateEnd.Value.Month), CInt(icHandoverDateEnd.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisMasterPKT.PKTDate", MatchType.GreaterOrEqual, Format(HandoverDateStart, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisMasterPKT.PKTDate", MatchType.LesserOrEqual, Format(HandoverDateEnd, "yyyy-MM-dd HH:mm:ss")))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        '-- Dealer code
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        '-- MCP Number , add by anh 20150623 for yurike
        If txtMCPNumber.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.MCPHeader.ReferenceNumber", MatchType.[Partial], txtMCPNumber.Text.Trim()))
        End If

        '-- Fleet Request Number , add by wdi 20161003 for isye
        If txtNoFleetReq.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(select ChassisMasterID from FleetFaktur f inner join FleetRequest r on f.FleetRequestID=r.ID where f.RowStatus=0 and r.RowStatus=0 and r.NoRegRequest='" & txtNoFleetReq.Text.Trim() & "')"))
        End If

        '-- LKPP Number , add by anh 20151217 for adi
        If txtLKPPNumber.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.LKPPHeader.ReferenceNumber", MatchType.[Partial], txtLKPPNumber.Text.Trim()))
        End If

        ' -- Temposrary Faktur
        If chkIsTemporary.Checked Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.IsTemporary", MatchType.Exact, CType(CInt(Int(chkIsTemporary.Checked)), Short)))
        End If

        If ddlCategory.SelectedValue.ToString() <> "" And ddlSubCategory.SelectedValue <> "-1" Then
            'Dim Sql As String = ""
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

            'Sql &= "  SELECT  vc.[ID] FROM    dbo.VechileType vt INNER JOIN dbo.[VechileColor] vc ON [vc].[VechileTypeID] = [vt].[ID] WHERE   vt.[RowStatus] = 0 AND vc.[RowStatus] = 0 "
            'Dim i As Integer
            'For i = 0 To sVals.Split(";").Length - 1
            '    If i = 0 Then
            '        Sql &= " and (vt.Description like '" & sVals.Split(";")(i) & "' "
            '        If sVals.Split(";").Length = 1 Then Sql &= ")"
            '    ElseIf i = sVals.Split(";").Length - 1 Then
            '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "') "
            '    Else
            '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "'"
            '    End If
            'Next
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "VechileColor.ID", MatchType.InSet, "(" & Sql & ")"))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            Dim strSql2 As String = "select a.ID from VechileColor a join VechileType b on a.VechileTypeID = b.ID and b.RowStatus = 0 "
            strSql2 += " join VechileModel c on b.ModelID = c.ID and c.RowStatus = 0 where a.RowStatus = 0 and c.ID in (" & strSql & ")"
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor.ID", MatchType.InSet, "(" & strSql2 & ")"))

        End If

        If CBTglPembuatan.Checked Then
            '-- Periode HandoverDate
            Dim TglPembuatanStart As New DateTime(CInt(ICTglPembuatanStart.Value.Year), CInt(ICTglPembuatanStart.Value.Month), CInt(ICTglPembuatanStart.Value.Day), 0, 0, 0)
            Dim TglPembuatanEnd As New DateTime(CInt(ICTglPembuatanEnd.Value.Year), CInt(ICTglPembuatanEnd.Value.Month), CInt(ICTglPembuatanEnd.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.CreatedTime", MatchType.GreaterOrEqual, Format(TglPembuatanStart, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.CreatedTime", MatchType.LesserOrEqual, Format(TglPembuatanEnd, "yyyy-MM-dd HH:mm:ss")))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        'oCM.EndCustomer.ValidateBy 
        sortColl.Add(New Sort(GetType(ChassisMaster), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))  '-- Nomor chassis

        '-- Retrieve recordset
        Dim InvoiceResultList As ArrayList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, sortColl)
        'Start  :by:dna;for:rina;on:20110630;sort by validasi status and mcpstatus
        InvoiceResultList = SortByMCPStatus(InvoiceResultList)
        'End    :by:dna;for:rina;on:20110630;sort by validasi status and mcpstatus
        '-- Store recordset into session for later use
        sessHelp.SetSession("InvoiceResList", InvoiceResultList)

        If InvoiceResultList.Count > 0 Then
            '-- Enable all buttons if any record exists
            btnTransfer.Enabled = True
            btnCancel.Enabled = True
            btnConfirm.Enabled = True
            btnDnLoad.Enabled = True
            btnDownloadFaktur.Enabled = True
        Else
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
            btnConfirm.Enabled = False
            btnCancel.Enabled = False
            btnTransfer.Enabled = False
            btnDnLoad.Enabled = False
            btnDownloadFaktur.Enabled = False
        End If

        'store profileDetail into session
        'add by anh 2012-01-10
        Dim critProfiles As New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critProfiles.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.ID", MatchType.InSet, "(5,6)"))

        Dim profileDetailList As ArrayList = New ProfileDetailFacade(User).Retrieve(critProfiles)
        sessHelp.SetSession("ProfileDetailList", profileDetailList)

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim InvoiceResList As ArrayList = CType(sessHelp.GetSession("InvoiceResList"), ArrayList)
        If InvoiceResList.Count <> 0 Then
            SortListControl(InvoiceResList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceResList, pageIndex, dgInvoiceList.PageSize)
            dgInvoiceList.DataSource = PagedList
            dgInvoiceList.VirtualItemCount = InvoiceResList.Count()
            dgInvoiceList.DataBind()
        Else
            dgInvoiceList.DataSource = New ArrayList
            dgInvoiceList.VirtualItemCount = 0
            dgInvoiceList.CurrentPageIndex = 0
            dgInvoiceList.DataBind()
        End If
        If dgInvoiceList.VirtualItemCount > 0 Then
            lblJumRecord.Text = "Jumlah record : " & dgInvoiceList.VirtualItemCount
        End If
    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)
        '-- Sort arraylist

        'If SortColumn.Trim <> "" Then
        '    Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
        '    Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
        '    pCompletelist.Sort(objListComparer)
        'End If

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

    End Sub

    Private Sub dgInvoiceList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceList.ItemCommand

        If e.CommandName = "lnkDetail" Then
            '-- Retrieve Invoice and its related end customer if any
            Dim ChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Store Invoice and its related end customer for display on form FrmEntryInvoice.aspx
            sessHelp.SetSession("ChassisMaster", ChassisMaster)

            '-- Store the calling page
            sessHelp.SetSession("FrmEntryInvoice_CalledBy", "FrmInvoiceResultList.aspx")

            storeCriteria()

            '-- Display Invoice and its related end customer on Entry Invoice page
            Response.Redirect("FrmEntryInvoice.aspx?ChassisMasterID=" & e.Item.Cells(0).Text.Trim)

        ElseIf e.CommandName = "SaveHandoverDate" Then
            Dim icHandoverDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icHandoverDate"), KTB.DNet.WebCC.IntiCalendar)

            '-- Retrieve Invoice and its related end customer if any
            Dim ChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
            Dim EndCustomer As EndCustomer = ChassisMaster.EndCustomer

            EndCustomer.HandoverDate = icHandoverDate.Value
            Dim critPDI As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critPDI.opAnd(New Criteria(GetType(PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, Integer)))
            critPDI.opAnd(New Criteria(GetType(PDI), "ChassisMaster.ID", MatchType.Exact, ChassisMaster.ID))

            Dim arlPDI As ArrayList = New PDIFacade(User).Retrieve(critPDI)
            If arlPDI.Count > 0 Then
                Dim objPDI As PDI = New PDI
                objPDI = CType(arlPDI(0), PDI)

                Dim days As Integer = (icHandoverDate.Value - objPDI.PDIDate).Days
                If days > 30 Then
                    MessageBox.Show("Tanggal PKT tidak boleh lebih dari 30 hari dari tanggal PDI")
                    Exit Sub
                End If
            Else
                MessageBox.Show("Chassis ini belum memiliki PDI dengan status selesai")
                Exit Sub
            End If

            Dim CheckFS As Boolean = False
            Dim CheckWSC As Boolean = False
            Dim objFS As FreeService = New FreeService
            Dim objWSC As WSCHeader = New WSCHeader

            Dim critFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critFS.opAnd(New Criteria(GetType(FreeService), "FSKind.KindCode", MatchType.Exact, 1)) 'request user hanya untuk fs kind 1
            critFS.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, ChassisMaster.ID))
            Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
            If arlFS.Count > 0 Then
                CheckFS = True
                objFS = CType(arlFS(0), FreeService)
            Else
                objFS = Nothing
            End If

            Dim critWSC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critWSC.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.ID", MatchType.Exact, ChassisMaster.ID))
            Dim arlWSC As ArrayList = New WSCHeaderFacade(User).Retrieve(critWSC)
            If arlWSC.Count > 0 Then
                CheckWSC = True
                objWSC = CType(arlWSC(0), WSCHeader)
            Else
                objWSC = Nothing
            End If

            If CheckFS Then
                If Not IsNothing(objFS) Then
                    If objFS.ServiceDate < icHandoverDate.Value Then
                        MessageBox.Show("Tanggal PKT tidak boleh lebih besar dari tanggal Free Service kind code 1 (FS 1000)")
                        Exit Sub
                    End If
                End If
            End If
            If CheckFS Then
                If Not IsNothing(objWSC) Then
                    If objWSC.ServiceDate < icHandoverDate.Value Then
                        MessageBox.Show("Tanggal PKT tidak boleh lebih besar dari tanggal WSC")
                        Exit Sub
                    End If
                End If
            End If
            'Dim EndCustomerFac As EndCustomerFacade = New EndCustomerFacade(User)
            Dim FPkt As ChassisMasterPKTFacade = New ChassisMasterPKTFacade(User)

            Dim pkt As New ChassisMasterPKT
            pkt.ChassisMaster = ChassisMaster
            pkt.PKTDate = icHandoverDate.Value


            Dim intResult As Integer = -1
            'intResult = EndCustomerFac.Update(EndCustomer)
            intResult = FPkt.Insert(pkt)
            Dim arl As ArrayList = New ArrayList()
            arl.Add(ChassisMaster.ChassisNumber & ";" & pkt.PKTDate.ToString("ddMMyyyy"))
            SendToSap(arl)
            If intResult <> -1 Then
                MessageBox.Show("Simpan Data Berhasil")
                ''Transfer to Salesforce
                'If Not IsNothing(EndCustomer.SPKFaktur.SPKHeader.SPKCustomer.SAPCustomer) Then
                '    Dim objSAPCustomer As SAPCustomer = EndCustomer.SPKFaktur.SPKHeader.SPKCustomer.SAPCustomer
                '    TransferToSF(objSAPCustomer, EnumSAPCustomerResponse.SAPCustomerResponse.Sudah_PKT)
                'End If
            Else
                MessageBox.Show("Simpan Data Gagal")
            End If

            ReadData()
            BindPage(dgInvoiceList.CurrentPageIndex)
        End If

    End Sub

    Private Sub dgInvoiceList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        '-- Handle data binding
        Try
            Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            Dim arlProfileDetail As ArrayList = CType(sessHelp.GetSession("ProfileDetailList"), ArrayList)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                '-- Grid detail items

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
                'Dim validateTime As DateTime = RowValue.EndCustomer.ValidateTime
                'If validateTime > New Date(1900, 1, 1, 0, 0, 0) Then

                'End If
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                Dim lnkPending As LinkButton = CType(e.Item.FindControl("lnkPending"), LinkButton)
                Try
                    lblDealer.ToolTip = RowValue.Dealer.SearchTerm1  '-- Bind searchTerm1 as tooltip

                Catch ex As Exception
                End Try

                Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
                If Not IsNothing(RowValue.EndCustomer) Then
                    If Not IsNothing(RowValue.EndCustomer.SPKFaktur) Then
                        If Not IsNothing(RowValue.EndCustomer.SPKFaktur.SPKHeader) Then

                            If Not IsNothing(RowValue.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) Then
                                If Not IsNothing(RowValue.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                                    lblDealerBranch.Text = RowValue.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & " / " & RowValue.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.Term1
                                End If
                            End If
                        End If

                    End If

                End If


                If (RowValue.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi) Or (RowValue.FakturStatus = EnumChassisMaster.FakturStatus.Validasi) Or (RowValue.FakturStatus = EnumChassisMaster.FakturStatus.Proses) Then
                    Dim validateTime As DateTime = RowValue.EndCustomer.ValidateTime
                    Dim i = Now.Subtract(validateTime).Days
                    If i >= 3 Then
                        e.Item.BackColor = Color.FromName("FFFF66")
                    End If

                    If i >= 7 Then
                        e.Item.BackColor = Color.FromName("FF6666")
                    End If
                    lnkPending.Visible = True
                    If RowValue.PendingDesc.Trim <> String.Empty Then
                        Dim str() As String = RowValue.PendingDesc.Split(Chr(1))
                        If str.Length = 3 Then
                            lnkPending.ToolTip = str(2) & Chr(13) & Chr(10) & "Dibuat Oleh: " & UserInfo.Convert(str(0)) & Chr(13) & Chr(10) & "Dibuat Pada: " & str(1)
                        Else
                            lnkPending.ToolTip = RowValue.PendingDesc
                        End If
                        lnkPending.Visible = True
                    Else
                        lnkPending.ToolTip = "Belum ada Pending Reason"
                        lnkPending.Visible = False
                    End If



                End If

                If Not IsNothing(RowValue.EndCustomer) AndAlso Not IsNothing(RowValue.EndCustomer.Customer) AndAlso Not IsNothing(RowValue.EndCustomer.Customer.MyCustomerRequest) AndAlso RowValue.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                    If RowValue.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                        e.Item.BackColor = System.Drawing.Color.Gainsboro
                    End If
                End If


                If Not IsNothing(RowValue.EndCustomer) Then
                    If (RowValue.EndCustomer.ChassisMaster.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "PC" OrElse RowValue.VechileColor.VechileType.Category.CategoryCode.ToUpper() = "LCV") AndAlso RowValue.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
                        e.Item.BackColor = System.Drawing.Color.Gainsboro
                    End If
                End If

                'tambahan 2 kolom -kepemilikan kendaraan & tipe
                'add by anh 2012-01-10
                Dim lblKepemilikan As Label = CType(e.Item.FindControl("lblKepemilikan"), Label)
                Dim lblKendaraan As Label = CType(e.Item.FindControl("lblKendaraan"), Label)
                If arlProfileDetail.Count > 0 Then
                    For i As Integer = 0 To RowValue.ChassisMasterProfiles.Count - 1

                        i = i + 1
                    Next
                    For Each profiles As ChassisMasterProfile In RowValue.ChassisMasterProfiles
                        For Each pd As ProfileDetail In arlProfileDetail
                            If pd.ProfileHeader.ID = profiles.ProfileHeader.ID And pd.Code = profiles.ProfileValue Then
                                If profiles.ProfileHeader.ID = 5 Then
                                    lblKepemilikan.Text = pd.Description
                                ElseIf profiles.ProfileHeader.ID = 6 Then
                                    lblKendaraan.Text = pd.Description
                                End If

                            End If
                        Next
                    Next
                End If

                '============
                'Start  :Add MCP Number by anh 20150623 for yurike

                If Not IsNothing(RowValue.EndCustomer) AndAlso Not IsNothing(RowValue.EndCustomer.MCPHeader) Then
                    If RowValue.EndCustomer.MCPHeader.ID > 0 Then
                        Dim lblMCPNumber As Label = CType(e.Item.FindControl("lblMCPNumber"), Label)
                        If Not IsNothing(RowValue.EndCustomer.MCPHeader) Then
                            lblMCPNumber.Text = RowValue.EndCustomer.MCPHeader.ReferenceNumber
                        End If
                    End If
                End If

                If Not IsNothing(RowValue.EndCustomer) Then
                    If RowValue.EndCustomer.MCPStatus = 1 Then
                        e.Item.BackColor = System.Drawing.Color.PeachPuff
                    End If
                End If

                If Not IsNothing(RowValue.EndCustomer) AndAlso Not IsNothing(RowValue.EndCustomer.LKPPHeader) Then
                    If RowValue.EndCustomer.LKPPHeader.ID > 0 Then
                        Dim lblLKPPNumber As Label = CType(e.Item.FindControl("lblLKPPNumber"), Label)
                        If Not IsNothing(RowValue.EndCustomer.LKPPHeader) Then
                            lblLKPPNumber.Text = RowValue.EndCustomer.LKPPHeader.ReferenceNumber
                        End If
                    End If
                End If

                If Not IsNothing(RowValue.EndCustomer) Then
                    If RowValue.EndCustomer.LKPPStatus = EnumLKPPStatus.LKPPStatus.IndicatedLKPP Then
                        e.Item.BackColor = System.Drawing.Color.PeachPuff
                    End If
                End If
                'End    :Add MCP Number by anh 20150623 for yurike

                'Add HandoverDate by wdi 20161109 for yurike
                objDealer = Session("DEALER")
                Dim icHandoverDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icHandoverDate"), KTB.DNet.WebCC.IntiCalendar)


                'If IsDate(RowValue.EndCustomer.HandoverDate) AndAlso RowValue.EndCustomer.HandoverDate <> "12:00:00 AM" AndAlso RowValue.EndCustomer.HandoverDate <> "01/01/1753 12:00:00 AM" AndAlso RowValue.EndCustomer.HandoverDate <> "01/01/1900 12:00:00 AM" Then
                'If (RowValue.Dealer.DealerCode = "100127" And RowValue.ChassisNumber = "JMYLTCV2WFJ000550") Then
                '    icHandoverDate.Enabled = CheckInputPKTPrivilege()
                'End If

                If Not IsNothing(RowValue.ChassisMasterPKT) Then
                    RowValue.EndCustomer.HandoverDate = RowValue.ChassisMasterPKT.PKTDate
                End If

                Dim isDealerPilotting As Boolean = CBool(ViewState("IsDealerPilotting"))

                If IsDate(RowValue.EndCustomer.HandoverDate) AndAlso RowValue.EndCustomer.HandoverDate.Year > 1753 AndAlso RowValue.EndCustomer.HandoverDate.Year > 1900 Then
                    icHandoverDate.Value = Format(RowValue.EndCustomer.HandoverDate, "dd/MM/yyyy")

                    If objDealer.Title = CInt(EnumDealerTittle.DealerTittle.KTB) Then
                        icHandoverDate.Enabled = CheckInputPKTPrivilege()
                        Dim btnSave As LinkButton = CType(e.Item.FindControl("lnkSaveHandoverDate"), LinkButton)
                        btnSave.Visible = CheckInputPKTPrivilege()
                    Else
                        icHandoverDate.Enabled = False
                        Dim btnSave As LinkButton = CType(e.Item.FindControl("lnkSaveHandoverDate"), LinkButton)
                        btnSave.Visible = False
                    End If

                Else
                    icHandoverDate.Value = Format(DateTime.Now, "dd/MM/yyyy")
                    If RowValue.FakturStatus = 4 Then 'faktur selesai
                        e.Item.BackColor = System.Drawing.Color.Aquamarine
                    End If

                    Dim btnSave As LinkButton = CType(e.Item.FindControl("lnkSaveHandoverDate"), LinkButton)
                    If objDealer.Title = CInt(EnumDealerTittle.DealerTittle.KTB) Then
                        icHandoverDate.Enabled = False
                        btnSave.Visible = False
                    ElseIf Not isDealerPilotting And Not objDealer.IsDealerDMS Then
                        icHandoverDate.Enabled = CheckInputPKTPrivilege()

                        Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objDealer.ID))
                        Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                        Dim arlDealerSystem As ArrayList = New ArrayList
                        arlDealerSystem = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
                        Dim IsDealerDnet As Boolean = True
                        Dim isHasPKTPriv As Boolean = CheckInputPKTPrivilege()
                        If Not IsNothing(arlDealerSystem) AndAlso arlDealerSystem.Count > 0 Then
                            If CType(arlDealerSystem(0), DealerSystems).SystemID <> 1 Then
                                IsDealerDnet = False
                            End If
                        End If
                        If RowValue.Dealer.ID = objDealer.ID AndAlso IsDealerDnet AndAlso isHasPKTPriv Then
                            btnSave.Visible = True
                        Else
                            btnSave.Visible = False
                        End If
                    Else
                        icHandoverDate.Enabled = False
                        btnSave.Visible = False
                    End If
                End If

                'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                '    icHandoverDate.Enabled = False
                '    Dim btnSave As LinkButton = CType(e.Item.FindControl("lnkSaveHandoverDate"), LinkButton)
                '    btnSave.Visible = False
                'End If

                '============
                'Start  :Add Fleet Request Number by wdi 20161003 for isye

                Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ID", MatchType.Exact, RowValue.ID))
                Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)
                Dim objFleetFaktur As FleetFaktur

                If arrFleetFaktur.Count > 0 Then
                    objFleetFaktur = arrFleetFaktur(0)
                End If

                If Not IsNothing(objFleetFaktur) Then
                    Dim lblNoFleetReq As Label = CType(e.Item.FindControl("lblNoFleetReq"), Label)
                    lblNoFleetReq.Text = objFleetFaktur.FleetRequest.NoRegRequest
                End If
                'End    :Add Fleet Request by wdi 20161003 for isye

            End If


            '============
            'Start  :Add MCP Number by anh 20151217 for Adi

            '============

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        '-- Change status 'Baru' to 'Validasi' for each invoice selected

        Dim InvoiceList As New ArrayList  '-- List of invoices selected
        Dim AnyBlockChassis As New ArrayList
        '-- Iterate all records in grid "dgInvoiceList"
        For Each item As DataGridItem In dgInvoiceList.Items

            '-- If it is selected then process it
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then

                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID

                '-- Only invoices with status 'Validasi' and with End Customer defined
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then

                    If Invoice.PendingDesc.Contains("Block_Faktur") Then
                        AnyBlockChassis.Add(Invoice)
                    End If

                    If Not IsNothing(Invoice.EndCustomer) Then
                        Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi  '-- Change invoice status
                        Invoice.EndCustomer.ConfirmBy = User.Identity.Name  '-- Set its confirmator
                        Invoice.EndCustomer.ConfirmTime = Date.Now  '-- Set its confirmation date

                        InvoiceList.Add(Invoice)  '-- Add to list of invoices
                    End If
                End If
            End If
        Next

        If AnyBlockChassis.Count > 0 Then
            Dim cNumber As String = ""
            For Each item As ChassisMaster In AnyBlockChassis
                If cNumber.Trim.Length = 0 Then
                    cNumber = item.ChassisNumber
                Else
                    cNumber = cNumber & ", " & item.ChassisNumber
                End If
            Next
            MessageBox.Show("Chassis Number berikut " & cNumber & " telah di blok")
            Exit Sub
        End If

        '-- If there exists at least an invoice selected then do update transaction
        If InvoiceList.Count > 0 Then
            Dim ChassisFac As New ChassisMasterFacade(User)
            ChassisFac.UpdateTransaction(InvoiceList)  '-- Update list of invoice selected

            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceList.CurrentPageIndex) '-- Re-bind current page
            MessageBox.Show("Konfirmasi data berhasil")
        Else
            MessageBox.Show("Konfirmasi data tidak bisa dilakukan\nkarena status faktur bukan 'validasi'")
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '-- Change status 'Validasi' to 'Baru' for each invoice selected

        Dim InvoiceList As New ArrayList  '-- List of invoices selected

        '-- Iterate all records in grid "dgInvoiceList"
        For Each item As DataGridItem In dgInvoiceList.Items

            '-- If it is selected then process it
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then

                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID

                '-- Only invoices with status 'Konfirmasi' and with End Customer defined
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                    If Not IsNothing(Invoice.EndCustomer) Then

                        Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Validasi  '-- Change invoice status
                        Invoice.EndCustomer.ConfirmBy = User.Identity.Name  '-- Set the eraser
                        Invoice.EndCustomer.ConfirmTime = Date.Now  '-- Reset its confirmation date

                        InvoiceList.Add(Invoice)  '-- Add to list of invoices
                    End If
                End If
            End If
        Next

        '-- If there exists at least an invoice selected then do update transaction
        If InvoiceList.Count > 0 Then
            Dim ChassisFac As New ChassisMasterFacade(User)
            ChassisFac.UpdateTransaction(InvoiceList)  '-- Update list of invoice selected

            ReadData()   '-- Re-read all data to refresh changes
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Re-bind data grid
            MessageBox.Show("Berhasil membatalkan konfirmasi")
        Else
            MessageBox.Show("Pembatalan konfirmasi data tidak bisa dilakukan\nkarena status faktur bukan 'konfirmasi'")
        End If

    End Sub

    Private Function PopulateInvoice(ByVal mode As Integer) As ArrayList
        Dim oExArgs As New System.Collections.ArrayList
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim _chsMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)
                    If mode = 0 Then
                        If _chsMaster.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                            oExArgs.Add(_chsMaster)
                        End If
                    End If
                    If mode = 1 Then
                        If _chsMaster.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                            oExArgs.Add(_chsMaster)
                        End If
                    End If
                    If mode = 3 Then
                        oExArgs.Add(_chsMaster)
                    End If
                End If
            End If
            If oExArgs.Count = 0 And mode = 3 Then
                oExArgs = CType(sessHelp.GetSession("InvoiceResList"), ArrayList)
                oExArgs = SortByMCPStatus(oExArgs)
            End If

        Next
        Return oExArgs
    End Function

    Private Function PopulateFakturSPK() As ArrayList
        Dim oExArgs As New System.Collections.ArrayList
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim _chsMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)
                    oExArgs.Add(_chsMaster)
                End If
            End If
        Next
        Return oExArgs
    End Function

    Private Sub Transfer()
        InvoiceList.Clear()
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                    InvoiceList.Add(Invoice)
                End If
            End If
        Next
        If InvoiceList.Count = 0 Then
            MessageBox.Show("Tidak ada faktur yang dipilih\natau faktur tidak bisa di-transfer")
            Exit Sub
        End If
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\fkopen" & sSuffix & ".txt"
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
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteInvoiceData(sw)
                sw.Close()
                fs.Close()
                Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK"
                If Not IO.Directory.Exists(DestFolder) Then
                    IO.Directory.CreateDirectory(DestFolder)
                End If
                Dim DestFile As String = DestFolder & "\fkopen" & sSuffix & ".txt"
                Dim finfo2 As FileInfo = New FileInfo(InvoiceData)
                finfo2.CopyTo(DestFile, True)
                imp.StopImpersonate()
                imp = Nothing
            End If
            InProcessStatus()  '-- Change invoice status from 'Konfirmasi' to 'Proses'
            ReadData()  '-- Read all data matching criteria
            BindPage(dgInvoiceList.CurrentPageIndex) '-- Re-bind current page
            MessageBox.Show("Transfer data berhasil")
        Catch ex As Exception
            MessageBox.Show("Transfer data gagal")
        End Try

    End Sub

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim BlockedChassis As ArrayList = AnyBlockedChassis()
        If BlockedChassis.Count > 0 Then
            Dim cNumber As String = ""
            For Each item As ChassisMaster In BlockedChassis
                If cNumber.Trim.Length = 0 Then
                    cNumber = item.ChassisNumber
                Else
                    cNumber = cNumber & ", " & item.ChassisNumber
                End If
            Next
            MessageBox.Show("Chassis Number berikut " & cNumber & " telah di blok")
            Exit Sub
        End If
        TransferChasisMasterProfile(0)
        Transfer()
        InsertResponseStatus()
    End Sub

    Private Function AnyBlockedChassis()
        Dim _return As New ArrayList
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID

                If Invoice.PendingDesc.Contains("Block_Faktur") Then
                    _return.Add(Invoice)
                End If

            End If
        Next
        Return _return
    End Function

    Private Sub InsertResponseStatus()
        Dim arrToSF As ArrayList = New ArrayList
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                    arrToSF.Add(Invoice)
                End If
            End If
        Next
        If arrToSF.Count > 0 Then
            For Each cm As ChassisMaster In arrToSF
                If Not IsNothing(cm.EndCustomer.SPKFaktur) Then
                    If Not IsNothing(cm.EndCustomer.SPKFaktur.SPKHeader.SPKCustomer.SAPCustomer) Then
                        Dim objSAPCustomer As SAPCustomer = cm.EndCustomer.SPKFaktur.SPKHeader.SPKCustomer.SAPCustomer
                        Dim objresponse As SAPCustomerResponse = New SAPCustomerResponse
                        objresponse.SAPCustomer = objSAPCustomer
                        objresponse.Status = CShort(EnumSAPCustomerResponse.SAPCustomerResponse.Sudah_Teralokasi)
                        objresponse.Description = EnumSAPCustomerResponse.GetStringValue(CInt(EnumSAPCustomerResponse.SAPCustomerResponse.Sudah_Teralokasi))

                        Dim iresult As Integer = 0
                        Dim objFacade As SAPCustomerResponseFacade = New SAPCustomerResponseFacade(User)
                        iresult = objFacade.Insert(objresponse)

                        'TransferToSF(objresponse, EnumSAPCustomerResponse.SAPCustomerResponse.Sudah_Teralokasi)

                    End If

                End If
            Next
        End If
    End Sub

    'Private Sub TransferToSF(ByVal objSAPCustomerResponse As SAPCustomerResponse, ByVal status As EnumSAPCustomerResponse.SAPCustomerResponse)
    '    Dim sf As SalesForceInterface = New SalesForceInterface()
    '    Dim msg As String = String.Empty
    '    Dim isSms As Boolean = False
    '    Dim vSFreturn As Boolean = False
    '    If objSAPCustomerResponse.SAPCustomer.SalesforceID.Trim <> "" Then
    '        vSFreturn = sf.UpdateOportunity(objSAPCustomerResponse, CType(status, Integer), isSms)
    '    End If

    '    If vSFreturn Then
    '        'Update IsSend
    '        Dim iResult As Integer
    '        Dim objResponseNew As SAPCustomerResponse = New SAPCustomerResponse()
    '        objResponseNew = New SAPCustomerResponseFacade(User).Retrieve(objSAPCustomerResponse.ID)
    '        If Not IsNothing(objResponseNew) Then
    '            objResponseNew.IsSend = 1 'sent
    '            iResult = New SAPCustomerResponseFacade(User).Update(objResponseNew)
    '        End If
    '    End If
    'End Sub

    Private Sub InProcessStatus()
        '-- Change invoice status from 'Konfirmasi' to 'Proses'

        Dim ConfirmList As New ArrayList  '-- List of confirmed invoices

        '-- Iterate invoices selected
        For Each item As ChassisMaster In InvoiceList

            '-- Only invoices with status 'Konfirmasi' and with End Customer defined
            If item.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                If Not IsNothing(item.EndCustomer) Then

                    item.FakturStatus = EnumChassisMaster.FakturStatus.Proses  '-- Change invoice status
                    item.EndCustomer.DownloadBy = UserInfo.Convert(User.Identity.Name)  '-- Set its downloader
                    item.EndCustomer.DownloadTime = Date.Now  '-- Set its download date

                    ConfirmList.Add(item)
                End If
            End If
        Next

        '-- If there exists at least a confirmed invoice selected then do update transaction
        Dim ChassisFac As New ChassisMasterFacade(User)
        ChassisFac.UpdateTransaction(ConfirmList)  '-- Update list of confirmed invoices

    End Sub

    Private Sub dgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        '-- Change datagrid page

        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
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
                DnLoadInvoiceData(sw)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
            '-- Download data to client!
            Response.Redirect("../downloadLocal.aspx?file=DataTemp\Permohonan_faktur_" & sSuffix & ".txt")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub btnDownloadFaktur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadFaktur.Click
        Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        '-- Temp file must be a randomly named text file!
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\Permohonan_Faktur_SPK_" & sSuffix & ".txt"
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
                DownloadFakturSPK(sw)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
            '-- Download data to client!
            Response.Redirect("../downloadLocal.aspx?file=DataTemp\Permohonan_Faktur_SPK_" & sSuffix & ".txt")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub DnLoadInvoiceData(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim InvoiceResList As ArrayList = PopulateInvoice(3)
        'Add header by anh 20110914
        InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
        InvoiceLine.Append("Chassis Number" & tab)  '-- Chassis number
        If Me.IsKTB Then
            InvoiceLine.Append("Material Number" & tab)
        End If
        InvoiceLine.Append("Tgl Faktur" & tab)  '-- Faktur date
        InvoiceLine.Append("No. Rangka Pengganti" & tab)  '-- Chassis number
        InvoiceLine.Append(tab)  '-- Empty column
        InvoiceLine.Append("Nama_1" & tab)   '-- Name1
        InvoiceLine.Append("Nama_2" & tab)   '-- Name2
        InvoiceLine.Append("Nama_3" & tab)   '-- Name3
        InvoiceLine.Append("Alamat" & tab)  '-- Alamat
        InvoiceLine.Append("Kelurahan" & tab)   '-- Kelurahan
        InvoiceLine.Append("Kecamatan" & tab)   '-- Kecamatan
        InvoiceLine.Append("Kode Pos" & tab)  '-- Kode Pos
        InvoiceLine.Append("Pre Area" & tab)  '-- Pre area
        InvoiceLine.Append("Kota" & tab)  '-- City name
        InvoiceLine.Append("Propinsi" & tab)  '-- Province name
        InvoiceLine.Append("Cetak" & tab)  '-- Cetak mark?
        InvoiceLine.Append("Email" & tab)  '-- Email
        InvoiceLine.Append("Phone" & tab)  '-- Phone
        InvoiceLine.Append("Tipe Pembayaran Plgrn Wil" & tab)  '-- Wilayah TOP
        InvoiceLine.Append("Jmlh Plgrn Wil" & tab)  '-- Wilayah amount
        InvoiceLine.Append("Nama Bank Plgrn Wil" & tab)  '-- Wilayah bank name
        InvoiceLine.Append("No.Gyro Plgrn Wil" & tab)  '-- Wilayah giro#
        InvoiceLine.Append("Tipe Pembayaran Penalti" & tab)  '-- Disc TOP
        InvoiceLine.Append("Jumlah Penalti" & tab) '-- Disc amount
        InvoiceLine.Append("Nama Bank Penalti" & tab)  '-- Disc bank name
        InvoiceLine.Append("No. Gyro Penalti" & tab)  '-- Disc giro#
        InvoiceLine.Append("No. Surat" & tab)  '-- Letter
        InvoiceLine.Append("Dibuat Oleh" & tab)  '-- Dibuat oleh
        InvoiceLine.Append("Tgl Buat" & tab)  '-- Tgl dibuat
        InvoiceLine.Append("Divalidasi Oleh" & tab)  '-- Divalidasi oleh
        InvoiceLine.Append("Tgl Validasi" & tab)   '-- Tgl divalidasi
        InvoiceLine.Append("Nomor Faktur" & tab)  '-- Nomor faktur
        InvoiceLine.Append("Nomor SPK" & tab)  '-- Nomor SPK
        InvoiceLine.Append("Nomor MCP" & tab)  '-- Nomor SPK
        InvoiceLine.Append("Nomor Pengadaan" & tab)  '-- Nomor SPK
        InvoiceLine.Append("Tanggal Surat")  '-- Nomor SPK
        sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        'End added header

        For Each objInvoice As ChassisMaster In InvoiceResList
            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
            InvoiceLine.Append(objInvoice.ChassisNumber & tab)  '-- Chassis number
            If Me.IsKTB Then
                InvoiceLine.Append(objInvoice.EndCustomer.ChassisMaster.VechileColor.MaterialNumber & tab)  '-- 21/05/2021 Moh Ridwan Req Bu Nung
            End If

            If Not IsNothing(objInvoice.EndCustomer) Then
                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "dd/MM/yyyy") & tab)  '-- Faktur date
                Dim objRefChassisMaster As ChassisMaster
                objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
                If objRefChassisMaster Is Nothing Then
                    InvoiceLine.Append(tab)  '-- Empty column
                Else
                    InvoiceLine.Append(objRefChassisMaster.ChassisNumber & tab)   '-- Ref chassis number
                End If

                InvoiceLine.Append(tab)  '-- Empty column
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name1 & tab)   '-- Name1
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name2 & tab)   '-- Name2
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Name3 & tab)   '-- Name3
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Alamat & tab)  '-- Alamat
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Kelurahan & tab)   '-- Kelurahan
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Kecamatan & tab)   '-- Kecamatan
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.PostalCode & tab)  '-- Kode Pos
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.PreArea & tab)  '-- Pre area
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.CityName & tab)  '-- City name
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.City.Province.ProvinceName & tab)  '-- Province name
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
                InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "ddMMyyyy") & tab)  '-- Tgl dibuat
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy), "") & tab)  '-- Divalidasi oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "ddMMyyyy") & tab)   '-- Tgl divalidasi
                InvoiceLine.Append(objInvoice.EndCustomer.FakturNumber & tab)  '-- Nomor faktur

                Dim critSPK As New CriteriaComposite(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critSPK.opAnd(New Criteria(GetType(SPKFaktur), "EndCustomer.ID", MatchType.Exact, objInvoice.EndCustomer.ID))
                'critSPK.opAnd(New Criteria(GetType(SPKFaktur), "ChassisMaster.ID", MatchType.Exact, objInvoice.EndCustomer.ChassisMaster.ID))

                Dim arlSPKFaktur As ArrayList = New SPKFakturFacade(User).Retrieve(critSPK)
                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    InvoiceLine.Append(objSPKFaktur.SPKHeader.SPKNumber & tab)  '-- Nomor SPK
                Else
                    InvoiceLine.Append(String.Empty)  '-- Nomor SPK
                End If
                'MCP , add by anh for yurike 20150625
                '---start---
                'If Not IsNothing(objInvoice.EndCustomer.MCPHeader) Then
                '    InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.ReferenceNumber.ToString & tab)  '-- Nomor MCP
                '    InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.LetterDate.ToString("ddMMyyyy"))  '-- Tanggal Surat
                'Else
                '    InvoiceLine.Append(String.Empty & tab)
                '    InvoiceLine.Append(String.Empty)
                'End If

                If Not IsNothing(objInvoice.EndCustomer.LKPPHeader) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.ReferenceNumber.ToString & tab)  '-- Nomor MCP
                    InvoiceLine.Append(objInvoice.EndCustomer.LKPPHeader.LetterDate.ToString("ddMMyyyy"))  '-- Tanggal Surat
                Else
                    InvoiceLine.Append(String.Empty & tab)
                    InvoiceLine.Append(String.Empty)
                End If
                '---end --
            End If
            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next
    End Sub

    Private Sub DownloadFakturSPK(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim InvoiceResList As ArrayList = CType(sessHelp.GetSession("InvoiceResList"), ArrayList)
        Dim arlProfileDetail As ArrayList = CType(sessHelp.GetSession("ProfileDetailList"), ArrayList)

        'add header anh 20110914
        InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
        InvoiceLine.Append("Tgl Faktur" & tab)  '-- Faktur date
        InvoiceLine.Append("No. Faktur" & tab)   '-- Nomor Faktur
        InvoiceLine.Append("No. SPK" & tab)  '-- Nomor SPK
        InvoiceLine.Append("No. Chassis" & tab)   '-- Nomor Faktur
        InvoiceLine.Append("Kode Dealer" & tab)  '-- Dealer Code
        InvoiceLine.Append("Nama Dealer" & tab)  '-- Dealer Name
        InvoiceLine.Append("Kepemilikan" & tab)  '-- Kepemilikan Kendaraan
        InvoiceLine.Append("Tipe" & tab)  '-- Tipe

        'start rudi
        InvoiceLine.Append("Nomor Telepon/Handphone" & tab)  '-- Nomor Telepon/Handphone
        InvoiceLine.Append("Nomor KTP" & tab)  '-- Nomor KTP
        InvoiceLine.Append("Nomor SPK Dealer" & tab)  '-- Nomor SPK Dealer
        InvoiceLine.Append("Kode Salesman" & tab)  '-- Kode Salesman
        InvoiceLine.Append("Nama Salesman" & tab)  '-- Nama Salesman
        'end rudi

        sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        'end added header anh 20110914

        For Each objInvoice As ChassisMaster In InvoiceResList
            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
            If Not IsNothing(objInvoice.EndCustomer) Then
                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "dd/MM/yyyy") & tab)  '-- Faktur date
                InvoiceLine.Append(objInvoice.EndCustomer.FakturNumber & tab)   '-- Nomor Faktur

                Dim critSPK As New CriteriaComposite(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critSPK.opAnd(New Criteria(GetType(SPKFaktur), "EndCustomer.ID", MatchType.Exact, objInvoice.EndCustomer.ID))

                Dim arlSPKFaktur As ArrayList = New SPKFakturFacade(User).Retrieve(critSPK)
                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    InvoiceLine.Append(objSPKFaktur.SPKHeader.SPKNumber & tab)  '-- Nomor SPK
                Else
                    InvoiceLine.Append(String.Empty & tab)  '-- Nomor SPK
                End If
                InvoiceLine.Append(objInvoice.ChassisNumber & tab)   '-- Nomor Faktur
                InvoiceLine.Append(objInvoice.Dealer.DealerCode & tab)  '-- Dealer Code
                InvoiceLine.Append(objInvoice.Dealer.DealerName & tab)  '-- Dealer Name

                If arlProfileDetail.Count > 0 Then
                    For Each profiles As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                        For Each pd As ProfileDetail In arlProfileDetail
                            If pd.ProfileHeader.ID = profiles.ProfileHeader.ID And pd.Code = profiles.ProfileValue Then
                                If profiles.ProfileHeader.ID = 5 Then
                                    InvoiceLine.Append(pd.Description & tab)  '-- Kepemilikan Kendaraan
                                ElseIf profiles.ProfileHeader.ID = 6 Then
                                    InvoiceLine.Append(pd.Description & tab)  '-- Tipe
                                End If
                            End If
                        Next
                    Next
                End If

                '---- add by rudi --
                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    Dim objSPKHeader As SPKHeader = objSPKFaktur.SPKHeader
                    If Not objSPKHeader Is Nothing Then
                        Dim critSPKCustP As New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critSPKCustP.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, objSPKHeader.SPKCustomer.ID))
                        critSPKCustP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, 30))
                        Dim arlSPKCustomerProfile As ArrayList = New SPKCustomerProfileFacade(User).Retrieve(critSPKCustP)
                        If arlSPKCustomerProfile.Count > 0 Then
                            Dim objSPKCustP As SPKCustomerProfile = arlSPKCustomerProfile(0)
                            InvoiceLine.Append(objSPKCustP.ProfileValue & "/" & objSPKHeader.SPKCustomer.HpNo & tab)  '-- Nomor Telepon/Handphone
                        Else
                            InvoiceLine.Append(String.Empty & tab)  '-- Nomor Telepon/Handphone
                        End If
                    Else
                        InvoiceLine.Append(String.Empty & tab)  '-- Nomor Telepon/Handphone
                    End If
                Else
                    InvoiceLine.Append(String.Empty & tab)  '-- Nomor Telepon/Handphone
                End If

                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    Dim objSPKHeader As SPKHeader = objSPKFaktur.SPKHeader
                    If Not objSPKHeader Is Nothing Then
                        Dim critSPKCustP As New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critSPKCustP.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, objSPKHeader.SPKCustomer.ID))
                        critSPKCustP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                        Dim arlSPKCustomerProfile As ArrayList = New SPKCustomerProfileFacade(User).Retrieve(critSPKCustP)
                        If arlSPKCustomerProfile.Count > 0 Then
                            Dim objSPKCustP As SPKCustomerProfile = arlSPKCustomerProfile(0)
                            InvoiceLine.Append(objSPKCustP.ProfileValue & tab)  '-- Nomor KTP
                        Else
                            InvoiceLine.Append(String.Empty & tab)  '-- Nomor KTP
                        End If
                    Else
                        InvoiceLine.Append(String.Empty & tab)  '-- Nomor KTP
                    End If
                Else
                    InvoiceLine.Append(String.Empty & tab)  '-- Nomor KTP
                End If

                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    Dim objSPKHeader As SPKHeader = objSPKFaktur.SPKHeader
                    If Not objSPKHeader Is Nothing Then
                        InvoiceLine.Append(objSPKHeader.DealerSPKNumber & tab)  '-- Nomor SPK Dealer
                    Else
                        InvoiceLine.Append(String.Empty & tab)  '-- Nomor SPK Dealer
                    End If
                Else
                    InvoiceLine.Append(String.Empty & tab)  '-- Nomor SPK Dealer
                End If

                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    Dim objSPKHeader As SPKHeader = objSPKFaktur.SPKHeader
                    If (Not objSPKHeader Is Nothing) And (Not objSPKHeader.SalesmanHeader Is Nothing) Then
                        InvoiceLine.Append(objSPKHeader.SalesmanHeader.SalesmanCode & tab)  '-- Kode Salesman
                    Else
                        InvoiceLine.Append(String.Empty & tab)  '-- Kode Salesman
                    End If
                Else
                    InvoiceLine.Append(String.Empty & tab)  '-- Kode Salesman
                End If

                If arlSPKFaktur.Count > 0 Then
                    Dim objSPKFaktur As SPKFaktur = arlSPKFaktur(0)
                    Dim objSPKHeader As SPKHeader = objSPKFaktur.SPKHeader
                    If (Not objSPKHeader Is Nothing) And (Not objSPKHeader.SalesmanHeader Is Nothing) Then
                        InvoiceLine.Append(objSPKHeader.SalesmanHeader.Name & tab)  '-- Nama Salesman
                    Else
                        InvoiceLine.Append(String.Empty & tab)  '-- Nama Salesman
                    End If
                Else
                    InvoiceLine.Append(String.Empty & tab)  '-- Nama Salesman
                End If
                '---- end add by rudi --
            End If
            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next
    End Sub

#End Region


    Private Sub ReTransfer()
        InvoiceList.Clear() '-- Clear list of invoices
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                    InvoiceList.Add(Invoice)  '-- Add to list of invoices
                End If
            End If
        Next
        If InvoiceList.Count = 0 Then
            MessageBox.Show("Tidak ada faktur yang dipilih\natau faktur tidak bisa di-transfer ulang")
            Exit Sub  '-- If empty then exit
        End If
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\fkopen" & sSuffix & ".txt"
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
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteInvoiceData(sw)
                sw.Close()
                fs.Close()
                Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK"
                If Not IO.Directory.Exists(DestFolder) Then
                    IO.Directory.CreateDirectory(DestFolder)
                End If
                Dim DestFile As String = DestFolder & "\fkopen" & sSuffix & ".txt"
                Dim finfo2 As FileInfo = New FileInfo(InvoiceData)
                finfo2.CopyTo(DestFile, True)
                imp.StopImpersonate()
                imp = Nothing
            End If
            InProcessStatus()  '-- Change invoice status from 'Konfirmasi' to 'Proses'
            ReadData()  '-- Read all data matching criteria
            BindPage(dgInvoiceList.CurrentPageIndex) '-- Re-bind current page
            MessageBox.Show("Transfer data berhasil")
        Catch ex As Exception
            MessageBox.Show("Transfer data gagal")
        End Try
    End Sub

    Private Sub btnRetransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetransfer.Click
        Dim BlockedChassis As ArrayList = AnyBlockedChassis()
        If BlockedChassis.Count > 0 Then
            Dim cNumber As String = ""
            For Each item As ChassisMaster In BlockedChassis
                If cNumber.Trim.Length = 0 Then
                    cNumber = item.ChassisNumber
                Else
                    cNumber = cNumber & ", " & item.ChassisNumber
                End If
            Next
            MessageBox.Show("Chassis Number berikut " & cNumber & " telah di blok")
            Exit Sub
        End If
        TransferChasisMasterProfile(1)
        ReTransfer()
    End Sub

    Private Sub btnSavePendingReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePendingReason.Click
        Dim InvoiceList As New ArrayList  '-- List of invoices selected
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Or Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                    Invoice.PendingDesc = User.Identity.Name & Chr(1) & DateTime.Now.ToString() & Chr(1) & txtPendingReason.Text.Trim
                    InvoiceList.Add(Invoice)  '-- Add to list of invoices
                End If
            End If
        Next
        If InvoiceList.Count > 0 Then
            Dim ChassisFac As New ChassisMasterFacade(User)
            ChassisFac.UpdateTransaction(InvoiceList)  '-- Update list of invoice selected
            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceList.CurrentPageIndex) '-- Re-bind current page
            MessageBox.Show("Ubah Pending Reason berhasil")
        Else
            MessageBox.Show("Tidak ada status konfirmasi atau proses dalam item yang terpilih.")
        End If
    End Sub

    Private Sub TransferChasisMasterProfile(ByVal mode As Integer)
        Dim filename = String.Format("{0}{1}{2}", "csprof", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK\" & filename  '-- Destination file
        TransferChassisProfile(DestFile, mode)
    End Sub

    Private Function TransferChassisProfile(ByVal DestFile As String, ByVal mode As Integer) As Boolean
        Dim listFaktur As ArrayList = PopulateInvoice(mode)
        If listFaktur.Count = 0 Then
            Return False
        End If
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                Dim fs As FileStream = New FileStream(DestFile, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                DnLoadInvoiceDataChasisProfile(sw, mode)
                sw.Close()
                fs.Close()
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Private Sub DnLoadInvoiceDataChasisProfile(ByRef sw As StreamWriter, ByVal mode As String)
        Dim tab As Char  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim InvoiceResList As ArrayList = PopulateInvoice(mode)
        Dim temp As String = String.Empty
        tab = Chr(9)
        For Each objInvoice As ChassisMaster In InvoiceResList
            For Each objChassisMasterProfile As ChassisMasterProfile In objInvoice.ChassisMasterProfiles
                If objChassisMasterProfile.ProfileValue.Trim <> "" Then
                    InvoiceLine.Append(objInvoice.ChassisNumber + tab)
                    InvoiceLine.Append(objChassisMasterProfile.ProfileHeader.Code + tab)
                    temp = objChassisMasterProfile.ProfileValue.Trim
                    InvoiceLine.Append(temp.Trim)
                    InvoiceLine.Append(vbNewLine)
                    temp = String.Empty
                End If
            Next
        Next
        sw.WriteLine(InvoiceLine.ToString())
    End Sub

    Private Function SortByMCPStatus(ByVal aCM) As ArrayList
        Dim aResult As New ArrayList
        Dim aMCPTop1 As New ArrayList
        Dim aMCPTop2 As New ArrayList
        Dim aMCPTop3 As New ArrayList
        Dim TOPSequence As Integer = 0

        For Each oCM As ChassisMaster In aCM
            TOPSequence = 3
            'Optimized by Firman
            'If Not IsNothing(oCM) AndAlso Not IsNothing(oCM.EndCustomer) AndAlso Not IsNothing(oCM.EndCustomer.Customer) AndAlso Not IsNothing(oCM.EndCustomer.Customer.MyCustomerRequest) Then
            '    If oCM.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
            '        If oCM.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then
            '            TOPSequence = 1
            '        Else
            '            TOPSequence = 2
            '        End If
            '    End If
            'End If
            'If TOPSequence = 1 Then
            '    aMCPTop1.Add(ocm)
            'ElseIf TOPSequence = 2 Then
            '    aMCPTop2.Add(ocm)
            'ElseIf TOPSequence = 3 Then
            '    aMCPTop3.Add(ocm)
            'End If

            Dim objHelper As V_CMHelper = New ChassisMasterFacade(User).RetrieveCmHelper(oCM.ID)
            If Not IsNothing(objHelper) AndAlso objHelper.ID > 0 Then
                If objHelper.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                    If objHelper.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then
                        TOPSequence = 1
                    Else
                        TOPSequence = 2
                    End If
                End If

            End If

            If TOPSequence = 1 Then
                aMCPTop1.Add(oCM)
            ElseIf TOPSequence = 2 Then
                aMCPTop2.Add(oCM)
            ElseIf TOPSequence = 3 Then
                aMCPTop3.Add(oCM)
            End If

        Next
        For Each oCM As ChassisMaster In aMCPTop1
            aResult.Add(oCM)
        Next
        For Each oCM As ChassisMaster In aMCPTop2
            aResult.Add(oCM)
        Next
        For Each oCM As ChassisMaster In aMCPTop3
            aResult.Add(oCM)
        Next

        Return aResult
    End Function

    Private Function SortByLKPPStatus(ByVal aCM) As ArrayList
        Dim aResult As New ArrayList
        Dim aLKPPTop1 As New ArrayList
        Dim aLKPPTop2 As New ArrayList
        Dim aLKPPTop3 As New ArrayList
        Dim TOPSequence As Integer = 0

        For Each oCM As ChassisMaster In aCM
            TOPSequence = 3
            'Optimized by Firman
            'If Not IsNothing(oCM) AndAlso Not IsNothing(oCM.EndCustomer) AndAlso Not IsNothing(oCM.EndCustomer.Customer) AndAlso Not IsNothing(oCM.EndCustomer.Customer.MyCustomerRequest) Then
            '    If oCM.EndCustomer.Customer.MyCustomerRequest.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
            '        If oCM.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then
            '            TOPSequence = 1
            '        Else
            '            TOPSequence = 2
            '        End If
            '    End If
            'End If
            'If TOPSequence = 1 Then
            '    aLKPPTop1.Add(ocm)
            'ElseIf TOPSequence = 2 Then
            '    aLKPPTop2.Add(ocm)
            'ElseIf TOPSequence = 3 Then
            '    aLKPPTop3.Add(ocm)
            'End If

            Dim objHelper As V_CMHelper = New ChassisMasterFacade(User).RetrieveCmHelper(oCM.ID)
            If Not IsNothing(objHelper) AndAlso objHelper.ID > 0 Then
                If objHelper.LKPPStatus = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP Then
                    If objHelper.FakturStatus = EnumChassisMaster.FakturStatus.Validasi Then
                        TOPSequence = 1
                    Else
                        TOPSequence = 2
                    End If
                End If

            End If

            If TOPSequence = 1 Then
                aLKPPTop1.Add(oCM)
            ElseIf TOPSequence = 2 Then
                aLKPPTop2.Add(oCM)
            ElseIf TOPSequence = 3 Then
                aLKPPTop3.Add(oCM)
            End If

        Next
        For Each oCM As ChassisMaster In aLKPPTop1
            aResult.Add(oCM)
        Next
        For Each oCM As ChassisMaster In aLKPPTop2
            aResult.Add(oCM)
        Next
        For Each oCM As ChassisMaster In aLKPPTop3
            aResult.Add(oCM)
        Next

        Return aResult
    End Function

    Protected Sub dgInvoiceList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgInvoiceList.SelectedIndexChanged

    End Sub


    Protected Sub btnReSendPKT_Click(sender As Object, e As EventArgs) Handles btnReSendPKT.Click
        Dim arrChM As New ArrayList  '-- List of invoices selected
        For Each item As DataGridItem In dgInvoiceList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text  '-- ID column
                Dim oChM As ChassisMaster = New ChassisMasterFacade(User).Retrieve(id)  '-- Retrieve chassis Number
                Dim strSAP As StringBuilder = New StringBuilder
                If CType(item.FindControl("ICHandoverDate"), IntiCalendar).Value.Year <= 1900 Then
                    Continue For
                End If
                Dim ICHandoverDate As String = CType(item.FindControl("ICHandoverDate"), IntiCalendar).Value.ToString("ddMMyyyy")
                strSAP.Append(oChM.ChassisNumber & ";" & ICHandoverDate)
                strSAP.Append(vbNewLine)
                arrChM.Add(strSAP.ToString)
            End If
        Next
        If arrChM.Count > 0 Then
            SendToSap(arrChM)
            ReadData()  '-- Re-read all data to refresh changes
            BindPage(dgInvoiceList.CurrentPageIndex) '-- Re-bind current page
        Else
            MessageBox.Show("Tidak ada item yang terpilih.")
        End If
    End Sub

    Private Sub SendToSap(ByVal chNumberNPktDate As ArrayList)

        Dim filename = String.Format("{0}{1}{2}", "PKTData", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt") 'PKTData[TimeStamp].txt
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("FinishUnitFileDirectory") & "\PKT\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename

        If (chNumberNPktDate.Count > 0) Then
            If Transfer(DestFile, LocalDest, chNumberNPktDate) Then         '>> Code utk upload data lsg ke SAP Folder
                MessageBox.Show("Send tanggal PKT ke SAP sukses")
            Else
                MessageBox.Show("Send tanggal PKT ke SAP gagal")
            End If
        End If
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal DestFileLocal As String, ByVal Val As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fInfoLocal As New FileInfo(DestFile)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                'Local
                If Not fInfoLocal.Directory.Exists Then Directory.CreateDirectory(fInfoLocal.DirectoryName)
                If fInfoLocal.Exists() Then fInfoLocal.Delete()
                Dim fs As FileStream = New FileStream(DestFileLocal, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                For Each item As String In Val
                    sw.Write(item)
                Next
                sw.Close()
                fs.Close()

                'Server
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                File.Copy(DestFileLocal, DestFile)
            End If
        Catch
            success = False
            sw.Close()
        End Try
        Return success
    End Function
End Class
