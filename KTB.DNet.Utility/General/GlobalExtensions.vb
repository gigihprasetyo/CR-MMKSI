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

    <Extension()>
    Public Function StringUIToDateTime(ByVal str As String) As DateTime
        Try
            Dim arrDate() As String = str.Split("/")
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
                DirectCast(ctrl, System.Web.UI.WebControls.TextBox).Disabled()
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
                Dim lbtn As LinkButton = DirectCast(ctrl, LinkButton)
                If lbtn.Parent IsNot Nothing Then
                    If Not (TypeOf (lbtn.Parent) Is DataGridItem Or TypeOf (lbtn.Parent) Is GridViewRow Or TypeOf (lbtn.Parent) Is TableCell) Then
                        DirectCast(ctrl, LinkButton).Visible = False
                    End If
                Else
                    DirectCast(ctrl, LinkButton).Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.ImageButton Then
                DirectCast(ctrl, System.Web.UI.WebControls.ImageButton).Visible = False
            ElseIf TypeOf ctrl Is Label Then
                Dim lblform As Label = DirectCast(ctrl, Label)
                If lblform.HasAttributes Then
                    lblform.Visible = False
                End If
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
        If String.IsNullorEmpty(textBox.Text.Trim) Then
            Return True
        End If
        Return False
    End Function

    <Extension()>
    Public Function IsNotEmpty(ByVal textBox As TextBox) As Boolean
        Return Not textBox.IsEmpty()
    End Function

    <Extension()>
    Public Sub Clear(ByVal textBox As TextBox)
        textBox.Text = String.Empty
        textBox.ReadOnly = False
        textBox.BackColor = Color.Empty
    End Sub

    <Extension()>
    Public Sub Enable(ByVal textBox As TextBox)
        textBox.ReadOnly = False
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
    Public Function GenerateInSet(ByVal arrData() As String) As String
        Return arrData.Cast(Of String).GenerateInSet()
    End Function

    <Extension()>
    Public Function GenerateInSet(ByVal str As String, Optional ByVal tandaKurung As Boolean = False) As String
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
        If tandaKurung Then
            Return String.Format("({0})", result)
        Else
            Return result
        End If
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
    Public Function DateDay(ByVal value As DateTime) As DateTime
        Try
            Return New DateTime(value.Year, value.Month, value.Day)
        Catch
            Return value
        End Try
    End Function

    <Extension()>
    Public Sub Disabled(ByVal textBox As TextBox)
        textBox.ReadOnly = 1
        textBox.BackColor = Color.Gainsboro
    End Sub

    <Extension()>
    Public Function GetTextByValue(ByVal iDropDownList As DropDownList, ByVal iValue As String)
        Try
            Return iDropDownList.Items.FindByValue(iValue).Text
        Catch
        End Try
        Return String.Empty
    End Function

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
            Try
                listResult(index) = New ListItem([Enum].GetName(typeEnum, index).Replace("_", " "), index)
            Catch
            End Try
        Next

        Return listResult
    End Function

    <Extension>
    Public Function GenerateIncrement(ByVal obj As Object, ByVal length As Integer) As String
        Return obj.ToString().PadLeft(length, "0")
    End Function

    <Extension>
    Public Function IsRowItems(ByVal dtgItem As DataGridItemEventArgs) As Boolean
        Try
            If dtgItem.Item.ItemType = ListItemType.Item Or _
                dtgItem.Item.ItemType = ListItemType.AlternatingItem Or dtgItem.Item.ItemType = ListItemType.SelectedItem Then
                Return True
            End If
        Catch
        End Try
        Return False
    End Function

    <Extension>
    Public Function IsRowItems(ByVal dtgItem As DataGridItem) As Boolean
        Try
            If dtgItem.ItemType = ListItemType.Item Or _
                dtgItem.ItemType = ListItemType.AlternatingItem Or dtgItem.ItemType = ListItemType.SelectedItem Then
                Return True
            End If
        Catch
        End Try
        Return False
    End Function

    <Extension>
    Public Sub SelectedRows(ByVal iCommand As DataGridCommandEventArgs)
        Try
            Dim iDataGrid As DataGrid = CType(iCommand.Item.Parent.Parent, DataGrid)
            iDataGrid.SelectedIndex = iCommand.Item.ItemIndex
        Catch
        End Try
    End Sub

    <Extension>
    Public Sub ClearSelectedRows(ByVal iDataGrid As DataGrid)
        Try
            iDataGrid.SelectedIndex = -1
        Catch
        End Try
    End Sub

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
    Public Function PrivilegeTrainingType(ByVal area As String) As String
        Select Case area
            Case "1"
                Return "A"
            Case "2"
                Return "B"
            Case "3"
                Return "C"
            Case Else
                Return "B"
        End Select
    End Function

    <Extension>
    Public Sub HiddenColumnbyIndex(ByVal dtgItem As DataGridItemEventArgs, ByVal index As Integer)
        Try
            dtgItem.Item.Cells(index).Visible = False
        Catch
        End Try
    End Sub

    <Extension>
    Public Function CreateNumberPage(ByVal dtgItem As DataGridItemEventArgs) As Integer
        Try
            Dim dataGrid As DataGrid = CType(dtgItem.Item.Parent.Parent, DataGrid)
            Dim result As Integer = dtgItem.Item.ItemIndex + 1 + (dataGrid.CurrentPageIndex * dataGrid.PageSize)
            Return result
        Catch ex As Exception
            Return 0
        End Try
    End Function

    <Extension>
    Public Function CreateNumberPage(ByVal dtgItem As GridViewRowEventArgs) As Integer
        Try
            Dim dataGrid As GridView = CType(dtgItem.Row.Parent.Parent, GridView)
            Dim result As Integer = dtgItem.Row.DataItemIndex + 1 + (dataGrid.PageIndex * dataGrid.PageSize)
            Return result
        Catch ex As Exception
            Return 0
        End Try
    End Function

    <Extension>
    Public Function DateToString(ByVal dt As DateTime) As String
        Try
            If dt.IsValid Then
                Return dt.ToString("dd/MM/yyyy")
            End If
        Catch
        End Try
        Return String.Empty
    End Function

    <Extension>
    Public Function IsValid(ByVal dt As DateTime) As Boolean
        If dt.Equals(CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)) Or dt.Equals(DateTime.MinValue) Then
            Return False
        End If
        Return True
    End Function

    <Extension>
    Public Function IsNotValid(ByVal dt As DateTime) As Boolean
        If dt.Equals(CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)) Or dt.Equals(DateTime.MinValue) Then
            Return True
        End If
        Return False
    End Function

    <Extension>
    Public Function DateNow(ByVal page As System.Web.UI.Page) As DateTime
        Return New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
    End Function

    <Extension>
    Public Function IsNullorEmpty(ByVal str As String) As Boolean
        Try
            Return String.IsNullorEmpty(str)
        Catch ex As Exception
            Return True
        End Try
    End Function

    <Extension>
    Public Function NotNullorEmpty(ByVal str As String) As Boolean
        Try
            Return Not String.IsNullorEmpty(str)
        Catch ex As Exception
            Return True
        End Try
    End Function

    <Extension>
    Public Function IsItems(ByVal arr As ArrayList) As Boolean
        Try
            Return arr.Count > 0
        Catch ex As Exception
            Return True
        End Try
    End Function

    <Extension>
    Public Function IsData(Of T As Class)(ByVal arr As IEnumerable(Of T)) As Boolean
        Try
            Return arr.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    <Extension>
    Public Function IsItems(ByVal list As IList) As Boolean
        Try
            Return list.Count > 0
        Catch ex As Exception
            Return True
        End Try
    End Function

    <Extension()>
    Public Sub ClearTextBoxWithPrefix(ByVal ctrl As Control, ByVal prefix As String)
        If ctrl.HasControls Then
            For Each childCtrl As Control In ctrl.Controls
                ClearTextBoxWithPrefix(childCtrl, prefix)
            Next
        Else
            If TypeOf ctrl Is System.Web.UI.WebControls.TextBox Then
                Dim txtBox As TextBox = DirectCast(ctrl, System.Web.UI.WebControls.TextBox)
                If txtBox.ID.ToString.ToUpper.StartsWith(prefix.ToUpper()) Then
                    txtBox.Clear()
                    txtBox.ReadOnly = False
                    txtBox.BackColor = Color.Empty
                End If
            End If
        End If
    End Sub

    <Extension()>
    Public Sub ClearTextBoxWithPrefix(ByVal ctrl As System.Web.UI.HtmlControls.HtmlControl, ByVal prefix As String)
        If ctrl.HasControls Then
            For Each childCtrl As Control In ctrl.Controls
                ClearTextBoxWithPrefix(childCtrl, prefix)
            Next
        Else
            If TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlInputText Then
                Dim txtBox As System.Web.UI.HtmlControls.HtmlInputText = DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlInputText)
                If txtBox.ID.ToString.ToUpper.StartsWith(prefix.ToUpper()) Then
                    txtBox.Value = String.Empty
                    txtBox.Disabled = True
                End If
            End If
        End If
    End Sub

    <Extension()>
    Public Sub DisabledTextBoxWithPrefix(ByVal ctrl As Control, ByVal prefix As String)
        If ctrl.HasControls Then
            For Each childCtrl As Control In ctrl.Controls
                DisabledTextBoxWithPrefix(childCtrl, prefix)
            Next
        Else
            If TypeOf ctrl Is System.Web.UI.WebControls.TextBox Then
                Dim txtBox As TextBox = DirectCast(ctrl, System.Web.UI.WebControls.TextBox)
                If txtBox.ID.ToString.ToUpper.StartsWith(prefix.ToUpper()) Then
                    txtBox.Disabled()
                End If
            End If
        End If
    End Sub

    <Extension()>
    Public Function IsDealer(ByVal page As System.Web.UI.Page) As Boolean
        Return page.IsDealerTitle(KTB.DNet.Domain.EnumDealerTittle.DealerTittle.DEALER)
    End Function

    <Extension()>
    Public Function IsKTB(ByVal page As System.Web.UI.Page) As Boolean
        Return page.IsDealerTitle(KTB.DNet.Domain.EnumDealerTittle.DealerTittle.KTB)
    End Function

    <Extension()>
    Public Function IsDealer(ByVal dealer As KTB.DNet.Domain.Dealer) As Boolean
        Try
            Return dealer.Title = CType(KTB.DNet.Domain.EnumDealerTittle.DealerTittle.DEALER, String)
        Catch
        End Try
        Return False
    End Function

    <Extension()>
    Public Function IsDealerTitle(ByVal page As System.Web.UI.Page, ByVal enumtitle As KTB.DNet.Domain.EnumDealerTittle.DealerTittle) As Boolean
        Try
            Dim session As New KTB.DNet.Utility.SessionHelper
            Dim dealer As KTB.DNet.Domain.Dealer = CType(session.GetSession("Dealer"), KTB.DNet.Domain.Dealer)

            Return dealer.Title = CType(enumtitle, String)
        Catch ex As Exception
            Return False
        End Try

    End Function

    <Extension()>
    Public Function GetDealer(ByVal page As System.Web.UI.Page) As KTB.DNet.Domain.Dealer
        Try
            Dim session As New KTB.DNet.Utility.SessionHelper

            Return CType(session.GetSession("Dealer"), KTB.DNet.Domain.Dealer)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    <Extension()>
    Public Sub BindDDLFromList(ByVal ddl As DropDownList, ByVal arrData As ArrayList, ByVal fielValue As String, fieldText As String, Optional ByVal defautValue As String = "-1")
        Try
            ddl.ClearSelection()
            ddl.Items.Clear()
            ddl.DataSource = arrData
            ddl.DataTextField = fieldText
            ddl.DataValueField = fielValue
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
            ddl.SelectedValue = defautValue
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    <Extension()>
    Public Function DistictList(Of T As Class)(ByVal listT As IEnumerable(Of T)) As HashSet(Of T)
        Dim listResult As New HashSet(Of T)
        For Each item As T In listT
            If listResult.Where(Function(x) x.Equals(item)).Count = 0 Then
                listResult.Add(item)
            End If
        Next
        Return listResult
    End Function

    <Extension()>
    Public Sub PlusOne(ByVal index As Integer, Optional ByVal value As Integer = 1)
        index = index + value
    End Sub

    <Extension()>
    Public Sub ValueBold(ByVal cells As ExcelRange, ByVal value As String)
        cells.Style.Font.Bold = True
        cells.Style.Font.Name = "Arial"
        cells.Style.Font.Size = 8
        cells.Value = value
    End Sub

    <Extension()>
    Public Sub SetValue(ByVal cells As ExcelRange, ByVal value As String, _
                        Optional ByVal HorizontalAlign As OfficeOpenXml.Style.ExcelHorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, _
                        Optional ByVal VerticalAlign As OfficeOpenXml.Style.ExcelVerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom)
        cells.Style.HorizontalAlignment = HorizontalAlign
        cells.Style.VerticalAlignment = VerticalAlign
        cells.Style.Font.Name = "Arial"
        cells.Style.Font.Size = 8
        cells.Value = value
    End Sub

    <Extension()>
    Public Sub SetHeaderValue(ByVal cells As ExcelRange, ByVal value As String, ByVal bgColor As Color, ByVal fontColor As Color)
        cells.Style.Font.Bold = True
        cells.Style.Font.Color.SetColor(fontColor)
        cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
        cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
        cells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
        cells.Style.Fill.BackgroundColor.SetColor(bgColor)
        cells.Style.Font.Name = "Arial"
        cells.Style.Font.Size = 8
        cells.Value = value
    End Sub

    <Extension()>
    Public Sub SetHeaderValue(ByVal cells As ExcelRange, ByVal value As String, Optional ByVal theme As Integer = 0)
        Select Case theme
            Case 1
                cells.SetHeaderValue(value, Color.DimGray, Color.White)
            Case Else
                cells.SetHeaderValue(value, Color.BurlyWood, Color.Black)
        End Select
    End Sub

    <Extension()>
    Public Sub AddOnClick(ByVal lbl As Label, ByVal stringJavascript As String)
        Try
            lbl.Attributes("onclick") = stringJavascript
        Catch
        End Try
    End Sub

    <Extension()>
    Public Function IsNull(Of T As Class)(ByVal obj As T) As Boolean
        If obj Is Nothing Then
            Return True
        End If
        Return False
    End Function

    <Extension()>
    Public Function IsNotNull(Of T As Class)(ByVal obj As T) As Boolean
        If obj Is Nothing Then
            Return False
        End If
        Return True
    End Function

    <Extension()>
    Public Function ExcelColumnName(ByVal columnNumber As Integer) As String
        Dim dividend As Integer = columnNumber
        Dim columnName As String = String.Empty
        Dim modulo As Integer

        While dividend > 0
            modulo = (dividend - 1) Mod 26
            columnName = Convert.ToChar(65 + modulo).ToString() & columnName
            dividend = CInt((dividend - modulo) / 26)
        End While

        Return columnName
    End Function

    <Extension()>
    Public Sub PageNoCache(ByVal page As System.Web.UI.Page)
        page.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
    End Sub

    <Extension()>
    Public Sub BindingMonth(ByVal ddlBulan As DropDownList)
        ddlBulan.ClearSelection()
        ddlBulan.Items.Clear()
        ddlBulan.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlBulan.Items.Add(New ListItem("Januari", "1"))
        ddlBulan.Items.Add(New ListItem("Febuari", "2"))
        ddlBulan.Items.Add(New ListItem("Maret", "3"))
        ddlBulan.Items.Add(New ListItem("April", "4"))
        ddlBulan.Items.Add(New ListItem("Mei", "5"))
        ddlBulan.Items.Add(New ListItem("Juni", "6"))
        ddlBulan.Items.Add(New ListItem("Juli", "7"))
        ddlBulan.Items.Add(New ListItem("Agustus", "8"))
        ddlBulan.Items.Add(New ListItem("September", "9"))
        ddlBulan.Items.Add(New ListItem("Oktober", "10"))
        ddlBulan.Items.Add(New ListItem("November", "11"))
        ddlBulan.Items.Add(New ListItem("Desember", "12"))
        ddlBulan.SelectedValue = "-1"
    End Sub

End Module