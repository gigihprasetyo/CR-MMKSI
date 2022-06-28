Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Principal
Imports System.Runtime.CompilerServices
Imports OfficeOpenXml
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Drawing

Public Module GlobalExtensions

    ''' <summary>
    ''' Create By Moh ridwan
    ''' Untuk mengambil value(string) cell dari excel dengan parameter row dan column
    ''' </summary>
    ''' <param name="ws"></param>
    ''' <param name="row"></param>
    ''' <param name="column"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function GetCellValue(ByVal ws As ExcelWorksheet, ByVal row As Integer, ByVal column As Integer) As String
        If ws.Cells(row, column).Value IsNot Nothing Then
            Return ws.Cells(row, column).Value.ToString()
        End If

        Return String.Empty
    End Function

    ''' <summary>
    ''' Create By Moh Ridwan
    ''' Format String dd-MM-yyyy Convert To DateTime
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function StringCellToDateTime(ByVal str As String) As DateTime
        Try
            Dim arrDate() As String = str.Split("-")
            Return New DateTime(Integer.Parse(
                                arrDate(2)), _
                                Integer.Parse(arrDate(1)), _
                                Integer.Parse(arrDate(0))
                                )
        Catch ex As Exception
            Return DateTime.MinValue
        End Try
    End Function

    ''' <summary>
    ''' Create By Moh Ridwan
    ''' Membuat Control Menjadi read Only
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub ModeReadOnly(ByVal ctrl As Control)
        If ctrl.HasControls Then
            For Each childCtrl As Control In ctrl.Controls
                ModeReadOnly(childCtrl)
            Next
        Else
            If TypeOf ctrl Is System.Web.UI.WebControls.TextBox Then
                DirectCast(ctrl, System.Web.UI.WebControls.TextBox).ReadOnly = True
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.DropDownList Then
                DirectCast(ctrl, System.Web.UI.WebControls.DropDownList).Enabled = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.CheckBox Then
                DirectCast(ctrl, System.Web.UI.WebControls.CheckBox).Enabled = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.Label Then
                Dim lblform As System.Web.UI.WebControls.Label = DirectCast(ctrl, System.Web.UI.WebControls.Label)
                If lblform.HasAttributes Then
                    lblform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlImage Then
                Dim imgform As System.Web.UI.HtmlControls.HtmlImage = DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlImage)
                If imgform.Attributes.Count > 0 Then
                    imgform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.FileUpload Then
                DirectCast(ctrl, System.Web.UI.WebControls.FileUpload).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlInputFile Then
                DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlInputFile).Disabled = True
            ElseIf TypeOf ctrl Is KTB.DNet.WebCC.IntiCalendar Then
                DirectCast(ctrl, KTB.DNet.WebCC.IntiCalendar).Enabled = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.LinkButton Then
                DirectCast(ctrl, System.Web.UI.WebControls.LinkButton).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.ImageButton Then
                DirectCast(ctrl, System.Web.UI.WebControls.ImageButton).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.Button Then
                Dim btn As System.Web.UI.WebControls.Button = CType(ctrl, System.Web.UI.WebControls.Button)
                If Not btn.Text.ToLower.Contains("kembali") Then
                    btn.Visible = False
                End If

            End If
        End If
    End Sub

    <Extension()>
    Public Function IsEmpty(ByVal textBox As TextBox) As Boolean
        If String.IsNullOrEmpty(textBox.Text.Trim) Then
            Return True
        End If
        Return False
    End Function

    <Extension()>
    Public Sub BindChkList(ByVal chkList As CheckBoxList, ByVal objSource As Object, _
                          Optional ByVal valueField As String = "ID", Optional ByVal textField As String = "Description")
        Try
            chkList.ClearSelection()
            chkList.Items.Clear()
            chkList.DataSource = objSource
            chkList.DataValueField = valueField
            chkList.DataTextField = textField
            chkList.DataBind()
        Catch
        End Try
    End Sub

    <Extension()>
    Public Function GetSelectedValue(ByVal chkList As CheckBoxList) As String
        Dim result As String = ""
        For Each iTem As ListItem In chkList.Items
            If iTem.Selected Then
                result = result + iTem.Value.ToString + ", "
            End If
        Next
        If Not String.IsNullorEmpty(result) Then
            result = result.Remove(result.Length - 2)
            result = String.Format("({0})", result)
        End If
        Return result
    End Function

    <Extension()>
    Public Function IsNotEmpty(ByVal textBox As TextBox) As Boolean
        Return Not textBox.IsEmpty()
    End Function

    <Extension()>
    Public Sub Clear(ByVal textBox As TextBox)
        textBox.Text = String.Empty
        textBox.Enabled = True
        textBox.BackColor = Color.Empty
    End Sub

    <Extension()>
    Public Function IsSelected(ByVal ddl As DropDownList) As Boolean
        Try
            If ddl.SelectedValue Is Nothing Then
                Return False
            End If

            If ddl.SelectedValue.Equals("") Or ddl.SelectedValue.Equals("-1") Then
                Return False
            End If
        Catch
        End Try
        Return True
    End Function

    <Extension()>
    Public Function NotSelected(ByVal ddl As DropDownList) As Boolean
        Return Not ddl.IsSelected()
    End Function

    <Extension()>
    Public Function IsUnhack(ByVal control As Control) As Boolean
        Try
            If control.HasControls Then
                For Each chilControl As Control In control.Controls
                    If chilControl.IsUnhack() Then
                        Return True
                    End If
                Next
            End If

            If TypeOf control Is System.Web.UI.WebControls.TextBox Then
                Dim txBox As TextBox = CType(control, TextBox)
                If txBox.Text.IndexOf("<") > -1 Or txBox.Text.IndexOf(">") > -1 Or txBox.Text.IndexOf("'") > -1 Then
                    Return True
                End If
            ElseIf TypeOf control Is HtmlInputText Then
                Dim txBox As HtmlInputText = CType(control, HtmlInputText)
                If txBox.Value.IndexOf("<") > -1 Or txBox.Value.IndexOf(">") > -1 Or txBox.Value.IndexOf("'") > -1 Then
                    Return True
                End If
            End If
        Catch
        End Try
        Return False
    End Function

    <Extension()>
    Public Function GenerateInSet(ByVal listData As IEnumerable(Of String), Optional isBraked As Boolean = True) As String
        Dim result As String = "({0})"
        If listData Is Nothing Then
            Return String.Empty
        End If
        If listData.Count.Equals(0) Then
            Return String.Empty
        End If

        Dim RestValue As String = String.Empty
        For Each item As String In listData
            RestValue = RestValue & String.Format("'{0}', ", item)
        Next
        RestValue = RestValue.Remove(RestValue.Length - 2)

        If isBraked Then
            Return String.Format(result, RestValue)
        Else
            Return RestValue
        End If


    End Function

    <Extension()>
    Public Function GetValueSelected(ByVal chkList As CheckBoxList) As String
        Return chkList.Items.GetValueSelected()
    End Function

    <Extension()>
    Public Function GetValueSelected(ByVal listItem As ListItemCollection) As String
        Dim strRest As String = String.Empty
        For Each iListItem As ListItem In listItem
            If iListItem.Selected Then
                strRest = strRest & "'" & iListItem.Value & "', "
            End If
        Next
        If strRest.Length > 2 Then
            strRest = "(" & strRest.Remove(strRest.Length - 2, 2) & ")"
        End If

        Return strRest
    End Function

    <Extension()>
    Public Function ConvertDealerCode(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    <Extension()>
    Public Function GenerateInSet(ByVal arrData() As String) As String
        Return arrData.Cast(Of String).GenerateInSet()
    End Function

    <Extension()>
    Public Function GenerateInSet(ByVal str As String) As String
        If String.IsNullorEmpty(str) Then
            Return String.Empty
        End If

        Dim result As String = String.Empty
        Dim arrRest() As String = str.Split(New Char() {";"}, StringSplitOptions.RemoveEmptyEntries)

        If arrRest.Length.Equals(0) Then
            Return String.Format("'{0}'", str)
        End If

        For index As Integer = 0 To arrRest.Length - 1
            result = result & String.Format("'{0}'", arrRest(index))

            If Not index.Equals(arrRest.Length - 1) Then
                result = result & ", "
            End If
        Next

        Return result
    End Function

    <Extension()>
    Public Function AddThousandDelimiter(ByVal value As Object) As String
        Try
            Dim strValue As String = value.ToString()
            If strValue.Equals("0") Or String.IsNullorEmpty(strValue) Or String.IsNullorEmpty(strValue.Replace("0", "").Replace(",", "")) Then
                Return "0"
            End If

            Dim convertString As Decimal = CDec(value)
            Dim result As String = Format(convertString, "#,###")
            Return result
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    <Extension()>
    Public Function RemoveThousandDelimeter(ByVal value As String) As String
        Return value.Replace(",", "").Replace(".", "")
    End Function

    <Extension()>
    Public Function DateFormat(ByVal value As Date) As String
        Try
            Return value.ToString("dd/MM/yyyy")
        Catch
            Return String.Empty
        End Try
    End Function

    <Extension()>
    Public Sub Disabled(ByVal textBox As TextBox)
        textBox.ReadOnly = 1
        textBox.BackColor = Color.LightGray
    End Sub

    <Extension()>
    Public Sub ActiveControl(ByVal ctr As Control)
        ctr.Visible = True
    End Sub

    <Extension()>
    Public Sub NonActiveControl(ByVal ctr As Control)
        ctr.Visible = False
    End Sub

    <Extension()>
    Public Sub NonVisible(ByVal ctr As Control)
        ctr.Visible = False
    End Sub

    <Extension>
    Public Function GetItems(ByVal typeEnum As Type) As ListItem()
        Dim countItem As Integer = [Enum].GetNames(typeEnum).Length - 1
        Dim listResult() As ListItem = New ListItem(countItem) {}
        listResult(0) = New ListItem([Enum].GetName(typeEnum, -1), -1)
        For index As Integer = 1 To countItem
            listResult(index) = New ListItem([Enum].GetName(typeEnum, index).Replace("_", " "), index)
        Next

        Return listResult
    End Function

    <Extension>
    Public Function GenerateIncrement(ByVal obj As Object, ByVal length As Integer) As String
        Return obj.ToString().PadLeft(length, "0")
    End Function

    <Extension>
    Public Function FindLabel(ByVal dtgItem As DataGridItemEventArgs, ByVal id As String) As Label
        Try
            Return CType(dtgItem.Item.FindControl(id), Label)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindLabel(ByVal dtgItem As DataGridItem, ByVal id As String) As Label
        Try
            Return CType(dtgItem.FindControl(id), Label)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindCheckBox(ByVal dtgItem As DataGridItemEventArgs, ByVal id As String) As CheckBox
        Try
            Return CType(dtgItem.Item.FindControl(id), CheckBox)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindCheckBox(ByVal dtgItem As DataGridItem, ByVal id As String) As CheckBox
        Try
            Return CType(dtgItem.FindControl(id), CheckBox)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindTextBox(ByVal dtgItem As DataGridItemEventArgs, ByVal id As String) As TextBox
        Try
            Return CType(dtgItem.Item.FindControl(id), TextBox)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindTextBox(ByVal dtgItem As DataGridItem, ByVal id As String) As TextBox
        Try
            Return CType(dtgItem.FindControl(id), TextBox)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindLinkButton(ByVal dtgItem As DataGridItemEventArgs, ByVal id As String) As LinkButton
        Try
            Return CType(dtgItem.Item.FindControl(id), LinkButton)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindLinkButton(ByVal dtgItem As DataGridItem, ByVal id As String) As LinkButton
        Try
            Return CType(dtgItem.FindControl(id), LinkButton)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindHyperLink(ByVal dtgItem As DataGridItemEventArgs, ByVal id As String) As HyperLink
        Try
            Return CType(dtgItem.Item.FindControl(id), HyperLink)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindHyperLink(ByVal dtgItem As DataGridItem, ByVal id As String) As HyperLink
        Try
            Return CType(dtgItem.FindControl(id), HyperLink)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindHiddenField(ByVal dtgItem As DataGridItemEventArgs, ByVal id As String) As HiddenField
        Try
            Return CType(dtgItem.Item.FindControl(id), HiddenField)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function FindHiddenField(ByVal dtgItem As DataGridItem, ByVal id As String) As HiddenField
        Try
            Return CType(dtgItem.FindControl(id), HiddenField)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function DataItem(Of T As Class)(ByVal dtgItem As DataGridItemEventArgs) As T
        Try
            Return CType(dtgItem.Item.DataItem, T)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <Extension>
    Public Function CreateNumberPage(ByVal dtgItem As DataGridItemEventArgs) As Integer
        Try
            Dim dataGrid As DataGrid = CType(dtgItem.Item.Parent.Parent, DataGrid)
            Return dtgItem.Item.ItemIndex + 1 + (dataGrid.CurrentPageIndex * dataGrid.PageSize)
        Catch ex As Exception
            Return 0
        End Try
    End Function

End Module