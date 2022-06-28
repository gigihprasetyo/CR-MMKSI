#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Namespace Imports"
Imports System.Text
Imports System.IO
#End Region

Public Class FrmTrClassAllocation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents pnl1 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtKodeKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgTrClassAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSetAllocation As System.Web.UI.WebControls.Button
    Protected WithEvents txtNamaKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKapasitas As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents grid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkAllocation As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchKodeKategori As System.Web.UI.WebControls.Label
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkBatal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtPeriod As System.Web.UI.WebControls.TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variabels"
    Private objSessionHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangeAllocation As Boolean = False
    Private sSuffix As String = CType(DateTime.Now.Year, String) & CType(DateTime.Now.Month, String) & _
        CType(DateTime.Now.Day, String) & CType(DateTime.Now.Hour, String) & CType(DateTime.Now.Minute, String) & _
        CType(DateTime.Now.Second, String) & CType(DateTime.Now.Millisecond, String)
#End Region

#Region "Private Method"

#Region "Initialize Page Method"
    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Dealer.DealerCode"
        ViewState("CurrentSortDirect") = "ASC"
    End Sub

    Private Sub ClearData()
        ClearDataForm()
        ClearDataGrid()
    End Sub

    Private Sub ClearDataForm()
        txtKodeKelas.Text = ""
        txtNamaKelas.Text = ""
        txtKapasitas.Text = ""
        txtKodeDealer.Text = ""
        SetEnabilityButton(False)
    End Sub

    Private Sub ClearDataGrid()
        If Not Session.Item("arlAllocation") Is Nothing Then
            Dim arlAllocation As ArrayList = _
                CType(Session.Item("arlAllocation"), ArrayList)
            arlAllocation.Clear()
            dtgTrClassAllocation.DataSource = arlAllocation
            dtgTrClassAllocation.DataBind()
        End If
        If Not Session.Item("arlAllocationTemp") Is Nothing Then
            objSessionHelper.SetSession("arlAllocationTemp", Nothing)
        End If
        If Not Session.Item("objClass") Is Nothing Then
            objSessionHelper.SetSession("objClass", Nothing)
        End If
    End Sub

    Private Sub AssignAttributeControl()
        lblPopUpClass.Attributes("onclick") = "ShowPPClassSelectionMany()"
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        lblSearchKodeKategori.Attributes("onclick") = "ShowCategoryManySelection()"
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangeAllocation = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateAlokasi_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewAlokasi_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Form Alokasi")
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangeAllocation
        btnBatal.Visible = bPrivilegeChangeAllocation
        btnDownload.Visible = bPrivilegeChangeAllocation
    End Sub

#End Region

#Region "Process & Bind Data Method"
    Private Function ArrListOnlyDealerSelected(ByVal arrAllDealerClass As ArrayList) As ArrayList
        '-----------------------------------
        'filtering only dealer that selected
        '-----------------------------------
        Dim arrDealerSelected As New ArrayList
        Dim counter As Integer
        'Dim allDealerCode As String = txtKodeDealer.Text.Trim() + ";"
        Dim dealerCode() As String = txtKodeDealer.Text.Split(";")
        For Each ObjTrClassAlloc As TrClassAllocation In arrAllDealerClass
            For counter = 0 To dealerCode.Length - 1
                If ObjTrClassAlloc.Dealer.DealerCode = dealerCode(counter) Then
                    arrDealerSelected.Add(ObjTrClassAlloc)
                    Exit For
                End If
            Next counter
        Next
        objSessionHelper.SetSession("ArrListDealerSelected", arrDealerSelected)
        Return arrDealerSelected
    End Function

    Private Sub SynchronizeGrid()
        Dim txt As TextBox
        Dim ctGrid As Integer = 0
        For i As Integer = 0 To dtgTrClassAllocation.Items.Count - 1
            If CType(dtgTrClassAllocation.Items(i).FindControl("lblDealerCode"), Label).Text = _
                grid.Items(ctGrid).Cells(2).Text Then
                txt = CType(dtgTrClassAllocation.Items(i).FindControl("txtAllocated"), TextBox)
                grid.Items(ctGrid).Cells(6).Text = txt.Text.Trim()
                ctGrid += 1
                If ctGrid > grid.Items.Count - 1 Then
                    Exit For
                End If
            End If
        Next i
    End Sub

    Private Function hitungSisa(ByVal classID As Integer) As Integer
        'Dim arlist As ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, classID))
        'arlist = New TrClassRegistrationFacade(User).Retrieve(criterias)
        'Return arlist.Count
        Return 0
    End Function

    Private Function AllDealerInClassCriteria(ByVal ClassID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, ClassID))
        Return criterias
    End Function

    Private Function GetAllDealerInClass(ByVal ClassID As Integer) As ArrayList
        Dim arlAllocation As ArrayList = New TrClassAllocationFacade(User).Retrieve( _
            AllDealerInClassCriteria(ClassID))
        Return arlAllocation
    End Function

    Private Function ConcatedDataDealer(ByVal DealerBySearchColl As ArrayList, _
        ByVal AllDealerInClassColl As ArrayList) As ArrayList
        Dim arlTemp As ArrayList = New ArrayList
        'find old dealer already selected by
        For Each objAllDealerInClass As TrClassAllocation In AllDealerInClassColl
            Dim isFoundInSearch As Boolean = False
            For Each objDealerBySearch As TrClassAllocation In DealerBySearchColl
                If objAllDealerInClass.ID = objDealerBySearch.ID Then
                    isFoundInSearch = True
                    Exit For
                End If
            Next

            objAllDealerInClass.IsPickOnSearch = isFoundInSearch
            objAllDealerInClass.MarkLoaded()
            If isFoundInSearch Then
                'objAllDealerInClass.History = GetCAUpdateHistory(objAllDealerInClass)
                arlTemp.Add(objAllDealerInClass)
            End If

        Next

        'find new dealer
        For Each objDealerBySearch As TrClassAllocation In DealerBySearchColl
            Dim isNewDealer As Boolean = True
            For Each objAllDealerInClass As TrClassAllocation In AllDealerInClassColl
                If objDealerBySearch.ID = objAllDealerInClass.ID Then
                    isNewDealer = False
                End If
            Next
            If isNewDealer Then
                objDealerBySearch.IsPickOnSearch = True
                objDealerBySearch.MarkLoaded()
                'objDealerbysearch.History = GetCAUpdateHistory(objDealerbysearch)
                arlTemp.Add(objDealerBySearch)
            End If
        Next

        Return arlTemp
    End Function

    Private Function DefaultCriteria(ByVal ClassCode As String, _
        ByVal DealerCode As String) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ' criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.TrCourse.CourseCode", MatchType.InSet, "('" & txtKodeKategori.Text.Trim().Replace(";", "','") & "')"))
        If ClassCode.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ClassCode", MatchType.InSet, "('" & ClassCode.Replace(";", "','") & "')"))
        End If
        If chkAllocation.Checked Then
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Allocated", MatchType.Greater, 0))
        End If
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        Return criterias
    End Function

    Private Function ParseDealerCode(ByVal DealerCodeColl As String, _
        ByRef nResult As Integer) As ArrayList

        DealerCodeColl = "('" & DealerCodeColl.Replace(";", "','") & "')"
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, DealerCodeColl))
        Dim arlTemp As ArrayList = New DealerFacade(User).Retrieve(criterias)
        Return arlTemp
    End Function

    Private Function GetAllDealerCode() As String
        Dim retValue As String = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        Dim arrListDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)
        For i As Integer = 0 To arrListDealer.Count - 1
            If retValue.Trim().Length > 0 Then
                retValue = retValue & ";" & CType(arrListDealer(i), Dealer).DealerCode
            Else
                retValue = CType(arrListDealer(i), Dealer).DealerCode
            End If
        Next i
        Return retValue
    End Function

    Private Function GetClass(ByVal year As Integer) As ArrayList
        'update by 54M
        Dim isCodeSame As Boolean
        Dim sbClassCode As New StringBuilder
        Dim arrStrClassCode() As String
        Dim arrListClass As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeKategori.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.CourseCode", MatchType.InSet, "('" & txtKodeKategori.Text.Trim().Replace(";", "','") & "')"))
        Else
            Dim criteriasCat As New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasCat.opAnd(New Criteria(GetType(TrCourse), "Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, String)))
            Dim arrListCat As ArrayList = New TrCourseFacade(User).Retrieve(criteriasCat)
            For i As Integer = 0 To arrListCat.Count - 1
                If txtKodeKategori.Text.Trim().Length <= 0 Then
                    txtKodeKategori.Text = CType(arrListCat(i), TrCourse).CourseCode
                Else
                    txtKodeKategori.Text += ";" & CType(arrListCat(i), TrCourse).CourseCode
                End If
            Next i
            criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.CourseCode", MatchType.InSet, "('" & txtKodeKategori.Text.Trim().Replace(";", "','") & "')"))
            txtKodeKategori.Text = ""
        End If
        criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.GreaterOrEqual, New Date(year, 1, 1, 0, 0, 0)))
        criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.LesserOrEqual, New Date(year, 12, 31, 23, 59, 59)))


        arrListClass = New TrClassFacade(User).Retrieve(criterias)
        If txtKodeKelas.Text.Trim().Length > 0 Then
            'For i As Integer = 0 To arrListClass.Count - 1
            '    If sbClassCode.Length > 0 Then
            '        sbClassCode.Append(";")
            '        sbClassCode.Append(CType(arrListClass(i), TrClass).ClassCode)
            '    Else
            '        sbClassCode.Append(CType(arrListClass(i), TrClass).ClassCode)
            '    End If
            'Next
            'arrStrClassCode = sbClassCode.ToString().Split(";")

            'Dim arrStrClassCodeNew() As String = txtKodeKelas.Text.Trim().Split(";")
            'For x As Integer = 0 To arrStrClassCodeNew.Length - 1
            '    isCodeSame = False
            '    For y As Integer = 0 To arrStrClassCode.Length - 1
            '        If arrStrClassCodeNew(x) = arrStrClassCode(y) Then
            '            isCodeSame = True
            '            Exit For
            '        End If
            '    Next y

            '    If Not isCodeSame Then
            '        If sbClassCode.Length > 0 Then
            '            sbClassCode.Append(";")
            '            sbClassCode.Append(arrStrClassCodeNew(x))
            '        Else
            '            sbClassCode.Append(arrStrClassCodeNew(x))
            '        End If
            '    End If
            'Next x
            Dim criteriasNew As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasNew.opAnd(New Criteria(GetType(TrClass), "ClassCode", MatchType.InSet, "('" & txtKodeKelas.Text.Trim().Replace(";", "','") & "')"))
            criteriasNew.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.GreaterOrEqual, New Date(year, 1, 1, 0, 0, 0)))
            criteriasNew.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.LesserOrEqual, New Date(year, 12, 31, 23, 59, 59)))

            arrListClass = New ArrayList
            arrListClass = New TrClassFacade(User).Retrieve(criteriasNew)
        End If

        Return arrListClass
    End Function

    Private Function GetDataFromDB() As ArrayList 'by dna:20101209
        Dim oTCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim cTCA As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aTCA As New ArrayList
        Dim oTCA As TrClassAllocation

        'otca.Allocated 
        'cTCA.opAnd(New Criteria(GetType(TrClassAllocation), "", MatchType.Exact, ""))
        If Me.txtKodeKategori.Text.Trim <> "" Then
            cTCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.TrCourse.CourseCode", MatchType.InSet, "('" & Me.txtKodeKategori.Text.Trim.Replace(";", "','") & "')"))
        End If
        If Me.txtKodeKelas.Text.Trim <> "" Then
            cTCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ClassCode", MatchType.InSet, "('" & Me.txtKodeKelas.Text.Trim.Replace(";", "','") & "')"))
        End If
        If Me.txtKodeDealer.Text.Trim <> "" Then
            cTCA.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.DealerCode", MatchType.InSet, "('" & Me.txtKodeDealer.Text.Trim.Replace(";", "','") & "')"))
        End If
        If Me.chkAllocation.Checked Then
            cTCA.opAnd(New Criteria(GetType(TrClassAllocation), "Allocated", MatchType.Greater, 0))
        End If
        If Me.chkBatal.Checked Then
            cTCA.opAnd(New Criteria(GetType(TrClassAllocation), "CancelReason", MatchType.No, ""), "(", True)
            cTCA.opOr(New Criteria(GetType(TrClassAllocation), "LastAllocated", MatchType.Greater, 0), ")", False)
        End If
        aTCA = oTCAFac.Retrieve(cTCA)
        SetEnabilityButton((aTCA.Count > 0))
        If aTCA.Count > 0 Then
            SortListControl(aTCA, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), String))
        End If
        Return aTCA
    End Function

    Private Function GetDataForDataGrid(ByVal IsGetFromDB As Boolean) As ArrayList
        'update by 54M
        Dim arlAllocation As ArrayList = New ArrayList
        Dim arlAllocTemp As New ArrayList
        Dim arlParse As ArrayList
        Dim nResult As Integer = 1
        If txtKodeDealer.Text.Trim().Length > 0 Then
            arlParse = ParseDealerCode(txtKodeDealer.Text.Trim(), nResult)
        Else
            arlParse = ParseDealerCode(GetAllDealerCode(), nResult)
        End If
        If IsGetFromDB Then
            Dim ArrListClass As ArrayList = New ArrayList
            If txtPeriod.Text.Length = 4 Then
                ArrListClass = GetClass(CType(txtPeriod.Text, Integer))
            ElseIf (txtPeriod.Text.Length > 0 And txtPeriod.Text.Length <> 4) Then
                MessageBox.Show("Format tahun tidak benar ")
                Exit Function
            End If

            For i As Integer = 0 To ArrListClass.Count - 1
                Dim objClass As TrClass = CType(ArrListClass(i), TrClass)
                objSessionHelper.SetSession("objClass", objClass)
                If Not IsNothing(objSessionHelper.GetSession("arlAllocation")) Then
                    arlAllocTemp = CType(objSessionHelper.GetSession("arlAllocation"), ArrayList)
                Else
                    arlAllocTemp = New ArrayList
                End If
                If arlParse.Count > 0 And nResult = 1 Then
                    Dim arlDealerBySearch As ArrayList = New ArrayList
                    For x As Integer = 0 To arlParse.Count - 1
                        Dim objDealer As Dealer = arlParse(x)
                        Dim strDealerCode As String = objDealer.DealerCode
                        Dim arlDealerAlreadyExist As ArrayList = New TrClassAllocationFacade(User).Retrieve( _
                            DefaultCriteria(objClass.ClassCode, strDealerCode))
                        If arlDealerAlreadyExist.Count > 0 Then
                            Dim objOldAllocation As TrClassAllocation = CType(arlDealerAlreadyExist(0), TrClassAllocation)
                            objOldAllocation.IsPickOnSearch = True
                            objOldAllocation.MarkLoaded()
                            arlDealerBySearch.Add(objOldAllocation)
                        Else
                            Dim objNewAllocation As New TrClassAllocation
                            objNewAllocation.RowStatus = CType(DBRowStatus.Active, Short)
                            objNewAllocation.TrClass = objClass
                            objNewAllocation.Dealer = objDealer
                            objNewAllocation.Allocated = 0
                            objNewAllocation.IsPickOnSearch = True
                            objNewAllocation.MarkLoaded()
                            arlDealerBySearch.Add(objNewAllocation)
                        End If
                    Next

                    Dim arlAllDealer As ArrayList = GetAllDealerInClass(objClass.ID)
                    arlAllocation = ConcatedDataDealer(arlDealerBySearch, arlAllDealer)
                    For ct As Integer = 0 To arlAllocTemp.Count - 1
                        Dim obj As TrClassAllocation = arlAllocTemp(ct)
                        'obj.History = GetCAUpdateHistory(obj)
                        arlAllocation.Add(obj)
                    Next ct

                    Dim arlAllocationClone As ArrayList = CloneArrayList(arlAllocation)
                    'session arlAllocationTemp uses if user doing "Batal" and then show the first data 
                    objSessionHelper.SetSession("arlAllocationTemp", arlAllocationClone)
                    'session arlAllocation uses if user modify the data in datagrid
                    objSessionHelper.SetSession("arlAllocation", arlAllocation)
                Else
                    MessageBox.Show(SR.DataNotFound("Dealer"))
                End If
            Next i
            If chkAllocation.Checked Or chkBatal.Checked Then
                Dim arlAlloc As ArrayList = CType(objSessionHelper.GetSession("arlAllocation"), ArrayList)
                If Not arlAlloc Is Nothing Then
                    Dim arlAllocNew As New ArrayList
                    Dim ObjTrAlloc As New TrClassAllocation
                    For i As Integer = 0 To arlAlloc.Count - 1
                        ObjTrAlloc = CType(arlAlloc(i), TrClassAllocation)
                        If chkAllocation.Checked And chkBatal.Checked Then
                            If ObjTrAlloc.Allocated > 0 And (ObjTrAlloc.CancelReason.Trim <> "" Or ObjTrAlloc.LastAllocated > 0) Then
                                arlAllocNew.Add(ObjTrAlloc)
                            End If
                        Else
                            If chkAllocation.Checked Then
                                If ObjTrAlloc.Allocated > 0 Then
                                    arlAllocNew.Add(ObjTrAlloc)
                                End If
                            ElseIf chkBatal.Checked Then
                                If ObjTrAlloc.CancelReason.Trim <> "" Or ObjTrAlloc.LastAllocated > 0 Then
                                    arlAllocNew.Add(ObjTrAlloc)
                                End If
                            End If

                        End If

                    Next i
                    Dim arlAllocClone As ArrayList = CloneArrayList(arlAllocNew)
                    objSessionHelper.SetSession("arlAllocationTemp", arlAllocClone)
                    objSessionHelper.SetSession("arlAllocation", arlAllocNew)
                    arlAllocation = New ArrayList
                    arlAllocation = CType(objSessionHelper.GetSession("arlAllocation"), ArrayList)
                End If
            End If
        Else
            If Not Session.Item("arlAllocation") Is Nothing Then
                arlAllocation = CType(Session.Item("arlAllocation"), ArrayList)
            End If
        End If
        If arlAllocation.Count > 0 Then
            SortListControl(arlAllocation, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), String))
            SetEnabilityButton(True)
        Else
            SetEnabilityButton(False)
        End If
        Return arlAllocation
    End Function

    Private Sub BindDataGrid(ByVal IsGetFromDB As Boolean)
        Dim arrList As ArrayList = GetDataForDataGrid(IsGetFromDB)

        'Optimize by firman 
        'allocated & history taken only once
        Dim arlClass As ArrayList = New ArrayList
        arlClass = GetClass(CType(txtPeriod.Text, Integer))

        Dim arrClassStr(2, arlClass.Count) As Integer

        Dim x As Integer = 0
        For Each obj As TrClass In arlClass

            arrClassStr(0, x) = obj.ID
            arrClassStr(1, x) = GetAllocatedCapacity(obj.ID)
            x += 1
        Next

        For i As Integer = 0 To arrList.Count - 1

            CType(arrList(i), TrClassAllocation).History = GetCAUpdateHistory(CType(arrList(i), TrClassAllocation))

            For z As Integer = 0 To arlClass.Count - 1
                If arrClassStr(0, z) = CType(arrList(i), TrClassAllocation).TrClass.ID Then
                    CType(arrList(i), TrClassAllocation).AllocatedTaken = arrClassStr(1, z)
                    Exit For
                End If
            Next
        Next
        'End Optimize

        dtgTrClassAllocation.DataSource = arrList
        dtgTrClassAllocation.DataBind()

        grid.DataSource = ArrListOnlyDealerSelected(arrList)
        grid.DataBind()
    End Sub

    Private Sub BindDataGridForBatal()
        Dim arlAllocationTemp As ArrayList = CType(Session.Item("arlAllocationTemp"), ArrayList)

        SortListControl(arlAllocationTemp, CType(ViewState("CurrentSortColumn"), String), _
        CType(ViewState("CurrentSortDirect"), String))

        Dim arlAllocation As ArrayList = CloneArrayList(arlAllocationTemp)
        objSessionHelper.SetSession("arlAllocation", arlAllocation)

        dtgTrClassAllocation.DataSource = arlAllocation
        dtgTrClassAllocation.DataBind()
    End Sub

    Private Sub DeleteRowData(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim arlAllocation As ArrayList = _
            CType(Session.Item("arlAllocation"), ArrayList)
        Dim objAllocation As TrClassAllocation = _
            CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)
        Try
            If New HelperFacade(User, GetType(TrClassRegistration)).IsRecordExist( _
                CreateCriteriaForCheckRecord(objAllocation.TrClass.ID), _
                CreateAggreateForCheckRecord()) Then
                MessageBox.Show(SR.CannotDelete)
            Else
                'didn't need set to session because session already reference
                Dim trClassAllocationFacade As trClassAllocationFacade = New trClassAllocationFacade(User)
                trClassAllocationFacade.DeleteFromDB(objAllocation)
                arlAllocation.RemoveAt(e.Item.ItemIndex)
                BindDataGrid(False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetColumnText(ByVal objAllocation As TrClassAllocation, _
        ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'If Not e.Item.FindControl("lblNo") Is Nothing Then
        '    CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1
        'End If

        If Not e.Item.FindControl("lblDealerCode") Is Nothing Then
            CType(e.Item.FindControl("lblDealerCode"), Label).Text = _
                objAllocation.Dealer.DealerCode '& "/" & objAllocation.Dealer.SearchTerm1
        End If

        If Not e.Item.FindControl("txtAllocated") Is Nothing Then
            CType(e.Item.FindControl("txtAllocated"), TextBox).Text = objAllocation.Allocated
        End If
        If Not e.Item.FindControl("txtTemp") Is Nothing Then
            CType(e.Item.FindControl("txtTemp"), TextBox).Text = objAllocation.Allocated
        End If
        If Not e.Item.FindControl("lblAllocationTaken") Is Nothing Then
            CType(e.Item.FindControl("lblAllocationTaken"), Label).Text = GetAllocationTaken(objAllocation)
        End If
    End Sub

    Private Sub SetControlAttribute(ByVal objAllocation As TrClassAllocation, _
        ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            If objAllocation.Allocated > 0 Then
                'belum masa naik
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeAllocation
            Else
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = False
            End If

        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeAllocation
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If Not objAllocation.IsPickOnSearch Then
            e.Item.BackColor = Color.LightSalmon
            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = False
            End If
            If Not e.Item.FindControl("txtAllocated") Is Nothing Then
                CType(e.Item.FindControl("txtAllocated"), TextBox).ReadOnly = True
            End If
        End If
    End Sub

#End Region

#Region "Validation & Update Form Method"


    Private Function IsDealerAllocated(ByVal ClassID As Integer, ByVal DealerID As Integer) As Boolean
        Dim objTrClassRegistrationFacade As New TrClassRegistrationFacade(User)
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Dealer.ID", MatchType.Exact, DealerID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, DealerID))
        arrList = objTrClassRegistrationFacade.Retrieve(criterias)
        If arrList.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CheckTotalAllocation() As Boolean
        Dim retValue As Boolean = True
        Dim ObjDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        Dim txtAlloc As TextBox
        If ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            For i As Integer = 0 To dtgTrClassAllocation.Items.Count - 1
                txtAlloc = CType(dtgTrClassAllocation.Items(i).FindControl("txtAllocated"), TextBox)
                If CType(txtAlloc.Text.Trim(), Integer) < 1 Then
                    retValue = False
                    Exit For
                End If
            Next i
        Else
            retValue = True
        End If
        Return retValue
    End Function

    Private Function ValidateTotalAllocation() As Boolean
        Dim retValue As Boolean = True
        Dim txtAlloc As TextBox
        Dim TotAlloc As Integer = 0
        Dim ArrTrClassAlloc As ArrayList = CType(objSessionHelper.GetSession("arlAllocation"), ArrayList)
        Dim objClassAlloc As TrClassAllocation = ArrTrClassAlloc.Item(0)

        Dim Kapasitas As Integer = CInt(objClassAlloc.TrClass.Capacity.ToString)

        For i As Integer = 0 To dtgTrClassAllocation.Items.Count - 1
            txtAlloc = CType(dtgTrClassAllocation.Items(i).FindControl("txtAllocated"), TextBox)
            TotAlloc = TotAlloc + CInt(txtAlloc.Text)
        Next i

        If Kapasitas < TotAlloc Then
            retValue = False
        End If
        Return retValue
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal ClassID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord() As Aggregate
        Dim aggregates As New Aggregate(GetType(TrClassRegistration), "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function CheckClassCapacity(ByVal arlAllocationToProcess As ArrayList) As Boolean
        'Dim objClass As TrClass = CType(Session.Item("objClass"), TrClass)
        'Dim objTrClassAllocation As TrClassAllocation
        'Dim totalClassSpace As Integer = 0
        'If arlAllocationToProcess.Count > 0 Then
        '    For x As Integer = 0 To arlAllocationToProcess.Count - 1
        '        objTrClassAllocation = CType(arlAllocationToProcess(x), TrClassAllocation)
        '        totalClassSpace += objTrClassAllocation.Allocated
        '    Next
        'End If
        'If totalClassSpace <= objClass.Capacity Then
        '    Return True
        'End If
        'Return False

        'Dim remaining As String = CType(objAllocation.TrClass.Capacity - hitungSisa(objAllocation.TrClass.ID) - allocated, String)


        For Each item As TrClassAllocation In arlAllocationToProcess
            Dim used As Integer = 0 'hitungSisa(item.TrClass.ID)
            Dim capacityClass As Integer = item.TrClass.Capacity - used
            Dim remain As Integer = capacityClass - GetAllocatedCapacity(item.TrClass.ID)
            If item.Allocated > (remain + GetOrgAllocated(item.ID)) Then
                Return False
            End If
        Next
        Return True
    End Function


    Private Function GetOrgAllocated(ByVal Id As Integer) As Integer
        Dim objFacade As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crit.opAnd(New Criteria(GetType(TrClassAllocation), "ID", MatchType.Exact, Id))
        Dim obj As TrClassAllocation = objFacade.Retrieve(Id)
        If Not obj Is Nothing Then
            Return obj.Allocated
        Else
            Return 0
        End If

    End Function


    Private Function GetAllocatedCapacity(ByVal classId As Integer) As Integer
        Dim objFacade As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, classId))

        Dim agg As Aggregate = New Aggregate(GetType(TrClassAllocation), "Allocated", AggregateType.Sum)

        'Dim list As ArrayList = objFacade.Retrieve(crit)
        'Dim total As Integer 
        'For Each item As TrClassAllocation In list
        '    total += item.Allocated()
        'Next
        'Return total

        Return objFacade.RetrieveScalar(crit, agg)
    End Function

    Private Function GetAllocationTaken(ByVal objClassAlloc As TrClassAllocation) As Integer
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, objClassAlloc.ClassID))
        'crit.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Dealer.ID", MatchType.Exact, objClassAlloc.Dealer.ID))
        crit.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, objClassAlloc.Dealer.ID))
        Dim arlReg As ArrayList = New TrClassRegistrationFacade(User).Retrieve(crit)
        Return arlReg.Count
    End Function

    Private Function CheckDataForm(ByRef ErrorMsg As String) As Boolean
        'If txtKodeKelas.Text = "" Then
        '    ErrorMsg = SR.GridIsEmpty("Field Kode Kelas")
        '    Return False
        'Else
        '    Dim nResult As Integer = New TrClassFacade(User).ValidateCode(txtKodeKelas.Text.Trim())
        '    If nResult < 1 Then
        '        ErrorMsg = SR.DataNotFound("Kode Kelas")
        '        Return False
        '    End If
        'End If
        'If txtKodeDealer.Text = "" Then
        '    ErrorMsg = SR.GridIsEmpty("Field Kode Dealer")
        '    Return False
        'End If
        'Return True
        If txtKodeDealer.Text.Trim().Length > 0 Then
            Dim ArrStrKodeDealer() As String = txtKodeDealer.Text.Trim().Split(";")
            Dim nResult As Integer
            For i As Integer = 0 To ArrStrKodeDealer.Length - 1
                nResult = New DealerFacade(User).ValidateCode(ArrStrKodeDealer(i))
                If nResult < 1 Then
                    ErrorMsg = SR.DataNotFound("Kode Dealer " & ArrStrKodeDealer(i))
                    Return False
                End If
            Next i
        Else
            ErrorMsg = SR.GridIsEmpty("Field Kode Dealer")
            Return False
        End If

        If txtKodeKelas.Text.Trim().Length > 0 Then
            Dim ArrStrKodeKelas() As String = txtKodeKelas.Text.Trim().Split(";")
            Dim nResult As Integer
            For i As Integer = 0 To ArrStrKodeKelas.Length - 1
                nResult = New TrClassFacade(User).ValidateCode(ArrStrKodeKelas(i))
                If nResult < 1 Then
                    ErrorMsg = SR.DataNotFound("Kode Kelas " & ArrStrKodeKelas(i))
                    Return False
                End If
            Next i
        End If

        If txtKodeKategori.Text.Trim().Length > 0 Then
            Dim ArrStrKodeKategori() As String = txtKodeKategori.Text.Trim().Split(";")
            Dim nResult As Integer
            For i As Integer = 0 To ArrStrKodeKategori.Length - 1
                nResult = New TrCourseFacade(User).ValidateCode(ArrStrKodeKategori(i))
                If nResult < 1 Then
                    ErrorMsg = SR.DataNotFound("Kode Kategori " & ArrStrKodeKategori(i))
                    Return False
                End If
            Next i
        End If

        If txtPeriod.Text.Trim().Length > 0 Then
            If txtPeriod.Text.Trim().Length <> 4 Then
                ErrorMsg = "Format Tahun Periode tidak benar"
                Return False
            End If
        Else
            ErrorMsg = SR.GridIsEmpty("Field Tahun Periode")
            Return False
        End If
        Return True
    End Function

    Private Function CreateCriteriaForCheckOverLimit(ByVal ClassID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckOverLimit() As Aggregate
        Dim aggregates As New Aggregate(GetType(TrClassRegistration), "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function IsAllocationOverLimit(ByVal ClassID As Integer, _
        ByVal CurrentAllocation As Integer) As Boolean

        Dim intAllocation As Integer = _
            New HelperFacade(User, GetType(TrClassRegistration)).RecordCount( _
            CreateCriteriaForCheckOverLimit(ClassID), _
            CreateAggreateForCheckOverLimit())

        If CurrentAllocation <= intAllocation Then
            Return False
        End If

        Return True
    End Function

    Private Function GetMessageError(ByVal ErrorDealerColl As ArrayList) As String
        Dim strMsg As String = String.Empty
        strMsg += "Alokasi baru tidak mencukupi, \n siswa telah terdaftar untuk dealer : \n"

        For x As Integer = 0 To ErrorDealerColl.Count - 1
            strMsg += CType(ErrorDealerColl(x), String)
            If x < ErrorDealerColl.Count - 1 Then
                strMsg += ","
            End If
        Next

        Return strMsg
    End Function

    Private Function GetMessageErrorClassCancelled(ByVal ErrorDealerColl As ArrayList) As String
        Dim strMsg As String = String.Empty
        strMsg += "Pembatalan / Alokasi baru gagal, \n siswa telah terdaftar untuk dealer : \n"

        For x As Integer = 0 To ErrorDealerColl.Count - 1
            strMsg += CType(ErrorDealerColl(x), String)
            If x < ErrorDealerColl.Count - 1 Then
                strMsg += ","
            End If
        Next

        Return strMsg
    End Function

    Private Sub CheckCanceledAllocation(ByRef arlAllocationToProcess As ArrayList, ByRef arlErrorDealer As ArrayList)
        arlErrorDealer = New ArrayList
        If arlAllocationToProcess.Count > 0 Then
            For Each item As TrClassAllocation In arlAllocationToProcess
                If IsTraineeExist(item.TrClass.ID, item.Dealer.ID) Then
                    arlErrorDealer.Add(item.Dealer.DealerCode)
                End If
            Next
        End If
    End Sub

    Private Function IsTraineeExist(ByVal classID As Integer, ByVal dealerID As Integer) As Boolean
        Dim vRet As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(v_trClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(v_trClass), "ClassID", MatchType.Exact, classID))
        criterias.opAnd(New Criteria(GetType(v_trClass), "DealerID", MatchType.Exact, dealerID))
        Dim arlAllocation As ArrayList = New v_trClassFacade(User).Retrieve(criterias)
        If arlAllocation.Count > 0 Then
            Dim objVTrClass As v_trClass = CType(arlAllocation(0), v_trClass)
            If objVTrClass.NumOfTrainee > 0 Then
                vRet = True
            End If
        End If
        Return vRet
    End Function

    Private Sub CheckCancelledAllocation(ByRef arlAllocationToProcess As ArrayList, ByRef arlErrorDealer As ArrayList)
        'Dim arlErrorDealer As ArrayList = New ArrayList
        For Each dgItem As DataGridItem In dtgTrClassAllocation.Items
            Dim objAllocation As TrClassAllocation
            Dim allocated As Integer = CInt(Request.Form(CType(dgItem.FindControl("txtAllocated"), TextBox).UniqueID))
            'Dim OrgQty As Integer = CInt(CType(dgItem.FindControl("txtTemp"), TextBox).Text)
            Dim allocationTaken As Integer = CInt(CType(dgItem.FindControl("lblAllocationTaken"), Label).Text) '
            If allocated < allocationTaken Then
                objAllocation = CType(arlAllocationToProcess(dgItem.ItemIndex), TrClassAllocation)
                arlErrorDealer.Add(objAllocation.Dealer.DealerCode)
            End If
        Next

    End Sub

    Private Sub UpdateDataBeforeSave(ByRef arlAllocationToProcess As ArrayList, _
        ByRef ErrorDataColl As ArrayList)
        Dim arlErrorDealer As ArrayList = New ArrayList
        Dim NewList As ArrayList = New ArrayList
        For Each dgItem As DataGridItem In dtgTrClassAllocation.Items
            Dim objAllocation As TrClassAllocation
            Dim allocated As Integer = _
                CInt(Request.Form(CType(dgItem.FindControl("txtAllocated"), TextBox).UniqueID))
            Dim OrgQty As Integer = CInt(CType(dgItem.FindControl("txtTemp"), TextBox).Text)
            If OrgQty <> allocated Then
                objAllocation = CType(arlAllocationToProcess(dgItem.ItemIndex), TrClassAllocation)
                Dim txtTemp As TextBox = CType(dgItem.FindControl("txtTemp"), TextBox)
                objAllocation.LastAllocated = CInt(txtTemp.Text) ' CInt(Request.Form(CType(dgItem.FindControl("txtTemp"), TextBox).UniqueID))
                'Dim txtCancelReason As TextBox = CType(dgItem.FindControl("txtCancelReason"), TextBox)
                'objAllocation.CancelReason = txtCancelReason.Text.Trim

                objAllocation.Allocated = allocated
                NewList.Add(objAllocation)
            End If
        Next
        arlAllocationToProcess = New ArrayList
        arlAllocationToProcess = NewList
        If NewList.Count > 0 Then
            For Each item As TrClassAllocation In NewList
                If IsAllocationOverLimit(item.TrClass.ID, item.Allocated) Then
                    arlErrorDealer.Add(item.Dealer.DealerCode)
                End If
            Next
        End If
    End Sub

    Private Function GetOriginalAllocation(ByVal list As ArrayList) As ArrayList
        For Each item As TrClassAllocation In list
            Dim facade As TrClassAllocationFacade = New TrClassAllocationFacade(User)
            Dim objTrAll As TrClassAllocation = facade.Retrieve(item.ID)
            Dim allQty As Integer = 0
            If Not objTrAll Is Nothing Then
                allQty = objTrAll.Allocated
            End If
            item.AllocatedBefore = allQty
        Next
        Return list
    End Function

    Private Sub SaveData()
        Dim arlAllocation As ArrayList = CType(Session.Item("arlAllocation"), ArrayList)
        arlAllocation = GetOriginalAllocation(arlAllocation)
        Dim objClass As TrClass = CType(Session.Item("objClass"), TrClass)
        Dim arlErrorDealer As ArrayList = New ArrayList

        If arlAllocation.Count > 0 Then
            'CheckCancelledAllocation(arlAllocation, arlErrorDealer)
            'If arlErrorDealer.Count > 0 Then
            '    MessageBox.Show(GetMessageErrorClassCancelled(arlErrorDealer))
            'Else
            UpdateDataBeforeSave(arlAllocation, arlErrorDealer)
            If arlErrorDealer.Count > 0 Then
                MessageBox.Show(GetMessageError(arlErrorDealer))
            Else
                'CheckCanceledAllocation(arlAllocation, arlErrorDealer)
                'If arlErrorDealer.Count > 0 Then
                '    MessageBox.Show(GetMessageErrorClassCancelled(arlErrorDealer))
                'Else
                If CheckClassCapacity(arlAllocation) Then
                    Dim nResult As Integer = -1
                    Dim arlAllAlocationDealerColl As ArrayList = _
                        GetAllAllocationDealer(objClass.ID)

                    nResult = New TrClassAllocationFacade(User).UpdateAllocation( _
                        arlAllAlocationDealerColl, arlAllocation, objClass)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ' SendEmail(arlAllAlocationDealerColl, arlAllocation, objClass)
                        SendEmailEnhan(arlAllocation)
                        SetEnabilityButton(False)
                        'ClearData()
                    End If
                Else
                    MessageBox.Show("Jumlah Alokasi melebihi Kapasitas Kelas")
                End If
                'End If
            End If
            'End If

        End If
    End Sub

#End Region

#Region "Send Email Method"
    Private Function DealerInClassCriteria(ByVal ClassID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, ClassID))
        Return criterias
    End Function

    Private Function GetAllAllocationDealer(ByVal ClassID As Integer) As ArrayList
        Dim arlAllocation As ArrayList = New TrClassAllocationFacade(User).Retrieve(DealerInClassCriteria(ClassID))
        Return arlAllocation
    End Function

    Private Function GetChangedAllocation(ByVal AllAllocationDealerColl As ArrayList, _
        ByVal AllocationToProcessColl As ArrayList) As ArrayList
        Dim arlAllocationChanged As ArrayList = New ArrayList
        For Each objAllAllocationDealer As TrClassAllocation In AllAllocationDealerColl
            Dim isDeletedAllocation As Boolean = True
            For Each objAllocationToProcess As TrClassAllocation In AllocationToProcessColl
                If objAllAllocationDealer.ID = objAllocationToProcess.ID Then
                    isDeletedAllocation = False
                    If objAllAllocationDealer.Allocated <> objAllocationToProcess.Allocated Then
                        objAllocationToProcess.AllocatedBefore = _
                            objAllAllocationDealer.Allocated
                        arlAllocationChanged.Add(objAllocationToProcess)
                        Exit For
                    End If
                End If
            Next
            If isDeletedAllocation Then
                objAllAllocationDealer.AllocatedBefore = objAllAllocationDealer.Allocated
                objAllAllocationDealer.Allocated = 0
                arlAllocationChanged.Add(objAllAllocationDealer)
            End If
        Next

        Return arlAllocationChanged
    End Function

    Private Function GetEmailUserCriteria(ByVal DealerID As Integer, _
        ByVal UserName As String) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, DealerID))
        criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, UserName))
        Return criterias
    End Function

    Private Function GetEmailUser() As String
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim strEmailUser As String = String.Empty
        Dim strUserName As String = User.Identity.Name.Trim().Substring(6)

        If Not objDealer Is Nothing And strUserName <> String.Empty Then
            Dim arlUser As ArrayList = New UserInfoFacade(User).Retrieve( _
                GetEmailUserCriteria(objDealer.ID, strUserName))
            If arlUser.Count <> 0 Then
                strEmailUser = CType(arlUser(0), UserInfo).Email
            End If
        End If

        Return strEmailUser
    End Function

    Private Function GetActiveListUserEmail() As ArrayList
        Return New TrUserEmailFacade(User).RetrieveActiveList()
    End Function

    Private Sub GenerateToAndCcAddress(ByRef ToAddress As String, _
        ByRef CcAddress As String, ByVal ChangeImpactToDealer As Dealer, _
        ByVal ListUserEmailColl As ArrayList)

        ToAddress += GetServiceManagerEmail(ChangeImpactToDealer)

        For Each objUserEmail As TrUserEmail In ListUserEmailColl
            If objUserEmail.Tipe = "TO" Then
                If ToAddress = String.Empty Then
                    ToAddress += objUserEmail.Email
                Else
                    ToAddress += "," & objUserEmail.Email
                End If
            ElseIf objUserEmail.Tipe = "CC" Then
                If CcAddress = String.Empty Then
                    CcAddress += objUserEmail.Email
                Else
                    CcAddress += "," & objUserEmail.Email
                End If
            End If
        Next
    End Sub

    Private Function GetServiceManagerEmailCriteria(ByVal DealerID As String) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, DealerID))
        criterias.opAnd(New Criteria(GetType(BusinessArea), "Kind", MatchType.Exact, CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Short)))
        Return criterias
    End Function

    Private Function GetServiceManagerEmail(ByVal ImpactToDealer As Dealer) As String
        Dim strSMEmail As String = String.Empty

        If Not ImpactToDealer Is Nothing Then
            Dim arlBusinessArea As ArrayList = New BusinessAreaFacade(User).Retrieve( _
                GetServiceManagerEmailCriteria(ImpactToDealer.ID))
            If arlBusinessArea.Count > 0 Then
                strSMEmail = CType(arlBusinessArea(0), BusinessArea).Email
            End If
        End If

        Return strSMEmail
    End Function

    Private Function SetFormatEmail(ByVal ChangedAllocation As TrClassAllocation) As String
        Dim sb As StringBuilder = New StringBuilder
        Dim strDate As String = Format(DateTime.Now, "dd-MMM-yyyy hh:mm:ss")

        sb.Append("<html><body><Table width=600px><h1><td colspan = 4 align=center><b>")
        sb.Append("PT Mitsubishi Motors Krama Yudha Sales Indonesia")
        sb.Append("</b></td></h1><tr><td><br></td></tr><tr><td>")
        sb.Append("Jakarta,")
        sb.Append(strDate)
        sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        sb.Append("Kepada Yth,")
        sb.Append("</td> </tr><tr><td>")
        sb.Append(ChangedAllocation.Dealer.DealerCode & " / " & ChangedAllocation.Dealer.DealerName)
        sb.Append("</td></tr><tr><td>")
        sb.Append(ChangedAllocation.Dealer.City.CityName)
        sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        sb.Append("Hal: Pemberitahuan Perubahan Alokasi")
        sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        sb.Append("Sehubungan dengan adanya perubahan alokasi yang telah kami beritahukan sebelumnya via e-mail, maka alokasi dealer Anda mengalami perubahan sbb:")
        sb.Append("</td></tr><tr><td><table width=400px bgcolor=black cellspacing=1 border=1 cellpadding=2><tr bgcolor=white ><td width=25%>&nbsp;</td> <td align=center>")
        sb.Append("Alokasi Awal")
        sb.Append("</td> <td align=center>")
        sb.Append("Alokasi Akhir")
        sb.Append("</td></tr><tr bgcolor=white><td width=25%>")
        sb.Append(ChangedAllocation.TrClass.ClassCode)
        sb.Append("</td><td align=right>" & ChangedAllocation.AllocatedBefore.ToString() & "</td><td align=right>" & ChangedAllocation.Allocated.ToString() & "</td></tr></table><tr><td><br></td></tr><tr><td>")
        If ChangedAllocation.Allocated > 0 Then
            sb.Append("Saat ini Anda dapat melakukan pendaftaran ulang untuk alokasi dan kode kelas seperti tersebut diatas.")
        End If
        sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        sb.Append("Terima kasih.")
        sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        sb.Append("Hormat kami,")
        sb.Append("</td></tr><tr><td>")
        sb.Append("Training Dept.")
        sb.Append("</td></tr></table></body></table></html>")

        Return sb.ToString()
    End Function

    Private Sub SendEmail(ByVal AllAllocationDealerColl As ArrayList, _
        ByVal AllocationToProcessColl As ArrayList, ByVal ClassToAllocated As TrClass)

        Dim strSmtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim objEmail As DNetMail = New DNetMail(strSmtp)
        Dim strFrom As String = GetEmailUser()
        Dim strSubject As String = "[MMKSI-DNet] Service - Pemberitahuan Perubahan Alokasi Training"
        Dim arlErrorSendEmail As ArrayList = New ArrayList

        Dim arlListUserEmail As ArrayList = GetActiveListUserEmail()

        Dim arlChangedAllocation As ArrayList = GetChangedAllocation(AllAllocationDealerColl, _
            AllocationToProcessColl)

        Try
            For Each objChangedAllocation As TrClassAllocation In arlChangedAllocation
                Dim strTo As String = String.Empty
                Dim strCc As String = String.Empty
                GenerateToAndCcAddress(strTo, strCc, _
                    objChangedAllocation.Dealer, arlListUserEmail)
                Dim strBody As String = SetFormatEmail(objChangedAllocation)

                If (strTo <> String.Empty Or strCc <> String.Empty) _
                    And strFrom <> String.Empty Then
                    'added by samuel : dealer will get email if dealer has been allocated
                    If IsDealerAllocated(objChangedAllocation.TrClass.ID, objChangedAllocation.Dealer.ID) Then
                        objEmail.sendMail(strTo, strCc, strFrom, strSubject, Mail.MailFormat.Html, strBody)
                    End If
                Else
                    arlErrorSendEmail.Add(objChangedAllocation.Dealer.DealerCode)
                End If
            Next
        Catch ex As Exception
            'MessageBox.Show("Proses Kirim Email ke dealer " & & " Tidak Berhasil")
        End Try

        If arlErrorSendEmail.Count > 0 Then
            Dim strErrorDealer As String = String.Empty
            For Each strDealer As String In arlErrorSendEmail
                If strErrorDealer = String.Empty Then
                    strErrorDealer += strDealer
                Else
                    strErrorDealer += "," & strDealer
                End If
            Next
            MessageBox.Show("Proses Kirim Email Tidak Berhasil ke Dealer : " & strErrorDealer)
        End If
        'DisplayChangedAllocation(arlChangedAllocation)
    End Sub


    Private Sub SendEmailEnhan(ByVal listTrAllocation As ArrayList)
        Dim strSmtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim objEmail As DNetMail = New DNetMail(strSmtp)
        Dim strFrom As String = GetEmailUser()
        Dim strSubject As String = "[MMKSI-DNet] Service - Pemberitahuan Perubahan Alokasi Training"
        Dim arlErrorSendEmail As ArrayList = New ArrayList
        Dim arlListUserEmail As ArrayList = GetActiveListUserEmail()
        Try
            For Each objChangedAllocation As TrClassAllocation In listTrAllocation
                If objChangedAllocation.Allocated <> objChangedAllocation.AllocatedBefore Then
                    If (objChangedAllocation.TrClass.TrClassRegistrations.Count > 0) And (objChangedAllocation.AllocatedBefore > 0) Then
                        Dim strTo As String = String.Empty
                        Dim strCc As String = String.Empty
                        GenerateToAndCcAddress(strTo, strCc, _
                            objChangedAllocation.Dealer, arlListUserEmail)
                        Dim strBody As String = SetFormatEmail(objChangedAllocation)
                        If (strTo <> String.Empty Or strCc <> String.Empty) _
                            And strFrom <> String.Empty Then
                            'added by samuel : dealer will get email if dealer has been allocated
                            'If IsDealerAllocated(objChangedAllocation.TrClass.ID, objChangedAllocation.Dealer.ID) Then
                            objEmail.sendMail(strTo, strCc, strFrom, strSubject, Mail.MailFormat.Html, strBody)
                            'End If
                        Else
                            arlErrorSendEmail.Add(objChangedAllocation.Dealer.DealerCode)
                        End If
                    End If

                End If

            Next
        Catch ex As Exception
            'MessageBox.Show("Proses Kirim Email ke dealer " & & " Tidak Berhasil")
        End Try

        If arlErrorSendEmail.Count > 0 Then
            Dim strErrorDealer As String = String.Empty
            For Each strDealer As String In arlErrorSendEmail
                If strErrorDealer = String.Empty Then
                    strErrorDealer += strDealer
                Else
                    strErrorDealer += "," & strDealer
                End If
            Next
            MessageBox.Show("Proses Kirim Email Tidak Berhasil ke Dealer : " & strErrorDealer)
        End If
        'DisplayChangedAllocation(arlChangedAllocation)
    End Sub
#End Region

#Region "General Method"
    Private Sub SetEnabilityButton(ByVal IsRowExist As Boolean)
        btnSimpan.Enabled = IsRowExist
        btnBatal.Enabled = IsRowExist
        btnDownload.Enabled = IsRowExist
    End Sub

    Private Function CloneArrayList(ByVal arlToBeClone As ArrayList) As ArrayList
        Dim arlResultClone As ArrayList = New ArrayList
        'For Each obj As TrClassAllocation In arlToBeClone
        '    arlResultClone.Add(obj)
        'Next
        arlResultClone = arlToBeClone.Clone()
        Return arlResultClone
    End Function

    'Private Sub DisplayChangedAllocation(ByVal ChangedAllocationColl As ArrayList)
    '    dtgChangedAllocation.DataSource = ChangedAllocationColl
    '    dtgChangedAllocation.DataBind()
    'End Sub

    Private Sub SortListControl(ByRef ListControl As ArrayList, ByVal SortColumn As String, _
    ByVal SortDirection As String)
        Dim IsAsc As Boolean = True
        If SortDirection = "ASC" Then
            IsAsc = True
        ElseIf SortDirection = "DESC" Then
            IsAsc = False
        End If
        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        ListControl.Sort(objListComparer)
    End Sub

    Private Sub BindDataGridForDownload(ByRef grid As DataGrid)
        'checkDealer()

        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOBillingRecap), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If Me.ddlOrderType.SelectedValue <> "A" Then
        '    crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "OrderType", MatchType.Exact, Me.ddlOrderType.SelectedValue))
        'End If

        ''crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
        ''If txtKodeDealer.Text.Trim() <> "" Then
        'crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim()))
        ''End If

        'If Me.ddlMonth.SelectedValue <= Me.ddlMonthTo.SelectedValue Then
        '    crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodMonth", MatchType.GreaterOrEqual, CType(Me.ddlMonth.SelectedValue, Integer)))
        '    crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodMonth", MatchType.LesserOrEqual, CType(Me.ddlMonthTo.SelectedValue, Integer)))
        'End If

        'crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "PeriodYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))

        'Dim objSPPOBillingFacade As SparePartPOBillingFacade
        'objSPPOBillingFacade = New SparePartPOBillingFacade(User)
        'Dim list As ArrayList = objSPPOBillingFacade.Retrieve(crit)

        'Dim result As Integer
        'If Not list Is Nothing Then
        '    If list.Count < 1 Then
        '        result = 0
        '    Else
        '        result = list.Count
        '    End If
        'End If
        'If result >= 1 Then
        '    grid.DataSource = list
        'Else
        '    grid.DataSource = New ArrayList
        'End If
        'grid.DataBind()
    End Sub

    'Private Sub WriteTrAllocData(ByRef sw As StreamWriter)

    '    Dim writer As HtmlTextWriter = New HtmlTextWriter(sw)



    '    grid.Visible = True
    '    grid.RenderControl(writer)
    '    writer.Flush()

    'End Sub

    Private Function IsFileExist(ByVal filename As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

        Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        sapImp.Start()

        Try
            '---Mode using transfer file---
            'Dim objUpload As TransferFile = New TransferFile(_user, _password, _sapServer)
            'objUpload.copyFile(filename, newFolder, False)

            '---Mode using sapImpersonate---
            Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)

            'Return fInfo.Exists
            If fInfo.Exists Then
                fInfo.Delete()
                Return False
            End If

        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(filename)))
        Finally
            sapImp.StopImpersonate()
        End Try
    End Function
    Private Sub SavingToFolder(ByVal targetFile As String, ByVal postedFile As HttpPostedFile)

        Try
            'Dim directoryFile As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(RootDestDir)
            'If Not directoryFile.Exists Then
            '    directoryFile.Create()
            'End If

            postedFile.SaveAs(targetFile)
            'fInfo.MoveTo(targetFile)

            Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
            If Not trgInfo.Exists Then
                Throw New IO.IOException("File gagal disimpan di Server. Harap hubungi Administrator")
            End If
        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(targetFile)))
        End Try
    End Sub

    Private Sub UploadAllocation()

        If (fileUpload.PostedFile Is Nothing) OrElse fileUpload.PostedFile.ContentLength <= 0 Then
            MessageBox.Show("Pilih file yang akan di-upload.")
            Return
        Else
            If fileUpload.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                MessageBox.Show("Bukan file EXCEL. File anda " & fileUpload.PostedFile.ContentType)
                Return

            Else
                'cek maxFileSize First
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fileUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                Else
                    Dim filename As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                    Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & filename
                    Dim arlParseResult As New ArrayList

                    If IsFileExist(targetFile) Then
                        MessageBox.Show("File exist")
                        Return
                    End If


                    Dim _user As String
                    _user = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String
                    _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String
                    _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        SavingToFolder(targetFile, fileUpload.PostedFile)
                        Dim objParser As UploadClassAllocationParser
                        Dim arlSheet As ArrayList = New ArrayList
                        Dim arlData As ArrayList = New ArrayList
                        objParser = New UploadClassAllocationParser
                        arlSheet = objParser.GetSheet(targetFile)
                        arlData = CType(objParser.ParsingExcel(targetFile, "[" & CType(arlSheet(0), String) & "]", "User"), ArrayList)
                        If arlData.Count > 0 Then
                            Dim objC As TrClass
                            Dim objD As Dealer
                            Dim objTC As TrCourse
                            Dim objCFac As TrClassFacade = New TrClassFacade(User)
                            Dim objDFac As DealerFacade = New DealerFacade(User)
                            Dim objTCFac As TrCourseFacade = New TrCourseFacade(User)
                            Dim objCA As TrClassAllocation
                            Dim arlCA As ArrayList = New ArrayList

                            Dim strData(4) As String
                            Dim newAllocation As Integer
                            Dim i As Integer
                            Dim Idx As Integer = 0

                            For Each strDatas As String In arlData
                                strData = strDatas.Split(";")

                                objTC = objTCFac.Retrieve(strData(0))
                                objTC.CourseCode = strData(0)
                                objC = objCFac.Retrieve(strData(1))
                                objC.ClassCode = strData(1)
                                objD = objDFac.Retrieve(strData(2))
                                objD.DealerCode = strData(2)
                                newAllocation = CInt(strData(3))
                                objCA = New TrClassAllocation
                                objD.MarkLoaded()
                                objC.MarkLoaded()
                                objCA.Dealer = objD
                                objCA.TrClass = objC

                                If objC.ID > 0 AndAlso Not IsNothing(objC.TrCourse) AndAlso objC.TrCourse.ID <> objTC.ID Then
                                    objC = New TrClass
                                    objC.ClassCode = strData(1)
                                    objC.MarkLoaded()
                                    objCA.TrClass = objC
                                End If
                                'If (IsNothing(objC) OrElse objC.ID < 1) OrElse IsNothing(objC.TrCourse) OrElse objC.TrCourse.ID <> objTC.ID Then
                                '    objCA.TrClass = Nothing
                                'Else
                                '    objCA.TrClass = objC
                                'End If
                                objCA.Allocated = newAllocation
                                arlCA.Add(objCA)
                                Idx += 1
                            Next

                            SetCapacityMaster(arlCA)
                            'arlCA = CommonFunction.SortArraylist(arlCA, GetType(TrClassAllocation), "ClassCode", Sort.SortDirection.ASC)
                            'SortListControl(arlCA, "ClassID", "ASC")
                            'ViewState("CurrentSortDirect")
                            objSessionHelper.SetSession("arlCA", arlCA)
                            dtgUpload.DataSource = arlCA
                            dtgUpload.DataBind()
                        Else
                            MessageBox.Show("Struktur data tidak sesuai.")
                            dtgUpload.DataSource = Nothing
                            dtgUpload.DataBind()
                        End If

                    Catch ex As Exception
                        Throw
                    Finally
                        sapImp.StopImpersonate()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub setUploadControl(ByVal IsUploading As Boolean)
        dtgTrClassAllocation.Visible = Not IsUploading
        dtgUpload.Visible = IsUploading
        If IsUploading Then
            objSessionHelper.SetSession("Upload.IsProcessing", True)
        Else
            objSessionHelper.SetSession("Upload.IsProcessing", False)
        End If
        btnSetAllocation.Enabled = Not IsUploading
        btnSimpan.Enabled = True
        btnBatal.Enabled = True
        txtKodeKategori.Text = ""
        txtKodeKelas.Text = ""
        txtKodeDealer.Text = ""

    End Sub

    Private Function TotalClassAllocation(ByVal ClassID As Integer) As Integer
        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", ClassID))
        'arlCA = objCAFac.Retrieve(crtCA)
        'For Each objCA As TrClassAllocation In arlCA
        '    Tot = Tot + objCA.Allocated
        'Next
        Dim aggCA As Aggregate = New Aggregate(GetType(TrClassAllocation), "Allocated", AggregateType.Sum)
        'arlCA = objCAFac.Retrieve(crtCA)
        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)
        Return Tot
    End Function

    Private Function TotalClassAllocationPerDealer(ByVal ClassID As Integer, ByVal DealerID As Integer) As Integer
        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", ClassID))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", DealerID))
        Dim aggCA As Aggregate = New Aggregate(GetType(TrClassAllocation), "Allocated", AggregateType.Sum)
        'arlCA = objCAFac.Retrieve(crtCA)
        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)
        'For Each objCA As TrClassAllocation In arlCA
        '    Tot = Tot + objCA.Allocated
        'Next
        Return Tot
    End Function
    Private Function TotalClassAllocationByOtherDealers(ByVal ClassID As Integer, ByVal DealerIDs As String) As Integer
        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, ClassID))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", MatchType.NotInSet, DealerIDs))
        Dim aggCA As Aggregate = New Aggregate(GetType(TrClassAllocation), "Allocated", AggregateType.Sum)

        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)

        Return Tot
    End Function




#End Region

#End Region

    Private Function GetCAUpdateHistory(ByVal oCA As TrClassAllocation) As String
        'If oCA.History <> "" Then
        '    Return oCA.History
        'Else
        If oCA.ID < 1 Then oCA = GetCA(oCA)
        If oCA.ID > 0 Then
            If oCA.LastUpdateTime.Year > 2000 AndAlso oCA.LastUpdateBy.Trim <> String.Empty Then
                Return oCA.LastUpdateBy & "-" & oCA.LastUpdateTime.ToString("dd/MM/yyyy hh:mm:ss")
            Else
                Return oCA.CreatedBy & "-" & oCA.CreatedTime.ToString("dd/MM/yyyy hh:mm:ss")
            End If
        Else
            Return ""
        End If

        'End If
    End Function

    Private Function GetCA(ByVal oCA As TrClassAllocation) As TrClassAllocation
        Try
            Dim oCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
            Dim cCA As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aCA As New ArrayList
            cCA.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", MatchType.Exact, oCA.Dealer.ID))
            cCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, oCA.TrClass.ID))
            aCA = oCAFac.Retrieve(cCA)
            If aCA.Count > 0 Then
                Return CType(aCA(aCA.Count - 1), TrClassAllocation)

            Else
                Return oCA
            End If
        Catch ex As Exception
            Return oCA
        End Try
    End Function


    Private Sub SetCapacityMaster(ByVal parlCA As ArrayList)
        Dim arlCA As New ArrayList
        For Each oCA As TrClassAllocation In parlCA 'copy:prevent byref 
            arlCA.Add(oca)
        Next
        Dim arlSorted As ArrayList = CommonFunction.SortArraylist(arlCA, GetType(TrClassAllocation), "ClassCode", Sort.SortDirection.ASC)

        Dim arlClassCapacity As New ArrayList
        Dim arlData As New ArrayList
        Dim sClassIDTemp As Integer = 0
        Dim Idx As Integer = 0
        Dim i As Integer = 0

        For Each oCA As TrClassAllocation In arlSorted
            Try
                If Not IsNothing(oCA.TrClass) AndAlso oCA.TrClass.ID > 0 AndAlso oCA.TrClass.ID <> sClassIDTemp Then
                    arlData = New ArrayList

                    arlData.Add(oCA.TrClass.ID)
                    arlData.Add(oCA.TrClass.Capacity)
                    arlData.Add(Me.TotalClassAllocation(oCA.TrClass.ID))

                    Dim TotAllocationInThisFile As Integer = 0
                    Dim DealerIDs As String = ""
                    Dim oCATemp As TrClassAllocation
                    For i = Idx To arlSorted.Count - 1
                        oCATemp = CType(arlSorted(i), TrClassAllocation)
                        If Not IsNothing(oCATemp.TrClass) AndAlso oCATemp.TrClass.ID > 0 AndAlso Not IsNothing(oCATemp.Dealer) AndAlso oCATemp.Dealer.ID > 0 AndAlso oCATemp.TrClass.ID = oCA.TrClass.ID Then
                            TotAllocationInThisFile += oCATemp.Allocated
                            DealerIDs &= IIf(DealerIDs = "", "", ",") & oCATemp.Dealer.ID
                        Else
                            Exit For
                        End If
                    Next
                    Dim TotAllocForOtherDealers As Integer = 0
                    If DealerIDs <> "" Then
                        TotAllocForOtherDealers = TotalClassAllocationByOtherDealers(oCATemp.TrClass.ID, DealerIDs)
                    End If
                    arlData.Add(TotAllocationInThisFile + TotAllocForOtherDealers)
                    Dim Remain As Integer = arlData(1) - arlData(3)
                    arlData.Add(IIf(Remain >= 0, 1, 0))
                    arlClassCapacity.Add(arlData)

                    sClassIDTemp = oCA.TrClass.ID
                End If
            Catch ex As Exception
                Dim s As String = ex.Message
            End Try
            Idx += 1
        Next
        objSessionHelper.SetSession("arlClassCapacity", arlClassCapacity)
    End Sub

    Private Function IsEnoughAllocation(ByVal ClassID As Integer) As Boolean
        Dim i As Integer
        Dim arlClassCapacity As ArrayList = CType(objSessionHelper.GetSession("arlClassCapacity"), ArrayList)
        Dim arlData As ArrayList

        For i = 0 To arlClassCapacity.Count - 1
            arlData = arlClassCapacity(i)
            If CType(arlData(0), Integer) = ClassID Then
                Return IIf(CType(arlData(4), Integer) = 1, True, False)
            End If
        Next
    End Function

    Private Function GetDataClassAllocation(ByVal ClassID As Integer) As ArrayList
        Dim i As Integer
        Dim arlClassCapacity As ArrayList = CType(objSessionHelper.GetSession("arlClassCapacity"), ArrayList)
        Dim arlData As ArrayList

        For i = 0 To arlClassCapacity.Count - 1
            arlData = arlClassCapacity(i)
            If CType(arlData(0), Integer) = ClassID Then
                Return arlData 'IIf(CType(arlData(4), Integer) = 1, True, False)
            End If
        Next
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 2000
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not Page.IsPostBack Then
            objSessionHelper.SetSession("Upload.IsProcessing", False)
            InitiatePage()
            SetControlPrivilege()
            AssignAttributeControl()
            txtPeriod.Text = Date.Now.Year.ToString
            If GetSessionCriteria() Then
                btnCari_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub SaveFromUploadedFile()
        Dim strInfo As String = ""

        'For Each dtgItem As DataGridItem In dtgUpload.Items
        '    Dim lblError As Label = dtgitem.FindControl("lblError")
        '    If lblError.Text.Trim <> "" Then
        '        MessageBox.Show("Data tidak valid, silahkan dicek kembali")
        '        Exit Sub
        '    End If
        'Next

        'Insert into database 
        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim objCA As TrClassAllocation
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim objCFac As TrClassFacade = New TrClassFacade(User)
        Dim objD As Dealer
        Dim objC As TrClass
        Dim nError As Integer = 0

        For Each dtgItem As DataGridItem In dtgUpload.Items
            Dim lblSelisih As Label = dtgItem.FindControl("lblSelisih")
            Dim txtClassID As TextBox = dtgItem.FindControl("txtClassID")
            Dim txtDealerID As TextBox = dtgItem.FindControl("txtDealerID")
            Dim txtAllocated As TextBox = dtgItem.FindControl("txtAllocated")
            Dim lblError As Label = dtgItem.FindControl("lblError")

            If lblError.Text.Trim <> "" Then
                nError += 1
            Else
                objD = objDFac.Retrieve(CInt(txtDealerID.Text))
                objC = objCFac.Retrieve(CInt(txtClassID.Text))
                objCA = GetTrClassAllocation(objC.ID, objD.ID)
                If objCA Is Nothing Then
                    objCA = New TrClassAllocation
                    objCA.Dealer = objD
                    objCA.TrClass = objC
                Else
                    If objCA.ID < 1 Then
                        objCA = New TrClassAllocation
                        objCA.Dealer = objD
                        objCA.TrClass = objC
                    Else
                        objCA.AllocatedBefore = objCA.Allocated
                        If objCA.Allocated > 0 And objCA.Allocated <> CInt(txtAllocated.Text) Then
                            objCA.CancelReason = ""
                            'objCA.CancelReason = "Automatically updated when upload"
                        End If
                    End If
                End If
                objCA.Allocated = CInt(txtAllocated.Text)
                If objCA.ID < 1 AndAlso objCAFac.Insert(objCA) = -1 Then
                    dtgItem.BackColor = System.Drawing.Color.Red
                    lblError.Text &= "(Error Insert Data)"
                    nError += 1
                ElseIf objCA.ID > 0 AndAlso objCAFac.Update(objCA) = -1 Then
                    dtgItem.BackColor = System.Drawing.Color.Red
                    lblError.Text &= "(Error Update Data)"
                    nError += 1
                End If
            End If
        Next
        If nError > 0 Then
            If dtgUpload.Items.Count = nError Then
                MessageBox.Show(nError.ToString & " data gagal disimpan")
                setUploadControl(True)
                btnDownload.Enabled = True
            Else
                MessageBox.Show((dtgUpload.Items.Count - nError).ToString & " data berhasil disimpan, " & nError.ToString & " gagal disimpan")
                Dim aTemp As New ArrayList
                'aTemp = CType(objSessionHelper.GetSession("arlCA"), ArrayList)
                For Each oDI As DataGridItem In dtgUpload.Items
                    Dim lblError As Label = oDI.FindControl("lblError")
                    If lblerror.Text.Trim <> "" Then
                        aTemp.Add(CType(CType(objSessionHelper.GetSession("arlCA"), ArrayList)(oDI.ItemIndex), TrClassAllocation))
                    End If
                Next
                objSessionHelper.SetSession("arlCA", aTemp)
                dtgUpload.DataSource = aTemp
                dtgUpload.DataBind()
                setUploadControl(True)
                btnDownload.Enabled = True
            End If
        Else
            MessageBox.Show(SR.SaveSuccess)
            setUploadControl(False)
            FillCriteriaBasedOnUpload()
        End If
    End Sub

    Private Sub FillCriteriaBasedOnUpload()
        Dim ClassCode As String = ""
        Dim DealerCode As String = ""
        Dim CourseCode As String = ""
        Dim objC As TrClass
        Dim objCFac As TrClassFacade = New TrClassFacade(User)

        txtKodeKategori.Text = ""
        txtKodeKelas.Text = ""
        txtKodeDealer.Text = ""
        For Each dtgItem As DataGridItem In dtgUpload.Items
            Dim lblClassCode As Label = dtgItem.FindControl("lblClassCode")
            Dim lblDealerCode As Label = dtgItem.FindControl("lblDealerCode")
            objC = objCFac.Retrieve(lblClassCode.Text)
            ClassCode = lblClassCode.Text
            DealerCode = lblDealerCode.Text
            CourseCode = objC.TrCourse.CourseCode
            If Not IsExistInArray(CourseCode, txtKodeKategori.Text) Then
                txtKodeKategori.Text = txtKodeKategori.Text & IIf(txtKodeKategori.Text.Trim = "", "", ";") & CourseCode
            End If
            If Not IsExistInArray(ClassCode, txtKodeKelas.Text) Then
                txtKodeKelas.Text = txtKodeKelas.Text & IIf(txtKodeKelas.Text.Trim = "", "", ";") & ClassCode
            End If
            If Not IsExistInArray(CourseCode, txtKodeDealer.Text) Then
                txtKodeDealer.Text = txtKodeDealer.Text & IIf(txtKodeDealer.Text.Trim = "", "", ";") & DealerCode
            End If

        Next

    End Sub

    Private Function IsExistInArray(ByVal Item As String, ByVal strData As String)
        Dim arrData() As String
        Dim i As Integer

        arrData = strData.Split(";")
        For i = 0 To arrData.Length - 1
            If Item.ToUpper = arrData(i).ToUpper Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function GetTrClassAllocation(ByVal ClassID As Integer, ByVal DealerID As Integer) As TrClassAllocation
        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim objCA As TrClassAllocation
        Dim crtCA As CriteriaComposite

        crtCA = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", ClassID))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", DealerID))
        arlCA = objCAFac.Retrieve(crtCA)
        objCA = New TrClassAllocation
        If arlCA.Count > 0 Then objCA = CType(arlCA(0), TrClassAllocation)

        Return objCA

    End Function
    Private Function IsOver(ByVal Remain As Integer, ByVal ClassCode As String) As Boolean
        Dim TotNewAllocation As Integer = 0
        Dim tmpTotal As Integer = 0

        For Each dtgItem As DataGridItem In dtgUpload.Items
            Dim lblClassCode As Label = dtgItem.FindControl("lblClassCode")
            If lblClassCode.Text.Trim.ToUpper = ClassCode.Trim.ToUpper Then
                Dim txtAllocated As TextBox = dtgItem.FindControl("txtAllocated")
                Dim lblError As Label = dtgItem.FindControl("lblError")
                Dim txtErrorOverLimit As TextBox = dtgItem.FindControl("txtErrorOverLimit")
                TotNewAllocation = TotNewAllocation + CInt(txtAllocated.Text)
                If TotNewAllocation > Remain Then
                    lblError.Text = lblError.Text & IIf(lblError.Text.Trim <> "", ";", "") & "Jumlah alokasi melebihi sisa alokasi"
                    txtErrorOverLimit.Text = "1"
                Else
                    txtErrorOverLimit.Text = "0"
                End If
            End If
        Next

        Return IIf(TotNewAllocation > Remain, True, False)
    End Function

    Private Sub ColorizeOverClass(ByVal ClassCode As String)
        For Each dtgItem As DataGridItem In dtgUpload.Items
            Dim lblClassCode As Label = dtgItem.FindControl("lblClassCode")
            If lblClassCode.Text.Trim.ToUpper = ClassCode.Trim.ToUpper Then
                dtgItem.BackColor = System.Drawing.Color.Red
                Dim lblError As Label = dtgItem.FindControl("lblError")
                lblError.Text = lblError.Text & IIf(lblError.Text.Trim <> "", ";", "") & "Jumlah alokasi melebihi sisa alokasi"
            End If
        Next
    End Sub


#Region "Event Handler "
    Private Sub dtgCourse_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTrClassAllocation.ItemCommand
        If e.CommandName = "Delete" Then
            DeleteRowData(e)
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            btnDownload.Enabled = False
            'tambahan, belum masa naik 20120106
        ElseIf e.CommandName = "Edit" Then
            Dim arlAllocation As ArrayList = CType(Session.Item("arlAllocation"), ArrayList)
            Dim objAllocation As TrClassAllocation = CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)
            SetSessionCriteria()
            Server.Transfer("FrmTrClassAllocationCancel.aspx?ID=" & objAllocation.ID & "&Opener=FrmTrClassAllocation.aspx")
        End If
    End Sub


    Private Sub SetSessionCriteria()
        Dim objClassAlloc As ArrayList = New ArrayList
        objClassAlloc.Add(txtKodeKategori.Text.Trim) '0
        objClassAlloc.Add(txtKodeKelas.Text.Trim) '1
        objClassAlloc.Add(txtNamaKelas.Text.Trim) '2
        objClassAlloc.Add(txtKapasitas.Text.Trim) '3
        objClassAlloc.Add(txtKodeDealer.Text.Trim) '4
        objClassAlloc.Add(chkAllocation.Checked) '5
        objClassAlloc.Add(chkBatal.Checked) '6
        objClassAlloc.Add(txtPeriod.Text.Trim) '7

        objSessionHelper.SetSession("SESSIONCLASSALLOC", objClassAlloc)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objClassAlloc As ArrayList = objSessionHelper.GetSession("SESSIONCLASSALLOC")
        If Not objClassAlloc Is Nothing Then
            txtKodeKategori.Text = objClassAlloc.Item(0) '0
            txtKodeKelas.Text = objClassAlloc.Item(1) '1
            txtNamaKelas.Text = objClassAlloc.Item(2) '2
            txtKapasitas.Text = objClassAlloc.Item(3) '3
            txtKodeDealer.Text = objClassAlloc.Item(4) '4
            chkAllocation.Checked = objClassAlloc.Item(5) '5
            chkBatal.Checked = objClassAlloc.Item(6) '6
            txtPeriod.Text = objClassAlloc.Item(7) '7
            Return True
        End If
        Return False
    End Function

    Private Sub dtgTrClass_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrClassAllocation.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
            e.Item.ItemType = ListItemType.AlternatingItem Or _
                e.Item.ItemType = ListItemType.SelectedItem Then
            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dtgTrClassAllocation.CurrentPageIndex * dtgTrClassAllocation.PageSize)

            Dim lblLastUpdateTime As Label = e.Item.FindControl("lblLastUpdateTime")

            Dim arlAllocation As ArrayList = _
                CType(Session.Item("arlAllocation"), ArrayList)
            Dim objAllocation As TrClassAllocation = _
                CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)

            e.Item.Cells(7).Text = CType(objAllocation.TrClass.Capacity, String)
            Dim allAlocated As Integer = objAllocation.AllocatedTaken 'GetAllocatedCapacity(objAllocation.TrClass.ID)
            'Dim sisa As Integer = objAllocation.TrClass.Capacity - hitungSisa(objAllocation.TrClass.ID)
            'Dim remaining As String = CType(objAllocation.TrClass.Capacity - hitungSisa(objAllocation.TrClass.ID) - allAlocated, String)
            Dim sisa As Integer = objAllocation.TrClass.Capacity
            Dim remaining As String = CType(objAllocation.TrClass.Capacity - allAlocated, String)
            If remaining < 0 Then
                e.Item.Cells(8).Text = 0
            Else
                e.Item.Cells(8).Text = remaining
            End If
            lblLastUpdateTime.Text = objAllocation.History 'GetCAUpdateHistory(objAllocation) 'objAllocation.History
            SetControlAttribute(objAllocation, e)
            SetColumnText(objAllocation, e)
        End If
    End Sub

    Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
        If Not e.Item.ItemIndex = -1 Then
            Dim lblLastUpdateTimeInGrid As Label = e.Item.FindControl("lblLastUpdateTimeInGrid")

            Dim arlAllocation As ArrayList = _
                CType(objSessionHelper.GetSession("ArrListDealerSelected"), ArrayList)
            Dim objAllocation As TrClassAllocation = _
                CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)
            If Not objAllocation.IsPickOnSearch Then
                e.Item.BackColor = Color.LightSalmon
            End If
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1
            e.Item.Cells(1).Text = objAllocation.TrClass.ClassCode
            e.Item.Cells(2).Text = objAllocation.Dealer.DealerCode
            e.Item.Cells(3).Text = objAllocation.Dealer.DealerName
            e.Item.Cells(4).Text = objAllocation.Dealer.City.CityName
            e.Item.Cells(5).Text = CType(objAllocation.TrClass.Capacity, String)
            Dim allAlocated As Integer = objAllocation.AllocatedTaken 'GetAllocatedCapacity(objAllocation.TrClass.ID)
            'Dim remaining As String = CType(objAllocation.TrClass.Capacity - hitungSisa(objAllocation.TrClass.ID) - allAlocated, String)
            Dim remaining As String = CType(objAllocation.TrClass.Capacity - allAlocated, String)
            Dim lblUsedAllocation As Label = e.Item.FindControl("lblUsedAllocation")
            lblUsedAllocation.Text = GetAllocationTaken(objAllocation).ToString()
            If remaining < 0 Then
                e.Item.Cells(6).Text = 0
            Else
                e.Item.Cells(6).Text = remaining
            End If
            lblLastUpdateTimeInGrid.Text = objAllocation.History 'GetCAUpdateHistory(objAllocation) 'bjAllocation.History
            '   e.Item.Cells(5).Text = CType(objAllocation.TrClass.Capacity - hitungSisa(objAllocation.TrClass.ID), String)
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If CType(objSessionHelper.GetSession("Upload.IsProcessing"), Boolean) = True Then
            SaveFromUploadedFile()
            btnCari_Click(Nothing, Nothing)
            'btnSetAllocation_Click(sender, e)
            Exit Sub
        End If

        If dtgTrClassAllocation.Items.Count < 1 Then
            Exit Sub
        End If

        Dim arlAllocation As ArrayList = CType(Session.Item("arlAllocation"), ArrayList)
        arlAllocation = GetOriginalAllocation(arlAllocation)
        Dim arlErrorDealer As ArrayList = New ArrayList

        CheckCancelledAllocation(arlAllocation, arlErrorDealer)
        If arlErrorDealer.Count > 0 Then
            MessageBox.Show(GetMessageErrorClassCancelled(arlErrorDealer))
            Exit Sub
        End If

        If CheckTotalAllocation() Then
            If ValidateTotalAllocation() Then
                If Not Page.IsValid Then
                    Return
                End If
                SaveData()
            Else
                MessageBox.Show("Jumlah alokasi tidak boleh melebihi kapasitas kelas")
            End If
        Else
            MessageBox.Show("Jumlah alokasi harus lebih besar dari 0")
        End If
    End Sub


    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If CType(objSessionHelper.GetSession("Upload.IsProcessing"), Boolean) = True Then
            setUploadControl(False)
            Exit Sub
        End If
        BindDataGridForBatal()
    End Sub

    Private Sub dtgTrClassAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTrClassAllocation.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), String)
                Case "ASC" 'Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = "DESC" 'Sort.SortDirection.DESC

                Case "DESC" 'Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
            End Select

        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
        End If
        BindDataGrid(False)
    End Sub

    Private Sub btnSetAllocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetAllocation.Click
        'BindDataGrid(True)
        Dim strErrorMsg As String = String.Empty
        If CheckDataForm(strErrorMsg) Then
            objSessionHelper.RemoveSession("arlAllocation")
            BindDataGrid(True)
            btnSimpan.Enabled = True
            btnBatal.Enabled = True
            btnDownload.Enabled = True
        Else
            MessageBox.Show(strErrorMsg)
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'If Not Page.IsValid Then
        '    Exit Sub
        'End If

        'SynchronizeGrid()

        '-- Temp file must be a randomly named file!
        Dim sFileName As String = "TrClassAllocation" & sSuffix & ".xls"
        Dim TrAllocData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(TrAllocData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(TrAllocData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                Dim writer As HtmlTextWriter

                If CType(Me.objSessionHelper.GetSession("Upload.IsProcessing"), Boolean) = True Then
                    WriteAllocation(sw, CType(Me.objSessionHelper.GetSession("arlCA"), ArrayList))
                Else
                    writer = New HtmlTextWriter(sw)
                    grid.Visible = True
                    grid.RenderControl(writer)
                    writer.Flush()

                End If

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName)
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub


    Private Sub WriteAllocation(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("TRAINING - Alokasi")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Kelas" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kota" & tab)
            itemLine.Append("Kapasitas" & tab)
            itemLine.Append("Selisih Alokasi" & tab)
            itemLine.Append("Jumlah Alokasi" & tab)
            itemLine.Append("Alokasi Sebelum" & tab)
            itemLine.Append("Last Update" & tab)
            itemLine.Append("Error" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each oCA As TrClassAllocation In data
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append(i.ToString & tab)
                itemLine.Append(IIf(IsNothing(oCA.TrClass), "", oCA.TrClass.ClassCode) & tab) 'Kode Kelas
                itemLine.Append(IIf(IsNothing(oCA.Dealer), "", oCA.Dealer.DealerCode) & tab)   'Kode Dealer
                itemLine.Append(IIf(IsNothing(oCA.Dealer), "", oCA.Dealer.DealerName) & tab) 'Nama Dealer
                itemLine.Append(IIf(IsNothing(oCA.Dealer), "", IIf(IsNothing(oCA.Dealer.City), "", oCA.Dealer.City.CityName)) & tab)    'Kota
                itemLine.Append(IIf(IsNothing(oCA.TrClass), "0", oCA.TrClass.Capacity) & tab)  'Kapasitas
                itemLine.Append(IIf(IsNothing(oCA.TrClass), "0", (oCA.TrClass.Capacity - TotalClassAllocation(oCA.TrClass.ID))) & tab)  'Selisih Alokasi
                'Dim allocated As Integer = GetAllocatedCapacity(oCA.TrClass.ID)
                'itemLine.Append(IIf(IsNothing(oCA.TrClass), "0", (oCA.TrClass.Capacity - allocated)) & tab)  'Selisih Alokasi 'oon
                itemLine.Append(oCA.Allocated & tab)  'Jumlah Alokasi
                itemLine.Append(IIf(IsNothing(oCA.TrClass), "0", IIf(IsNothing(oCA.Dealer), "0", TotalClassAllocationPerDealer(oCA.TrClass.ID, oCA.Dealer.ID))) & tab) 'Alokasi Sebelum
                itemLine.Append(GetCAUpdateHistory(oCA) & tab) 'Last Update
                itemLine.Append(CType(dtgUpload.Items(i - 1).FindControl("lblError"), Label).Text & tab) 'Error

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub


    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        setUploadControl(True)
        Me.btnSimpan.Enabled = True
        UploadAllocation()


        Exit Sub

        Dim NamaFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)
        If txtKodeKelas.Text.Trim = "" Then
            MessageBox.Show("Silahkan memilih Kelas yang akan diupload")
            Exit Sub
        End If
        Dim objClass As TrClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)
        If IsNothing(objClass) Then
            MessageBox.Show("Kelas " & txtKodeKelas.Text & " tidak terdaftar ")
            Exit Sub
        End If


        If (fileUpload.PostedFile Is Nothing) OrElse fileUpload.PostedFile.ContentLength <= 0 Then
            MessageBox.Show("Pilih file yang akan di-upload.")
            Return
        Else
            If fileUpload.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                MessageBox.Show("Bukan file EXCEL. File anda " & fileUpload.PostedFile.ContentType)
                Return

            Else
                'cek maxFileSize First
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fileUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                Else
                    Dim filename As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                    Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & filename
                    Dim arlParseResult As New ArrayList

                    If IsFileExist(targetFile) Then
                        MessageBox.Show("File exist")
                        Return
                    End If


                    Dim _user As String
                    _user = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String
                    _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String
                    _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        'MessageBox.Show("Temporary out of service :)")
                        'Return
                        SavingToFolder(targetFile, fileUpload.PostedFile)
                        Dim objParser As UploadClassAllocationParser
                        Dim arlSheet As ArrayList = New ArrayList
                        Dim arlData As ArrayList = New ArrayList
                        objParser = New UploadClassAllocationParser
                        arlSheet = objParser.GetSheet(targetFile)
                        arlData = CType(objParser.ParsingExcel(targetFile, "[" & CType(arlSheet(0), String) & "]", "User"), ArrayList)
                        'targetFile = "D:\Project\Phase4\Solution\KTB.DNet\KTB.DNet.UI\DataTemp\Data.xls"
                        'arlCA = CType(objParser.ParsingExcel(targetFile, "[Sheet1]", "User"), ArrayList)

                        If txtKodeKelas.Text.Trim = "" Then
                            MessageBox.Show("Upload Gagal - Kode kelas ada yang kosong")
                            Exit Sub
                        End If
                        Dim objC As TrClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)
                        Dim objD As Dealer
                        Dim objDFac As DealerFacade = New DealerFacade(User)
                        Dim TotAllocation As Integer = 0
                        Dim TotAllocated As Integer = 0
                        Dim TotNewAllocation As Integer = 0
                        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
                        Dim objCA As TrClassAllocation
                        Dim arlCA As ArrayList = New ArrayList
                        Dim strErrMessage As String = ""

                        Dim strData(3) As String
                        Dim DealerCode As String
                        Dim newAllocation As Integer
                        Dim i As Integer

                        If objC Is Nothing Then
                            MessageBox.Show("Upload Gagal - Kode kelas " & txtKodeKelas.Text & " tidak terdaftar")
                            Exit Sub
                        Else
                            TotAllocation = objC.Capacity
                            Dim critsCA As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critsCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, objC.ID))
                            arlCA = objCAFac.Retrieve(critsCA)
                            For i = 0 To arlCA.Count - 1
                                TotAllocated = TotAllocated + CType(arlCA(0), TrClassAllocation).Allocated
                            Next
                            TotNewAllocation = 0
                            For Each strItem As String In arlData
                                strData = strItem.Split(";")
                                If CType(strData(2), Integer) = -1000 Then
                                    MessageBox.Show("Upload Gagal - Nilai Alokasi yang akan diupload tidak valid")
                                    Exit Sub
                                End If
                                TotNewAllocation = TotNewAllocation + CType(strData(2), Integer)
                            Next
                            If TotNewAllocation > (TotAllocation - TotAllocated) Then
                                MessageBox.Show("Upload Gagal - Total alokasi yang akan diupload melebihi batas")
                                Exit Sub
                            End If
                            'Check dealer existance
                            Dim arlCAInserted As ArrayList = New ArrayList
                            For Each strItem As String In arlData
                                strData = strItem.Split(";")
                                DealerCode = strData(0)
                                newAllocation = CType(strData(2), Integer)
                                If DealerCode.Trim = "" Then
                                    MessageBox.Show("Upload Gagal - Kode Dealer tidak boleh kosong")
                                    Exit Sub
                                End If
                                objD = objDFac.Retrieve(DealerCode)
                                'MessageBox.Show("DealerCode='" & DealerCode & "'")
                                If objD Is Nothing Then
                                    MessageBox.Show("Upload Gagal - Kode Dealer yang akan diupload tidak valid")
                                    Exit Sub
                                Else
                                    If objD.ID < 1 Then
                                        MessageBox.Show("Upload Gagal - Kode Dealer yang akan diupload tidak valid")
                                        Exit Sub
                                    End If
                                End If
                                objCA = New TrClassAllocation
                                objCA.TrClass = objC
                                objCA.Dealer = objD
                                objCA.Allocated = newAllocation

                                arlCAInserted.Add(objCA)
                            Next
                            'inserting data
                            Dim IsAnySucceed As Boolean = False
                            Dim arlDealerToDisplay As ArrayList = New ArrayList
                            'For Each objCAInserted As TrClassAllocation In arlCAInserted
                            '    Try
                            '        If objCAFac.Insert(objCAInserted) = -1 Then
                            '            strErrMessage = strErrMessage & IIf(strErrMessage.Trim = "", "", ", ") & objCAInserted.Dealer.DealerCode
                            '        Else
                            '            IsAnySucceed = True
                            '            arlDealerToDisplay.Add(objCAInserted.Dealer.DealerCode)
                            '        End If
                            '    Catch ex As Exception
                            '        strErrMessage = strErrMessage & IIf(strErrMessage.Trim = "", "", ", ") & objCAInserted.Dealer.DealerCode
                            '    End Try
                            'Next
                            If strErrMessage.Trim <> "" Then
                                If IsAnySucceed Then
                                    MessageBox.Show("Proses Upload gagal untuk Dealer : " & strErrMessage)
                                Else
                                    MessageBox.Show("Proses Upload Gagal:" & strErrMessage)
                                End If
                            Else
                                MessageBox.Show("Upload success")
                                'Show Data 
                                txtKodeDealer.Text = ""
                                For i = 0 To arlDealerToDisplay.Count - 1
                                    txtKodeDealer.Text = txtKodeDealer.Text & IIf(txtKodeDealer.Text.Trim = "", "", ";") & arlDealerToDisplay(i)
                                Next
                                btnSetAllocation_Click(sender, e)
                            End If
                        End If
                    Catch
                        Throw
                    Finally
                        sapImp.StopImpersonate()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub dtgUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim txtClassID As TextBox = CType(e.Item.FindControl("txtClassID"), TextBox)
            Dim txtDealerID As TextBox = CType(e.Item.FindControl("txtDealerID"), TextBox)
            Dim objCFac As TrClassFacade = New TrClassFacade(User)
            Dim objDFac As DealerFacade = New DealerFacade(User)
            Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
            Dim objC As TrClass
            Dim objD As Dealer
            Dim objCA As TrClassAllocation
            Dim arlCA As ArrayList = New ArrayList
            Dim strErrorUnregClass As String = ""
            Dim strErrorUnregDealer As String = ""
            Dim strErrorDuplicate As String = "" 'Duplicate Data
            Dim strErrorOverLimit As String = ""
            Dim strErrorNullAllocation As String = ""
            Dim lblClassCode As Label = e.Item.FindControl("lblClassCode")
            Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
            Dim lblDealerName As Label = e.Item.FindControl("lblDealerName")
            Dim lblCapacity As Label = e.Item.FindControl("lblKapasitas")
            Dim lblSelisih As Label = e.Item.FindControl("lblSelisih")
            Dim lblLastAllocated As Label = e.Item.FindControl("lblLastAllocated")
            Dim txtAllocated As TextBox = e.Item.FindControl("txtAllocated")
            Dim txtErrorFlag As TextBox = e.Item.FindControl("txtErrorFlag")
            Dim txtErrorOverLimit As TextBox = e.Item.FindControl("txtErrorOverLimit")
            Dim lblError As Label = e.Item.FindControl("lblError")
            Dim lblCity As Label = e.Item.FindControl("lblCity")
            Dim lblLastUpdateUpload As Label = e.Item.FindControl("lblLastUpdateUpload")
            Dim i As Integer = 0

            txtAllocated.Enabled = False

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1
            'objC = objCFac.Retrieve(CInt(txtClassID.Text))
            'objD = objDFac.Retrieve(CInt(txtDealerID.Text))
            objCA = CType(CType(Me.objSessionHelper.GetSession("arlCA"), ArrayList)(e.Item.ItemIndex), TrClassAllocation)
            objC = objCA.TrClass
            objD = objCA.Dealer
            lblLastUpdateUpload.Text = objCA.History

            'trClass Data
            If objC Is Nothing Then
                strErrorUnregClass = "Kelas tidak terdaftar"
                lblClassCode.Text = "xxx"
                lblCapacity.Text = "0"
                lblSelisih.Text = "0"
                lblLastAllocated.Text = "0"
            Else
                If objC.ID = 0 Then
                    If objC.ClassCode.Trim <> "" Then
                        strErrorUnregClass = "Kelas tidak sesuai dengan Kategori"
                    Else
                        strErrorUnregClass = "Kelas tidak terdaftar"
                    End If
                    lblClassCode.Text = objC.ClassCode ' "xxx"
                    lblCapacity.Text = "0"
                    lblSelisih.Text = "0"
                    lblLastAllocated.Text = "0"
                Else
                    lblClassCode.Text = objC.ClassCode
                    lblCapacity.Text = objC.Capacity
                    lblLastAllocated.Text = TotalClassAllocationPerDealer(objC.ID, objD.ID)  ' TotalClassAllocation(objC.ID)
                    Dim arlData As ArrayList = GetDataClassAllocation(objC.ID)
                    lblSelisih.Text = objC.Capacity - CType(arlData(2), Integer) ' TotalClassAllocation(objC.ID)

                    'Start  :Optimize Calculation
                    'Dim RemainOld As Integer = lblSelisih.Text
                    'Dim RemainNew As Integer = 0
                    'Dim idx As Integer = 0
                    'Dim TotLastAllocatedInDTG As Integer = 0
                    'Dim TotNewAllocatedInDTG As Integer = 0

                    'arlCA = Me.objSessionHelper.GetSession("arlCA")
                    'For idx = 0 To arlCA.Count - 1
                    '    If IsNothing(CType(arlCA(idx), TrClassAllocation).TrClass) OrElse CType(arlCA(idx), TrClassAllocation).TrClass.ID < 1 OrElse IsNothing(CType(arlCA(idx), TrClassAllocation).Dealer) Then
                    '    Else
                    '        TotLastAllocatedInDTG += TotalClassAllocationPerDealer(CType(arlCA(idx), TrClassAllocation).TrClass.ID, CType(arlCA(idx), TrClassAllocation).Dealer.ID)
                    '        TotNewAllocatedInDTG += CType(arlCA(idx), TrClassAllocation).Allocated
                    '    End If
                    'Next
                    'RemainNew = RemainOld + TotLastAllocatedInDTG '- TotNewAllocatedInDTG
                    'Dim TotalAllocatedTemp As Integer = 0
                    'For idx = 0 To arlCA.Count - 1
                    '    If idx <= e.Item.ItemIndex Then
                    '        TotalAllocatedTemp += CType(arlCA(idx), TrClassAllocation).Allocated
                    '    End If
                    'Next
                    'If TotalAllocatedTemp > RemainNew Then
                    '    strErrorOverLimit = "Jumlah alokasi melebihi sisa alokasi"
                    'End If
                    strErrorOverLimit = IIf(CType(arlData(4), Integer) = 1, "", "Jumlah alokasi melebihi sisa alokasi")
                    strErrorNullAllocation = IIf(CType(objCA.Allocated, Integer) = -1000, "Jumlah alokasi < 0", "")
                    'End    :Optimize Calculation
                End If
            End If
            'Dealer Data 
            If objD Is Nothing Then
                strErrorUnregDealer = "Dealer tidak terdaftar"
                lblDealerCode.Text = "" ' "xxx"
                lblDealerName.Text = "" '"xxx"
                lblCity.Text = "" '"xxx"
            Else
                If objD.ID = 0 Then
                    strErrorUnregDealer = "Dealer tidak terdaftar"
                    lblDealerCode.Text = objD.DealerCode ' "xxx"
                    lblDealerName.Text = "" ' "xxx"
                    lblCity.Text = "" ' "xxx"
                Else
                    lblDealerCode.Text = objD.DealerCode
                    lblDealerName.Text = objD.DealerName
                    lblCity.Text = objD.City.CityName
                End If
            End If
            'Check duplicate data 
            If e.Item.ItemIndex > 0 Then
                If Not IsNothing(objCA.TrClass) AndAlso Not IsNothing(objCA.Dealer) Then
                    Dim Idx As Integer = 0
                    For Each oCA As TrClassAllocation In CType(Me.objSessionHelper.GetSession("arlCA"), ArrayList)
                        If Idx < e.Item.ItemIndex AndAlso Not IsNothing(oCA.TrClass) AndAlso Not IsNothing(oCA.Dealer) AndAlso objCA.TrClass.ClassCode = oCA.TrClass.ClassCode AndAlso objCA.Dealer.DealerCode = oCA.Dealer.DealerCode Then
                            strErrorDuplicate = "Data duplicate"
                            e.Item.BackColor = System.Drawing.Color.Red
                        End If
                        Idx += 1
                    Next
                End If
                'For Each dgIt As DataGridItem In dtgUpload.Items
                '    If dgIt.ItemIndex < e.Item.ItemIndex Then
                '        Dim txtClassIDBef As TextBox = dgIt.FindControl("txtClassID")
                '        Dim txtDealerIDBef As TextBox = dgIt.FindControl("txtDealerID")
                '        If txtClassID.Text = txtClassIDBef.Text And txtDealerID.Text = txtDealerIDBef.Text Then
                '            strErrorDuplicate = "Data duplicate"
                '            e.Item.BackColor = System.Drawing.Color.Red
                '        End If
                '    End If
                'Next
            End If

            If strErrorUnregClass.Trim <> "" Or strErrorUnregDealer.Trim <> "" Or strErrorDuplicate.Trim <> "" Then
                lblError.Text = strErrorUnregClass
                lblError.Text = lblError.Text & IIf(lblError.Text.Trim = "", "", ";") & strErrorUnregDealer
                lblError.Text = lblError.Text & IIf(lblError.Text.Trim = "", "", ";") & strErrorDuplicate
                e.Item.BackColor = System.Drawing.Color.Red
                txtErrorFlag.Text = "1"
            Else
                txtErrorFlag.Text = "0"
            End If

            If strErrorOverLimit.Trim <> "" Then
                lblError.Text = lblError.Text & IIf(lblError.Text.Trim = "", "", ";") & strErrorOverLimit
                e.Item.BackColor = System.Drawing.Color.Red
                txtErrorOverLimit.Text = "1"
            Else
                txtErrorOverLimit.Text = "0"
            End If
            'strErrorNullAllocation
            If strErrorNullAllocation.Trim <> "" Then
                lblError.Text = lblError.Text & IIf(lblError.Text.Trim = "", "", ";") & strErrorNullAllocation
                e.Item.BackColor = System.Drawing.Color.Red
                txtErrorOverLimit.Text = "1"
            Else
                txtErrorOverLimit.Text = "0"
            End If


            If lblError.Text.Trim <> String.Empty Then
                Me.btnSimpan.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click

        Dim strErrorMsg As String = String.Empty
        If CheckDataForm(strErrorMsg) Then
            objSessionHelper.RemoveSession("arlAllocation")
            BindDataGrid(True)
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            btnDownload.Enabled = True
        Else
            MessageBox.Show(strErrorMsg)
        End If
    End Sub
#End Region


End Class
