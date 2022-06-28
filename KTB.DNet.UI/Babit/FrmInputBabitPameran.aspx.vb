#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit

Imports System.Collections.Generic
Imports System.Linq
Imports System.IO

#End Region

Public Class FrmInputBabitPameran
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private objDealer As Dealer
    Private sessHelper As New SessionHelper
    Private _vstICPameranStart As String = "ICPameranStart.Value"
    Private _vstICPameranEnd As String = "ICPameranEnd.Value"
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property
    Private TempDirectory As String
    Private objBabitHeader As BabitHeader
    Private intItemIndex As Integer = 0
    Private intBabitParameterHeaderID As Integer = 0
    Private intItemIndex2 As Integer = 0
    Private intBabitDisplayCarID As Integer = 0
    Private TargetDirectory As String
    Private objLoginUser As UserInfo
    Private arlPameranDetail As ArrayList = New ArrayList
    Private arlEvent As ArrayList = New ArrayList
    Private arlDocument As ArrayList = New ArrayList
    Private arlDisplayAndTarget As ArrayList = New ArrayList
    Private arlAlloc As ArrayList = New ArrayList
    Private strBabitType As String

    Private arlDelPameranDetail As ArrayList = New ArrayList
    Private arlDelEvent As ArrayList = New ArrayList
    Private arlDelDocument As ArrayList = New ArrayList
    Private arlDelDisplayAndTarget As ArrayList = New ArrayList
    Private Mode As String = "New"
    Private oHeader As BabitHeader
    Private Const strTypeCode As String = "P"
#End Region

#Region "Event Handler"
    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sessHelper.GetSession("FrmInputBabitPameran.DEALER"), Dealer)
            If IsNothing(objDealer) Then
                objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            End If
        End If
        objLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            oHeader = New BabitHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitHeaderID")))
        End If

        If Not IsNothing(ViewState.Item(Me._vstICPameranStart)) Then
            If CType(ViewState.Item(Me._vstICPameranStart), Date).Day <> Me.ICPameranStart.Value.Day OrElse _
            CType(ViewState.Item(Me._vstICPameranStart), Date).Month <> Me.ICPameranStart.Value.Month OrElse _
            CType(ViewState.Item(Me._vstICPameranStart), Date).Year <> Me.ICPameranStart.Value.Year Then
                CalculatePeriodePameran()
            End If
        End If

        If Not IsNothing(ViewState.Item(Me._vstICPameranEnd)) Then
            If CType(ViewState.Item(Me._vstICPameranEnd), Date).Day <> Me.ICPameranEnd.Value.Day OrElse _
            CType(ViewState.Item(Me._vstICPameranEnd), Date).Month <> Me.ICPameranStart.Value.Month OrElse _
            CType(ViewState.Item(Me._vstICPameranEnd), Date).Year <> Me.ICPameranStart.Value.Year Then
                CalculatePeriodePameran()
            End If
        End If

        Initialization()
        If Not IsPostBack Then
            InitiateAuthorization()
            objBabitHeader = New BabitHeader
            strBabitType = New AppConfigFacade(User).Retrieve("BabitCognitoSharePoint.BabitType.Pameran").Value
            'LoadMarBox()
            BindMarbox(strBabitType)
            BindddlProvince()
            'BindDDLAllocationType()
            'BindDDLAllocationBabit()
            BindGridDisplayAndTarget()

            lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
            lblPULocation.Attributes("onclick") = "ShowPopUpLocation()"
            lnkReload.Attributes("onclick") = "DisableButton()"

            BindGridEventUploadFile()
            BindGridBabitEvent()
            BindddlLocationType()
            BindDDLMaterialPromosi()
            BindDDLProfilPengunjung()
            BindDDLLokasiPameran()
            CalculatePeriodePameran()
            FromList()
            'UsedMarbox()

            Dim strDealerGroupID As String = ""
            If IsDealer() Then
                strDealerGroupID = CStr(objLoginUser.Dealer.DealerGroup.ID)
            Else
                If Not IsNothing(oHeader) AndAlso oHeader.ID > 0 Then
                    strDealerGroupID = oHeader.Dealer.DealerGroup.ID
                End If
            End If
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionGab('" & strDealerGroupID & "','" & CType(GetFromSession("DEALER"), Dealer).DealerCode & "');"

            lblKodeDealer.Text = objDealer.DealerCode
            lblNamaDealer.Text = " / " & objDealer.DealerName

            BindGridAlloc()

            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                If Mode = "Edit" Then
                    If objBabitHeader.BabitStatus >= 2 Then  '--> status Konfirmasi
                        dgAlloc.ShowFooter = True
                        dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = True
                        'ddlAllocationBabit.Enabled = True
                        'txtSubsidyAmount.Enabled = True
                    Else
                        dgAlloc.ShowFooter = False
                        dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = False
                        'ddlAllocationBabit.Enabled = False
                        'txtSubsidyAmount.Enabled = False
                    End If
                    txtNotes.Enabled = True
                End If
            Else
                dgAlloc.ShowFooter = False
                dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = False
                'ddlAllocationBabit.Enabled = False
                'txtSubsidyAmount.Enabled = False
                txtNotes.Enabled = False
                If Mode = "New" Then
                    TR_Alokasi_Babit.Visible = False
                    TR_Alokasi_Babit2.Visible = False
                    TR_Jml_Subsidi.Visible = False
                    TR_CatatanMKS.Visible = False
                Else
                    TR_Alokasi_Babit.Visible = True
                    TR_Alokasi_Babit2.Visible = True
                    'TR_Jml_Subsidi.Visible = True
                    TR_CatatanMKS.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub BindDDLAllocationType()
        With ddlAllocationType
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            .Items.Insert(1, New ListItem("Reguler", 0))
            .Items.Insert(2, New ListItem("Tambahan", 1))
        End With
        ddlAllocationType.SelectedIndex = 0
    End Sub

    Private Sub BindDDLAllocationBabit_Old()
        With ddlAllocationBabit
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))

            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            Dim arrDDL As ArrayList = New CategoryFacade(User).Retrieve(criterias, sortColl)
            Dim i% = 1
            For Each objCategory As Category In arrDDL
                .Items.Insert(i, New ListItem("BABIT " & objCategory.CategoryCode, objCategory.CategoryCode))
                i += 1
            Next
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitMasterPrice), "SpecialCategoryFlag", MatchType.Exact, 1))
            Dim arrDDL2 As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias2)
            Dim newArrDDL2 = From obj As BabitMasterPrice In arrDDL2
                                         Group By obj.SubCategoryVehicle.ID Into Group
                                    Select ID
            For Each id As Integer In newArrDDL2
                Dim obj As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CType(id, Short))
                .Items.Insert(i, New ListItem("BABIT " & obj.Name, obj.Name.Replace(" ", "_")))
                i += 1
            Next
        End With
        ddlAllocationBabit.SelectedIndex = 0
    End Sub

    Private Sub BindDDLAllocationBabit(ByVal _dealerID As Integer, ByVal ddlAllocationBabit As DropDownList)
        Dim strAllocationBabitValue As String = String.Empty
        Dim strAllocationBabitDesc As String = String.Empty
        With ddlAllocationBabit
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))

            Dim arrDDL As ArrayList = New BabitBudgetHeaderFacade(User).GetDataAllocationByDealer(_dealerID, ICPameranStart.Value)
            Dim allocationBabitList As Dictionary(Of String, String) = New Dictionary(Of String, String)

            For Each obj As BabitBudgetHeader In arrDDL
                If Not IsNothing(obj.Dealer) Then
                    If obj.SubCategoryVehicleID = 0 Then
                        strAllocationBabitValue = obj.Category.CategoryCode
                        strAllocationBabitDesc = obj.Category.CategoryCode
                    Else
                        strAllocationBabitValue = obj.SubCategoryVehicle.Name.Replace(" ", "_")
                        strAllocationBabitDesc = obj.SubCategoryVehicle.Name
                    End If
                Else
                    strAllocationBabitValue = obj.Description.Replace(" ", "_").ToUpper
                    strAllocationBabitDesc = obj.Description.ToUpper
                End If

                If Not allocationBabitList.ContainsKey(strAllocationBabitValue) Then
                    allocationBabitList.Add(strAllocationBabitValue, "BABIT " & strAllocationBabitDesc.ToUpper())
                End If
            Next

            Dim i% = 1
            For Each iKey As String In allocationBabitList.Keys
                Dim value As String = allocationBabitList(iKey)
                .Items.Insert(i, New ListItem(value, iKey))
                i += 1
            Next
        End With
        ddlAllocationBabit.SelectedIndex = 0
    End Sub

    Protected Sub ddlProvinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinsi.SelectedIndexChanged
        'BindddlCity()

        Dim _isSpecial As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objLoginUser.Dealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            _isSpecial = True
        End If

        BindddlCity(ddlProvinsi.SelectedValue, _isSpecial)
    End Sub

    Protected Sub hdnTemporaryOutlet_ValueChanged(sender As Object, e As EventArgs) Handles hdnTemporaryOutlet.ValueChanged
        Dim data As String() = hdnTemporaryOutlet.Value.Trim.Split(";")
        Dim DBF As DealerBranch = New DealerBranchFacade(User).Retrieve(data(0))

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(Area2), "ID", MatchType.Exact, DBF.Area2.ID))
        Dim oArea2 As ArrayList = New Area2Facade(User).Retrieve(crits)

        If oArea2.Count > 0 AndAlso data.Length >= 1 Then
            txtTemporaryOutlet.Text = data(0)
            lblArea.Text = CType(oArea2(0), Area2).Description
        End If

        lblNamaCabang.Text = DBF.Name
    End Sub

    Public Sub ddlFKategoriKendaraan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFKategoriKendaraan As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFKategoriKendaraan.Parent.Parent
        Dim ddlFModelKendaraan As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlFModelKendaraan = gridItem.FindControl("ddlEModelKendaraan")
        Else
            ddlFModelKendaraan = gridItem.FindControl("ddlFModelKendaraan")
        End If
        If ddlFKategoriKendaraan.SelectedIndex > 0 Then
            arrDDL = New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, ddlFKategoriKendaraan.SelectedValue))

            arrDDL = New SubCategoryVehicleFacade(User).Retrieve(criterias)

            ddlFModelKendaraan.DataSource = arrDDL
            ddlFModelKendaraan.DataTextField = "Name"
            ddlFModelKendaraan.DataValueField = "ID"
            ddlFModelKendaraan.DataBind()
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sessHelper.GetSession("sessDataUploadFile"), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim objPostedData As HttpPostedFile
                Dim objBabitDocument As BabitDocument = New BabitDocument
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindGridEventUploadFile()
                        Return
                    End If

                    If objPostedData.ContentLength > 5120000 Then
                        MessageBox.Show("Batas maximum ukuran file yang boleh di upload adalah 5MB.")
                        BindGridEventUploadFile()
                        Return
                    End If

                    Dim ext As String = System.IO.Path.GetExtension(sFileName)
                    If ext <> ".pdf" AndAlso ext <> ".jpg" AndAlso ext <> ".jpeg" Then
                        MessageBox.Show("File yang boleh di upload adalah PDF, JPG, atau JPEG.")
                        BindGridEventUploadFile()
                        Return
                    End If

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\BABIT\" & objDealer.DealerCode & "\Pameran\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitDocument.BabitHeader = New BabitHeader()
                        objBabitDocument.AttachmentData = objPostedData
                        objBabitDocument.FileName = sFileName
                        objBabitDocument.Path = strDestFile
                        objBabitDocument.FileDescription = IIf(txtKeterangan.Text.Trim = String.Empty, "Babit Pameran Dokumen", txtKeterangan.Text.Trim)

                        UploadAttachment(objBabitDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitDocument)
                        sessHelper.SetSession("sessDataUploadFile", _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitDocument As BabitDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitDocument)
                If oBabitDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession("sessDelDataUploadFile"), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitDocument)
                    sessHelper.SetSession("sessDelDataUploadFile", arrDelete)
                End If

                RemoveBabitAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindGridEventUploadFile()
    End Sub

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)

            Dim arrUpload As ArrayList = CType(sessHelper.GetSession("sessDataUploadFile"), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objBabitDocument As BabitDocument = arrUpload(e.Item.ItemIndex)

                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objBabitDocument.FileName)
            End If
        End If
    End Sub

    Private Sub dgBabitPameran_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitPameran.ItemCommand
        Dim oBabitParameterDetail As New BabitParameterDetail
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim txtEItem As TextBox
        Dim txtFItem As TextBox
        Dim txtEQty As TextBox
        Dim txtFQty As TextBox
        Dim txtEPrice As TextBox
        Dim txtFPrice As TextBox
        Dim txtETotalPrice As TextBox
        Dim txtFTotalPrice As TextBox
        Dim txtEDesc As TextBox
        Dim txtFDesc As TextBox

        objBabitHeader = CType(Session("sessBabitHeader"), BabitHeader)
        arlEvent = CType(Session("sessBabitPameranExpense"), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
                ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)
                txtFItem = CType(e.Item.FindControl("txtFItem"), TextBox)
                txtFQty = CType(e.Item.FindControl("txtFQty"), TextBox)
                txtFPrice = CType(e.Item.FindControl("txtFPrice"), TextBox)
                txtFTotalPrice = CType(e.Item.FindControl("txtFTotalPrice"), TextBox)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)

                If ddlFCategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Event Kategori harus diisi.")
                    Return
                End If
                If ddlFJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Event harus diisi.")
                    Return
                End If
                If txtFItem.Text.Trim = "" Then
                    MessageBox.Show("Item Event harus diisi.")
                    Return
                End If
                If txtFQty.Text.Trim = "" OrElse txtFQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtFPrice.Text.Trim = "" OrElse txtFPrice.Text.Trim = "0" Then
                    MessageBox.Show("Price Event harus diisi.")
                    Return
                End If
                txtFTotalPrice.Text = txtFQty.Text * txtFPrice.Text()

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlFJenisBabitEvent.SelectedValue))

                Dim oBabitPameranExpense As New BabitPameranExpense
                oBabitPameranExpense.BabitHeader = New BabitHeader
                oBabitPameranExpense.BabitParameterDetail = oBabitParameterDetail
                oBabitPameranExpense.Item = txtFItem.Text.Trim
                oBabitPameranExpense.Qty = txtFQty.Text.Trim
                oBabitPameranExpense.Price = txtFPrice.Text.Trim
                oBabitPameranExpense.TotalPrice = (oBabitPameranExpense.Qty * oBabitPameranExpense.Price)
                oBabitPameranExpense.Description = txtFDesc.Text.Trim
                arlEvent.Add(oBabitPameranExpense)

            Case "save" 'Update this datagrid item   
                ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
                ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
                txtEItem = CType(e.Item.FindControl("txtEItem"), TextBox)
                txtEQty = CType(e.Item.FindControl("txtEQty"), TextBox)
                txtEPrice = CType(e.Item.FindControl("txtEPrice"), TextBox)
                txtETotalPrice = CType(e.Item.FindControl("txtETotalPrice"), TextBox)
                txtEDesc = CType(e.Item.FindControl("txtEDesc"), TextBox)

                If ddlECategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Event Kategori harus diisi.")
                    Return
                End If
                If ddlEJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Event harus diisi.")
                    Return
                End If
                If txtEItem.Text.Trim = "" Then
                    MessageBox.Show("Item Event harus diisi.")
                    Return
                End If
                If txtEQty.Text.Trim = "" OrElse txtEQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtEPrice.Text.Trim = "" OrElse txtEPrice.Text.Trim = "0" Then
                    MessageBox.Show("Price Event harus diisi.")
                    Return
                End If
                txtETotalPrice.Text = txtEQty.Text * txtEPrice.Text()

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlEJenisBabitEvent.SelectedValue))
                Dim oBabitPameranExpense As BabitPameranExpense = CType(arlEvent(e.Item.ItemIndex), BabitPameranExpense)
                oBabitPameranExpense.BabitHeader = objBabitHeader
                oBabitPameranExpense.BabitParameterDetail = oBabitParameterDetail
                oBabitPameranExpense.Item = txtEItem.Text.Trim
                oBabitPameranExpense.Qty = txtEQty.Text.Trim
                oBabitPameranExpense.Price = txtEPrice.Text.Trim
                oBabitPameranExpense.Description = txtEDesc.Text.Trim()

                dgBabitPameran.EditItemIndex = -1
                dgBabitPameran.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitPameran.ShowFooter = False
                dgBabitPameran.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oBabitPameranExpense As BabitPameranExpense = CType(arlEvent(e.Item.ItemIndex), BabitPameranExpense)
                    If oBabitPameranExpense.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession("sessDelBabitPameranExpense"), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitPameranExpense)
                        sessHelper.SetSession("sessDelBabitPameranExpense", arrDelete)
                    End If
                    arlEvent.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitPameran.EditItemIndex = -1
                dgBabitPameran.ShowFooter = True

        End Select

        sessHelper.SetSession("sessBabitPameranExpense", arlEvent)
        BindGridBabitEvent()
    End Sub

    Private Sub dgBabitPameran_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitPameran.ItemDataBound
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim lblItem As Label
        Dim lblQty As Label
        Dim lblPrice As Label
        Dim lblTotalPrice As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
            ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)

            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact,
                                         CommonFunction.GetStandardCodeValueID("EnumBabit.BabitParameterCategory", "Biaya")))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)

            With ddlFCategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With

            ddlFJenisBabitEvent.Items.Clear()
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBED As BabitPameranExpense = CType(e.Item.DataItem, BabitPameranExpense)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBED.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            'e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitPameran.CurrentPageIndex * dgBabitPameran.PageSize)
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitPameran.CurrentPageIndex * dgBabitPameran.PageSize)

            lblCategoryBabitEvent = CType(e.Item.FindControl("lblCategoryBabitEvent"), Label)
            lblJenisBabitEvent = CType(e.Item.FindControl("lblJenisBabitEvent"), Label)
            lblItem = CType(e.Item.FindControl("lblItem"), Label)
            lblQty = CType(e.Item.FindControl("lblQty"), Label)
            lblPrice = CType(e.Item.FindControl("lblPrice"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lblTotalPrice = CType(e.Item.FindControl("lblTotalPrice"), Label)

            lblCategoryBabitEvent.Text = oBED.BabitParameterDetail.BabitParameterHeader.ParameterName()
            lblJenisBabitEvent.Text = oBED.BabitParameterDetail.ParameterDetailName()
            lblQty.Text = oBED.Qty
            lblPrice.Text = oBED.Price.ToString("#,##0")

            If lblItem.Text.Trim.ToLower = "total biaya :" Then
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(6).BackColor = Color.SkyBlue
                lblItem.Font.Bold = True
                lblTotalPrice.Font.Bold = True
                lblJenisBabitEvent.Text = ""
                lblQty.Text = ""
                lblPrice.Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                lblCategoryBabitEvent.Attributes("style") = "display:none"
                e.Item.Cells(0).Text = ""
            Else
                lblTotalPrice.Text = Format(oBED.Qty * oBED.Price, "###,###,##0")
                lblItem.Font.Bold = False
                lblTotalPrice.Font.Bold = False
                lbtnEdit.Attributes("style") = "display:table-row"
                lbtnDelete.Attributes("style") = "display:table-row"
                lblCategoryBabitEvent.Attributes("style") = "display:table-row"
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBED As BabitPameranExpense = CType(e.Item.DataItem, BabitPameranExpense)

            ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact,
                                         CommonFunction.GetStandardCodeValueID("EnumBabit.BabitParameterCategory", "Biaya")))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)
            With ddlECategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBED.BabitParameterDetail.BabitParameterHeader.ID
            End With

            ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, oBED.BabitParameterDetail.BabitParameterHeader.ID))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)
            With ddlEJenisBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBED.BabitParameterDetail.ID
            End With
        End If
    End Sub

    Public Sub ddlFCategoryBabitEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFCategoryBabitEvent As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFCategoryBabitEvent.Parent.Parent()
        Dim ddlFJenisBabitEvent As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlFJenisBabitEvent = gridItem.FindControl("ddlEJenisBabitEvent")
        Else
            ddlFJenisBabitEvent = gridItem.FindControl("ddlFJenisBabitEvent")
        End If
        ddlFJenisBabitEvent.Items.Clear()
        If ddlFCategoryBabitEvent.SelectedIndex > 0 Then
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, ddlFCategoryBabitEvent.SelectedValue))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)

            ddlFJenisBabitEvent.DataSource = arrDDL
            ddlFJenisBabitEvent.DataTextField = "ParameterDetailName"
            ddlFJenisBabitEvent.DataValueField = "ID"
            ddlFJenisBabitEvent.DataBind()
            ddlFJenisBabitEvent.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlFJenisBabitEvent.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If
    End Sub

    Protected Sub dgDisplayAndTarget_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDisplayAndTarget.ItemDataBound
        Dim ddlFKategoriKendaraan As DropDownList
        Dim ddlFModelKendaraan As DropDownList
        Dim ddlEKategoriKendaraan As DropDownList
        Dim ddlEModelKendaraan As DropDownList
        Dim lblKategoriKendaraan As Label
        Dim lblModelKendaraan As Label
        Dim lblQtyDisplay As Label
        Dim lblTargetPenjualan As Label
        Dim lblTestDrive As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton
        Dim CBETestDrive As CheckBox

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFKategoriKendaraan = CType(e.Item.FindControl("ddlFKategoriKendaraan"), DropDownList)
            ddlFModelKendaraan = CType(e.Item.FindControl("ddlFModelKendaraan"), DropDownList)

            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            arrDDL = New CategoryFacade(User).Retrieve(criterias)

            With ddlFKategoriKendaraan
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "CategoryCode"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With

            ddlFModelKendaraan.Items.Clear()
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBDC As BabitDisplayCar = CType(e.Item.DataItem, BabitDisplayCar)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgDisplayAndTarget.CurrentPageIndex * dgDisplayAndTarget.PageSize)

            lblKategoriKendaraan = CType(e.Item.FindControl("lblKategoriKendaraan"), Label)
            lblModelKendaraan = CType(e.Item.FindControl("lblModelKendaraan"), Label)
            lblQtyDisplay = CType(e.Item.FindControl("lblQtyDisplay"), Label)
            lblTargetPenjualan = CType(e.Item.FindControl("lblTargetPenjualan"), Label)
            lblTestDrive = CType(e.Item.FindControl("lblTestDrive"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lblKategoriKendaraan.Text = oBDC.SubCategoryVehicle.Category.CategoryCode
            lblModelKendaraan.Text = oBDC.SubCategoryVehicle.Name
            lblQtyDisplay.Text = oBDC.Qty.ToString
            lblTargetPenjualan.Text = Format(oBDC.SalesTarget, "#,##0")
            If oBDC.IsTestDrive Then
                lblTestDrive.Text = "Ya"
            Else
                lblTestDrive.Text = "Tidak"
            End If
            lbtnEdit.Attributes("style") = "display:table-row"
            lbtnDelete.Attributes("style") = "display:table-row"
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBDC As BabitDisplayCar = CType(e.Item.DataItem, BabitDisplayCar)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitDisplayCarID = oBDC.ID
            Else
                If intBabitDisplayCarID <> oBDC.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitDisplayCarID = oBDC.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgDisplayAndTarget.CurrentPageIndex * dgDisplayAndTarget.PageSize)

            ddlEKategoriKendaraan = CType(e.Item.FindControl("ddlEKategoriKendaraan"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            arrDDL = New CategoryFacade(User).Retrieve(criterias)

            With ddlEKategoriKendaraan
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "CategoryCode"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBDC.SubCategoryVehicle.Category.ID
            End With

            ddlEModelKendaraan = CType(e.Item.FindControl("ddlEModelKendaraan"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, ddlEKategoriKendaraan.SelectedValue))

            arrDDL = New SubCategoryVehicleFacade(User).Retrieve(criterias2)
            With ddlEModelKendaraan
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "Name"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBDC.SubCategoryVehicle.ID
            End With

            CBETestDrive = CType(e.Item.FindControl("CBETestDrive"), CheckBox)
            If oBDC.IsTestDrive Then
                CBETestDrive.Checked = True
            End If
        End If
    End Sub

    Protected Sub dgDisplayAndTarget_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDisplayAndTarget.ItemCommand
        Dim ddlFKategoriKendaraan As DropDownList
        Dim ddlEKategoriKendaraan As DropDownList
        Dim lblKategoriKendaraan As Label

        Dim ddlFModelKendaraan As DropDownList
        Dim ddlEModelKendaraan As DropDownList
        Dim lblModelKendaraan As Label

        Dim txtFQtyDisplay As TextBox
        Dim txtEQtyDisplay As TextBox

        Dim txtFTargetPenjualan As TextBox
        Dim txtETargetPenjualan As TextBox

        Dim CBFTestDrive As CheckBox
        Dim CBETestDrive As CheckBox

        Dim oSubCategoryVehicle As New SubCategoryVehicle

        arlDisplayAndTarget = CType(Session("sessBabitPameranDisplayTarget"), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFKategoriKendaraan = CType(e.Item.FindControl("ddlFKategoriKendaraan"), DropDownList)
                ddlFModelKendaraan = CType(e.Item.FindControl("ddlFModelKendaraan"), DropDownList)
                txtFQtyDisplay = CType(e.Item.FindControl("txtFQtyDisplay"), TextBox)
                txtFTargetPenjualan = CType(e.Item.FindControl("txtFTargetPenjualan"), TextBox)
                CBFTestDrive = CType(e.Item.FindControl("CBFTestDrive"), CheckBox)

                If ddlFKategoriKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Kategori Kendaraan harus dipilih.")
                    Return
                End If
                If ddlFModelKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Model Kendaraan harus dipilih.")
                    Return
                End If
                If txtFQtyDisplay.Text = String.Empty OrElse txtFQtyDisplay.Text.Trim = "0" Then
                    MessageBox.Show("Qty Display harus diisi dan harus lebih dari 0")
                    Return
                End If
                If txtFTargetPenjualan.Text = String.Empty OrElse txtFTargetPenjualan.Text.Trim = "0" Then
                    MessageBox.Show("Target Penjualan harus diisi dan harus lebih dari 0.")
                    Return
                End If

                oSubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CShort(ddlFModelKendaraan.SelectedValue))

                Dim isSingleCategory As Boolean = True
                For Each arItem As BabitDisplayCar In arlDisplayAndTarget
                    If arItem.SubCategoryVehicle.Category.ID <> oSubCategoryVehicle.Category.ID Then
                        isSingleCategory = False
                    End If
                Next

                If Not isSingleCategory Then
                    MessageBox.Show("Hanya boleh satu jenis kategori dalam satu pameran")
                    Return
                End If

                Dim oBabitDisplayCar As New BabitDisplayCar
                oBabitDisplayCar.BabitHeader = New BabitHeader
                oBabitDisplayCar.SubCategoryVehicle = oSubCategoryVehicle
                oBabitDisplayCar.Qty = CInt(txtFQtyDisplay.Text.Trim)
                oBabitDisplayCar.SalesTarget = CInt(txtFTargetPenjualan.Text.Trim)
                If CBFTestDrive.Checked Then
                    oBabitDisplayCar.IsTestDrive = True
                Else
                    oBabitDisplayCar.IsTestDrive = False
                End If
                arlDisplayAndTarget.Add(oBabitDisplayCar)
            Case "save"
                ddlEKategoriKendaraan = CType(e.Item.FindControl("ddlEKategoriKendaraan"), DropDownList)
                ddlEModelKendaraan = CType(e.Item.FindControl("ddlEModelKendaraan"), DropDownList)
                txtEQtyDisplay = CType(e.Item.FindControl("txtEQtyDisplay"), TextBox)
                txtETargetPenjualan = CType(e.Item.FindControl("txtETargetPenjualan"), TextBox)
                CBETestDrive = CType(e.Item.FindControl("CBETestDrive"), CheckBox)

                If ddlEKategoriKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Kategori Kendaraan harus dipilih.")
                    Return
                End If
                If ddlEModelKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Model Kendaraan harus dipilih.")
                    Return
                End If
                If txtEQtyDisplay.Text = String.Empty OrElse txtEQtyDisplay.Text.Trim = "0" Then
                    MessageBox.Show("Qty Display harus diisi dan harus lebih dari 0")
                    Return
                End If
                If txtETargetPenjualan.Text = String.Empty OrElse txtETargetPenjualan.Text.Trim = "0" Then
                    MessageBox.Show("Target Penjualan harus diisi dan harus lebih dari 0.")
                    Return
                End If

                oSubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CShort(ddlEModelKendaraan.SelectedValue))

                Dim oBabitDisplayCar As BabitDisplayCar = CType(arlDisplayAndTarget(e.Item.ItemIndex), BabitDisplayCar)
                oBabitDisplayCar.BabitHeader = New BabitHeader
                oBabitDisplayCar.SubCategoryVehicle = oSubCategoryVehicle
                oBabitDisplayCar.Qty = CInt(txtEQtyDisplay.Text.Trim)
                oBabitDisplayCar.SalesTarget = CInt(txtETargetPenjualan.Text.Trim)
                If CBETestDrive.Checked Then
                    oBabitDisplayCar.IsTestDrive = True
                Else
                    oBabitDisplayCar.IsTestDrive = False
                End If
                dgDisplayAndTarget.EditItemIndex = -1
                dgDisplayAndTarget.ShowFooter = True
            Case "edit"
                dgDisplayAndTarget.ShowFooter = False
                dgDisplayAndTarget.EditItemIndex = e.Item.ItemIndex
            Case "delete"
                Try
                    Dim oBabitDisplayCar As BabitDisplayCar = CType(arlDisplayAndTarget(e.Item.ItemIndex), BabitDisplayCar)
                    If oBabitDisplayCar.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession("sessDelBabitPameranDisplayTarget"), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitDisplayCar)
                        sessHelper.SetSession("sessDelBabitPameranDisplayTarget", arrDelete)
                    End If
                    arlDisplayAndTarget.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel"
                dgDisplayAndTarget.EditItemIndex = -1
                dgDisplayAndTarget.ShowFooter = True
        End Select

        sessHelper.SetSession("sessBabitPameranDisplayTarget", arlDisplayAndTarget)
        BindGridDisplayAndTarget()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If
        If Me.txtBabitDealerGroup.Text.Trim <> "" Then
            If Not ValidateBabitDealerGroup(Me.txtBabitDealerGroup.Text.Trim) Then Exit Sub
        End If

        arlEvent = CType(sessHelper.GetSession("sessBabitPameranExpense"), ArrayList)
        If IsNothing(arlEvent) Then arlEvent = New ArrayList

        'Remove row SubTotal
        For i As Integer = 0 To arlEvent.Count - 1
            Dim objEvent As BabitPameranExpense = CType(arlEvent(i), BabitPameranExpense)
            If objEvent.Item.ToLower = "total biaya :" Then
                arlEvent.RemoveAt(i)
                i -= 1
            End If
            If i = arlEvent.Count - 1 Then
                Exit For
            End If
        Next

        Dim _arrBabitDealerAlloc As ArrayList = CType(sessHelper.GetSession("sessionAlloc"), ArrayList)
        If IsNothing(_arrBabitDealerAlloc) Then _arrBabitDealerAlloc = New ArrayList

        Dim _arrBabitDocs As ArrayList = CType(sessHelper.GetSession("sessDataUploadFile"), ArrayList)
        Dim _babitHeader As New BabitHeader
        If Mode = "Edit" Then
            _babitHeader = oHeader
            _babitHeader.Dealer = oHeader.Dealer
        ElseIf Mode = "New" Then
            _babitHeader.BabitRegNumber = GetRegNumber()
            _babitHeader.Dealer = objDealer
            _babitHeader.BabitStatus = 0  '--- Status Baru
        End If

        If txtTemporaryOutlet.Text <> String.Empty Then
            _babitHeader.DealerBranch = New DealerBranchFacade(User).Retrieve(txtTemporaryOutlet.Text)
        End If
        '_babitHeader.BabitType = "P"
        _babitHeader.BabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(strTypeCode)

        _babitHeader.BabitDealerNumber = txtNomorSurat.Text
        _babitHeader.AllocationType = CommonFunction.GetStandardCodeValueID("EnumBabit.BabitAllocationType", "Alokasi_Reguler") 'ddlAllocationType.SelectedValue
        _babitHeader.PeriodStart = ICPameranStart.Value
        _babitHeader.PeriodEnd = ICPameranEnd.Value
        _babitHeader.MarboxID = IIf(ddlMarBox.SelectedIndex = 0, "NULL", ddlMarBox.SelectedValue)

        If txtOtherLocation.Visible Then
            _babitHeader.Location = txtOtherLocation.Text
            _babitHeader.City = New CityFacade(User).Retrieve(CInt(ddlKota.SelectedValue))
            _babitHeader.BabitMasterLocation = Nothing
        Else
            _babitHeader.Location = txtLocation.Text
            Dim MasterLocID As Integer = GetBabitMasterLocation(txtLocation.Text)
            Dim oBMasterLocation As BabitMasterLocation = New BabitMasterLocationFacade(User).Retrieve(MasterLocID)
            _babitHeader.BabitMasterLocation = oBMasterLocation
            _babitHeader.City = oBMasterLocation.City
        End If
        _babitHeader.ProspectTarget = CInt(txtTargetProspek.Text)
        _babitHeader.LuasArea = CInt(txtLuasPameran.Text)
        _babitHeader.Notes = txtNotes.Text

        Dim strDealerID As String = String.Empty
        If txtBabitDealerGroup.Text.Trim() <> "" Then
            Dim BabitDealerGroupCode As String() = txtBabitDealerGroup.Text.Split(";")
            For Each dealerCode As String In BabitDealerGroupCode
                Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                    If strDealerID = String.Empty Then
                        strDealerID = CStr(oDealer.ID)
                    Else
                        strDealerID += ";" & CStr(oDealer.ID)
                    End If
                End If
            Next
        End If
        _babitHeader.BabitDealerGroup = strDealerID

        Dim _babitDealerAllocation As BabitDealerAllocation
        If Mode = "Edit" Then
            If Not IsLoginAsDealer() Then
                'Dim dblBiaya As Double = 0
                'Dim dblSubsidyAmount As Double = 0
                'If txtSubsidyAmount.Text.Trim = "" Then txtSubsidyAmount.Text = 0
                'dblSubsidyAmount = txtSubsidyAmount.Text
                'If dblSubsidyAmount = 0 Then
                '    For Each bed As BabitPameranExpense In arlEvent
                '        dblBiaya += CDbl(bed.Price) * CInt(bed.Qty)
                '    Next
                '    If dblBiaya > 0 Then
                '        dblSubsidyAmount = (dblBiaya * 0.5)
                '    End If
                'End If

                'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, _babitHeader.ID))
                'Dim arr As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(criterias)
                'If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                '    _babitDealerAllocation = CType(arr(0), BabitDealerAllocation)
                'Else
                '    _babitDealerAllocation = New BabitDealerAllocation
                'End If

                'txtSubsidyAmount.Text = dblSubsidyAmount
                '_babitDealerAllocation.BabitHeader = _babitHeader
                '_babitDealerAllocation.BabitCategory = IIf(ddlAllocationBabit.SelectedIndex = 0, "", ddlAllocationBabit.SelectedValue)
                '_babitDealerAllocation.SubsidyAmount = dblSubsidyAmount

            End If
        End If

        Dim iMP As Integer = 0
        For Each cb As ListItem In CBMaterialPromosi.Items
            iMP = iMP + 1
            If cb.Selected Then
                Dim PD As BabitPameranDetail = New BabitPameranDetail
                PD.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(cb.Value))
                PD.BabitParameterHeader = PD.BabitParameterDetail.BabitParameterHeader
                If iMP = CBMaterialPromosi.Items.Count Then
                    If txtOtherMaterialPromosi.Text <> String.Empty Then
                        PD.Notes = txtOtherMaterialPromosi.Text
                    End If
                End If
                arlPameranDetail.Add(PD)
            End If
        Next

        Dim iPP As Integer = 0
        For Each cbp As ListItem In CBProfilPengunjung.Items
            iPP = iPP + 1
            If cbp.Selected Then
                Dim PD As BabitPameranDetail = New BabitPameranDetail
                PD.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(cbp.Value))
                PD.BabitParameterHeader = PD.BabitParameterDetail.BabitParameterHeader
                If iPP = CBProfilPengunjung.Items.Count Then
                    If txtOtherProfilPengunjung.Text <> String.Empty Then
                        PD.Notes = txtOtherProfilPengunjung.Text
                    End If
                End If
                arlPameranDetail.Add(PD)
            End If
        Next

        For Each cbl As ListItem In CBLokasiPameran.Items
            If cbl.Selected Then
                Dim PD As BabitPameranDetail = New BabitPameranDetail
                PD.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(cbl.Value))
                PD.BabitParameterHeader = PD.BabitParameterDetail.BabitParameterHeader
                arlPameranDetail.Add(PD)
            End If
        Next

        Dim _arlDisplayAndTarget As ArrayList = CType(sessHelper.GetSession("sessBabitPameranDisplayTarget"), ArrayList)
        Dim _result As Integer
        If Mode = "New" Then
            _result = New BabitPameranDetailFacade(User).InsertTransaction(_babitHeader, arlPameranDetail, arlEvent, _arrBabitDocs, _arlDisplayAndTarget, _arrBabitDealerAlloc)
        Else
            Dim arlDelDisplayAndTarget As ArrayList = CType(sessHelper.GetSession("sessDelBabitPameranDisplayTarget"), ArrayList)
            Dim arlDelEvent As ArrayList = CType(sessHelper.GetSession("sessDelBabitPameranExpense"), ArrayList)
            Dim arlDelDocument As ArrayList = CType(sessHelper.GetSession("sessDelDataUploadFile"), ArrayList)
            Dim _arrDelBabitDealerAlloc As ArrayList = CType(sessHelper.GetSession("sessDelAlloc"), ArrayList)
            If IsNothing(_arrDelBabitDealerAlloc) Then _arrDelBabitDealerAlloc = New ArrayList

            _result = New BabitPameranDetailFacade(User).UpdateTransaction(_babitHeader, arlPameranDetail, arlEvent, arlDelEvent, _arrBabitDocs, arlDelDocument, _arlDisplayAndTarget, arlDelDisplayAndTarget, _arrBabitDealerAlloc, _arrDelBabitDealerAlloc)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            CommitAttachment(_arrBabitDocs)
            If Request.QueryString("Mode") = "Edit" Then
                If Not IsNothing(arlDelDocument) Then
                    RemoveBabitDocumentAttachment(arlDelDocument, TargetDirectory)
                End If
            End If
            ClearTempData()
            ClearAll()

            'MessageBox.Show("Simpan Data Berhasil !")
            'Server.Transfer("~/Babit/FrmInputBabitPameran.aspx")
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmBabitList.aspx?Back=OK';"
        Else
            'MessageBox.Show("Simpan Data Gagal")
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub RemoveBabitDocumentAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitDocument In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub HFLocation_ValueChanged(sender As Object, e As EventArgs) Handles HFLocation.ValueChanged
        Dim data As String() = HFLocation.Value.Trim.Split(";")

        If data.Length >= 1 Then
            txtLocation.Text = data(1)
        End If
    End Sub

    Protected Sub ddlLocationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationType.SelectedIndexChanged
        LocationTypeControl()
    End Sub

    Private Sub LocationTypeControl()
        If ddlLocationType.SelectedValue = 2 Then
            txtOtherLocation.Visible = True
            txtLocation.Visible = False
            lblPULocation.Visible = False
            lblProvinsi.Visible = True
            ddlProvinsi.Visible = True
            lblProvinsiTitik2.Visible = True
            lblKota.Visible = True
            ddlKota.Visible = True
            lblKotaTitik2.Visible = True
        ElseIf ddlLocationType.SelectedValue = -1 Then
            txtOtherLocation.Visible = False
            txtLocation.Visible = False
            txtOtherLocation.Text = ""
            txtLocation.Text = ""
            lblPULocation.Visible = False
            lblProvinsi.Visible = False
            ddlProvinsi.Visible = False
            ddlProvinsi.SelectedIndex = 0
            lblProvinsiTitik2.Visible = False
            lblKota.Visible = False
            ddlKota.Visible = False
            lblKotaTitik2.Visible = False
        Else
            txtOtherLocation.Visible = False
            txtLocation.Visible = True
            txtOtherLocation.Text = ""
            lblPULocation.Visible = True
            lblProvinsi.Visible = False
            ddlProvinsi.Visible = False
            ddlProvinsi.SelectedIndex = 0
            lblProvinsiTitik2.Visible = False
            lblKota.Visible = False
            ddlKota.Visible = False
            lblKotaTitik2.Visible = False
        End If
    End Sub

    Protected Sub CBMaterialPromosi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBMaterialPromosi.SelectedIndexChanged
        For Each cbi As ListItem In CBMaterialPromosi.Items
            If cbi.Text.Trim.ToUpper = "LAINNYA" Then
                If cbi.Selected Then
                    txtOtherMaterialPromosi.Enabled = True
                Else
                    txtOtherMaterialPromosi.Enabled = False
                    txtOtherMaterialPromosi.Text = String.Empty
                End If
            End If
        Next
    End Sub

    Protected Sub CBProfilPengunjung_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBProfilPengunjung.SelectedIndexChanged
        For Each cbi As ListItem In CBProfilPengunjung.Items
            If cbi.Text.Trim.ToUpper = "LAINNYA" Then
                If cbi.Selected Then
                    txtOtherProfilPengunjung.Enabled = True
                Else
                    txtOtherProfilPengunjung.Enabled = False
                    txtOtherProfilPengunjung.Text = String.Empty
                End If
            End If
        Next
    End Sub
#End Region

#Region "Custom Method"
    'Private Sub BindddlAllocationType()
    '    ddlAllocationType.Items.Clear()
    '    ddlAllocationType.Items.Add(New ListItem("Silahkan Pilih", -1))
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitAllocationType"))

    '    Dim arlAllocationType As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
    '    For Each sc As StandardCode In arlAllocationType
    '        ddlAllocationType.Items.Add(New ListItem(sc.ValueDesc, sc.ValueId))
    '    Next
    'End Sub

    Private Sub BindddlProvince()
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlProvinsi.Items.Clear()
        ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

        If CType(sessHelper.GetSession("DEALER"), Dealer).Title <> EnumDealerTittle.DealerTittle.KTB Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objLoginUser.Dealer.City.ID))
            criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
            If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                For Each prov As BabitSpecialCity In arlBabitSpecialCity
                    ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
                Next
                If arlBabitSpecialCity.Count = 1 Then
                    ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                    BindddlCity(Me.ddlProvinsi.SelectedValue, True)
                End If
            Else
                Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & objLoginUser.Dealer.City.ID
                criterias2.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSQL & ")"))
                Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias2)
                If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                    For Each prov As Province In arlProvince
                        ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                    Next
                    If arlProvince.Count = 1 Then
                        ddlProvinsi.SelectedValue = arlProvince(0).ID()
                        BindddlCity(ddlProvinsi.SelectedValue, False)
                    End If
                End If
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & objLoginUser.Dealer.City.ID
            'criterias2.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                For Each prov As Province In arlProvince
                    ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                Next
                If arlProvince.Count = 1 Then
                    ddlProvinsi.SelectedValue = arlProvince(0).ID()
                    BindddlCity(ddlProvinsi.SelectedValue, False)
                End If
            End If
        End If
    End Sub

    Private Sub BindddlCity(ProvinceID As Integer, _isSpecial As Boolean)
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))

        If IsLoginAsDealer() Then
            If _isSpecial Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "BabitSpecialProvince.ID", MatchType.Exact, ProvinceID))
                criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
                Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
                If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                    For Each c As BabitSpecialCity In arlBabitSpecialCity
                        ddlKota.Items.Add(New ListItem(c.City.CityName, c.City.ID))
                    Next
                End If
            Else
                Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ProvinceID))
                Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias2)
                If Not IsNothing(arlCity) AndAlso arlCity.Count > 0 Then
                    For Each c As City In arlCity
                        ddlKota.Items.Add(New ListItem(c.CityName, c.ID))
                    Next
                End If
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlCity) AndAlso arlCity.Count > 0 Then
                For Each c As City In arlCity
                    ddlKota.Items.Add(New ListItem(c.CityName, c.ID))
                Next
            End If
        End If
    End Sub

    Private Sub BindGridAlloc()
        If Not IsNothing(CType(sessHelper.GetSession("sessionAlloc"), ArrayList)) Then
            arlAlloc = CType(sessHelper.GetSession("sessionAlloc"), ArrayList)
        Else
            arlAlloc = New ArrayList
        End If
        dgAlloc.DataSource = arlAlloc
        dgAlloc.DataBind()
    End Sub

    Private Sub BindGridDisplayAndTarget()
        If Not IsNothing(CType(sessHelper.GetSession("sessBabitPameranDisplayTarget"), ArrayList)) Then
            arlDisplayAndTarget = CType(sessHelper.GetSession("sessBabitPameranDisplayTarget"), ArrayList)
        Else
            arlDisplayAndTarget = New ArrayList
        End If

        dgDisplayAndTarget.DataSource = arlDisplayAndTarget
        dgDisplayAndTarget.DataBind()
        sessHelper.SetSession("sessBabitPameranDisplayTarget", arlDisplayAndTarget)
    End Sub

    Private Sub Initialization()
        Me.ViewState.Add(Me._vstICPameranStart, Me.ICPameranStart.Value)
        Me.ViewState.Add(Me._vstICPameranEnd, Me.ICPameranEnd.Value)
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ScriptPP", "BindCBProfilPengunjungEvent();", True)
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "BindCBMaterialPromosiEvent();", True)
    End Sub

    Private Sub CalculatePeriodePameran()
        Dim TotalPeriode As Integer = 0
        If ICPameranEnd.Value < ICPameranStart.Value Then
            MessageBox.Show("Tanggal Pameran selesai tidak boleh kurang dari tanggal pameran dimulai")
            Exit Sub
        End If
        TotalPeriode = (ICPameranEnd.Value - ICPameranStart.Value).TotalDays + 1
        lblPeriodePameran.Text = TotalPeriode.ToString() & " Hari"
    End Sub

    Sub BindGridEventUploadFile()
        arlDocument = CType(sessHelper.GetSession("sessDataUploadFile"), ArrayList)
        If IsNothing(arlDocument) Then arlDocument = New ArrayList
        dgUploadFile.DataSource = arlDocument
        dgUploadFile.DataBind()
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As BabitDocument In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Private Sub UploadAttachment(ByVal ObjAttachment As BabitDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.Path) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.Path)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)

                    imp.StopImpersonate()
                    imp = Nothing
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RemoveBabitAttachment(ByVal ObjAttachment As BabitDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Input_Pameran_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT BABIT PAMERAN")
        End If
    End Sub

    Sub BindGridBabitEvent()
        arlEvent = CType(sessHelper.GetSession("sessBabitPameranExpense"), ArrayList)
        If IsNothing(arlEvent) Then
            arlEvent = GetArrayGridEvent(hdnBabitHeaderID.Value)
        End If

        Dim dataList As ArrayList = New ArrayList
        dataList = New System.Collections.ArrayList(
                        (From obj As BabitPameranExpense In arlEvent.OfType(Of BabitPameranExpense)()
                            Where obj.Item <> "Total Biaya :"
                            Select obj).ToList())

        CommonFunction.SortListControl(dataList, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        Dim blnFindTotalBiaya As Boolean = False
        Dim objBabitParamDtl As New BabitParameterDetail
        Dim oBabitPameranExpense As New BabitPameranExpense
        Dim intBabitParameterHeaderID As Integer = 0

        Dim arlEvent2 As ArrayList = New ArrayList
        For i As Integer = 0 To dataList.Count - 1
            Dim objBabitEvent As BabitPameranExpense = CType(dataList(i), BabitPameranExpense)
            If i = 0 Then
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If
            If intBabitParameterHeaderID <> objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID Then
                oBabitPameranExpense = New BabitPameranExpense
                oBabitPameranExpense.BabitHeader = New BabitHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.Status", MatchType.Exact, "1"))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitPameranExpense.BabitParameterDetail = objBabitParamDtl
                oBabitPameranExpense.Item = "Total Biaya :"
                oBabitPameranExpense.TotalPrice = GetTotalPriceByCategory(intBabitParameterHeaderID)
                arlEvent2.Add(oBabitPameranExpense)
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If

            arlEvent2.Add(objBabitEvent)
            If i = dataList.Count - 1 Then
                oBabitPameranExpense = New BabitPameranExpense
                oBabitPameranExpense.BabitHeader = New BabitHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.Status", MatchType.Exact, "1"))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitPameranExpense.BabitParameterDetail = objBabitParamDtl
                oBabitPameranExpense.Item = "Total Biaya :"
                oBabitPameranExpense.TotalPrice = GetTotalPriceByCategory(intBabitParameterHeaderID)
                arlEvent2.Add(oBabitPameranExpense)
            End If
        Next

        If Mode <> "New" Then
            Dim newArl As New ArrayList
            For a As Integer = 0 To arlEvent2.Count - 1 Step 1
                Dim q As BabitPameranExpense = CType(arlEvent2(a), BabitPameranExpense)
                'If q.ID <> 0 Then
                newArl.Add(q)
                'End If
            Next

            arlEvent2 = newArl
        End If

        CommonFunction.SortListControl(arlEvent2, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        dgBabitPameran.DataSource = arlEvent2
        dgBabitPameran.DataBind()
        sessHelper.SetSession("sessBabitPameranExpense", arlEvent2)
    End Sub

    Private Function GetArrayGridEvent(ByVal _babitHeaderID As Integer) As ArrayList
        Dim arr As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, _babitHeaderID))
        arr = New BabitPameranExpenseFacade(User).Retrieve(criterias)
        If IsNothing(arr) Then arr = New ArrayList
        Return arr
    End Function

    Private Function GetTotalPriceByCategory(ByVal intBabitParameterHeaderID As Integer) As Double
        Dim dblSumTotalPrice As Double = 0
        dblSumTotalPrice = (From item As BabitPameranExpense In arlEvent
                            Where item.BabitParameterDetail.BabitParameterHeader.ID = intBabitParameterHeaderID And item.Item <> "Total Biaya :"
                                Select (item.Qty * item.Price)).Sum()
        Return dblSumTotalPrice
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtNomorSurat.Text.Trim = String.Empty) Then
            sb.Append("- No Surat Harus Diisi\n")
        End If

        'If (ddlAllocationType.SelectedValue = "-1") Then
        '    sb.Append("- Tipe Alokasi Babit harus Diisi\n")
        'End If

        If (txtLocation.Text.Trim = String.Empty) AndAlso (txtOtherLocation.Text.Trim = String.Empty) Then
            sb.Append("- Lokasi harus Diisi\n")
        End If

        If txtOtherLocation.Text <> String.Empty Then
            If (ddlProvinsi.SelectedValue = "-1") Then
                sb.Append("- Provinsi harus Diisi\n")
            End If

            If (ddlKota.SelectedValue = "-1") Then
                sb.Append("- Kota\Kab harus Diisi\n")
            End If
        End If

        If (ICPameranStart.Value > ICPameranEnd.Value) Then
            sb.Append("- Tanggal Pameran dimulai harus lebih kecil atau sama dengan tanggal berakhir\n")
        End If

        'If (ICPameranStart.Value < Date.Now.Date) Then
        '    sb.Append("- Tanggal Pameran dimulai tidak boleh kurang dari hari ini\n")
        'End If

        Dim isLocationUsed As Boolean = True
        If txtLocation.Text <> String.Empty AndAlso Not IsNothing(HFLocation.Value) Then
            'Dim data As String() = HFLocation.Value.Trim.Split(";")
            Dim MasterLocID As Integer = GetBabitMasterLocation(txtLocation.Text)
            'Dim LocationName As String = data(0)
            'Dim CrtCheckLoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'CrtCheckLoc.opAnd(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.GreaterOrEqual, ICPameranStart.Value))
            'CrtCheckLoc.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterLocation.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'CrtCheckLoc.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterLocation.ID", MatchType.Exact, MasterLocID))
            'CrtCheckLoc.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterLocation.Status", MatchType.Exact, "1"))

            Dim SP As New StringBuilder
            SP.Append(" SELECT * FROM BabitHeader")
            SP.Append(" INNER JOIN BabitMasterLocation ON BabitHeader.BabitMasterLocationID = " + MasterLocID.ToString)
            SP.Append(" WHERE (BabitHeader.RowStatus = 0")
            SP.Append(" AND BabitMasterLocation.RowStatus = 0")
            SP.Append(" AND BabitMasterLocation.ID = 695")
            SP.Append(" AND BabitMasterLocation.Status = 1)")
            SP.Append(" AND (('" + ICPameranStart.Value.ToString("yyyy/MM/dd") + "' BETWEEN BabitHeader.[PeriodStart] AND BabitHeader.[PeriodEnd])")
            SP.Append(" OR ('" + ICPameranEnd.Value.ToString("yyyy/MM/dd") + "' BETWEEN BabitHeader.[PeriodStart] AND BabitHeader.[PeriodEnd]))")

            'Dim arrUsedLoc As ArrayList = New BabitHeaderFacade(User).Retrieve(CrtCheckLoc)
            Dim arrUsedLoc As ArrayList = New BabitHeaderFacade(User).DoRetrieveSP(SP.ToString)

            For Each head As BabitHeader In arrUsedLoc
                If head.BabitRegNumber <> lblNomorRegistrasi.Text Then
                    If arrUsedLoc.Count > 0 Then
                        Dim objBabitHeader As BabitHeader = arrUsedLoc(0)
                        'sb.Append("- Telah ada pameran pada " + objBabitHeader.BabitMasterLocation.LocationName + " sampai tanggal " + objBabitHeader.PeriodEnd + " dengan nomor pengajuan babit " & objBabitHeader.BabitRegNumber & "\n")
                        sb.Append("- Telah ada pameran pada " + objBabitHeader.BabitMasterLocation.LocationName + " dari tanggal " + objBabitHeader.PeriodStart + " sampai tanggal " + objBabitHeader.PeriodEnd + " dengan nomor pengajuan babit " & objBabitHeader.BabitRegNumber & "\n")
                    End If
                End If
            Next
        End If

        If (txtLuasPameran.Text.Trim = String.Empty OrElse txtLuasPameran.Text.Trim = "0") Then
            sb.Append("- Luas Pameran harus diisi dan tidak boleh 0\n")
        End If

        If (txtTargetProspek.Text.Trim = String.Empty OrElse txtTargetProspek.Text.Trim = "0") Then
            sb.Append("- Target Prospek harus diisi dan tidak boleh 0\n")
        End If

        If (sessHelper.GetSession("sessBabitPameranDisplayTarget") Is Nothing) Then
            sb.Append("- Data Display dan Target Penjualan belum ada\n")
        Else
            If CType(Session("sessBabitPameranDisplayTarget"), ArrayList).Count = 0 Then
                sb.Append("- Data Display dan Target Penjualan belum ada\n")
            End If
        End If

        If (sessHelper.GetSession("sessDataUploadFile") Is Nothing) Then
            sb.Append("- Data Contoh Foto Lokasi belum ada\n")
        Else
            If CType(Session("sessDataUploadFile"), ArrayList).Count = 0 Then
                sb.Append("- Data Contoh Foto Lokasi belum ada\n")
            End If
        End If

        If (sessHelper.GetSession("sessBabitPameranExpense") Is Nothing) Then
            sb.Append("- Data Biaya Babit Pameran belum ada\n")
        Else
            If CType(Session("sessBabitPameranExpense"), ArrayList).Count = 0 Then
                sb.Append("- Data Biaya Babit Pameran belum ada\n")
            End If
        End If

        Dim strParamName As String = String.Empty
        If ValidateIsMandatoryParamBabitPameranExpense(strParamName) <> String.Empty Then
            sb.Append("- Biaya: " & strParamName & " belum diinputkan \n")
        End If

        Dim SCcrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        SCcrit.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitParameterCategory"))

        Dim strParamCategoryName As String = String.Empty
        Dim arlParamCategory As ArrayList = New StandardCodeFacade(User).Retrieve(SCcrit)
        For Each SCItem As StandardCode In arlParamCategory
            If ValidateIsMandatoryPameranDetail(strParamCategoryName, SCItem.ValueId) <> String.Empty Then
                sb.Append("- " & SCItem.ValueDesc & " belum dipilih\n")
            End If
        Next

        'If txtSubsidyAmount.Text.Trim = "" Then txtSubsidyAmount.Text = 0

        '--Sementara di buang mandatorynya
        'If ddlMarBox.SelectedIndex = 0 Then
        '    sb.Append("- Marbox harus dipilih! \n")
        'End If

        If ddlMarBox.SelectedValue = "-1" Then
            sb.Append("- Marbox harus dipilih! \n")
        End If

        If ddlMarBox.SelectedValue = "" Then
            sb.Append("- Data Marbox harus dibuat! \n")
        End If

        If IsLoginAsDealer() Then
            Dim dteGetDate As Date = Now.ToShortDateString()
            Dim dtePeriodStart As Date = ICPameranStart.Value
            Dim dtePeriodStartCalculate As Date
            Dim countWorkDays As Integer = 0
            Dim limitWorkDays As Integer = 0

            Dim objAppConfig As New AppConfig
            Dim criterias As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "BabitSubmissionLimit"))
            Dim arrConfig As ArrayList = New AppConfigFacade(User).Retrieve(criterias)
            If Not IsNothing(arrConfig) AndAlso arrConfig.Count > 0 Then
                objAppConfig = CType(arrConfig(0), AppConfig)
                limitWorkDays = objAppConfig.Value
            End If

            dtePeriodStartCalculate = dtePeriodStart
            If limitWorkDays > 0 Then
                For i As Integer = 1 To 30
                    dtePeriodStartCalculate = dtePeriodStart.AddDays(-i)

                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, dtePeriodStartCalculate.Year))
                    criterias2.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, dtePeriodStartCalculate.Month))
                    criterias2.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, dtePeriodStartCalculate.Day))
                    Dim arrDDL2 As ArrayList = New NationalHolidayFacade(User).Retrieve(criterias2)
                    If IsNothing(arrDDL2) OrElse (Not IsNothing(arrDDL2) AndAlso arrDDL2.Count = 0) Then
                        countWorkDays += 1
                        If countWorkDays = limitWorkDays Then
                            Exit For
                        End If
                    End If
                Next
                Dim intdteGetDate As Double = CDbl(Format(dteGetDate, "yyyyMMdd"))
                Dim intPeriodMaxInput As Double = CDbl(Format(dtePeriodStartCalculate, "yyyyMMdd"))
                If intdteGetDate > intPeriodMaxInput Then
                    sb.Append("- Pengajuan proposal hanya boleh dilakukan selambat-lambatnya " & limitWorkDays.ToString & " hari kerja sebelum kegiatan.\n")
                End If
            End If
        End If

        If Mode = "Edit" Then
            If Not IsLoginAsDealer() Then
                If TR_Alokasi_Babit.Visible = True Then
                    Dim arrAlloc As ArrayList = CType(sessHelper.GetSession("sessionAlloc"), ArrayList)
                    If Not IsNothing(arrAlloc) Then
                        If arrAlloc.Count = 0 Then
                            sb.Append("- Alokasi Babit harus Diisi.\n")
                        Else
                            Dim dblSumSubsidyAmountBeforeEdit As Double = 0
                            Dim dblMaxJumlahSubsidy As Double = 0
                            Dim dblSumJumlahSubsidy As Double = 0
                            Dim strDealerCode As String = ""
                            Dim arrAlloc2 As ArrayList = New ArrayList
                            arrAlloc2 = New System.Collections.ArrayList((From item As BabitDealerAllocation In arrAlloc.OfType(Of BabitDealerAllocation)()
                                        Order By item.Dealer.DealerCode Ascending
                                        Select item).ToList())
                            If arrAlloc2.Count > 0 Then
                                dblSumSubsidyAmountBeforeEdit = 0
                                dblMaxJumlahSubsidy = 0
                                dblSumJumlahSubsidy = 0
                                strDealerCode = CType(arrAlloc2(0), BabitDealerAllocation).Dealer.DealerCode
                                For i As Integer = 0 To arrAlloc2.Count - 1
                                    Dim objAlloc As BabitDealerAllocation = CType(arrAlloc2(i), BabitDealerAllocation)
                                    If objAlloc.Dealer.DealerCode <> strDealerCode Then
                                        dblMaxJumlahSubsidy += dblSumSubsidyAmountBeforeEdit
                                        If objAlloc.BabitCategory <> "SPESIAL" AndAlso dblSumJumlahSubsidy > dblMaxJumlahSubsidy Then
                                            sb.Append("- Jumlah Subsidi untuk Kode Dealer " & strDealerCode & " sudah melebihi maksimal subsidinya.\n")
                                        End If
                                        dblMaxJumlahSubsidy = objAlloc.MaxSubsidyAmount
                                        dblSumJumlahSubsidy = objAlloc.SubsidyAmount
                                        dblSumSubsidyAmountBeforeEdit = objAlloc.SubsidyAmountBeforeEdit
                                        strDealerCode = objAlloc.Dealer.DealerCode
                                    Else
                                        dblMaxJumlahSubsidy = objAlloc.MaxSubsidyAmount
                                        dblSumJumlahSubsidy += objAlloc.SubsidyAmount
                                        dblSumSubsidyAmountBeforeEdit += objAlloc.SubsidyAmountBeforeEdit
                                    End If
                                    If i = arrAlloc2.Count - 1 Then
                                        dblMaxJumlahSubsidy += dblSumSubsidyAmountBeforeEdit
                                        If objAlloc.BabitCategory <> "SPESIAL" AndAlso dblSumJumlahSubsidy > dblMaxJumlahSubsidy Then
                                            sb.Append("- Jumlah Subsidi untuk Kode Dealer " & strDealerCode & " sudah melebihi maksimal subsidinya.\n")
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    Else
                        sb.Append("- Alokasi Babit harus Diisi.\n")
                    End If
                    'If ddlAllocationBabit.SelectedIndex = 0 Then
                    '    sb.Append("- Alokasi Babit harus Diisi.\n")
                    'End If

                End If
                'If TR_Jml_Subsidi.Visible = True Then
                '    If txtSubsidyAmount.Text.Trim = 0 OrElse txtSubsidyAmount.Text.Trim = "" Then
                '        sb.Append("- Jumlah Subsidi harus Diisi.\n")
                '    End If
                'End If

                'Dim strSQL As String = String.Empty
                'strSQL = "select distinct a.ID "
                'strSQL += "from BabitBudgetHeader a "
                'strSQL += "where a.RowStatus = 0 "
                'strSQL += "and a.DealerID Is null "
                'strSQL += "and a.CategoryID is null "
                'strSQL += "and a.YearPeriod = year('" & ICPameranStart.Value.ToString("yyyy/MM/dd") & "')  "
                'strSQL += "and month('" & ICPameranStart.Value.ToString("yyyy/MM/dd") & "') IN ("
                'strSQL += "Select value "
                'strSQL += "from funcListToTableInt("
                'strSQL += "case when a.QuarterPeriod = 1 then '04,05,06' "
                'strSQL += "when a.QuarterPeriod = 2 then '07,08,09' "
                'strSQL += "when a.QuarterPeriod = 3 then '10,11,12' "
                'strSQL += "when a.QuarterPeriod = 4 then '01,02,03' "
                'strSQL += "else '' end,','))  "
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "ID", MatchType.InSet, "(" & strSQL & ")"))
                'criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "Description", MatchType.Partial, ddlAllocationBabit.SelectedValue.Trim))
                'Dim arrBBH As ArrayList = New BabitBudgetHeaderFacade(User).Retrieve(criterias)
                'If Not IsNothing(arrBBH) AndAlso arrBBH.Count <= 0 Then
                '    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudget(ddlAllocationBabit.SelectedValue, lblNomorRegistrasi.Text.Trim)
                '    If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                '        'Dim objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget = CType(arrView(0), V_BabitMasterRetailTarget)
                '        Dim dblRemains As Double = 0
                '        Dim dblSubsidyAmount As Double = txtSubsidyAmount.Text
                '        For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                '            dblRemains = dblRemains + (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)

                '            'Dim dblSubsidyAmount As Double = txtSubsidyAmount.Text
                '            'If dblRemains < dblSubsidyAmount Then
                '            '    sb.Append("- Jumlah Subsidi Tidak Boleh Melebihi Sisa Alokasi Babit\n")
                '            'End If
                '        Next
                '        If dblRemains < dblSubsidyAmount Then
                '            sb.Append("- Jumlah Subsidi Tidak Boleh Melebihi Sisa Alokasi Babit\n")
                '        End If
                '    Else
                '        sb.Append("- Data Alokasi Babit " & ddlAllocationBabit.SelectedItem.Text & " tidak tersedia.\n")
                '    End If
                'End If
            End If
        End If

        Dim iMP As Integer = 0
        For Each cbm As ListItem In CBMaterialPromosi.Items
            If cbm.Selected Then
                iMP += 1
            End If
        Next
        If iMP = 0 Then
            sb.Append("- Material Promosi harus Dipilih minimal 1.\n")
        End If

        Dim iPP As Integer = 0
        For Each cbp As ListItem In CBProfilPengunjung.Items
            If cbp.Selected Then
                iPP += 1
            End If
        Next
        If iPP = 0 Then
            sb.Append("- (Gambar Lokasi) Profile Pengunjung harus Dipilih minimal 1.\n")
        End If

        Dim iLP As Integer = 0
        For Each cbl As ListItem In CBLokasiPameran.Items
            If cbl.Selected Then
                iLP += 1
            End If
        Next
        If iLP = 0 Then
            sb.Append("- (Gambar Lokasi) Lokasi Pameran harus Dipilih minimal 1.\n")
        End If


        Return sb.ToString()
    End Function

    Private Function GetRegNumber() As String
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.StartsWith, "P"))
        crit.opAnd(New Criteria(GetType(BabitHeader), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(BabitHeader), "CreatedTime", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New BabitHeaderFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            'Dim objBH As BabitHeader = CommonFunction.SortListControl(arrl, "ID", Sort.SortDirection.DESC)(0)
            Dim objBH As BabitHeader = CommonFunction.SortListControl(arrl, "BabitRegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.BabitRegNumber
            _return = "P" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + 1).ToString("d5")
        Else
            _return = "P" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & CInt(1).ToString("d5")
        End If
        Return _return
    End Function

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitDocument In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Path)
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Private Sub ClearAll()
        hdnIndexSelectedGrid.Value = ""
        hdnBabitHeaderID.Value = ""
        txtTemporaryOutlet.Text = ""
        hdnTemporaryOutlet.Value = ""
        lblNamaCabang.Text = ""
        lblArea.Text = ""
        txtNomorSurat.Text = ""
        'ddlAllocationType.SelectedIndex = 0
        txtOtherLocation.Text = ""
        txtLocation.Text = ""
        If ddlProvinsi.Items.Count > 0 Then
            ddlProvinsi.SelectedIndex = 0
        End If
        If ddlKota.Items.Count > 0 Then
            ddlKota.SelectedIndex = 0
        End If

        ICPameranStart.Value = Date.Now
        ICPameranEnd.Value = Date.Now
        txtLuasPameran.Text = ""
        txtTargetProspek.Text = ""

        'ddlAllocationBabit.SelectedIndex = 0
        'ddlAllocationType.SelectedIndex = 0
        'txtSubsidyAmount.Text = 0
        txtBabitDealerGroup.Text = ""

        sessHelper.SetSession("sessBabitPameranExpense", New ArrayList)
        sessHelper.SetSession("sessDataUploadFile", New ArrayList)
        sessHelper.SetSession("sessBabitPameranDisplayTarget", New ArrayList)
        sessHelper.SetSession("sessionAlloc", New ArrayList)

        BindGridBabitEvent()
        BindGridEventUploadFile()
        BindGridDisplayAndTarget()
        BindGridAlloc()
    End Sub

    Private Sub BindddlLocationType()
        ddlLocationType.Items.Clear()
        ddlLocationType.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "EnumBabit.MasterLocationType"))

        Dim arlLocationType As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
        For Each sc As StandardCode In arlLocationType
            ddlLocationType.Items.Add(New ListItem(sc.ValueDesc, sc.ValueId))
        Next

    End Sub

    Private Sub BindDDLMaterialPromosi(Optional ByVal arlBPD As ArrayList = Nothing)
        Dim isEdit As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.Status", MatchType.Exact, "1"))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ParameterCategory", MatchType.Exact, 1))

        Dim arlMaterialPromosi As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBPD) Then
            isEdit = True
        End If
        Dim Litems As ListItem = New ListItem

        CBMaterialPromosi.Items.Clear()
        For Each mp As BabitParameterDetail In arlMaterialPromosi
            Dim Litem As ListItem = New ListItem
            Litem.Text = mp.ParameterDetailName
            Litem.Value = mp.ID
            If isEdit Then
                For Each ibpd As BabitPameranDetail In arlBPD
                    If mp.ID = ibpd.BabitParameterDetail.ID Then
                        Litem.Selected = True
                        If ibpd.Notes.Trim <> String.Empty Then
                            txtOtherMaterialPromosi.Text = ibpd.Notes
                        End If
                        Exit For
                    End If
                Next
            End If
            If mp.ParameterDetailName.ToUpper <> "LAINNYA" Then
                CBMaterialPromosi.Items.Add(Litem)
            Else
                Litems = Litem
            End If
        Next
        CBMaterialPromosi.Items.Add(Litems)
    End Sub

    Private Sub BindDDLProfilPengunjung(Optional ByVal arlBPD As ArrayList = Nothing)
        Dim isEdit As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.Status", MatchType.Exact, "1"))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ParameterCategory", MatchType.Exact, 2))

        Dim arlProfilPengunjung As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBPD) Then
            isEdit = True
        End If
        Dim Litems As ListItem = New ListItem
        CBProfilPengunjung.Items.Clear()
        For Each mp As BabitParameterDetail In arlProfilPengunjung
            Dim Litem As ListItem = New ListItem
            Litem.Text = mp.ParameterDetailName
            Litem.Value = mp.ID
            If isEdit Then
                For Each ibpd As BabitPameranDetail In arlBPD
                    If mp.ID = ibpd.BabitParameterDetail.ID Then
                        Litem.Selected = True
                        If ibpd.Notes.Trim <> String.Empty Then
                            txtOtherProfilPengunjung.Text = ibpd.Notes
                        End If
                        Exit For
                    End If
                Next
            End If
            If mp.ParameterDetailName.ToUpper <> "LAINNYA" Then
                CBProfilPengunjung.Items.Add(Litem)
            Else
                Litems = Litem
            End If
        Next
        CBProfilPengunjung.Items.Add(Litems)
    End Sub

    Private Sub BindDDLLokasiPameran(Optional ByVal arlBPD As ArrayList = Nothing)
        Dim isEdit As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.Status", MatchType.Exact, "1"))
        criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ParameterCategory", MatchType.Exact, 3))

        Dim arlLokasiPameran As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBPD) Then
            isEdit = True
        End If
        CBLokasiPameran.Items.Clear()
        For Each mp As BabitParameterDetail In arlLokasiPameran
            Dim Litem As ListItem = New ListItem
            Litem.Text = mp.ParameterDetailName
            Litem.Value = mp.ID
            If isEdit Then
                For Each ibpd As BabitPameranDetail In arlBPD
                    If mp.ID = ibpd.BabitParameterDetail.ID Then
                        Litem.Selected = True
                        Exit For
                    End If
                Next
            End If
            CBLokasiPameran.Items.Add(Litem)
        Next

    End Sub

    Private Function ValidateIsMandatoryParamBabitPameranExpense(ByRef strParamName As String) As String
        strParamName = String.Empty
        Dim dataList As ArrayList = New ArrayList
        arlEvent = CType(sessHelper.GetSession("sessBabitPameranExpense"), ArrayList)

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            For Each objParam As BabitParameterHeader In arrBabitParamHdr
                dataList = New ArrayList(
                                (From obj As BabitPameranExpense In arlEvent.OfType(Of BabitPameranExpense)()
                                    Where obj.BabitParameterDetail.BabitParameterHeader.ID = objParam.ID And obj.Item.Trim <> "Total Biaya :"
                                    Select obj).ToList())
                If dataList.Count = 0 Then
                    If strParamName = String.Empty Then
                        strParamName = objParam.ParameterName
                    Else
                        strParamName += ", " & objParam.ParameterName
                    End If
                End If
            Next
        End If

        Return strParamName
    End Function

    Private Function ValidateIsMandatoryPameranDetail(ByRef strParamName As String, ByVal ParameterCategory As Integer) As String
        strParamName = String.Empty

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, ParameterCategory))

        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            Dim CBList As ListItemCollection = New ListItemCollection
            If ParameterCategory = EnumBabit.BabitParameterCategory.MaterialPromosi Then
                CBList = CBMaterialPromosi.Items
            ElseIf ParameterCategory = EnumBabit.BabitParameterCategory.ProfilPengunjung Then
                CBList = CBProfilPengunjung.Items
            Else
                CBList = CBLokasiPameran.Items
            End If
            For Each oBPH As BabitParameterHeader In arrBabitParamHdr
                For Each cbm As ListItem In CBList
                    Dim PD As BabitPameranDetail = New BabitPameranDetail
                    PD.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(cbm.Value))
                    If PD.BabitParameterDetail.BabitParameterHeader.ID = oBPH.ID Then
                        If cbm.Selected = False Then
                            If strParamName = String.Empty Then
                                strParamName = oBPH.ParameterName
                            Else
                                strParamName += ", " & oBPH.ParameterName
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            Next
        End If

        Return strParamName
    End Function

    Private Function GetBabitMasterLocation(ByVal LocationName As String) As Integer
        Dim ID As Integer = 0
        Dim CrtCheckLoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterLocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CrtCheckLoc.opAnd(New Criteria(GetType(BabitMasterLocation), "LocationName", MatchType.Exact, LocationName))
        CrtCheckLoc.opAnd(New Criteria(GetType(BabitMasterLocation), "Status", MatchType.Exact, "1"))

        Dim arrLoc As ArrayList = New BabitMasterLocationFacade(User).Retrieve(CrtCheckLoc)
        If arrLoc.Count > 0 Then
            Dim oBML As BabitMasterLocation = arrLoc(0)
            ID = oBML.ID
        End If
        Return ID
    End Function

    Private Sub FromList()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")

            objBabitHeader = New BabitHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitHeaderID")))
            Dim oBabitPameranDetail As BabitPameranDetail = New BabitPameranDetailFacade(User).Retrieve(objBabitHeader.ID)
            objDealer = objBabitHeader.Dealer
            hdnBabitHeaderID.Value = objBabitHeader.ID
            sessHelper.SetSession("FrmInputBabitPameran.DEALER", objDealer)
            BindMarbox(strBabitType)

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
            Dim arlBPE As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crit)
            sessHelper.SetSession("sessBabitPameranExpense", IIf(IsNothing(arlBPE), New ArrayList, arlBPE))

            crit = New CriteriaComposite(New Criteria(GetType(BabitDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitDocument), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
            Dim arlDoc As ArrayList = New BabitDocumentFacade(User).Retrieve(crit)
            sessHelper.SetSession("sessDataUploadFile", IIf(IsNothing(arlDoc), New ArrayList, arlDoc))

            crit = New CriteriaComposite(New Criteria(GetType(BabitDisplayCar), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitDisplayCar), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
            Dim arlMed As ArrayList = New BabitDisplayCarFacade(User).Retrieve(crit)
            sessHelper.SetSession("sessBabitPameranDisplayTarget", IIf(IsNothing(arlMed), New ArrayList, arlMed))

            crit = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitPameranDetail), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
            Dim arlBPD As ArrayList = New BabitPameranDetailFacade(User).Retrieve(crit)

            Dim arlDAlloc As New ArrayList
            If Not IsLoginAsDealer() Then
                crit = New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, oHeader.ID))
                arlDAlloc = New BabitDealerAllocationFacade(User).Retrieve(crit)
                Dim dblRemains As Double = 0
                For Each objAlloc As BabitDealerAllocation In arlDAlloc
                    objAlloc.SubsidyAmountBeforeEdit = objAlloc.SubsidyAmount
                    dblRemains = 0
                    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(objAlloc.Dealer.ID.ToString(), objBabitHeader.PeriodStart)
                    If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                        For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                            dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                        Next
                    End If
                    objAlloc.MaxSubsidyAmount = dblRemains
                Next
            End If
            sessHelper.SetSession("sessionAlloc", IIf(IsNothing(arlDAlloc), New ArrayList, arlDAlloc))

            BindDDLMaterialPromosi(arlBPD)
            BindDDLProfilPengunjung(arlBPD)
            BindDDLLokasiPameran(arlBPD)

            BindGridBabitEvent()
            BindGridEventUploadFile()
            BindGridDisplayAndTarget()

            lnkReload.Visible = True
            If Mode = "Detail" Then
                dgBabitPameran.ShowFooter = False
                dgBabitPameran.Columns(dgBabitPameran.Columns.Count - 1).Visible = False
                dgUploadFile.ShowFooter = False
                dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False
                dgDisplayAndTarget.ShowFooter = False
                dgDisplayAndTarget.Columns(dgDisplayAndTarget.Columns.Count - 1).Visible = False
                dgAlloc.ShowFooter = False
                dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = False

                btnSave.Enabled = False
                txtNomorSurat.Attributes("ReadOnly") = True
                txtLocation.Attributes("ReadOnly") = True
                txtOtherLocation.Attributes("ReadOnly") = True
                txtLuasPameran.Attributes("ReadOnly") = True
                txtTargetProspek.Attributes("ReadOnly") = True
                lblPopUpTO.Visible = False
                lblPULocation.Visible = False
                ddlLocationType.Enabled = False
                ddlProvinsi.Enabled = False
                ddlKota.Enabled = False
                ICPameranStart.Enabled = False
                ICPameranEnd.Enabled = False
                CBMaterialPromosi.Enabled = False
                CBProfilPengunjung.Enabled = False
                CBLokasiPameran.Enabled = False
                ddlMarBox.Enabled = False

                txtOtherMaterialPromosi.Attributes("ReadOnly") = True
                txtOtherProfilPengunjung.Attributes("ReadOnly") = True
                'ddlMarBox.Enabled = False
                lnkReload.Visible = False

                'ddlAllocationBabit.Enabled = False
                'ddlAllocationType.Enabled = False
                'txtSubsidyAmount.Enabled = False
                txtNotes.Enabled = False
                txtBabitDealerGroup.Enabled = False
                lblSearchDealer.Visible = False
            End If
            'ddlMarBox.SelectedValue = objBabitHeader.MarboxID
            'ddlMarBox_SelectedIndexChanged(Nothing, Nothing)
            btnBack.Visible = True
            LoadData(objBabitHeader, arlBPE, arlDoc, arlMed)
            BindGridAlloc()
        End If
    End Sub

    Private Sub LoadData(ByVal _oHeader As BabitHeader, oExpense As ArrayList, oDoc As ArrayList, oDispCar As ArrayList)
        lblNomorRegistrasi.Text = _oHeader.BabitRegNumber
        ICPameranStart.Value = _oHeader.PeriodStart
        'BindDDLAllocationBabit()

        lblKodeDealer.Text = _oHeader.Dealer.DealerCode
        lblNamaDealer.Text = " / " & _oHeader.Dealer.DealerName

        If Not IsNothing(_oHeader.DealerBranch) Then
            txtTemporaryOutlet.Text = _oHeader.DealerBranch.DealerBranchCode
            lblNamaCabang.Text = _oHeader.DealerBranch.Name
        End If
        lblArea.Text = _oHeader.Dealer.Area2.Description
        txtNomorSurat.Text = _oHeader.BabitDealerNumber
        'ddlAllocationType.SelectedValue = oHeader.AllocationType
        If Not IsNothing(_oHeader.BabitMasterLocation) Then
            ddlLocationType.SelectedIndex = 1
            txtLocation.Text = _oHeader.BabitMasterLocation.LocationName
        Else
            ddlLocationType.SelectedIndex = 2

            Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, _oHeader.City.ID))
            criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias4)
            If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                ddlProvinsi.Items.Clear()
                ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

                For Each prov As BabitSpecialCity In arlBabitSpecialCity
                    ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
                Next
                ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                BindddlCity(ddlProvinsi.SelectedValue, True)
            Else
                Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & _oHeader.City.ID
                criterias5.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSQL & ")"))
                Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias5)
                If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                    ddlProvinsi.Items.Clear()
                    ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

                    For Each prov As Province In arlProvince
                        ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                    Next
                    ddlProvinsi.SelectedValue = arlProvince(0).ID
                    BindddlCity(ddlProvinsi.SelectedValue, False)
                End If
            End If
            ddlKota.SelectedValue = _oHeader.City.ID
            txtOtherLocation.Text = _oHeader.Location
        End If
        LocationTypeControl()
        ICPameranStart.Value = _oHeader.PeriodStart
        ICPameranEnd.Value = _oHeader.PeriodEnd
        CalculatePeriodePameran()
        txtLuasPameran.Text = _oHeader.LuasArea
        txtTargetProspek.Text = _oHeader.ProspectTarget
        txtNotes.Text = _oHeader.Notes
        ddlMarBox.SelectedValue = _oHeader.MarboxID
        ddlMarBox_SelectedIndexChanged(Nothing, Nothing)

        'If Not IsLoginAsDealer() Then
        '    Dim dblBiaya As Double = 0
        '    Dim dblSubsidyAmount As Double = 0
        '    Dim _babitDealerAllocation As New BabitDealerAllocation
        '    Dim criterias6 As New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias6.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, _oHeader.ID))
        '    Dim arr As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(criterias6)
        '    If Not IsNothing(arr) AndAlso arr.Count > 0 Then
        '        _babitDealerAllocation = CType(arr(0), BabitDealerAllocation)
        '        If Not IsNothing(_babitDealerAllocation) AndAlso _babitDealerAllocation.ID > 0 Then
        '            dblSubsidyAmount = _babitDealerAllocation.SubsidyAmount
        '            Dim criterias7 As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias7.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
        '            criterias7.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, _babitDealerAllocation.BabitCategory))
        '            Dim arr1 As ArrayList = New CategoryFacade(User).Retrieve(criterias7)
        '            If Not IsNothing(arr1) AndAlso arr1.Count > 0 Then
        '                Me.ddlAllocationBabit.SelectedValue = CType(arr1(0), Category).CategoryCode
        '            Else
        '                Me.ddlAllocationBabit.SelectedValue = _babitDealerAllocation.BabitCategory
        '            End If
        '        End If
        '    Else
        '        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        crits.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
        '        Dim eventArr As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crits)
        '        dblBiaya = 0
        '        For Each bed As BabitPameranExpense In eventArr
        '            dblBiaya += CDbl(bed.Price) * CInt(bed.Qty)
        '        Next
        '        If dblBiaya > 0 Then
        '            dblSubsidyAmount = (dblBiaya * 0.5)
        '        End If
        '    End If
        '    Me.txtSubsidyAmount.Text = IIf(Format(dblSubsidyAmount, "###,###") = "", 0, Format(dblSubsidyAmount, "###,###"))
        'End If

        Dim strDealerCodes As String = String.Empty
        If Not IsNothing(_oHeader.BabitDealerGroup) AndAlso _oHeader.BabitDealerGroup.Trim() <> "" Then
            Dim arrBabitDealerGroupID As String() = _oHeader.BabitDealerGroup.Split(";")
            For Each dealerID As String In arrBabitDealerGroupID
                Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(CInt(dealerID))
                If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                    If strDealerCodes = String.Empty Then
                        strDealerCodes = oDealer.DealerCode
                    Else
                        strDealerCodes += ";" & oDealer.DealerCode
                    End If
                End If
            Next
        End If
        Me.txtBabitDealerGroup.Text = strDealerCodes

    End Sub

    Private Function ValidateBabitDealerGroup(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 1
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + " tidak valid")
                bcheck = False
                Exit For
            End If
        Next
        Dim strDealerDuplication As String = ValidateDealerDuplication(_dealers)
        If strDealerDuplication <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + strDealerDuplication)
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
    End Function

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmBabitList.aspx?Back=OK")
    End Sub

    Private Sub LoadMarBox()
        'Dim client As System.Net.WebClient = New System.Net.WebClient()
        'client.Headers("Authorization") = "Bearer Db7i7pVn8QbQbvVSniUDRqMHGtgY9DygYo25VPVEfGoX"
        'client.Headers("Content-type") = "application/json"
        'Dim mylist As New BabitMarBox.RootObject

        'Try
        '    client.BaseAddress = "https://api.typeform.com/forms/Zq1Mc6/responses"
        '    Dim _jsonResponse = client.DownloadString("https://api.typeform.com/forms/Zq1Mc6/responses")
        '    mylist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BabitMarBox.RootObject)(_jsonResponse)
        'Catch ex As Net.WebException
        '    If ex.Status = Net.WebExceptionStatus.ProtocolError Then
        '        Dim wrsp As Net.HttpWebResponse = CType(ex.Response, Net.HttpWebResponse)
        '        Dim statusCode As Integer = CType(wrsp.StatusCode, Integer)
        '        Dim msg = wrsp.StatusDescription
        '        Throw New HttpException(statusCode, msg)
        '    Else
        '        Throw New HttpException(500, ex.Message)
        '    End If
        'End Try
        'sessHelper.SetSession("Marbox", mylist)
        Try
            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            Dim objBabit As BabitInterfaceHelper = New BabitInterfaceHelper()
            Dim strBabitType As String = New AppConfigFacade(User).Retrieve("BabitCognitoSharePoint.BabitType.Pameran").Value
            'If objBabit.RetrieveBabitMarbox(objDealer.DealerCode, strBabitType) Then
            '    BindMarbox(strBabitType)
            'End If
            If KTB.DNet.SFIntegration.SchedulingNonSF.BabitCognitoLogic.Read(objDealer.DealerCode, strBabitType) Then
                BindMarbox(strBabitType)
            End If
            btnSave.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message.ToString())
        End Try

    End Sub

    Private Function GetMarboxFromBabitTransaction() As List(Of BabitHeader)
        Dim arr As List(Of BabitHeader)
        Dim objfac As New BabitHeaderFacade(User)
        Dim cri As New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Request Erwin
        'cri.opAnd(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.GreaterOrEqual, New DateTime(Now.Year, Now.Month, 1).ToString("yyyyMMdd")))

        cri.opAnd(New Criteria(GetType(BabitHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        arr = objfac.Retrieve(cri, "tambahan")
        Return arr
    End Function

    Private Sub BindMarbox(strBabitType As String)
        Dim listBabitHeader As List(Of BabitHeader) = GetMarboxFromBabitTransaction()

        'Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim cri As New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(BabitMarketingBox), "DealerID", MatchType.Exact, objDealer.ID))
        cri.opAnd(New Criteria(GetType(BabitMarketingBox), "BabitType", MatchType.Partial, strBabitType))

        'Request Erwin
        'cri.opAnd(New Criteria(GetType(BabitMarketingBox), "EndDate", MatchType.GreaterOrEqual, New DateTime(Now.Year, Now.Month, 1).ToString("yyyyMMdd")))

        'cri.opAnd(New Criteria(GetType(BabitMarketingBox), "StartDate", MatchType.GreaterOrEqual, Now.Date.ToString("yyyyMMdd")))
        'cri.opAnd(New Criteria(GetType(BabitMarketingBox), "EndDate", MatchType.LesserOrEqual, Now.Date.ToString("yyyyMMdd")))

        Dim arrMarbox As ArrayList = New BabitMarketingBoxFacade(User).Retrieve(cri)
        If arrMarbox.Count > 0 Then
            ddlMarBox.Enabled = True
            sessHelper.SetSession("Marbox", arrMarbox)
            ddlMarBox.Items.Clear()
            With ddlMarBox.Items
                .Add(New ListItem("Silahkan Pilih", "-1", True))
                For Each MB As BabitMarketingBox In arrMarbox
                    Try
                        Dim obj = (From obj1 As BabitHeader In listBabitHeader Where obj1.MarboxID = MB.SubMissionID And obj1.ID <> hdnBabitHeaderID.Value).Count
                        If obj = 0 Then
                            .Add(New ListItem(MB.EventName, MB.SubMissionID))
                        End If
                    Catch
                    End Try
                Next
                If Mode = "Edit" Then
                    Dim crMar As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crMar.opAnd(New Criteria(GetType(BabitMarketingBox), "SubMissionID", MatchType.Exact, oHeader.MarboxID))
                    Dim arlMar As ArrayList = New BabitMarketingBoxFacade(User).Retrieve(crMar)
                    If arlMar.Count > 0 Then
                        Dim ada = False
                        Dim oMar As BabitMarketingBox = arlMar(0)
                        For Each q As ListItem In ddlMarBox.Items
                            If q.Value = oMar.SubMissionID Then
                                ada = True
                            End If
                        Next
                        If Not ada Then
                            .Add(New ListItem(oMar.EventName, oMar.SubMissionID))
                        End If
                    End If
                End If
            End With
            ddlMarBox.SelectedIndex = 0
        End If
    End Sub

    'Private Sub BindMarbox()
    '    Dim Marbox As BabitMarBox.RootObject = CType(sessHelper.GetSession("Marbox"), BabitMarBox.RootObject)
    '    Dim arlMarb As New ArrayList
    '    ddlMarBox.Items.Clear()
    '    'For Each MB As List(Of BabitMarBox.Item) In Marbox.items
    '    '.Add(New ListItem("Silahkan Pilih", "-1", True))
    '    arlMarb.Add(New ListItem("Silahkan Pilih", "-1", True))
    '    For Each MB As BabitMarBox.Item In Marbox.items
    '        Try
    '            If MB.answers.Item(6).number = objDealer.DealerCode Then
    '                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                crits.opAnd(New Criteria(GetType(BabitHeader), "MarboxID", MatchType.Exact, MB.landing_id))
    '                Dim arlCheck As ArrayList = New BabitHeaderFacade(User).Retrieve(crits)
    '                If arlCheck.Count = 0 Then
    '                    arlMarb.Add(New ListItem(MB.answers.Item(1).text, MB.landing_id))
    '                ElseIf arlCheck.Count = 1 AndAlso Mode <> "New" Then
    '                    arlMarb.Add(New ListItem(MB.answers.Item(1).text, MB.landing_id))
    '                End If
    '            End If
    '        Catch
    '        End Try
    '        'arlMarb.Add(New ListItem("Silahkan Pilih", "-1", True))
    '    Next
    '    'Next
    '    'sessHelper.SetSession("ArlMarb", arlMarb)
    '    ddlMarBox.DataSource = arlMarb
    '    ddlMarBox.DataBind()
    '    ddlMarBox.SelectedIndex = 0
    'End Sub

    Private Sub UsedMarbox()
        'Dim Marbox As BabitMarBox.RootObject = CType(sessHelper.GetSession("Marbox"), BabitMarBox.RootObject)
        'Dim arlM As List(Of BabitMarBox.Item) = Marbox.items
        'Dim arl As List(Of BabitMarBox.Item) = From ar As BabitMarBox.Item In arlM Where ar.answers.Item(5).number = objDealer.DealerCode Select ar
        Dim i As Integer = 0
        Dim arl As ArrayList = CType(sessHelper.GetSession("ArlMarb"), ArrayList)
        For Each li As ListItem In CType(sessHelper.GetSession("ArlMarb"), ArrayList)
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(BabitHeader), "MarboxID", MatchType.Exact, ddlMarBox.Items(i).Value))
            Dim arlCheck As ArrayList = New BabitHeaderFacade(User).Retrieve(crits)
            If Mode <> "New" Then
                If arlCheck.Count > 0 Then
                    If oHeader.MarboxID <> ddlMarBox.Items(i).Value Then
                        If i <> 0 Then
                            arl.RemoveAt(i)
                        End If
                    End If
                End If
            Else
                If arlCheck.Count > 0 Then
                    arl.RemoveAt(i)
                End If
            End If
            i += 1
        Next
        ddlMarBox.Items.Clear()
        ddlMarBox.DataSource = arl
        ddlMarBox.DataBind()
    End Sub

    Protected Sub ddlMarBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMarBox.SelectedIndexChanged
        If ddlMarBox.SelectedIndex = 0 Then
            lblLokasiMarbox.Text = ""
            lblPeriodeMarbox.Text = ""
        Else
            Dim Marbox As ArrayList = sessHelper.GetSession("Marbox")
            If Not IsNothing(Marbox) Then
                For Each MB As BabitMarketingBox In Marbox
                    Try
                        If ddlMarBox.SelectedValue = MB.SubMissionID Then
                            lblPeriodeMarbox.Text = MB.StartDate.ToString("dd/MM/yyyy") & " - " & MB.EndDate.ToString("dd/MM/yyyy")
                            lblLokasiMarbox.Text = MB.EventLocation
                            Exit For
                        End If
                    Catch
                    End Try
                Next
            End If
        End If
    End Sub

    Protected Sub lnkReload_Click(sender As Object, e As EventArgs) Handles lnkReload.Click
        LoadMarBox()
    End Sub

    Private Function CalculateBiaya() As Integer
        Dim _return As Integer = 0

        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            Try
                Dim headerID As Integer = CInt(Request.QueryString("BabitHeaderID"))

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, headerID))
                Dim expenseArr As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crit)
                For Each objExp As BabitPameranExpense In expenseArr
                    _return += (objExp.Qty * objExp.Price)
                Next
                _return = _return / 2
            Catch
            End Try
        End If

        Return _return
    End Function

    Protected Sub dgAlloc_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAlloc.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim arrAlloc As ArrayList = CType(sessHelper.GetSession("sessionAlloc"), ArrayList)
                Dim objBabitDealerAllocation As BabitDealerAllocation = arrAlloc(e.Item.ItemIndex)

                Dim lblDealerID As Label = CType(e.Item.FindControl("lblDealerID"), Label)
                Dim lblDealerCodeName As Label = CType(e.Item.FindControl("lblDealerCodeName"), Label)
                If Not IsNothing(objBabitDealerAllocation.Dealer) Then
                    lblDealerID.Text = objBabitDealerAllocation.Dealer.ID
                    lblDealerCodeName.Text = objBabitDealerAllocation.Dealer.DealerCode & " / " & objBabitDealerAllocation.Dealer.DealerName
                End If

                Dim dealerID As Integer = 0
                If Not IsNothing(objBabitDealerAllocation.Dealer) Then
                    dealerID = objBabitDealerAllocation.Dealer.ID
                End If
                'Dim intSubCategoryVehicleID As Integer = 0
                'Dim intCategoryID As Integer = 0
                'Dim oCategory As Category
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, objBabitDealerAllocation.BabitCategory))
                'Dim arrCat As ArrayList = New CategoryFacade(User).Retrieve(criterias)
                'If Not IsNothing(arrCat) AndAlso arrCat.Count > 0 Then
                '    oCategory = CType(arrCat(0), Category)
                '    intCategoryID = oCategory.ID
                '    intSubCategoryVehicleID = 0
                'Else
                '    Dim strSQL As String = String.Empty
                '    strSQL = "select distinct a.ID "
                '    strSQL += "from SubCategoryVehicle a "
                '    strSQL += "where a.RowStatus = 0 "
                '    strSQL += "and replace(a.name,' ','') = '" & objBabitDealerAllocation.BabitCategory & "'"

                '    Dim crit As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '    crit.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSQL & ")"))
                '    Dim arrSCV As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(crit)
                '    If Not IsNothing(arrSCV) AndAlso arrSCV.Count > 0 Then
                '        Dim oSubCategoryVehicle As SubCategoryVehicle = CType(arrSCV(0), SubCategoryVehicle)
                '        intCategoryID = oSubCategoryVehicle.Category.ID
                '        intSubCategoryVehicleID = oSubCategoryVehicle.ID
                '    End If
                'End If

                Dim dblRemains As Double = 0
                'Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(objBabitDealerAllocation.BabitCategory, oDealer.ID.ToString(), intCategoryID, intSubCategoryVehicleID, oHeader.PeriodStart)
                Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(dealerID.ToString, oHeader.PeriodStart)
                If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                    For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                        dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                    Next
                End If
                Dim lblJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblJmlMaxSubsidy"), Label)
                lblJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")

                Dim lblAllocationBabit As Label = CType(e.Item.FindControl("lblAllocationBabit"), Label)
                Dim lblJmlSubsidy As Label = CType(e.Item.FindControl("lblJmlSubsidy"), Label)
                lblAllocationBabit.Text = "BABIT " & objBabitDealerAllocation.BabitCategory.ToString()
                lblJmlSubsidy.Text = objBabitDealerAllocation.SubsidyAmount.ToString("#,##0")

            Case ListItemType.Footer
                Dim ddlAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                Dim txtJmlSubsidy As TextBox = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                Dim txtDealerCode As TextBox = CType(e.Item.FindControl("txtDealerCode"), TextBox)

                Dim lblFJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)
                lblFJmlMaxSubsidy.Text = 0

                Dim dealerID As Integer = 0
                Dim _oDealer As Dealer = New DealerFacade(User).Retrieve(lblKodeDealer.Text)
                If Not IsNothing(_oDealer) AndAlso _oDealer.ID > 0 Then
                    dealerID = _oDealer.ID
                End If
                Dim lblSearchDealerGrid As Label = CType(e.Item.FindControl("lblSearchDealerGrid"), Label)
                lblSearchDealerGrid.Attributes("onclick") = "ShowPPDealerSelectionAlokasi(this," & dealerID & ");"

                If txtDealerCode.Text.Trim <> "" Then
                    Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
                    If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                        BindDDLAllocationBabit(oDealer.ID, ddlAllocationBabit)
                    End If
                End If
                txtJmlSubsidy.Text = Format(CalculateBiaya(), "###,###")

            Case ListItemType.EditItem
                Dim arrAlloc As ArrayList = CType(sessHelper.GetSession("sessionAlloc"), ArrayList)
                Dim objBabitDealerAllocation As BabitDealerAllocation = arrAlloc(e.Item.ItemIndex)
                Dim txtEDealerCode As TextBox = CType(e.Item.FindControl("txtEDealerCode"), TextBox)
                If Not IsNothing(objBabitDealerAllocation.Dealer) Then
                    txtEDealerCode.Text = objBabitDealerAllocation.Dealer.DealerCode
                End If
                Dim dealerID As Integer = 0
                Dim _oDealer As Dealer = New DealerFacade(User).Retrieve(lblKodeDealer.Text)
                If Not IsNothing(_oDealer) AndAlso _oDealer.ID > 0 Then
                    dealerID = _oDealer.ID
                End If
                Dim lblESearchDealerGrid As Label = CType(e.Item.FindControl("lblESearchDealerGrid"), Label)
                lblESearchDealerGrid.Attributes("onclick") = "ShowPPDealerSelectionAlokasi(this," & dealerID & ");"

                Dim oDealer As Dealer = New Dealer
                Dim ddlEAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlEAllocationBabit"), DropDownList)
                If txtEDealerCode.Text.Trim <> "" Then
                    oDealer = New DealerFacade(User).Retrieve(txtEDealerCode.Text.Trim)
                    If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                        BindDDLAllocationBabit(oDealer.ID, ddlEAllocationBabit)
                        ddlEAllocationBabit.SelectedValue = objBabitDealerAllocation.BabitCategory
                    End If
                End If

                'Dim intSubCategoryVehicleID As Integer = 0
                'Dim intCategoryID As Integer = 0
                'Dim oCategory As Category
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, objBabitDealerAllocation.BabitCategory))
                'Dim arrCat As ArrayList = New CategoryFacade(User).Retrieve(criterias)
                'If Not IsNothing(arrCat) AndAlso arrCat.Count > 0 Then
                '    oCategory = CType(arrCat(0), Category)
                '    intCategoryID = oCategory.ID
                '    intSubCategoryVehicleID = 0
                'Else
                '    Dim strSQL As String = String.Empty
                '    strSQL = "select distinct a.ID "
                '    strSQL += "from SubCategoryVehicle a "
                '    strSQL += "where a.RowStatus = 0 "
                '    strSQL += "and replace(a.name,' ','') = '" & objBabitDealerAllocation.BabitCategory & "'"

                '    Dim crit As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '    crit.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSQL & ")"))
                '    Dim arrSCV As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(crit)
                '    If Not IsNothing(arrSCV) AndAlso arrSCV.Count > 0 Then
                '        Dim oSubCategoryVehicle As SubCategoryVehicle = CType(arrSCV(0), SubCategoryVehicle)
                '        intCategoryID = oSubCategoryVehicle.Category.ID
                '        intSubCategoryVehicleID = oSubCategoryVehicle.ID
                '    End If
                'End If

                Dim dblRemains As Double = 0
                'Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(objBabitDealerAllocation.BabitCategory, oDealer.ID.ToString(), intCategoryID, intSubCategoryVehicleID, oHeader.PeriodStart)
                Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(oDealer.ID.ToString(), oHeader.PeriodStart)
                If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                    For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                        dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                    Next
                End If
                Dim lblEJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblEJmlMaxSubsidy"), Label)
                lblEJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")

                Dim txtEJmlSubsidy As TextBox = CType(e.Item.FindControl("txtEJmlSubsidy"), TextBox)
                txtEJmlSubsidy.Text = objBabitDealerAllocation.SubsidyAmount.ToString("###,###,##0")
        End Select
    End Sub

    Protected Sub dgAlloc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAlloc.ItemCommand
        Dim arrAlloc As ArrayList = CType(sessHelper.GetSession("sessionAlloc"), ArrayList)
        If IsNothing(arrAlloc) Then arrAlloc = New ArrayList

        Select Case e.CommandName
            Case "Add"
                Dim txtDealerCode As TextBox = CType(e.Item.FindControl("txtDealerCode"), TextBox)
                Dim ddlAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                Dim txtJmlSubsidy As TextBox = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                Dim lblFJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)

                If txtDealerCode.Text.Trim = "" Then
                    MessageBox.Show("Dealer Alokasi harus diisi")
                    Exit Sub
                End If
                If ddlAllocationBabit.SelectedIndex = 0 Then
                    MessageBox.Show("Alokasi BABIT harus diisi")
                    Exit Sub
                End If
                If txtJmlSubsidy.Text.Trim = "" OrElse txtJmlSubsidy.Text.Trim = "0" Then
                    MessageBox.Show("Jumlah Subsidi harus diisi")
                    Exit Sub
                End If

                Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
                If (IsNothing(oDealer)) OrElse (Not IsNothing(oDealer) AndAlso oDealer.ID = 0) Then
                    MessageBox.Show("Dealer Alokasi tidak valid")
                    Exit Sub
                End If

                For Each obj As BabitDealerAllocation In arrAlloc
                    If IsNothing(obj.Dealer) Then
                        obj.Dealer = New Dealer
                    End If
                    If txtDealerCode.Text.Trim = obj.Dealer.DealerCode Then
                        If ddlAllocationBabit.SelectedValue = obj.BabitCategory Then
                            MessageBox.Show("Dealer Alokasi dan Tipe Alokasi tidak boleh sama dalam satu pengajuan Babit")
                            Exit Sub
                        End If
                    End If
                Next
                Dim dblRemains As Double = lblFJmlMaxSubsidy.Text
                Dim dblSubsidyAmount As Double = txtJmlSubsidy.Text
                If ddlAllocationBabit.SelectedValue <> "SPESIAL" Then
                    If dblRemains < dblSubsidyAmount Then
                        MessageBox.Show("Jumlah Subsidi Tidak Boleh Melebihi Maksimal Subsidi Babit")
                        Exit Sub
                    End If
                End If

                Dim oAlloc As New BabitDealerAllocation
                oAlloc.Dealer = oDealer
                oAlloc.BabitCategory = IIf(ddlAllocationBabit.SelectedIndex = 0, "", ddlAllocationBabit.SelectedValue)
                Try
                    oAlloc.SubsidyAmount = txtJmlSubsidy.Text
                Catch
                    oAlloc.SubsidyAmount = CalculateBiaya()
                End Try
                oAlloc.MaxSubsidyAmount = dblRemains
                arrAlloc.Add(oAlloc)

            Case "Save"
                Dim lblEID As Label = CType(e.Item.FindControl("lblEID"), Label)
                Dim txtEDealerCode As TextBox = CType(e.Item.FindControl("txtEDealerCode"), TextBox)
                Dim ddlEAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlEAllocationBabit"), DropDownList)
                Dim txtEJmlSubsidy As TextBox = CType(e.Item.FindControl("txtEJmlSubsidy"), TextBox)
                Dim lblEJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblEJmlMaxSubsidy"), Label)

                If txtEDealerCode.Text.Trim = "" Then
                    MessageBox.Show("Dealer Alokasi harus diisi")
                    Exit Sub
                End If
                If ddlEAllocationBabit.SelectedIndex = 0 Then
                    MessageBox.Show("Alokasi BABIT harus diisi")
                    Exit Sub
                End If
                If txtEJmlSubsidy.Text.Trim = "" OrElse txtEJmlSubsidy.Text.Trim = "0" Then
                    MessageBox.Show("Jumlah Subsidi harus diisi")
                    Exit Sub
                End If

                Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtEDealerCode.Text)
                If (IsNothing(oDealer)) OrElse (Not IsNothing(oDealer) AndAlso oDealer.ID = 0) Then
                    MessageBox.Show("Dealer Alokasi tidak valid")
                    Exit Sub
                End If

                For Each obj As BabitDealerAllocation In arrAlloc
                    If IsNothing(obj.Dealer) Then
                        obj.Dealer = New Dealer
                    End If
                    If txtEDealerCode.Text.Trim = obj.Dealer.DealerCode Then
                        If ddlEAllocationBabit.SelectedValue = obj.BabitCategory And obj.ID <> lblEID.Text Then
                            MessageBox.Show("Tipe Alokasi tidak boleh sama dalam satu pengajuan Babit")
                            Exit Sub
                        End If
                    End If
                Next

                Dim oAlloc As BabitDealerAllocation = CType(arrAlloc(e.Item.ItemIndex), BabitDealerAllocation)
                Dim dblRemains As Double = lblEJmlMaxSubsidy.Text
                Dim dblSubsidyAmount As Double = txtEJmlSubsidy.Text
                If oAlloc.ID > 0 Then
                    dblRemains = dblRemains + oAlloc.SubsidyAmount
                End If
                If dblRemains < dblSubsidyAmount Then
                    MessageBox.Show("Jumlah Subsidi Tidak Boleh Melebihi Maksimal Subsidi Babit")
                    Exit Sub
                End If

                oAlloc.Dealer = oDealer
                oAlloc.BabitCategory = IIf(ddlEAllocationBabit.SelectedIndex = 0, "", ddlEAllocationBabit.SelectedValue)
                Try
                    oAlloc.SubsidyAmount = txtEJmlSubsidy.Text
                Catch
                    oAlloc.SubsidyAmount = CalculateBiaya()
                End Try
                dgAlloc.EditItemIndex = -1
                dgAlloc.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgAlloc.ShowFooter = False
                dgAlloc.EditItemIndex = e.Item.ItemIndex

            Case "cancel" 'Cancel Update this datagrid item 
                dgAlloc.EditItemIndex = -1
                dgAlloc.ShowFooter = True

            Case "Delete"
                Try
                    Dim oAlloc As BabitDealerAllocation = CType(arrAlloc(e.Item.ItemIndex), BabitDealerAllocation)
                    If oAlloc.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession("sessDelAlloc"), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oAlloc)
                        sessHelper.SetSession("sessDelAlloc", arrDelete)
                    End If
                    arrAlloc.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
        End Select
        sessHelper.SetSession("sessionAlloc", arrAlloc)
        BindGridAlloc()
    End Sub

    Public Sub ddlAllocationBabit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlAllocationBabit As DropDownList = sender
        Dim gridItem As DataGridItem = ddlAllocationBabit.Parent.Parent
        Dim txtDealerCode As TextBox
        Dim lblFJmlMaxSubsidy As Label
        If gridItem.DataSetIndex > -1 Then
            txtDealerCode = gridItem.FindControl("txtEDealerCode")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblEJmlMaxSubsidy")
        Else
            txtDealerCode = gridItem.FindControl("txtDealerCode")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblFJmlMaxSubsidy")
        End If
        'If ddlAllocationBabit.SelectedIndex > 0 AndAlso txtDealerCode.Text.Trim <> "" Then
        '    Dim dblRemains As Double = 0
        '    Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)

        '    Dim intSubCategoryVehicleID As Integer = 0
        '    Dim intCategoryID As Integer = 0
        '    Dim oCategory As Category
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, ddlAllocationBabit.SelectedValue))
        '    Dim arrCat As ArrayList = New CategoryFacade(User).Retrieve(criterias)
        '    If Not IsNothing(arrCat) AndAlso arrCat.Count > 0 Then
        '        oCategory = CType(arrCat(0), Category)
        '        intCategoryID = oCategory.ID
        '        intSubCategoryVehicleID = 0
        '    Else
        '        Dim strSQL As String = String.Empty
        '        strSQL = "select distinct a.ID "
        '        strSQL += "from SubCategoryVehicle a "
        '        strSQL += "where a.RowStatus = 0 "
        '        strSQL += "and replace(a.name,' ','') = '" & ddlAllocationBabit.SelectedValue & "'"

        '        Dim crit As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        crit.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSQL & ")"))
        '        Dim arrSCV As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(crit)
        '        If Not IsNothing(arrSCV) AndAlso arrSCV.Count > 0 Then
        '            Dim oSubCategoryVehicle As SubCategoryVehicle = CType(arrSCV(0), SubCategoryVehicle)
        '            intCategoryID = oSubCategoryVehicle.Category.ID
        '            intSubCategoryVehicleID = oSubCategoryVehicle.ID
        '        End If
        '    End If

        '    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(ddlAllocationBabit.SelectedValue, oDealer.ID.ToString(), intCategoryID, intSubCategoryVehicleID, oHeader.PeriodStart)
        '    If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
        '        For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
        '            dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
        '        Next
        '    End If
        '    lblFJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")
        'Else
        'End If
    End Sub

    Public Sub txtDealerCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim txtDealerCode As TextBox = sender
        Dim gridItem As DataGridItem = txtDealerCode.Parent.Parent
        Dim ddlAllocationBabit As DropDownList
        Dim lblFJmlMaxSubsidy As Label
        If gridItem.DataSetIndex > -1 Then
            ddlAllocationBabit = gridItem.FindControl("ddlEAllocationBabit")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblEJmlMaxSubsidy")
        Else
            ddlAllocationBabit = gridItem.FindControl("ddlAllocationBabit")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblFJmlMaxSubsidy")
        End If

        lblFJmlMaxSubsidy.Text = 0
        ddlAllocationBabit.Items.Clear()
        ddlAllocationBabit.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        If txtDealerCode.Text.Trim <> "" Then
            Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
            If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                BindDDLAllocationBabit(oDealer.ID, ddlAllocationBabit)
                Dim dblRemains As Double = 0
                Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(oDealer.ID.ToString(), oHeader.PeriodStart)
                If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                    For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                        dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                    Next
                End If
                lblFJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")
            End If
        End If
    End Sub

#End Region

End Class