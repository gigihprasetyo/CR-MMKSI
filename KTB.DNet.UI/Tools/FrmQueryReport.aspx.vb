#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade.Tools
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Collections.Generic

#End Region

Public Class FrmQueryReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlReport As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlInfo As System.Web.UI.WebControls.Panel
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCriterias As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgColumns As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ltrSQl As System.Web.UI.WebControls.Literal
    Protected WithEvents lbtnRefKode As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Declaration"
    Private sessHelper As New SessionHelper
    Private arlColumns As ArrayList
    Private arlCriteria As ArrayList
    Private listBCP As New List(Of BCPDynamicQuery)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Authorization()
        If Not IsPostBack Then
            Initialization()
        End If

    End Sub

    Private Sub ddlReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReport.SelectedIndexChanged
        If ddlReport.SelectedValue > 0 Then
            pnlInfo.Visible = True
            sessHelper.SetSession("ARLCRITERIA", Nothing)
            sessHelper.SetSession("ArlBCPDynamic", Nothing)
            BindGrid(0)
            BindCriteria()
        Else
            pnlInfo.Visible = False
        End If
    End Sub

    Sub dtgCriterias_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        If E.Item.ItemType = ListItemType.AlternatingItem Or E.Item.ItemType = ListItemType.Item Then
            Dim lblAndOr As Label = E.Item.FindControl("lblAndOr")
            If E.Item.ItemIndex = 0 Then
                lblAndOr.Text = String.Empty
            Else
                lblAndOr.Text = CType(E.Item.DataItem, CriteriaCondition).AndOr
            End If
        ElseIf E.Item.ItemType = ListItemType.Footer Then
            Dim ddlAndOr As DropDownList = E.Item.FindControl("ddlAndOrF")
            FillAndOr(ddlAndOr)
            If Not IsNothing(arlCriteria) Then
                If arlCriteria.Count = 0 Then
                    ddlAndOr.Visible = False
                Else
                    ddlAndOr.Visible = True
                End If
            End If
            Dim ddlColumn As DropDownList = E.Item.FindControl("ddlColumnF")
            FillColumn(ddlColumn)
            Dim ddlOperator As DropDownList = E.Item.FindControl("ddlOperatorF")
            FillOperator(ddlOperator)
        End If
    End Sub

    Private Sub FillAndOr(ByVal ddl As DropDownList)
        ddl.Items.Add("And")
        ddl.Items.Add("Or")
        ddl.SelectedIndex = 0
    End Sub

    Private Sub FillOperator(ByVal ddl As DropDownList)
        ddl.Items.Add("sama dengan") '0
        ddl.Items.Add("tidak sama dengan") '1
        ddl.Items.Add("sebagian sama dengan") '2
        ddl.Items.Add("diawali dengan") '3
        ddl.Items.Add("diakhiri dengan") '4
        ddl.Items.Add("lebih besar dari") '5
        ddl.Items.Add("lebih kecil dari") '6
        ddl.Items.Add("lebih besar / sama dengan") '7
        ddl.Items.Add("lebih kecil / sama dengan") '8
        ddl.Items.Add("terdiri dari") '9
        ddl.Items.Add("tidak terdiri dari") '10

        ddl.SelectedIndex = 0
    End Sub

    Private Sub FillColumn(ByVal ddl As DropDownList)
        arlColumns = CType(sessHelper.GetSession("ARLCOLUMNS"), ArrayList)
        If arlColumns.Count > 0 Then

            If sessHelper.GetSession("ArlBCPDynamic") IsNot Nothing Then
                For Each item As BCPDynamicQuery In arlColumns
                    ddl.Items.Add(item.FieldNameInAlias)
                Next
            Else
                For Each item As VwBCPColumns In arlColumns
                    ddl.Items.Add(item.ColoumnName)
                Next
            End If

        End If
    End Sub

    Sub dtgCriterias_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "Delete"
                DeleteCriteria(e)
            Case "Add"
                AddCriteria(e)
        End Select
    End Sub

    Sub dtgCriterias_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        'objPkHeader = SessionHelper.GetSession("PK")
        'dtgCriterias.ShowFooter = False
        'dtgCriterias.EditItemIndex = CInt(e.Item.ItemIndex)
        'BindDetailToGrid()
    End Sub

    Sub dtgCriterias_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        'dtgCriterias.EditItemIndex = -1
        'BindDetailToGrid()
        'dtgCriterias.ShowFooter = True
    End Sub

    Sub dtgCriterias_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        'If Not Page.IsValid Then
        '    Return
        'End If
        'UpdateCommand(E)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim strQuery As String = String.Empty
        Dim strWhere As String = String.Empty
        Dim strOrder As String = String.Empty
        Dim strHeaderColumns As String = String.Empty
        Dim strQry As String = String.Empty

        If ddlReport.SelectedValue <> 0 Then
            If IsAuthorized() Then
                Dim oBCPQueryFacade As BCPQueryFacade = New BCPQueryFacade(User)
                Dim objBPCQuery As BCPQuery = oBCPQueryFacade.Retrieve(CType(ddlReport.SelectedValue, Integer))

                If String.IsNullOrEmpty(objBPCQuery.SPName) Then
                    strQuery = CreateStrQuery(objBPCQuery.ViewName.Trim)
                    strWhere = CreateStrWhere()

                    ltrSQl.Text = strQuery & " " & strWhere
                    Dim objReportList As ArrayList = oBCPQueryFacade.RetrieveFromSP(strQuery, strWhere, "", objBPCQuery.FlName)
                    If objReportList.Count > 0 Then
                        Dim objReport As BCPQuery = CType(objReportList(0), BCPQuery)
                        If objReport.FlName <> String.Empty Then
                            DownloadFile(objReport.FlName)
                        End If
                    End If
                Else
                    Dim arl As New ArrayList
                    Dim nChecked As Integer = 0
                    Dim strColumns As String


                    For Each item As DataGridItem In dtgColumns.Items
                        Dim chk As CheckBox = CType(item.FindControl("cbCheck"), CheckBox)
                        If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                            If chk.Checked Then
                                nChecked = nChecked + 1
                            End If
                        End If
                    Next

                    arl = CommonFunction.GetDynamicQuery(CType(ddlReport.SelectedValue, Integer))

                    For Each row As BCPDynamicQuery In arl
                        For Each item As DataGridItem In dtgColumns.Items
                            Dim chk As CheckBox = CType(item.FindControl("cbCheck"), CheckBox)
                            Dim lblColumnName As Label = CType(item.FindControl("lblColumnName"), Label)
                            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                                If chk.Checked Then

                                    Dim lblColumn As Label = item.FindControl("lblColumnName")

                                    If row.FieldNameInAlias = lblColumnName.Text Then
                                        strHeaderColumns &= "''" & row.FieldNameInAlias & "'',"

                                        If nChecked > 1 Then
                                            strColumns = strColumns + row.ConvertFieldName + ", "
                                        Else
                                            strColumns = strColumns + row.ConvertFieldName
                                        End If
                                        nChecked = nChecked - 1
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    Next



                    'strQuery = strQuery + " " + strColumns + oBCPQueryFacade.RetrieveFromSP(objBPCQuery.SPName)
                    strQuery = CType(arl.Item(0), BCPDynamicQuery).DefaultWhereClause

                    strWhere = CreateStrParameter(arl)

                    strQry = "SELECT " & strHeaderColumns.Substring(0, strHeaderColumns.Length - 1)
                    strQry &= " UNION ALL "
                    strQry &= "SELECT " & strColumns & " " & strQuery & "  "

                    ltrSQl.Text = strQuery & " " & strWhere
                    Dim objReportList As ArrayList = oBCPQueryFacade.RetrieveFromSP_V3(strQry, strWhere, "", objBPCQuery.FlName)
                    If objReportList.Count > 0 Then
                        Dim objReport As BCPQuery = CType(objReportList(0), BCPQuery)
                        If objReport.FlName <> String.Empty Then
                            DownloadFile(objReport.FlName)
                        End If
                    End If



                End If

            End If
        End If
    End Sub

    'Private Function IsValidRange(ByVal obj As BCPQuery) As Boolean
    '    Dim vReturn As Boolean = True
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VwBCPColumns), "ViewName", MatchType.Exact, obj.ViewName))
    '    Dim arlColumns As ArrayList = New VwBCPColumnsFacade(User).Retrieve(criterias)
    '    Dim iCount As Integer = 0
    '    Dim strColumn As String = String.Empty
    '    For Each item As VwBCPColumns In arlColumns
    '        If item.ColumnType = 61 Then
    '            If strColumn <> String.Empty AndAlso item.ColoumnName = strColumn Then

    '            End If
    '            strColumn = item.ColoumnName
    '            iCount += 1
    '        End If
    '    Next
    '    arlCriteria = CType(sessHelper.GetSession("ARLCRITERIA"), ArrayList)
    '    If (iCount Mod 2 = 0) Then
    '        For Each item As CriteriaCondition In arlCriteria

    '        Next
    '    Else

    '    End If
    '    Return vReturn
    'End Function

    Private Function CreateStrQuery(ByVal viewName As String) As String
        Dim nChecked As Integer = 0
        Dim strQry As String = String.Empty
        Dim strHeaderColumns As String = String.Empty
        Dim strColumns As String = String.Empty
        For Each item As DataGridItem In dtgColumns.Items
            Dim chk As CheckBox = CType(item.FindControl("cbCheck"), CheckBox)
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                If chk.Checked Then
                    Dim lblColumn As Label = item.FindControl("lblColumnName")
                    strHeaderColumns &= "''" & lblColumn.Text & "'',"
                    strColumns &= "[" & lblColumn.Text & "],"
                    nChecked += 1
                End If
            End If
        Next
        If nChecked = 0 Then
            For Each item As DataGridItem In dtgColumns.Items
                Dim chk As CheckBox = CType(item.FindControl("cbCheck"), CheckBox)
                If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                    Dim lblColumn As Label = item.FindControl("lblColumnName")
                    strHeaderColumns &= "''" & lblColumn.Text & "'',"
                    strColumns &= "[" & lblColumn.Text & "],"
                End If
            Next
        End If

        strQry = "SELECT " & strHeaderColumns.Substring(0, strHeaderColumns.Length - 1)
        strQry &= " UNION ALL "
        strQry &= "SELECT " & strColumns.Substring(0, strColumns.Length - 1) & " FROM dbo." & viewName
        Return strQry
    End Function

    Private Function CreateStrQuerySP(ByRef nChecked As Integer) As String
        Dim strQry As String = String.Empty
        Dim strHeaderColumns As String = String.Empty
        Dim strColumns As String = String.Empty
        Dim commfunc As New CommonFunction
        Dim strValueCode As String

        For Each item As DataGridItem In dtgColumns.Items
            Dim chk As CheckBox = CType(item.FindControl("cbCheck"), CheckBox)
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                If chk.Checked Then
                    nChecked = nChecked + 1
                End If
            End If
        Next

        For Each item As DataGridItem In dtgColumns.Items
            Dim chk As CheckBox = CType(item.FindControl("cbCheck"), CheckBox)
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                If chk.Checked Then

                    Dim lblColumn As Label = item.FindControl("lblColumnName")
                    strHeaderColumns &= "''" & lblColumn.Text & "'',"
                    strValueCode = commfunc.GetEnumValueCode(lblColumn.Text, "EnumMapperBCP_SPBCP_VehicleHandoverDate")

                End If
            End If
        Next


        strQry = "SELECT " & strHeaderColumns.Substring(0, strHeaderColumns.Length - 1)
        strQry &= " UNION ALL "
        strQry &= "SELECT " & strColumns
        Return strQry
    End Function

    Private Function CreateStrWhere() As String
        Dim str As String = String.Empty
        Dim opr As String = String.Empty
        Dim iCount As Integer = 0

        arlCriteria = CType(sessHelper.GetSession("ARLCRITERIA"), ArrayList)
        If Not IsNothing(arlCriteria) Then
            For iCount = 0 To arlCriteria.Count - 1
                Dim crit As CriteriaCondition = CType(arlCriteria(iCount), CriteriaCondition)
                str &= crit.AndOr & "  [" & crit.Column & "] "
                Select Case crit.[Operator]
                    Case "sama dengan"
                        opr = " = ''" & crit.ValueCondition & "''"
                    Case "tidak sama dengan"
                        opr = " <> ''" & crit.ValueCondition & "''"
                    Case "sebagian sama dengan"
                        opr = " like ''%" & crit.ValueCondition & "%''"
                    Case "diawali dengan"
                        opr = " like ''" & crit.ValueCondition & "%''"
                    Case "diakhiri dengan"
                        opr = " like ''%" & crit.ValueCondition & "''"
                    Case "lebih besar dari"
                        opr = " > ''" & crit.ValueCondition & "''"
                    Case "lebih kecil dari"
                        opr = " < ''" & crit.ValueCondition & "''"
                    Case "lebih besar / sama dengan"
                        opr = " >= ''" & crit.ValueCondition & "''"
                    Case "lebih kecil / sama dengan"
                        opr = " <> ''" & crit.ValueCondition & "''"
                    Case "terdiri dari"
                        Dim strValue As String = GetStringValue(crit.ValueCondition)
                        opr = " IN (''" & strValue & ")"
                    Case "tidak terdiri dari"
                        Dim strValue As String = GetStringValue(crit.ValueCondition)
                        opr = " NOT IN (''" & strValue & ")"
                End Select
                str = str & opr
                opr = ""
            Next

            If iCount > 0 Then
                str = " WHERE " & str.Substring(4, str.Length - 4)
            Else
                str = ""
            End If
        End If

        Return str
    End Function

    Private Function CreateStrParameter(ByVal arl As ArrayList) As String
        Dim str As String = String.Empty
        Dim opr As String = String.Empty
        Dim iCount As Integer = 0
        Dim criBCP As CriteriaComposite
        Dim strCriCol As String = ""
        Dim arlbcp As New ArrayList

        arlCriteria = CType(sessHelper.GetSession("ARLCRITERIA"), ArrayList)
        If Not IsNothing(arlCriteria) Then
            For iCount = 0 To arlCriteria.Count - 1
                Dim crit As CriteriaCondition = CType(arlCriteria(iCount), CriteriaCondition)

                criBCP = New CriteriaComposite(New Criteria(GetType(BCPDynamicQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criBCP.opAnd(New Criteria(GetType(BCPDynamicQuery), "FieldNameInAlias", MatchType.Exact, crit.Column))

                arlbcp = New BCPDynamicQueryFacade(User).Retrieve(criBCP)
                strCriCol = CType(arlbcp.Item(0), BCPDynamicQuery).OriginalFieldName
                'strCriCol = commFunc.GetEnumValueCode(crit.Column, "EnumMapperBCP_SPBCP_VehicleHandoverDate_where")

                str &= crit.AndOr & " " & strCriCol

                Select Case crit.[Operator]
                    Case "sama dengan"
                        opr = " = ''" & crit.ValueCondition & "''"
                    Case "tidak sama dengan"
                        opr = " <> ''" & crit.ValueCondition & "''"
                    Case "sebagian sama dengan"
                        opr = " like ''%" & crit.ValueCondition & "%''"
                    Case "diawali dengan"
                        opr = " like ''" & crit.ValueCondition & "%''"
                    Case "diakhiri dengan"
                        opr = " like ''%" & crit.ValueCondition & "''"
                    Case "lebih besar dari"
                        opr = " > ''" & crit.ValueCondition & "''"
                    Case "lebih kecil dari"
                        opr = " < ''" & crit.ValueCondition & "''"
                    Case "lebih besar / sama dengan"
                        opr = " >= ''" & crit.ValueCondition & "''"
                    Case "lebih kecil / sama dengan"
                        opr = " <> ''" & crit.ValueCondition & "''"
                    Case "terdiri dari"
                        Dim strValue As String = GetStringValue(crit.ValueCondition)
                        opr = " IN (''" & strValue & ")"
                    Case "tidak terdiri dari"
                        Dim strValue As String = GetStringValue(crit.ValueCondition)
                        opr = " NOT IN (''" & strValue & ")"
                End Select
                str = str & opr
                opr = ""
            Next

            If iCount > 0 Then
                'str =  str.Substring(4, str.Length - 4)
            Else
                'str = ""
            End If
        End If

        Return str
    End Function

    Private Function GetStringValue(ByVal strCondition As String) As String
        Dim strValue As String = String.Empty
        If strCondition.Split(",").Length > 1 Then
            For i As Integer = 0 To strCondition.Split(",").Length - 1
                If i = 0 Then
                    strValue &= strCondition.Split(",")(i) & "''"
                ElseIf (i > 0) And (i <= strCondition.Split(",").Length - 1) Then
                    strValue &= ",''" & strCondition.Split(",")(i) & "''"
                End If
            Next
        Else
            strValue = strCondition
        End If
        Return strValue
    End Function

    Private Sub AddCriteria(ByVal e As DataGridCommandEventArgs)
        arlCriteria = CType(sessHelper.GetSession("ARLCRITERIA"), ArrayList)
        If IsNothing(arlCriteria) Then
            arlCriteria = New ArrayList
        End If
        Dim ddlAndOr As DropDownList = e.Item.FindControl("ddlAndOrF")
        Dim ddlColumn As DropDownList = e.Item.FindControl("ddlColumnF")
        Dim ddlOperator As DropDownList = e.Item.FindControl("ddlOperatorF")
        Dim txtValue As TextBox = e.Item.FindControl("txtValueF")

        If txtValue.Text.Trim = String.Empty Then
            MessageBox.Show("Nilai tidak boleh kosong")
            Exit Sub
        End If

        Dim strValue As String = String.Empty
        Dim strValueCondition As String = String.Empty
        txtValue.Text = txtValue.Text.Replace("'", "")
        If txtValue.Text.Split(",").Length > 1 Then
            For i As Integer = 0 To txtValue.Text.Split(",").Length - 1
                If i = 0 Then
                    strValue &= txtValue.Text.Split(",")(i)
                    'strValueCondition &= "''" & txtValue.Text.Split(",")(i) & "''"
                    strValueCondition &= txtValue.Text.Split(",")(i)
                ElseIf (i > 0) And (i <= txtValue.Text.Split(",").Length - 1) Then
                    strValue &= "," & txtValue.Text.Split(",")(i)
                    strValueCondition &= "," & txtValue.Text.Split(",")(i)
                End If
            Next
        Else
            strValue = txtValue.Text
            strValueCondition = txtValue.Text
        End If

        Dim crit As New CriteriaCondition
        crit.AndOr = ddlAndOr.SelectedItem.Text
        crit.Column = ddlColumn.SelectedItem.Text
        crit.[Operator] = ddlOperator.SelectedItem.Text
        crit.Value = strValue
        crit.ValueCondition = strValueCondition

        arlCriteria.Add(crit)

        sessHelper.SetSession("ARLCRITERIA", arlCriteria)
        BindCriteria()


    End Sub

    Private Function CriteriaValidation(ByVal crit As CriteriaCondition) As Boolean
        Dim vReturn As Boolean = False
        Dim msg As String = String.Empty
        Select Case crit.[Operator]
            Case "Exact"
                If crit.Value.Split(",").Length > 1 Then
                    msg = "Operator Exact ( = ), tidak boleh ada tanda koma ( , )"
                End If
            Case "No"
                msg = " <> " & crit.Value
            Case "Partial"
                msg = " like %" & crit.Value & "%"
            Case "Starts With"
                msg = " like " & crit.Value & "%"
            Case "Ends With"
                msg = " like %" & crit.Value
            Case "Greater"
                msg = " > " & crit.Value
            Case "Lesser"
                msg = " < " & crit.Value
            Case "Is Null"
                msg = " IS NULL "
            Case "Is Not Null"
                msg = " IS NOT NULL "
            Case "Greater Or Equal"
                msg = " >= " & crit.Value
            Case "Lesser Or Equal"
                msg = " <> " & crit.Value
            Case "In Set"
                msg = " IN (" & crit.Value & ")"
            Case "Not In Set"
                msg = " NOT IN (" & crit.Value & ")"
        End Select
        If msg.Length > 0 Then
            MessageBox.Show(msg)
        Else
            vReturn = True
        End If
        Return vReturn
    End Function

    Private Sub DeleteCriteria(ByVal e As DataGridCommandEventArgs)
        arlCriteria = CType(sessHelper.GetSession("ARLCRITERIA"), ArrayList)
        If Not IsNothing(arlCriteria) Then
            arlCriteria.RemoveAt(e.Item.ItemIndex)
        End If
        sessHelper.SetSession("ARLCRITERIA", arlCriteria)
        BindCriteria()
    End Sub

    Private Sub BindGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim objQuery As BCPQuery = New BCPQueryFacade(User).Retrieve(CType(ddlReport.SelectedValue, Integer))
        If Not IsNothing(objQuery) Then

            If Not String.IsNullOrEmpty(objQuery.SPName) Then
                Dim criBCP As New CriteriaComposite(New Criteria(GetType(BCPDynamicQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criBCP.opAnd(New Criteria(GetType(BCPDynamicQuery), "BCPQueryID", MatchType.Exact, objQuery.ID))
                arlColumns = New BCPDynamicQueryFacade(User).Retrieve(criBCP)

                For Each row As BCPDynamicQuery In arlColumns
                    listBCP.Add(row)
                Next

                sessHelper.SetSession("ArlBCPDynamic", arlColumns)
                sessHelper.SetSession("ARLCOLUMNS", arlColumns)

                dtgColumns.CurrentPageIndex = idxPage
                If arlColumns.Count > 0 Then
                    dtgColumns.DataSource = arlColumns
                Else
                    dtgColumns.DataSource = New ArrayList
                    MessageBox.Show("Data tidak ditemukan")
                End If

                dtgColumns.VirtualItemCount = totalRow
                dtgColumns.DataBind()

            Else

                arlColumns = New VwBCPColumnsFacade(User).RetrieveList(objQuery.ViewName.Trim)

                sessHelper.SetSession("ARLCOLUMNS", arlColumns)
                dtgColumns.CurrentPageIndex = idxPage
                If arlColumns.Count > 0 Then
                    dtgColumns.DataSource = arlColumns
                Else
                    dtgColumns.DataSource = New ArrayList
                    MessageBox.Show("Data tidak ditemukan")
                End If

                dtgColumns.VirtualItemCount = totalRow
                dtgColumns.DataBind()
            End If
        Else
            dtgColumns.DataSource = New ArrayList
            MessageBox.Show("Data tidak ditemukan")

        End If

    End Sub

    Protected Sub dtgColumns_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgColumns.ItemDataBound
        Dim objSPBilling As SparePartBilling
        If listBCP.Count > 0 Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If Not e.Item.DataItem Is Nothing Then
                    Dim RowValue As BCPDynamicQuery = CType(e.Item.DataItem, BCPDynamicQuery)
                    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                        Dim lblColumnName As Label = e.Item.FindControl("lblColumnName")
                        lblColumnName.Text = RowValue.FieldNameInAlias
                    End If
                End If
            End If
        Else
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If Not e.Item.DataItem Is Nothing Then
                    Dim RowValue As VwBCPColumns = CType(e.Item.DataItem, VwBCPColumns)
                    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                        Dim lblColumnName As Label = e.Item.FindControl("lblColumnName")
                        lblColumnName.Text = RowValue.ColoumnName
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BindCriteria()

        arlCriteria = CType(sessHelper.GetSession("ARLCRITERIA"), ArrayList)
        If IsNothing(arlCriteria) Then
            arlCriteria = New ArrayList
        End If

        dtgCriterias.DataSource = arlCriteria
        dtgCriterias.DataBind()

        dtgCriterias.ShowFooter = True
    End Sub

    Private Sub Authorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.Cc_akses_download_report_privilege) Then
        '    Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        'End If
        'btnSave.Enabled = SecurityProvider.Authorize(Context.Usexr, SR.cc_akses_download_report_sales_privilege) Or SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_aftersales_privilege)
    End Sub

    Private Sub Initialization()
        GetReportList()
        pnlInfo.Visible = False
        arlCriteria = New ArrayList
    End Sub

    Private Function IsAuthorized() As Boolean
        Dim oLoginUser As UserInfo
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        'KTB or Dealer Report
        Dim objQuery As BCPQuery = New BCPQueryFacade(User).Retrieve(CInt(ddlReport.SelectedValue))
        If oLoginUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If objQuery.IsKTB = 0 Then
                MessageBox.Show("Anda tidak berhak untuk download report ini.")
                Return False
                Exit Function
            End If
        Else
            If objQuery.IsDealer = 0 Then
                MessageBox.Show("Anda tidak berhak untuk download report ini.")
                Return False
                Exit Function
            End If
        End If


        'Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critCol.opAnd(New Criteria(GetType(UserRole), "UserInfo.ID", MatchType.Exact, oLoginUser.ID))

        'Dim sortCol As SortCollection = New SortCollection
        'sortCol.Add(New Sort(GetType(UserRole), "ID", Sort.SortDirection.ASC))
        'Dim arlUserRole As ArrayList = New KTB.DNet.BusinessFacade.UserManagement.UserRoleFacade(User).Retrieve(critCol, sortCol)
        'If arlUserRole.Count > 0 Then
        '    Dim strID As String = ""
        '    For Each item As UserRole In arlUserRole
        '        If strID <> "" Then
        '            strID = item.ID & "," & strID
        '        Else
        '            strID = item.ID
        '        End If
        '    Next
        '    If strID.Length > 0 Then

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPRoles), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crits.opAnd(New Criteria(GetType(BCPRoles), "UserRole.ID", MatchType.InSet, "(" & strID.ToString & ")"))
        crits.opAnd(New Criteria(GetType(BCPRoles), "UserRole.ID", MatchType.InSet, "( select ID from UserRole where UserID = " & oLoginUser.ID & ")"))
        '
        crits.opAnd(New Criteria(GetType(BCPRoles), "BCPQuery.ID", MatchType.Exact, CInt(ddlReport.SelectedValue)))
        Dim arlBCPRoles As ArrayList = New KTB.DNet.BusinessFacade.Tools.BCPRolesFacade(User).Retrieve(crits)
        If arlBCPRoles.Count > 0 Then
            Return True
        Else
            MessageBox.Show("Anda tidak berhak untuk download report. Hubungi Administrator")
            Return False
        End If
        '    Else
        '        MessageBox.Show("Anda tidak berhak untuk download report. Hubungi Administrator")
        '        Return False
        '    End If
        'Else
        '    MessageBox.Show("Anda tidak berhak untuk download report. Hubungi Administrator")
        '    Return False
        'End If

    End Function

    Private Sub GetReportList()
        ddlReport.Items.Clear()
        ddlReport.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(BCPQuery), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New BCPQueryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objReport As BCPQuery In objReportList
            li = New ListItem(objReport.Title, objReport.ID)
            ddlReport.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlReport.Items.Insert(0, li)

    End Sub

    Private Sub DownloadFile(ByVal fileName As String)

        Dim path As System.IO.Path
        Dim fullpath As String = KTB.DNet.Lib.WebConfig.GetValue("DL_Folder")

        fullpath = fullpath & fileName
        Dim name = path.GetFileName(fullpath)
        Dim ext = path.GetExtension(fullpath)
        Dim type As String = ""
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("DL_User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("DL_Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("DL_Server")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            Dim FileCheck As FileInfo = New FileInfo(fullpath)
            If FileCheck.Exists = True Then
                'Select Case ext
                '    'Case ".htm", ".html"
                '    '    type = "text/HTML"
                '    'Case ".txt"
                '    '    type = "text/plain"
                '    'Case ".doc", ".rtf"
                '    '    type = "Application/msword"
                'Case ".csv", ".xls"
                '        type = "Application/x-msexcel"
                '        'Case ".exe"
                '        'MessageBox.Show("File bermasalah tidak dapat di download")
                '        'Exit Sub
                '    Case Else
                '        type = "application/x-download"
                'End Select
                'Response.AppendHeader("content-disposition", "attachment; filename=" + name)
                'If type <> "" Then
                '    Response.ContentType = type
                'End If

                'Response.WriteFile(fullpath)
                'Response.End()
                Try
                    Response.Redirect("../Download.aspx?file=" & fullpath)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(fileName.ToString))
                End Try
                imp.StopImpersonate()
                imp = Nothing
            Else
                MessageBox.Show("File tidak tersedia")
            End If
        Else
            MessageBox.Show("File tidak dapat diakses")
        End If
    End Sub

    Private Class CriteriaCondition
        Dim _andOr As String = String.Empty
        Dim _column As String = String.Empty
        Dim _operator As String = String.Empty
        Dim _value As String = String.Empty
        Dim _valueCondition As String = String.Empty

        Property AndOr() As String
            Get
                Return _andOr
            End Get
            Set(ByVal value As String)
                _andOr = value
            End Set
        End Property
        Property Column() As String
            Get
                Return _column
            End Get
            Set(ByVal value As String)
                _column = value
            End Set
        End Property
        Property [Operator]() As String
            Get
                Return _operator
            End Get
            Set(ByVal value As String)
                _operator = value
            End Set
        End Property
        Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property
        Property ValueCondition() As String
            Get
                Return _valueCondition
            End Get
            Set(ByVal value As String)
                _valueCondition = value
            End Set
        End Property
    End Class


End Class



