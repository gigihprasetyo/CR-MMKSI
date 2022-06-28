#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports Excel
Imports System.IO
Imports System.Linq

#End Region

Public Class FrmRecallCategoryDetail
    Inherits System.Web.UI.Page

    Private Const ObjRecallCategoryIdentifier As String = "ObjRecallCategory"
    Private RecallCategoryID As Integer = 0
    Private paramStrId As String
    Private Const _deletedData As String = "RecallCategoryDetail_DeletedData"
    Private Const _dataList As String = "RecallCategoryDetail_ListData"
    Private Const _strId As String = "RecallCategoryDetail_strId"
    Private Const _dataUploadList As String = "RecallCategoryDetail_ListUploadData"
    Private tambahVisible As Boolean = True
    Private sessionHelper As SessionHelper = New SessionHelper()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strId As String = Request.QueryString("strId")
        sessionHelper.SetSession(_strId, strId)
        Dim actionMode As String = Request.QueryString("actionMode")
        paramStrId = strId

        If (Not IsPostBack) Then
            LoadRecallCategoryInformation(strId)
            If (actionMode = "First") Then
                'Edit by Reza
                'If (actionMode = "Add") Then
                '    Dim typeCode As String = Request.QueryString("typeCode")
                '    Dim positionCode As String = Request.QueryString("positionCode")
                '    Dim workCode As String = Request.QueryString("workCode")

                '    SaveRecallCategoryDetailIntoList(typeCode, positionCode, workCode, strId)
                'ElseIf (actionMode = "Delete") Then
                '    Dim paramRecallCategoryDetailId As Integer = CType(Request.QueryString("recallCategoryDetailId"), Integer)
                '    Dim paramTypeCode As String = CType(Request.QueryString("typeCode"), String)
                '    Dim paramPositionCode As String = CType(Request.QueryString("positionCode"), String)
                '    Dim paramWorkCode As String = CType(Request.QueryString("workCode"), String)

                '    RemoveRecallCategoryDetailFromList(paramRecallCategoryDetailId, paramTypeCode, paramPositionCode, paramWorkCode)
                'ElseIf (actionMode = "First") Then
                dgRecallUpload.Visible = False
                dgRecallCategoryDetail.Visible = True
                LoadGridDataSource()
            End If
            BindGrid()
        End If
        'End If
    End Sub

    Function SaveRecallCategoryDetailIntoList(ByVal categoryId As String, ByVal typeCode As String, ByVal positionCode As String, ByVal workCode As String, ByVal strId As String, Optional ByVal FromUpload As Boolean = False) As Boolean
        Dim vehicleTypeId As Integer = GetVehicleTypeId(typeCode, categoryId)
        Dim laborCodeForPositionCode As String = positionCode
        Dim laborCodeForWorkCode As String = workCode
        Dim laborId As Integer = GetLaborId(vehicleTypeId, laborCodeForPositionCode, laborCodeForWorkCode)
        Dim recallCategoryId As Integer = GetRecallCategoryId(strId)
        Dim objRecallCategoryDetail As RecallCategoryDetail
        Dim objRecallCategoryDetailFacade As RecallCategoryDetailFacade

        If vehicleTypeId = 0 Then
            MessageBox.Show("Kode Tipe tidak ditemukan")
            Return False
        End If
        '        Dim result As Integer

        'Edit By Reza
        If laborId = 0 Then
            MessageBox.Show("Kode Posisi atau Kode Kerja tidak ditemukan")
            Return False
        End If

        If IsExistRecallCategoryDetailFromList(categoryId, typeCode, positionCode, workCode) Then
            MessageBox.Show("Data sudah pernah di input !")
            Return False
        End If


        objRecallCategoryDetail = New RecallCategoryDetail()
        objRecallCategoryDetail.ID = 0
        objRecallCategoryDetail.CreatedBy = User.Identity.Name
        objRecallCategoryDetail.RecallCategoryID = recallCategoryId
        objRecallCategoryDetail.LaborMasterID = laborId
        objRecallCategoryDetail.PositionCode = laborCodeForPositionCode
        objRecallCategoryDetail.WorkCode = laborCodeForWorkCode

        Dim recallCategoryDetailList As ArrayList = CType(sessionHelper.GetSession(_dataList), ArrayList)
        recallCategoryDetailList.Add(objRecallCategoryDetail)

        Dim objForSesion As ArrayList = New ArrayList
        For Each item As RecallCategoryDetail In recallCategoryDetailList
            objForSesion.Add(item)
        Next

        sessionHelper.SetSession(_dataList, objForSesion)
        Return True

    End Function

    Private Function GetLaborId(ByVal vehicleTypeId As Integer, ByVal positionCode As String, ByVal workCode As String) As Integer
        Dim objLaborMaster As LaborMaster
        Dim objLaborMasterList As ArrayList
        Dim objLaborMasterFacade As LaborMasterFacade
        Dim criterias As CriteriaComposite

        objLaborMasterFacade = New LaborMasterFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", vehicleTypeId))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", positionCode))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", workCode))

        objLaborMasterList = objLaborMasterFacade.Retrieve(criterias)
        If (Not IsNothing(objLaborMasterList) And objLaborMasterList.Count > 0) Then
            objLaborMaster = CType(objLaborMasterList(0), LaborMaster)

            Return objLaborMaster.ID
        End If

        Return 0
    End Function

    Private Function GetRecallCategoryIdInformation(ByVal positionCode As String)
        Return Nothing
    End Function

    Private Function GetCategoryId(ByVal categoryCode As String)
        Dim objCategory As Category
        Dim objCategoryFacade As CategoryFacade
        Dim criterias As CriteriaComposite
        Dim objCategoryList As ArrayList

        objCategoryFacade = New CategoryFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, categoryCode))
        objCategoryList = objCategoryFacade.Retrieve(criterias)

        If (Not IsNothing(objCategoryList) And objCategoryList.Count > 0) Then
            objCategory = CType(objCategoryList(0), Category)
            Return objCategory.ID
        End If

        Return Nothing
    End Function

    Private Function GetVehicleTypeId(ByVal typeCode As String, Optional ByVal categoryId As String = "")
        Dim objVehicleType As VechileType
        Dim objVehicleTypeFacade As VechileTypeFacade
        Dim criterias As CriteriaComposite
        Dim objVehicleTypeList As ArrayList

        objVehicleTypeFacade = New VechileTypeFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, typeCode))
        If categoryId <> "" Then
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, categoryId))
        End If
        objVehicleTypeList = objVehicleTypeFacade.Retrieve(criterias)

        If (Not IsNothing(objVehicleTypeList) And objVehicleTypeList.Count > 0) Then
            objVehicleType = CType(objVehicleTypeList(0), VechileType)
            Return objVehicleType.ID
        End If

        Return Nothing
    End Function

    Private Function GetRecallCategoryId(ByVal bulletinServiceNo As String)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategory), "BuletinDescription", bulletinServiceNo))
        criterias.opAnd(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objRecallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)

        Dim recallCategoryList As ArrayList = objRecallCategoryFacade.Retrieve(criterias)
        If (Not IsNothing(recallCategoryList) And recallCategoryList.Count > 0) Then
            Dim objRecallCategory As RecallCategory = CType(recallCategoryList(0), RecallCategory)
            lblServiceBulletinNo.Text = objRecallCategory.BuletinDescription
            lblDescription.Text = objRecallCategory.Description

            ViewState(ObjRecallCategoryIdentifier) = objRecallCategory.ID
            RecallCategoryID = objRecallCategory.ID
            Return objRecallCategory.ID
        End If

        Return Nothing
    End Function

    Private Sub LoadRecallCategoryInformation(ByVal bulletinDescription As String)
        bulletinDescription = bulletinDescription.Split(",")(0)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategory), "BuletinDescription", bulletinDescription))
        criterias.opAnd(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objRecallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)

        Dim recallCategoryList As ArrayList = objRecallCategoryFacade.Retrieve(criterias)

        If (Not IsNothing(recallCategoryList) And recallCategoryList.Count > 0) Then
            Dim objRecallCategory As RecallCategory = CType(recallCategoryList(0), RecallCategory)
            lblServiceBulletinNo.Text = objRecallCategory.BuletinDescription
            lblDescription.Text = objRecallCategory.Description

            ViewState(ObjRecallCategoryIdentifier) = objRecallCategory.ID
            RecallCategoryID = objRecallCategory.ID
        End If
    End Sub


    Protected Sub dgRecallCategoryDetail_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgRecallCategoryDetail.ItemCommand
        Dim result As Boolean
        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim strID As String = CType(sessionHelper.GetSession(_strId), String)

                Dim ddlModelType As DropDownList = CType(e.Item.FindControl("ddlModelType"), DropDownList)
                Dim txtTypeCode As TextBox = CType(e.Item.FindControl("txtTypeCode"), TextBox)
                Dim txtPositionCode As TextBox = CType(e.Item.FindControl("txtPositionCode"), TextBox)
                Dim txtWorkCode As TextBox = CType(e.Item.FindControl("txtWorkCode"), TextBox)

                Dim categoryId As String = ddlModelType.SelectedValue
                Dim typeCode As String = txtTypeCode.Text.Trim
                Dim positionCode As String = txtPositionCode.Text.Trim
                Dim workCode As String = txtWorkCode.Text.Trim
                'If positionCode.Trim.ToUpper = "XEE999" AndAlso workCode.Trim.ToUpper = "99" Then
                '    MessageBox.Show("Kode posisi XEE999 dan Kode kerja 99 tidak dapat digunakan")
                '    Return
                'End If
                'Dim sValueAsArray = positionCode.ToCharArray()
                'If IsNumeric(sValueAsArray(0)) Then
                '    MessageBox.Show("Kode posisi harus berawalan Huruf")
                '    Return
                'End If
                result = SaveRecallCategoryDetailIntoList(categoryId, typeCode, positionCode, workCode, strID)

            Case "delete" 'Delete this datagrid item 
                Dim hidRecallCategoryDetailId As HiddenField = CType(e.Item.FindControl("hidRecallCategoryDetailId"), HiddenField)
                Dim lblCategoryCode As Label = CType(e.Item.FindControl("lblCategoryCode"), Label)
                Dim lblTypeCode As Label = CType(e.Item.FindControl("lblTypeCode"), Label)
                Dim lblPositionCode As Label = CType(e.Item.FindControl("lblPositionCode"), Label)
                Dim lblWorkCode As Label = CType(e.Item.FindControl("lblWorkCode"), Label)

                Dim paramRecallCategoryDetailId As String = hidRecallCategoryDetailId.Value
                Dim paramCategoryId As String = GetCategoryId(lblCategoryCode.Text.Trim)
                Dim paramTypeCode As String = lblTypeCode.Text.Trim
                Dim paramPositionCode As String = lblPositionCode.Text.Trim
                Dim paramWorkCode As String = lblWorkCode.Text.Trim
                result = RemoveRecallCategoryDetailFromList(paramRecallCategoryDetailId, paramCategoryId, paramTypeCode, paramPositionCode, paramWorkCode)

        End Select

        If result = True Then
            BindGrid()
            buttonSave.Enabled = True
        End If
    End Sub

    Protected Sub dgKerusakan_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRecallCategoryDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            'SetInlineButtonEvents(e)
            LoadGridElementDataSource(sender, e)
        End If

        Dim rowIndex As Integer = e.Item.ItemIndex
        If (rowIndex >= 0) Then
            e.Item.Cells(0).Text = rowIndex + 1
        End If
    End Sub

    Private Sub SetInlineButtonEvents(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblSearchTypeCode As Label = CType(e.Item.FindControl("lblSearchTypeCode"), Label)
        lblSearchTypeCode.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpVechileType.aspx", "", 710, 700, "getSelectedTypeCode")
    End Sub

    Private Sub LoadGridDataSource()
        Dim recallCategoryDetailList As ArrayList

        Dim objRecallCategoryIdentifier As Integer = CType(ViewState(objRecallCategoryIdentifier), Integer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallCategoryDetail), "RecallCategoryID", RecallCategoryID))
        criterias.opAnd(New Criteria(GetType(RecallCategoryDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objRecallCategoryFacade As RecallCategoryDetailFacade = New RecallCategoryDetailFacade(User)

        recallCategoryDetailList = objRecallCategoryFacade.Retrieve(criterias)
        sessionHelper.SetSession(_dataList, recallCategoryDetailList)
        buttonSave.Enabled = False
        buttonTambah.Visible = False
    End Sub

    Private Sub BindGrid()
        Dim recallCategoryDetailList As ArrayList = CType(sessionHelper.GetSession(_dataList), ArrayList)

        If (Not IsNothing(recallCategoryDetailList)) Then
            dgRecallCategoryDetail.DataSource = recallCategoryDetailList
            dgRecallCategoryDetail.VirtualItemCount = 0
            dgRecallCategoryDetail.DataBind()
        End If
    End Sub



    Private Sub LoadGridElementDataSource(sender As Object, e As DataGridItemEventArgs)
        Dim ddlModelType As DropDownList = e.Item.FindControl("ddlModelType")

        Dim objCategoryFacade As CategoryFacade = New CategoryFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim categories As ArrayList = objCategoryFacade.Retrieve(criterias)

        Dim li As ListItem
        If (Not IsNothing(categories)) Then
            ddlModelType.Items.Clear()

            For Each objCategory As Category In categories
                If objCategory.ID <> 3 Then
                    li = New ListItem(objCategory.CategoryCode, objCategory.ID.ToString)
                    ddlModelType.Items.Add(li)
                End If
            Next
        End If
    End Sub

    Private Sub AddRecallCategoryDetail(source As Object, e As DataGridCommandEventArgs)

    End Sub

    Function RemoveRecallCategoryDetailFromList(ByVal strId As String, ByVal paramCategoryId As String, ByVal paramTypeCode As String, ByVal paramPositionCode As String, ByVal paramWorkCode As String) As Boolean
        Dim recallCategoryDetailList As ArrayList = CType(sessionHelper.GetSession(_dataList), ArrayList)
        Dim removedRecallCategoryDetailList As ArrayList = CType(sessionHelper.GetSession(_deletedData), ArrayList)
        If (IsNothing(removedRecallCategoryDetailList)) Then
            removedRecallCategoryDetailList = New ArrayList()
        End If
        Dim removedItemObject As ArrayList = New ArrayList

        For Each item As RecallCategoryDetail In recallCategoryDetailList
            If (strId.Trim <> "0") Then
                If (strId = item.ID) Then
                    removedItemObject.Add(item)
                    removedRecallCategoryDetailList.Add(item)
                End If
            Else
                If (item.CategoryMaster.ID = paramCategoryId And item.WorkCode = paramWorkCode And item.PositionCode = paramPositionCode And item.VehicleTypeCode = paramTypeCode) Then
                    removedItemObject.Add(item)
                End If
            End If
        Next

        For Each item As RecallCategoryDetail In removedItemObject
            recallCategoryDetailList.Remove(item)
        Next

        sessionHelper.SetSession(_dataList, recallCategoryDetailList)
        sessionHelper.SetSession(_deletedData, removedRecallCategoryDetailList)

        Return True
    End Function

    Function IsExistRecallCategoryDetailFromList(ByVal paramCategoryId As String, ByVal paramTypeCode As String, ByVal paramPositionCode As String, ByVal paramWorkCode As String) As Boolean
        Dim recallCategoryDetailList As ArrayList = CType(sessionHelper.GetSession(_dataList), ArrayList)

        If Not IsNothing(recallCategoryDetailList) Then
            For Each item As RecallCategoryDetail In recallCategoryDetailList
                If (item.CategoryMaster.ID = paramCategoryId And item.VehicleTypeCode = paramTypeCode And item.LaborMaster.LaborCode = paramPositionCode And item.LaborMaster.WorkCode = paramWorkCode) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Private Sub RemoveRecallCategoryDetail(ByVal strId As Integer)
        Dim objRecallCategory As RecallCategoryDetail = (New RecallCategoryDetailFacade(User)).Retrieve(strId)
        Dim objRecallCategoryDetailFacade As RecallCategoryDetailFacade = New RecallCategoryDetailFacade(User)
        objRecallCategoryDetailFacade.Delete(objRecallCategory)
        MessageBox.Show("Data berhasil dihapus!")
        Server.Transfer("./FrmRecallCategoryDetail.aspx?strId=" + paramStrId)
    End Sub

    Protected Sub buttonBack_Click(sender As Object, e As EventArgs) Handles buttonBack.Click
        If dgRecallUpload.Visible Then
            dgRecallCategoryDetail.Visible = True
            dgRecallUpload.Visible = False
            buttonTambah.Visible = False
            buttonSave.Visible = True
        Else
        sessionHelper.RemoveSession(_dataList)
        sessionHelper.RemoveSession(_deletedData)
        Server.Transfer("./FrmRecallCategory.aspx")
        End If
    End Sub

    Protected Sub buttonSave_Click(sender As Object, e As EventArgs) Handles buttonSave.Click
        ApplyInsertedData()
        ApplyRemovedData()
        MessageBox.Show("Simpan data berhasil")
        Server.Transfer("./FrmRecallCategoryDetail.aspx?actionMode=First&strId=" + paramStrId.Split(",")(0))
        buttonSave.Enabled = False
    End Sub

    Private Sub ApplyInsertedData()
        Dim insertedDataList As ArrayList = sessionHelper.GetSession(_dataList)
        Dim strId As String = sessionHelper.GetSession(_strId)

        If (Not IsNothing(insertedDataList)) Then
            For Each item As RecallCategoryDetail In insertedDataList
                If (item.ID = 0) Then
                    Dim objRecallCategoryDetailFacade As RecallCategoryDetailFacade

                    objRecallCategoryDetailFacade = New RecallCategoryDetailFacade(User)
                    objRecallCategoryDetailFacade.Insert(item)
                End If
            Next
        End If
    End Sub

    Private Sub ApplyRemovedData()
        Dim removedDataList As ArrayList = sessionHelper.GetSession(_deletedData)
        If (Not IsNothing(removedDataList)) Then
            For Each item As RecallCategoryDetail In removedDataList
                Dim objRecallCategory As RecallCategoryDetail = (New RecallCategoryDetailFacade(User)).Retrieve(item.ID)
                Dim objRecallCategoryDetailFacade As RecallCategoryDetailFacade = New RecallCategoryDetailFacade(User)
                objRecallCategoryDetailFacade.Delete(objRecallCategory)
            Next
        End If
    End Sub

    Protected Sub LnkTemplate_Click(sender As Object, e As EventArgs) Handles LnkTemplate.Click
        Dim strName As String = "Templates-UploadAssignKodePosisi.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Recall\" & strName)
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Upload()
        buttonTambah.Enabled = tambahVisible
        buttonTambah.Visible = True
        buttonSave.Visible = False
    End Sub

    Private Sub Upload()
        If (Not infWSCData.PostedFile Is Nothing) AndAlso (infWSCData.PostedFile.ContentLength > 0) Then

            Dim fileExt As String = Path.GetExtension(infWSCData.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            Dim result As Boolean = False
            Me.buttonSave.Enabled = False
            Me.dgRecallUpload.Visible = True
            Me.dgRecallUpload.DataSource = New ArrayList
            Me.dgRecallUpload.DataBind()

            Try
                Dim SrcFile As String = Path.GetFileName(infWSCData.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                Dim i As Integer = 0
                Dim objReader As IExcelDataReader = Nothing
                Dim ArrUpload As New ArrayList
                Dim recallCatDetail As RecallCategoryDetail

                Dim objUpload As New UploadToWebServer
                objUpload.Upload(infWSCData.PostedFile.InputStream, targetFile)

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)
                    '   objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    If fileExt.ToLower.Contains("xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    Else
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then
                        While objReader.Read()
                            If i >= 4 Then
                                recallCatDetail = New RecallCategoryDetail
                                Dim strID As String = CType(sessionHelper.GetSession(_strId), String)
                                Dim No As String = objReader.GetString(0).Trim()
                                Dim typeCode As String = objReader.GetString(1).Trim()
                                Dim positionCode As String = objReader.GetString(2).Trim()
                                Dim workCode As String = objReader.GetString(3).Trim()
                                Dim _category As Category = GetCategoryIdFromVehicleTypeCode(typeCode)
                                Dim vehicleTypeId As Integer = GetVehicleTypeId(typeCode, _category.ID)
                                Dim laborId As Integer = GetLaborId(vehicleTypeId, positionCode, workCode)
                                Dim recallCategoryId As Integer = GetRecallCategoryId(strID)

                                Dim sValueAsArray = positionCode.ToCharArray()
                                'If IsNumeric(sValueAsArray(0)) Then
                                '    recallCatDetail.ErrorMessage = "Kode posisi harus berawalan Huruf"
                                'End If
                                'If positionCode.Trim.ToUpper = "XEE999".ToUpper AndAlso workCode.Trim.ToUpper = "99" Then
                                '    recallCatDetail.ErrorMessage = "Kode posisi XEE999 dan Kode kerja 99 tidak dapat digunakan"
                                'End If
                                If typeCode = "" Then
                                    recallCatDetail.ErrorMessage = "Tipe Kendaraan tidak terdaftar"
                                    tambahVisible = False
                                End If
                                If IsNothing(_category) Then
                                    recallCatDetail.ErrorMessage = "Tipe Kendaraan tidak terdaftar"
                                    tambahVisible = False
                                End If
                                If positionCode = "" Then
                                    recallCatDetail.ErrorMessage = "Kode posisi tidak terdaftar"
                                    tambahVisible = False
                                End If
                                If workCode = "" Then
                                    recallCatDetail.ErrorMessage = "Kode kerja tidak terdaftar"
                                    tambahVisible = False
                                End If
                                If laborId = 0 Then
                                    recallCatDetail.ErrorMessage = "Kode Posisi dan Kode kerja tidak terdaftar"
                                    tambahVisible = False
                                End If

                                recallCatDetail.VehicleTypeCodeTemp = typeCode
                                recallCatDetail.RecallCategoryID = recallCategoryId
                                recallCatDetail.LaborMasterID = laborId
                                recallCatDetail.PositionCode = positionCode
                                recallCatDetail.WorkCode = workCode
                                recallCatDetail.Status = 0

                                ArrUpload.Add(recallCatDetail)
                            End If
                            i = i + 1
                        End While
                    End If
                End Using

                If ArrUpload.Count > 0 Then
                    sessionHelper.SetSession(_dataUploadList, ArrUpload)
                    buttonSave.Enabled = True
                End If

                dgRecallUpload.DataSource = ArrUpload
                dgRecallUpload.DataBind()
                dgRecallUpload.Visible = True
                dgRecallCategoryDetail.Visible = False

            Catch ex As Exception
                MessageBox.Show("Fail To Process")
                dgRecallCategoryDetail.Visible = True
                dgRecallUpload.Visible = False
            End Try
        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If
    End Sub

    Private Function GetCategoryIdFromVehicleTypeCode(ByVal typeCode As String) As Category
        'SELECT TOP 100 CategoryCode FROM dbo.VechileType
        'JOIN dbo.Category ON Category.ID = VechileType.CategoryID
        'WHERE VechileTypeCode = 'WL20'
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, typeCode))
        Try
            Dim _vechileType As VechileType = New VechileTypeFacade(User).Retrieve(crit)(0)
            Return _vechileType.Category
        Catch
            Return Nothing
        End Try
    End Function

    Protected Sub dgRecallUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRecallUpload.ItemDataBound
        'Dim tempArr As ArrayList = ViewState("TypeCode")
        'For i As Integer = 0 To dgRecallUpload.Items.Count - 1
        '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '        Dim recallCatDetail As RecallCategoryDetail = tempArr(i)
        '        Dim lblTypeCode As Label = CType(e.Item.FindControl("lblTypeCode"), Label)
        '        lblTypeCode.Text = recallCatDetail.VehicleTypeCode
        '        Dim aa = ""
        '    End If
        'Next
    End Sub

    Protected Sub buttonTambah_Click(sender As Object, e As EventArgs) Handles buttonTambah.Click
        For Each item As DataGridItem In dgRecallUpload.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim strID As String = CType(sessionHelper.GetSession(_strId), String)

                Dim lblTypeCode As Label = CType(item.FindControl("lblTypeCode"), Label)
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, lblTypeCode.Text))
                Dim arrCat As ArrayList = New VechileTypeFacade(User).Retrieve(crit)
                Dim cat As Category
                If arrCat.Count > 0 Then
                    cat = CType(arrCat(0), VechileType).Category
                Else
                    Return
                End If
                Dim lblKodePosisi As Label = CType(item.FindControl("lblKodePosisi"), Label)
                Dim lblKodeKerja As Label = CType(item.FindControl("lblKodeKerja"), Label)

                Dim categoryId As String = cat.ID
                Dim typeCode As String = lblTypeCode.Text.Trim
                Dim positionCode As String = lblKodePosisi.Text.Trim
                Dim workCode As String = lblKodeKerja.Text.Trim
                Dim result As Boolean = SaveRecallCategoryDetailIntoList(categoryId, typeCode, positionCode, workCode, strID)

            End If
        Next
        BindGrid()
        dgRecallCategoryDetail.Visible = True
        dgRecallUpload.Visible = False
        buttonTambah.Visible = False
        buttonSave.Visible = True
    End Sub
End Class