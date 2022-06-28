Imports System.Collections.Specialized
Imports System.Object
Imports System.Text

Public Class NavigationHelper
    ' Methods
    Public Shared Function BuildMenuDataTableStructure() As DataTable
        Dim table As New DataTable("Menu")
        table.Columns.Add("AppMenuId", Type.GetType("System.Int32"))
        table.Columns.Add("MenuName", Type.GetType("System.String"))
        table.Columns.Add("ScreenId", Type.GetType("System.Int32"))
        table.Columns.Add("ParentMenuId", Type.GetType("System.Int32"))
        table.Columns.Add("SortOrder", Type.GetType("System.Int32"))
        table.Columns.Add("NavigateUrl", Type.GetType("System.String"))
        table.Columns.Add("QueryString", Type.GetType("System.String"))
        table.Columns.Add("GroupId", Type.GetType("System.Int32"))
        Return table
    End Function

    Private Sub FindParentMenu(ByVal selRow As DataRow, ByVal dtMenu As DataTable)
        If ((Not selRow.Item("ParentMenuId") Is DBNull.Value) AndAlso (CInt(selRow.Item("ParentMenuId")) <> 0)) Then
            Dim rowArray As DataRow() = dtMenu.Select(("AppMenuId=" & selRow.Item("ParentMenuId").ToString))
            If (rowArray.Length > 0) Then
                Dim row As DataRow = DirectCast(rowArray.GetValue(0), DataRow)
                row.Item("SelectedParent") = 1
                Me.FindParentMenu(row, dtMenu)
            End If
        End If
    End Sub

    Public Function GetMenuDataSource(ByVal selectedScreenId As Integer, ByVal queryParams As NameValueCollection, ByVal dt As DataTable, ByVal applicationRootPath As String) As DataTable
        Dim dtMenu As DataTable = NavigationHelper.BuildMenuDataTableStructure.Clone
        dtMenu.Columns.Add("ResolvedUrl", Type.GetType("System.String"))
        dtMenu.Columns.Add("Selected", Type.GetType("System.Int32"))
        dtMenu.Columns.Add("SelectedParent", Type.GetType("System.Int32"))
        If (selectedScreenId > 0) Then
            Dim rowArray As DataRow() = dt.Select(("ScreenId = " & selectedScreenId))
            If ((Not rowArray Is Nothing) AndAlso (rowArray.Length > 0)) Then
                Dim num As Integer = IIf((Not rowArray(0).Item("GroupId") Is DBNull.Value), CInt(rowArray(0).Item("GroupId")), 0)
                Dim row As DataRow
                For Each row In dt.Select(("GroupId = " & num), "SortOrder")
                    Dim row2 As DataRow = dtMenu.NewRow
                    row2.Item("AppMenuId") = row.Item("AppMenuId")
                    row2.Item("MenuName") = row.Item("MenuName")
                    row2.Item("ScreenId") = row.Item("ScreenId")
                    row2.Item("ParentMenuId") = row.Item("ParentMenuId")
                    row2.Item("SortOrder") = row.Item("SortOrder")
                    row2.Item("NavigateUrl") = row.Item("NavigateUrl")
                    row2.Item("QueryString") = row.Item("QueryString")
                    row2.Item("GroupId") = row.Item("GroupId")
                    Dim RSUrl As String = String.Concat(New Object() {applicationRootPath, "/", row.Item("NavigateUrl"), "?", Me.ResolveQueryParams(CInt(row.Item("ScreenId")), row.Item("QueryString").ToString, queryParams)})
                    RSUrl = RSUrl.Replace("://", ":////")
                    RSUrl = RSUrl.Replace("//", "/")
                    row2.Item("ResolvedUrl") = RSUrl
                    If (CInt(row.Item("ScreenId")) = selectedScreenId) Then
                        row2.Item("Selected") = 1
                    Else
                        row2.Item("Selected") = 0
                    End If
                    row2.Item("SelectedParent") = 0
                    dtMenu.Rows.Add(row2)
                Next
            End If
            Me.UpdateSelectedMenu(dtMenu)
        End If
        Return dtMenu
    End Function

    Private Function ResolveQueryParams(ByVal screenId As Integer, ByVal QueryString As String, ByVal queryParams As NameValueCollection) As String
        Dim str As String = ("screenid=" & screenId)
        If ((Not QueryString Is Nothing) AndAlso (QueryString.Trim <> "")) Then
            Try
                Dim str2 As String
                For Each str2 In QueryString.Split(New Char() {"&"c})
                    Dim strArray2 As String() = str2.Split(New Char() {"="c})
                    If ((strArray2.Length = 2) AndAlso (strArray2(1).Trim.IndexOf("@") >= 0)) Then
                        Try
                            Dim str3 As String = strArray2(1).Trim.Remove(0, 1)
                            Dim newValue As String = IIf((Not queryParams.Item(str3) Is Nothing), queryParams.Item(str3).ToString.Trim, "")
                            str = (str & "&" & str2.Replace(strArray2(1), newValue))
                        Catch exception1 As Exception
                        End Try
                    End If
                Next
            Catch exception As Exception
                Throw exception
            End Try
        End If
        If str.StartsWith("&") Then
            str = str.Remove(0, 1)
        End If
        Return str
    End Function

    Private Sub UpdateSelectedMenu(ByVal dtMenu As DataTable)
        Dim rowArray As DataRow() = dtMenu.Select("Selected=1")
        If (rowArray.Length > 0) Then
            Dim selRow As DataRow = DirectCast(rowArray.GetValue(0), DataRow)
            Me.FindParentMenu(selRow, dtMenu)
        End If
    End Sub

    

End Class
