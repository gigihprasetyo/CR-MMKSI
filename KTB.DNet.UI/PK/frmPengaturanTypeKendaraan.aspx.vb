Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.Drawing.Color
Imports System
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports System.Linq
Imports System.Collections.Generic
Imports System.IO
Imports OfficeOpenXml
Imports Excel
Imports System.Reflection

Public Class frmPengaturanTypeKendaraan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub
#End Region

#Region "Custom Variable Declaration"
    Private arlDummy As ArrayList = New ArrayList
    Private m_input_PK_Privilege As Boolean = False
    Private m_konfirmasi_PK_Privilege As Boolean = False
    Private objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK
#End Region

#Region "Custom Method"
    Private Sub SetControlPrivilege()
        If m_input_PK_Privilege Or m_konfirmasi_PK_Privilege Then
            btnActivate.Visible = True
            btnNoActivate.Visible = True
        Else
            btnActivate.Visible = False
            btnNoActivate.Visible = False
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Transaction_Control_Input_PK_tambahan_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PK- Transaction Control")
        End If

        m_input_PK_Privilege = SecurityProvider.Authorize(Context.User, SR.Transaction_Control_Input_PK_tambahan_Privilege)

        m_konfirmasi_PK_Privilege = SecurityProvider.Authorize(Context.User, SR.Transaction_Control_Konfirmasi_PK_Privilege)

    End Sub

    Private Sub InitiatePage()
        ViewState("currSortColumn") = "VechileColor.ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        SetControlPrivilege()
        InitiateComboItem()
    End Sub

    Private Sub InitiateComboItem()
        ddlstatus.Items.Clear()
        ddlstatus.Items.Add(New ListItem("Silahkan Pilih", 9))
        ddlstatus.Items.Add(New ListItem("Aktif", 1))
        ddlstatus.Items.Add(New ListItem("Tidak Aktif", 0))

        'ddlDataUpload.Items.Clear()
        'With ddlDataUpload.Items
        '    .Add(New ListItem("Silahkan Pilih", "0"))
        '    .Add(New ListItem("Data Lengkap", "1"))
        '    .Add(New ListItem("Data Tidak Lengkap", "2"))
        'End With

        txtTahunPerakitan.Text = ""

        LoadItem_DDLKategori()

        txtDeskripsiKendaraan.Text = ""
    End Sub

    Private Sub LoadItem_DDLKategori()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        ddlKategori.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            listItem.Selected = False
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                    ddlKategori.Items.Add(listItem)
                End If
            End If
        Next
        ddlKategori.ClearSelection()
        ddlKategori.SelectedValue = 0
    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK = CType(CType(dtgVechileColorList.DataSource, ArrayList)(e.Item.ItemIndex), VechileColorIsActiveOnPK)
        If Not IsNothing(objVechileColorIsActiveOnPK) Then
            Dim _cb As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbltglUpdate As Label = CType(e.Item.FindControl("lbltglUpdate"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblUbahOleh As Label = CType(e.Item.FindControl("lblUbahOleh"), Label)
            Dim lblMaterialNumber As Label = CType(e.Item.FindControl("lblMaterialNumber"), Label)
            Dim lblMaterialDescription As Label = CType(e.Item.FindControl("lblMaterialDescription"), Label)
            Dim lblMaterialDescriptionDealer As Label = CType(e.Item.FindControl("lblMaterialDescriptionDealer"), Label)
            Dim lblTipeGeneral As Label = CType(e.Item.FindControl("lblTipeGeneral"), Label)
            Dim lblKodeKendaraan As Label = CType(e.Item.FindControl("lblKodeKendaraan"), Label)
            Dim lblModelKendaraan As Label = CType(e.Item.FindControl("lblModelKendaraan"), Label)
            Dim lblSalesModelKendaraan As Label = CType(e.Item.FindControl("lblSalesModelKendaraan"), Label)
            Dim lblWarnaKendaraan As Label = CType(e.Item.FindControl("lblWarnaKendaraan"), Label)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgVechileColorList.CurrentPageIndex * dtgVechileColorList.PageSize), String)
            End If
            lblKodeKendaraan.Text = objVechileColorIsActiveOnPK.VechileColor.VechileType.VechileTypeCode
            lblModelKendaraan.Text = objVechileColorIsActiveOnPK.VechileColor.VechileType.VechileModel.IndDescription
            If Not objVechileColorIsActiveOnPK.VechileColor.VechileType.SalesVechileModel Is Nothing Then
                lblSalesModelKendaraan.Text = objVechileColorIsActiveOnPK.VechileColor.VechileType.SalesVechileModel.NewVechileModelDesc
            End If
            lblMaterialDescription.Text = objVechileColorIsActiveOnPK.VechileColor.VechileType.Description
            lblMaterialDescriptionDealer.Text = objVechileColorIsActiveOnPK.VechileColor.VechileType.DescriptionDealer
            lblMaterialNumber.Text = objVechileColorIsActiveOnPK.VechileColor.MaterialNumber
            lblWarnaKendaraan.Text = objVechileColorIsActiveOnPK.VechileColor.ColorIndName
            lbltglUpdate.Text = Format(objVechileColorIsActiveOnPK.LastUpdateTime, "dd/MM/yyyy")
            lblStatus.Text = If(objVechileColorIsActiveOnPK.Status = 1, "Aktif", "Tidak Aktif")
            If Not objVechileColorIsActiveOnPK.VechileTypeGeneral Is Nothing Then
                lblTipeGeneral.Text = objVechileColorIsActiveOnPK.VechileTypeGeneral.Name
            End If
            e.Item.BackColor = If(objVechileColorIsActiveOnPK.Status = 0, System.Drawing.Color.Red, System.Drawing.Color.Empty)

            'Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
            'lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistory.aspx?DocType=" & LookUp.DocumentType.VechileColorIsActiveOnPKStatus & "&DocNumber=" & objVechileColorIsActiveOnPK.VechileColor.MaterialNumber & "_" & objVechileColorIsActiveOnPK.ProductionYear, "", 370, 400, "MaterialNumber")
            ''lblHistoryStatus.Visible = historyPriv

        End If
    End Sub

    Private Function BindDatagrid(ByVal Criterias As CriteriaComposite, indexPage As Integer, ByRef totalRow As Long) As ArrayList
        Dim dataResult As ArrayList
        'dataResult = New VechileColorIsActiveOnPKFacade(User).Retrieve(Criterias)
        dataResult = New VechileColorIsActiveOnPKFacade(User).RetrieveByCriteria(Criterias, indexPage, dtgVechileColorList.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        _sessHelper.SetSession("sessSearchResult", dataResult)
        Return dataResult
    End Function

    Private Sub DoSearch(indexPage As Integer)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileColorIsActiveOnPK), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))
        Dim listSource As ArrayList = New VechileColorIsActiveOnPKFacade(User).Retrieve(CType(_sessHelper.GetSession("sessCriteria"), CriteriaComposite), sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, indexPage, dtgVechileColorList.PageSize)
            dtgVechileColorList.DataSource = PagedList
            dtgVechileColorList.VirtualItemCount = listSource.Count
            dtgVechileColorList.DataBind()
        Else
            dtgVechileColorList.DataSource = New ArrayList
            dtgVechileColorList.VirtualItemCount = 0
            dtgVechileColorList.CurrentPageIndex = 0
            dtgVechileColorList.DataBind()
        End If
        _sessHelper.SetSession("sessSearchResult", listSource)
        'Dim totRow As Integer = 0
        'dtgVechileColorList.CurrentPageIndex = indexPage
        'Dim arrDealerAll As ArrayList = BindDatagrid(CType(_sessHelper.GetSession("sessCriteria"), CriteriaComposite), indexPage, totRow)
        'CommonFunction.SortListControl(arrDealerAll, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
        'dtgVechileColorList.DataSource = arrDealerAll
        'dtgVechileColorList.VirtualItemCount = totRow
        'dtgVechileColorList.DataBind()

        If (listSource.Count > 0) Then
            btnActivate.Enabled = True
            btnNoActivate.Enabled = True
        Else
            btnActivate.Enabled = False
            btnNoActivate.Enabled = False
        End If
    End Sub

    Private Sub BuildSearchCriteria()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileColor.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileColor.VechileType.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlKategori.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileColor.VechileType.Category.ID", MatchType.Exact, CType(ddlKategori.SelectedValue, Short)))
        End If
        If ddlSubCategory.Items.Count > 0 AndAlso ddlSubCategory.SelectedIndex <> 0 Then
            Dim strSql As String = String.Format("" & vbCrLf &
                "SELECT A.VechileModelID FROM SubCategoryVehicleToModel AS A " & vbCrLf &
                "INNER JOIN SubCategoryVehicle B ON B.ID = A.SubCategoryVehicleID " & vbCrLf &
                "INNER JOIN VechileModel AS C ON C.ID = A.VechileModelID " & vbCrLf &
                "WHERE 1=1 " & vbCrLf &
                "AND A.RowStatus=0 " & vbCrLf &
                "AND B.RowStatus=0 " & vbCrLf &
                "AND C.RowStatus=0 " & vbCrLf &
                "AND A.SubCategoryVehicleID={0} ", ddlSubCategory.SelectedValue)
            criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If txtMaterialNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileColor.MaterialNumber", MatchType.InSet, "('" & Me.txtMaterialNumber.Text.Replace(";", "','") & "')"))
        End If
        If txtTahunPerakitan.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "ProductionYear", MatchType.Exact, txtTahunPerakitan.Text))
        End If
        If txtTahunModel.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "ModelYear", MatchType.Exact, txtTahunModel.Text))
        End If
        If txtTypeGeneral.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileTypeGeneral.Name", MatchType.Partial, txtTypeGeneral.Text.Trim))
        End If
        If txtModelKendaraan.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileColor.VechileType.SalesVechileModel.NewVechileModelDesc", MatchType.Partial, txtModelKendaraan.Text.Trim))
        End If
        If txtVechileType.Text.Trim <> "" Then
            Dim sqlStr As String = String.Format("" & vbCrLf &
                "select a.ID from VechileColor a " & vbCrLf &
                "join VechileType b on a.VechileTypeID=b.ID " & vbCrLf &
                "where b.VechileTypeCode in ('" & txtVechileType.Text.Replace(";", "','") & "')")

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "VechileColor.ID", MatchType.InSet, "(" & sqlStr & ")"))
        End If
        Dim _status As Short = CType(ddlstatus.SelectedValue, Short)
        If Not _status = 9 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "Status", MatchType.Exact, CType(ddlstatus.SelectedValue, Short)))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColorIsActiveOnPK), "Status", MatchType.No, "X"))

        _sessHelper.SetSession("sessCriteria", criterias)
    End Sub

    Private Function UpdateToDB(ByVal dataFor As List(Of VechileColorIsActiveOnPK)) As Boolean
        Dim _dataFor As ArrayList = New ArrayList()
        _dataFor.AddRange(dataFor)
        Dim nTransResult = New VechileColorIsActiveOnPKFacade(User).UpdateTransaction(_dataFor)
        Return nTransResult > -1
        'Return True
    End Function

    Private Function getVechileTypeGeneral(ByVal categoryID As Integer, ByVal name As String) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileTypeGeneral), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(VechileTypeGeneral), "Name", MatchType.Exact, name))
        crit.opAnd(New Criteria(GetType(VechileTypeGeneral), "SubCategoryVehicle", MatchType.Exact, categoryID))
        Dim arrVechileTypeGen As ArrayList = New VechileTypeGeneralFacade(User).Retrieve(crit)
        If Not arrVechileTypeGen Is Nothing AndAlso arrVechileTypeGen.Count > 0 Then
            Return CType(arrVechileTypeGen(0), VechileTypeGeneral).ID
        End If

        Return 0
    End Function

    Private Function validateVechileTypeandMaterialNumber(ByVal vechileTypeCode As String, ByVal materialNo As String, ByRef materialNoErr As String) As Boolean
        Dim arrVechileTpe As String() = vechileTypeCode.Trim.Split(";")
        Dim arrMaterialNo As String() = txtMaterialNumber.Text.Trim.Split(";")
        Dim status As Boolean = False

        For Each Type As String In arrVechileTpe
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, Type))
            Dim vechileType As VechileType = New VechileTypeFacade(User).Retrieve(crit)(0)
            For Each material As String In arrMaterialNo
                Dim crit1 As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, 0))
                crit1.opAnd(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, material))
                Dim arrVechileColor As ArrayList = New VechileColorFacade(User).Retrieve(crit1)
                If IsNothing(arrVechileColor) OrElse arrVechileColor.Count = 0 Then
                    materialNoErr = material
                    Return False
                End If

                If vechileType.ID = CType(arrVechileColor(0), VechileColor).VechileType.ID Then
                    status = True
                End If
            Next

            If Not status Then
                Return False
            End If
        Next


        Return True
    End Function

    Private Sub setEditField(ByVal status As Boolean)
        ddlKategori.Enabled = status
        ddlSubCategory.Enabled = status
        txtVechileType.Enabled = status
        txtMaterialNumber.Enabled = status
        'txtTahunPerakitan.Enabled = status
        btnSearch.Enabled = status
        btnActivate.Enabled = status
        btnNoActivate.Enabled = status
        lblVechileType.Visible = status
        lblMaterialNumber.Visible = status
    End Sub

    Private Function validateTypeGeneral(Optional ByRef ID As Integer = 0) As Boolean
        If txtTypeGeneralID.Text <> "" And txtTypeGeneralID.Text <> "0" Then
            If txtTypeGeneral.Text <> "" Then
                Dim tempTypeGeneral As VechileTypeGeneral = New VechileTypeGeneralFacade(User).Retrieve(CShort(txtTypeGeneralID.Text))
                If Not IsNothing(tempTypeGeneral) Then
                    If tempTypeGeneral.Name = txtTypeGeneral.Text.Trim Then
                        ID = tempTypeGeneral.ID
                        Return True
                    Else
                        GoTo retyr
                    End If
                End If
            End If
retyr:
            ID = getVechileTypeGeneral(ddlSubCategory.SelectedValue, txtTypeGeneral.Text)
            If ID = 0 Then
                Return False
            End If
        End If
    End Function

    Private Function validateVechileClrActiveOnPK(ByVal obj As VechileColorIsActiveOnPK, ByVal vcID As Integer) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.ID", MatchType.Exact, vcID))
        criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ModelYear", MatchType.Exact, obj.ModelYear))
        criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ProductionYear", MatchType.Exact, obj.ProductionYear))
        Dim arrTemp As ArrayList = New VechileColorIsActiveOnPKFacade(User).Retrieve(criterias)

        If arrTemp.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Function getSubCategory(ByVal typeCode As String) As SubCategoryVehicle
        Dim strSql As String = String.Format("select a.ID from SubCategoryVehicle a " &
            "join SubCategoryVehicleToModel b on a.ID = b.SubCategoryVehicleID " &
            "join VechileModel c on c.ID = b.VechileModelID " &
            "join VechileType d on d.ModelID = c.ID " &
            "where d.VechileTypeCode = '{0}'", typeCode)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.Exact, "( " & strSql & ")"))

        Return CType(New SubCategoryVehicleFacade(User).Retrieve(criterias)(0), SubCategoryVehicle)
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            'BindDDLVechileType()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)

            'lblKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"
            lblMaterialNumber.Attributes("onclick") = "ShowPPMaterialNumberSelection();"

            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub

    'Private Sub BindDDLVechileType()
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
    '    If ddlKategori.SelectedIndex > 0 Then
    '        Dim cat As Short = CInt(ddlKategori.SelectedValue)
    '        criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, cat))
    '    End If

    '    If ddlSubCategory.SelectedIndex > 0 Then
    '        Dim strSql As String = String.Format("" & vbCrLf &
    '            "SELECT A.VechileModelID FROM SubCategoryVehicleToModel AS A " & vbCrLf &
    '            "INNER JOIN SubCategoryVehicle B ON B.ID = A.SubCategoryVehicleID " & vbCrLf &
    '            "INNER JOIN VechileModel AS C ON C.ID = A.VechileModelID " & vbCrLf &
    '            "WHERE 1=1 " & vbCrLf &
    '            "AND A.RowStatus=0 " & vbCrLf &
    '            "AND B.RowStatus=0 " & vbCrLf &
    '            "AND C.RowStatus=0 " & vbCrLf &
    '            "AND A.SubCategoryVehicleID={0} ", ddlSubCategory.SelectedValue)
    '        criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
    '    End If
    '    Dim arrList As ArrayList = New VechileTypeFacade(User).Retrieve(criterias)

    '    Dim i%
    '    ddlVechileType.Items.Clear()
    '    If arrList.Count > 0 Then
    '        ddlVechileType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    '        i = 1
    '        For Each obj As VechileType In arrList
    '            With ddlVechileType
    '                .Items.Insert(i, New ListItem(obj.Description, obj.VechileTypeCode))
    '            End With
    '            i += 1
    '        Next
    '    End If
    '    ddlVechileType.SelectedIndex = 0
    'End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BuildSearchCriteria()
        dtgVechileColorList.CurrentPageIndex = 0
        DoSearch(dtgVechileColorList.CurrentPageIndex)
    End Sub

    Private Sub dtgVechileColorList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgVechileColorList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
        End If
    End Sub

    Protected Sub btnActivate_Click(sender As Object, e As EventArgs) Handles btnActivate.Click
        Dim listFor As List(Of VechileColorIsActiveOnPK) = New List(Of VechileColorIsActiveOnPK)
        Dim tempArrayList As ArrayList = _sessHelper.GetSession("sessSearchResult")
        Dim tempList As List(Of VechileColorIsActiveOnPK) = New List(Of VechileColorIsActiveOnPK)(From data As VechileColorIsActiveOnPK In tempArrayList Select data)
        Dim checkedLineCount As Integer = 0
        For Each dtgItem As DataGridItem In dtgVechileColorList.Items
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                Dim _ID As Integer = CType(dtgItem.Cells(2).Text, Integer)
                Dim objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK = tempList.Where(Function(i) i.id = _ID).FirstOrDefault
                objVechileColorIsActiveOnPK.Status = 1
                listFor.Add(objVechileColorIsActiveOnPK)
                checkedLineCount += 1
            End If
        Next
        If checkedLineCount > 0 Then
            If UpdateToDB(listFor) Then
                dtgVechileColorList.CurrentPageIndex = 0
                DoSearch(dtgVechileColorList.CurrentPageIndex)
                MessageBox.Show("Update Aktivasi Sukses")
            Else
                MessageBox.Show("Update Aktivasi Gagal")
            End If
        Else
            MessageBox.Show("Tidak ada data yang dipilih")
        End If
    End Sub

    Protected Sub btnNoActivate_Click(sender As Object, e As EventArgs) Handles btnNoActivate.Click
        Dim listFor As List(Of VechileColorIsActiveOnPK) = New List(Of VechileColorIsActiveOnPK)
        Dim tempArrayList As ArrayList = _sessHelper.GetSession("sessSearchResult")
        Dim tempList As List(Of VechileColorIsActiveOnPK) = New List(Of VechileColorIsActiveOnPK)(From data As VechileColorIsActiveOnPK In tempArrayList Select data)
        Dim checkedLineCount As Integer = 0
        For Each dtgItem As DataGridItem In dtgVechileColorList.Items
            If CType(dtgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked Then
                Dim _ID As Integer = CType(dtgItem.Cells(2).Text, Integer)
                Dim objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK = tempList.Where(Function(i) i.id = _ID).FirstOrDefault
                objVechileColorIsActiveOnPK.Status = 0
                listFor.Add(objVechileColorIsActiveOnPK)
                checkedLineCount += 1
            End If
        Next
        If checkedLineCount > 0 Then
            If UpdateToDB(listFor) Then
                dtgVechileColorList.CurrentPageIndex = 0
                DoSearch(dtgVechileColorList.CurrentPageIndex)
                MessageBox.Show("Update Non Aktivasi Sukses")
            Else
                MessageBox.Show("Update Non Aktivasi Gagal")
            End If
        Else
            MessageBox.Show("Tidak ada data yang dipilih")
        End If

    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        'BindDDLVechileType()
    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlKategori.SelectedItem.Text)
        'BindDDLVechileType()
    End Sub

    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        'Dim strName As String = "Template_Upload_Tipe_Kendaraan.xlsx"
        'Response.Redirect("../downloadlocal.aspx?file=DataFile\PK\" & strName)

        Dim wb As FileInfo = New FileInfo(Server.MapPath("~/DataFile/PK/Template_Upload_Tipe_Kendaraan.xlsx"))
        Using package As ExcelPackage = New ExcelPackage(wb)
            Dim qr As String = ""
            Dim tempdata As DataTable

            Dim wsTypeGeneral As ExcelWorksheet = package.Workbook.Worksheets(2)
            Dim wsSalesModel As ExcelWorksheet = package.Workbook.Worksheets(3)
            Dim wsAktivasiKendaraan As ExcelWorksheet = package.Workbook.Worksheets(1)

            Dim tempCell As String = ""

            Dim currentRow As Long = 0

            'Load Data Tipe General
            Dim critVechileTypeGeneral As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileTypeGeneral), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arrVechileTypeGeneral As ArrayList = New VechileTypeGeneralFacade(User).Retrieve(critVechileTypeGeneral)

            currentRow = 2
            For Each objVechileTypeGeneral As VechileTypeGeneral In arrVechileTypeGeneral
                wsTypeGeneral.Cells(currentRow, 1).Value = objVechileTypeGeneral.ID
                wsTypeGeneral.Cells(currentRow, 2).Value = objVechileTypeGeneral.ID & "-" & objVechileTypeGeneral.Name

                currentRow += 1
            Next

            'Load Data Sales Model
            Dim critSalesVechileModel As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesVechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arrSalesVechileModel As ArrayList = New SalesVechileModelFacade(User).Retrieve(critSalesVechileModel)

            currentRow = 2
            For Each objSalesVechileModel As SalesVechileModel In arrSalesVechileModel
                wsSalesModel.Cells(currentRow, 1).Value = objSalesVechileModel.ID
                wsSalesModel.Cells(currentRow, 2).Value = objSalesVechileModel.ID & "-" & objSalesVechileModel.NewVechileModelDesc
                wsSalesModel.Cells(currentRow, 3).Value = objSalesVechileModel.Category.CategoryCode
                wsSalesModel.Cells(currentRow, 4).Value = objSalesVechileModel.VechileModel.IndDescription

                currentRow += 1
            Next

            'Load Data VechileColorIsActiveOnPK
            Dim arrData As ArrayList = CType(_sessHelper.GetSession("sessSearchResult"), ArrayList)
            currentRow = 2
            For Each objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK In arrData
                If Not objVechileColorIsActiveOnPK.VechileTypeGeneral Is Nothing Then
                    wsAktivasiKendaraan.Cells(currentRow, 1).Value = objVechileColorIsActiveOnPK.VechileTypeGeneral.ID & "-" & objVechileColorIsActiveOnPK.VechileTypeGeneral.Name
                End If
                wsAktivasiKendaraan.Cells(currentRow, 2).Value = objVechileColorIsActiveOnPK.VechileColor.VechileType.VechileTypeCode
                wsAktivasiKendaraan.Cells(currentRow, 3).Value = objVechileColorIsActiveOnPK.VechileColor.VechileType.VechileModel.IndDescription
                If Not objVechileColorIsActiveOnPK.VechileColor.VechileType.SalesVechileModel Is Nothing Then
                    wsAktivasiKendaraan.Cells(currentRow, 4).Value = objVechileColorIsActiveOnPK.VechileColor.VechileType.SalesVechileModel.ID & "-" & objVechileColorIsActiveOnPK.VechileColor.VechileType.SalesVechileModel.NewVechileModelDesc
                End If
                wsAktivasiKendaraan.Cells(currentRow, 5).Value = objVechileColorIsActiveOnPK.VechileColor.VechileType.Description
                wsAktivasiKendaraan.Cells(currentRow, 6).Value = objVechileColorIsActiveOnPK.VechileColor.VechileType.DescriptionDealer
                wsAktivasiKendaraan.Cells(currentRow, 7).Value = objVechileColorIsActiveOnPK.VechileColor.MaterialNumber
                wsAktivasiKendaraan.Cells(currentRow, 8).Value = objVechileColorIsActiveOnPK.VechileColor.ColorIndName
                wsAktivasiKendaraan.Cells(currentRow, 9).Value = objVechileColorIsActiveOnPK.ProductionYear
                wsAktivasiKendaraan.Cells(currentRow, 10).Value = objVechileColorIsActiveOnPK.ModelYear
                If Not objVechileColorIsActiveOnPK.Status = "" Then
                    wsAktivasiKendaraan.Cells(currentRow, 11).Value = IIf(objVechileColorIsActiveOnPK.Status = "1", "Aktif", "Tidak Aktif")
                End If
                wsAktivasiKendaraan.Cells(currentRow, 28).Value = objVechileColorIsActiveOnPK.VechileColor.VechileType.ID
                wsAktivasiKendaraan.Cells(currentRow, 29).Value = objVechileColorIsActiveOnPK.id

                currentRow += 1
            Next

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=TemplateUploadTipeKendaraan.xlsx")
            Response.Charset = ""
            Me.EnableViewState = False
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.BinaryWrite(package.GetAsByteArray())
            Response.End()

        End Using
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim sb As StringBuilder = New StringBuilder
        Dim retValue As Integer = 0
        If fileUploadExcel.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If fileUploadExcel.PostedFile.ContentLength <> fileUploadExcel.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "SalesCampaign_Benefit"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)

                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(fileUploadExcel.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Salah Extention")
                End If

                Dim SrcFile As String = Path.GetFileName(fileUploadExcel.PostedFile.FileName)   '-- Source file name
                SrcFile = New Date().Now.ToString("yyyyMMddhhmmss") & SrcFile
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

                fileUploadExcel.PostedFile.SaveAs(targetFile)

                Dim objReader As IExcelDataReader = Nothing
                Dim list As ArrayList = New ArrayList
                Dim checkSalah As Boolean = False
                Dim checkKosong As Boolean = True
                Dim totalUpload As Integer = 0
                Dim arrVechileColorIsActiveOnPK As New ArrayList

                Dim i As Integer = 0

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                    '   objReader = ExcelReaderFactory.CreateBinaryReader(stream)

                    If ext.ToLower.Contains("xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    Else
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()
                            Dim oVechileColorIsActiveOnPKFac As New VechileColorIsActiveOnPKFacade(User)
                            Dim objVechileColorIsActiveOnPK As New VechileColorIsActiveOnPK
                            Dim oVechileTypeFac As New VechileTypeFacade(User)
                            Dim objVechileType As New VechileType

                            If Not i > 0 Then
                                i += 1
                                Continue While
                            End If

                            If Not IsNothing(objReader.GetString(27)) Then
                                Try
                                    objVechileType = oVechileTypeFac.Retrieve(CInt(objReader.GetString(27).Trim()))
                                    If Not objVechileType.ID > 0 Then
                                        sb.Append("Kode Kendaraan " & objReader.GetString(1).Trim() & " Tidak Ditemukan;")
                                        checkSalah = True
                                    End If

                                Catch ex As Exception
                                    sb.Append("Kode Kendaraan " & objReader.GetString(1).Trim() & " Tidak Valid;")
                                    checkSalah = True
                                End Try
                            Else
                                Continue While
                            End If

                            If Not IsNothing(objReader.GetString(28)) Then
                                Try
                                    objVechileColorIsActiveOnPK = oVechileColorIsActiveOnPKFac.Retrieve(CInt(objReader.GetString(28).Trim()))
                                    If Not objVechileColorIsActiveOnPK.id > 0 Then
                                        sb.Append("Aktivasi Kode Kendaraan " & objReader.GetString(28).Trim() & " Tidak Ditemukan;")
                                        checkSalah = True
                                    End If

                                Catch ex As Exception
                                    sb.Append("Aktivasi Kode Kendaraan " & objReader.GetString(28).Trim() & " Tidak Valid;")
                                    checkSalah = True
                                End Try
                            Else
                                Continue While
                            End If

                            If Not IsNothing(objReader.GetString(29)) Then
                                If objVechileColorIsActiveOnPK.id > 0 Then
                                    Try
                                        Dim tipeGeneral As String = objReader.GetString(0).Trim().Split("-")(0)
                                        objVechileColorIsActiveOnPK.VechileTypeGeneral = New VechileTypeGeneralFacade(User).Retrieve(CShort(tipeGeneral))
                                        objVechileColorIsActiveOnPK.ModelYear = CShort(objReader.GetString(9).Trim())
                                        objVechileColorIsActiveOnPK.Status = IIf(objReader.GetString(10).Trim().ToLower() = "aktif", 1, 0)
                                        If objVechileColorIsActiveOnPK.ProductionYear = CShort(objReader.GetString(8).Trim()) Then
                                            oVechileColorIsActiveOnPKFac.Update(objVechileColorIsActiveOnPK)
                                        Else
                                            objVechileColorIsActiveOnPK.ProductionYear = CShort(objReader.GetString(8).Trim())
                                            objVechileColorIsActiveOnPK.id = 0
                                            oVechileColorIsActiveOnPKFac.Insert(objVechileColorIsActiveOnPK)
                                        End If

                                        arrVechileColorIsActiveOnPK.Add(objVechileColorIsActiveOnPK)
                                    Catch ex As Exception
                                        sb.Append("Tahun Perakitan atau Tahun Model Tidak Valid;")
                                        checkSalah = True
                                    End Try
                                End If
                            Else
                                sb.Append("Tipe General Tidak Valid;")
                                Continue While
                            End If

                            If Not IsNothing(objReader.GetString(30)) Then
                                If objVechileType.ID > 0 Then
                                    Try
                                        Dim salesID = objReader.GetString(3).Trim().Split("-")(0)
                                        objVechileType.DescriptionDealer = objReader.GetString(5)
                                        objVechileType.SalesVechileModel = New SalesVechileModel(CShort(salesID))
                                        oVechileTypeFac.Update(objVechileType)
                                    Catch ex As Exception
                                        sb.Append("Deskripsi Kendaraan Dealer " & objReader.GetString(5) & " Tidak Valid;")
                                        checkSalah = True
                                    End Try
                                End If
                            Else
                                sb.Append("Sales Model Tidak Valid;")
                                Continue While
                            End If

                            i += 1
                        End While

                    End If

                End Using

                If i > 1 Then
                    checkKosong = False
                End If

                If checkSalah = True Then
                    MessageBox.Show(sb.ToString)
                    Return
                End If
                If checkKosong = True Then
                    MessageBox.Show("Data Excel tidak boleh ada yang kosong.")
                    Return
                End If

                dtgVechileColorList.DataSource = arrVechileColorIsActiveOnPK
                dtgVechileColorList.DataBind()

                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
                sapImp = Nothing
            End Try
        End If

        If retValue = 1 Then
            MessageBox.Show("Data Berhasil Diupdate.")
        End If
    End Sub

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "XLS" Then
            retValue = True
        ElseIf ext.ToUpper() = "XLSX" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dtgVechileColorList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(_sessHelper.GetSession("sessSearchResult"), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("TipeKendaraan", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim wb As FileInfo = New FileInfo(Server.MapPath("~/DataFile/PK/Detail_Tipe_Kendaraan.xlsx"))
        Using pck As New ExcelPackage(wb)
            Dim ws As ExcelWorksheet = pck.Workbook.Worksheets("TipeKendaraan")

            For i As Integer = 0 To Data.Count - 1
                Dim item As VechileColorIsActiveOnPK = Data(i)
                If Not item.VechileTypeGeneral Is Nothing Then
                    ws.Cells(i + 3, 1).Value = item.VechileTypeGeneral.Name
                End If
                ws.Cells(i + 3, 2).Value = item.VechileColor.VechileType.VechileTypeCode
                ws.Cells(i + 3, 3).Value = item.VechileColor.VechileType.VechileModel.IndDescription
                If Not item.VechileColor.VechileType.SalesVechileModel Is Nothing Then
                    ws.Cells(i + 3, 4).Value = item.VechileColor.VechileType.SalesVechileModel.NewVechileModelDesc
                End If
                ws.Cells(i + 3, 5).Value = item.VechileColor.VechileType.Description
                ws.Cells(i + 3, 6).Value = item.VechileColor.VechileType.DescriptionDealer
                ws.Cells(i + 3, 7).Value = item.VechileColor.MaterialNumber
                ws.Cells(i + 3, 8).Value = item.VechileColor.ColorIndName
                ws.Cells(i + 3, 9).Value = item.ProductionYear
                ws.Cells(i + 3, 10).Value = item.ModelYear
                ws.Cells(i + 3, 11).Value = Format(item.LastUpdateTime, "dd/MM/yyyy")
                ws.Cells(i + 3, 12).Value = IIf(item.Status = 1, "Aktif", "Tidak Aktif")
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim temp As VechileColorIsActiveOnPK = CType(_sessHelper.GetSession("VECHCLREDIT"), VechileColorIsActiveOnPK)
        If Not IsNothing(temp) Then
            _sessHelper.RemoveSession("VECHCLREDIT")
            setEditField(True)
        End If

        ddlKategori.SelectedIndex = 0
        ddlSubCategory.SelectedIndex = 0
        txtTahunPerakitan.Text = DateTime.Now.Year.ToString
        txtTahunModel.Text = ""
        txtTypeGeneral.Text = ""
        ddlstatus.SelectedIndex = 1
        'ddlVechileType.SelectedIndex = 0
        txtVechileType.Text = ""
        txtMaterialNumber.Text = ""
        dtgVechileColorList.CurrentPageIndex = 0
        BuildSearchCriteria()
        DoSearch(dtgVechileColorList.CurrentPageIndex)
        txtDeskripsiKendaraan.Text = ""
        txtModelKendaraan.Text = ""
        'ddlDataUpload.SelectedIndex = 0
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim vechileTypeGeneralID As Integer = 0
        If txtVechileType.Text.Trim = "" Then
            MessageBox.Show("Silahkan isi tipe kendaraan")
            Exit Sub
        End If
        If txtMaterialNumber.Text.Trim = "" Then
            MessageBox.Show("Material Number harap diisi dahulu")
            Exit Sub
        Else
            Dim materialNoErr As String = ""
            If Not validateVechileTypeandMaterialNumber(txtVechileType.Text, txtMaterialNumber.Text.Trim, materialNoErr) Then
                MessageBox.Show("Material Number " & materialNoErr & " tidak sesuai dengan tipe kendaraan")
                Exit Sub
            End If
        End If
        If txtTypeGeneral.Text = "" Then
            MessageBox.Show("Silahkan isi Tipe General")
            Exit Sub
        Else
            validateTypeGeneral(vechileTypeGeneralID)
            If vechileTypeGeneralID = 0 Then
                If txtTypeGeneral.Text.Trim <> "" Then
                    vechileTypeGeneralID = getVechileTypeGeneral(ddlSubCategory.SelectedValue, txtTypeGeneral.Text)
                    If vechileTypeGeneralID = 0 Then
                        MessageBox.Show("Tipe General tidak ditemukan")
                        Exit Sub
                    End If
                End If
            End If
        End If
        If txtTahunModel.Text = "" OrElse txtTahunModel.Text = "0" Then
            MessageBox.Show("Tahun Model tidak boleh kosong atau 0")
            Exit Sub
        End If
        If txtTahunPerakitan.Text = "" OrElse txtTahunPerakitan.Text = "0" Then
            MessageBox.Show("Tahun Perakitan tidak boleh kosong atau 0")
            Exit Sub
        End If
        If ddlstatus.SelectedIndex = 0 Then
            MessageBox.Show("Status harap diisi dahulu")
            Exit Sub
        End If

        Dim vechileTypeGeneral As VechileTypeGeneral = New VechileTypeGeneral(vechileTypeGeneralID)
        Dim oVechileColorIsActiveOnPKFac As New VechileColorIsActiveOnPKFacade(User)
        Dim oVechileTypeFac As New VechileTypeFacade(User)
        Dim _result As Integer = 0

        Dim arrVechileColorIsActiveOnPK As New ArrayList
        Dim arrVechileType As New ArrayList

        Dim temp As VechileColorIsActiveOnPK = CType(_sessHelper.GetSession("VECHCLREDIT"), VechileColorIsActiveOnPK)
        If Not IsNothing(temp) Then
            Dim tempVehType As VechileType = temp.VechileColor.VechileType
            temp.VechileTypeGeneral = vechileTypeGeneral
            temp.ModelYear = txtTahunModel.Text
            temp.Status = ddlstatus.SelectedValue
            If Not txtDeskripsiKendaraan.Text.Trim = "" Then
                tempVehType.DescriptionDealer = txtDeskripsiKendaraan.Text
            Else
                tempVehType.DescriptionDealer = ""
            End If
            If Not txtModelKendaraan.Text = "" Then
                Dim objSalesVechileModel As SalesVechileModel = New SalesVechileModelFacade(User).Retrieve(CShort(txtModelKendaraanID.Text.Trim))
                tempVehType.SalesVechileModel = objSalesVechileModel
            Else
                tempVehType.SalesVechileModel = Nothing
            End If
            If temp.ProductionYear = txtTahunPerakitan.Text Then
                _result = oVechileColorIsActiveOnPKFac.Update(temp)
            Else
                temp.id = 0
                temp.ProductionYear = txtTahunPerakitan.Text
                _result = oVechileColorIsActiveOnPKFac.Insert(temp)
            End If
            oVechileTypeFac.Update(tempVehType)
            setEditField(True)
            arrVechileColorIsActiveOnPK.Add(temp)
            arrVechileType.Add(tempVehType)
            GoTo result
        End If

        Dim aVechileColorIsActiveOnPKs As New ArrayList
        Dim oVechileColorFac As New VechileColorFacade(User)
        Dim objVechileColor As New VechileColor
        Dim objVechileType As New VechileType
        Dim arrMaterialNumber As String() = txtMaterialNumber.Text.Trim.Split(";")
        For Each matrial As String In arrMaterialNumber
            If matrial.Trim <> "" Then
                Dim objVechileColorIsActiveOnPK As New VechileColorIsActiveOnPK
                objVechileColor = oVechileColorFac.RetrieveByMaterialNumber(matrial.Trim)
                If Not IsNothing(objVechileColor) AndAlso objVechileColor.ID > 0 Then
                    objVechileType = objVechileColor.VechileType
                    Dim cVechileColorIsActiveOnPK As New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    cVechileColorIsActiveOnPK.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.ID", MatchType.Exact, objVechileColor.ID))
                    cVechileColorIsActiveOnPK.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ProductionYear", MatchType.Exact, txtTahunPerakitan.Text))
                    cVechileColorIsActiveOnPK.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ModelYear", MatchType.Exact, txtTahunModel.Text))
                    cVechileColorIsActiveOnPK.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileTypeGeneral", MatchType.Exact, vechileTypeGeneralID))
                    aVechileColorIsActiveOnPKs = oVechileColorIsActiveOnPKFac.Retrieve(cVechileColorIsActiveOnPK)
                    If aVechileColorIsActiveOnPKs.Count > 0 Then
                        objVechileColorIsActiveOnPK = CType(aVechileColorIsActiveOnPKs(0), VechileColorIsActiveOnPK)
                    End If
                    objVechileColorIsActiveOnPK.VechileColor = objVechileColor
                    objVechileColorIsActiveOnPK.ModelYear = txtTahunModel.Text
                    objVechileColorIsActiveOnPK.VechileTypeGeneral = vechileTypeGeneral
                    objVechileColorIsActiveOnPK.Status = ddlstatus.SelectedValue
                    If Not validateVechileClrActiveOnPK(objVechileColorIsActiveOnPK, objVechileColor.ID) Then
                        MessageBox.Show(String.Format("Tipe kendaraan dengan material number {0}, tahun model {1}, dan tahun perakitan {2} sudah ada", matrial, txtTahunModel.Text, txtTahunPerakitan.Text))
                        Exit Sub
                    End If
                    If Not txtDeskripsiKendaraan.Text.Trim = "" Then
                        objVechileType.DescriptionDealer = txtDeskripsiKendaraan.Text
                    End If
                    If Not objVechileColorIsActiveOnPK.ProductionYear = txtTahunPerakitan.Text Then
                        objVechileColorIsActiveOnPK.ProductionYear = txtTahunPerakitan.Text
                        objVechileColorIsActiveOnPK.id = 0
                    End If
                    arrVechileColorIsActiveOnPK.Add(objVechileColorIsActiveOnPK)
                    arrVechileType.Add(objVechileType)
                End If
            End If
        Next

        If arrVechileColorIsActiveOnPK.Count > 0 Then
            _result = oVechileColorIsActiveOnPKFac.UpdateTransaction(arrVechileColorIsActiveOnPK)
            oVechileTypeFac.UpdateWithTransaction(arrVechileType)
result:
            If _result < 1 Then
                MessageBox.Show("Simpan data gagal")
                Exit Sub
            Else
                MessageBox.Show("Simpan data sukses")
                RecordStatusChangeHistory(arrVechileColorIsActiveOnPK, ddlstatus.SelectedValue)
            End If
        Else
            MessageBox.Show("Material Number tidak valid")
            Exit Sub
        End If

        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Function IsStatusExist(ByVal _obj As VechileColorIsActiveOnPK, ByVal newStatus As Integer) As Boolean
        Try
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CInt(LookUp.DocumentType.VechileColorIsActiveOnPKStatus)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, _obj.VechileColor.MaterialNumber & "_" & _obj.ProductionYear))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, newStatus))
            Dim arlHistStatus As ArrayList = objStatusChangeHistoryFacade.Retrieve(criterias)
            If Not IsNothing(arlHistStatus) AndAlso arlHistStatus.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub RecordStatusChangeHistory(ByVal arrList As ArrayList, ByVal newStatus As Integer)
        For Each item As VechileColorIsActiveOnPK In arrList
            If Not IsStatusExist(item, newStatus) Then
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.VechileColorIsActiveOnPKStatus), item.VechileColor.MaterialNumber & "_" & item.ProductionYear, item.Status, newStatus)
            End If
        Next
    End Sub

    Private Sub dtgVechileColorList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgVechileColorList.PageIndexChanged
        dtgVechileColorList.CurrentPageIndex = e.NewPageIndex
        DoSearch(dtgVechileColorList.CurrentPageIndex)
    End Sub

    Private Sub dtgVechileColorList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgVechileColorList.SortCommand
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
        dtgVechileColorList.CurrentPageIndex = 0
        DoSearch(dtgVechileColorList.CurrentPageIndex)
    End Sub

    Protected Sub dtgVechileColorList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgVechileColorList.ItemCommand
        If e.CommandName = "Edit" Then
            _sessHelper.RemoveSession("VECHCLREDIT")
            Dim vechileClrEdit As VechileColorIsActiveOnPK = New VechileColorIsActiveOnPKFacade(User).Retrieve(CInt(e.Item.Cells(2).Text))
            _sessHelper.SetSession("VECHCLREDIT", vechileClrEdit)

            ddlKategori.SelectedValue = vechileClrEdit.VechileColor.VechileType.Category.ID
            Dim evt As EventArgs = New EventArgs()
            ddlKategori_SelectedIndexChanged(source, evt)
            If Not IsNothing(vechileClrEdit.VechileTypeGeneral) Then
                txtTypeGeneral.Text = vechileClrEdit.VechileTypeGeneral.Name
                ddlSubCategory.SelectedValue = vechileClrEdit.VechileTypeGeneral.SubCategoryVehicle.ID
                txtTypeGeneralID.Text = vechileClrEdit.VechileTypeGeneral.ID
            Else
                Dim subcategory As SubCategoryVehicle = getSubCategory(vechileClrEdit.VechileColor.VechileType.VechileTypeCode)
                ddlSubCategory.SelectedValue = subcategory.ID
            End If
            ddlSubCategory_SelectedIndexChanged(source, evt)
            txtVechileType.Text = vechileClrEdit.VechileColor.VechileType.VechileTypeCode

            txtMaterialNumber.Text = vechileClrEdit.VechileColor.MaterialNumber
            txtTahunPerakitan.Text = vechileClrEdit.ProductionYear
            txtTahunModel.Text = vechileClrEdit.ModelYear
            ddlstatus.SelectedValue = vechileClrEdit.Status
            If Not IsNothing(vechileClrEdit.VechileColor.VechileType.SalesVechileModel) Then
                txtModelKendaraan.Text = vechileClrEdit.VechileColor.VechileType.SalesVechileModel.NewVechileModelDesc
                txtModelKendaraanID.Text = vechileClrEdit.VechileColor.VechileType.SalesVechileModel.ID
            End If
            txtDeskripsiKendaraan.Text = vechileClrEdit.VechileColor.VechileType.DescriptionDealer

            setEditField(False)
        End If
    End Sub

#End Region

End Class





