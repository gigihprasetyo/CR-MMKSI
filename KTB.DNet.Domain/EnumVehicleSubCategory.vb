Imports System.Web.UI.WebControls

Public Class EnumVehicleSubCategory
    Public Enum SubOfPC
        StradaTriton = 0
        'Lancer = 1
        'MavenGrandis = 2
        Pajero_PajeroSport = 1
        OutlanderSport = 2
        Lancer_Grandis_Maven = 3
        Mirage = 4
        Delica = 5
        XPander = 6

        '-- add start rudi
        'All_New_StradaTriton = 7
        'All_New_PajeroSport = 8
        ''-- add end rudi 2018-01-18

    End Enum
    Public Enum SubOfCV
        FE = 0
        Fuso = 1
    End Enum
    Public Enum SubOfLCV
        T120ss = 0
        L300 = 1
    End Enum
    Public Function GetSubOfPCString(ByVal val As Short) As String
        Dim strRsl As String = ""
        If val = SubOfPC.StradaTriton Then strRsl = SubOfPC.StradaTriton.ToString
        'If val = SubOfPC.Lancer Then strRsl = SubOfPC.Lancer.ToString
        'If val = SubOfPC.MavenGrandis Then strRsl = SubOfPC.MavenGrandis.ToString
        If val = SubOfPC.Lancer_Grandis_Maven Then strRsl = SubOfPC.Lancer_Grandis_Maven.ToString
        If val = SubOfPC.Pajero_PajeroSport Then strRsl = SubOfPC.Pajero_PajeroSport.ToString
        If val = SubOfPC.OutlanderSport Then strRsl = SubOfPC.OutlanderSport.ToString
        If val = SubOfPC.Mirage Then strRsl = SubOfPC.Mirage.ToString
        If val = SubOfPC.Delica Then strRsl = SubOfPC.Delica.ToString
        If val = SubOfPC.XPander Then strRsl = SubOfPC.XPander.ToString

        '-- add start rudi
        'If val = SubOfPC.All_New_StradaTriton Then strRsl = SubOfPC.All_New_StradaTriton.ToString
        'If val = SubOfPC.All_New_PajeroSport Then strRsl = SubOfPC.All_New_PajeroSport.ToString
        ''-- add end rudi 2018-01-18

        Return strRsl
    End Function
    Public Function GetSubOfCVString(ByVal val As Short) As String
        Dim strRsl As String = ""
        If val = SubOfCV.FE Then strRsl = SubOfCV.FE.ToString
        If val = SubOfCV.Fuso Then strRsl = SubOfCV.Fuso.ToString
        Return strRsl
    End Function
    Public Function GetSubOfLCVString(ByVal val As Short) As String
        Dim strRsl As String = ""
        If val = SubOfLCV.T120ss Then strRsl = SubOfLCV.T120ss.ToString
        If val = SubOfLCV.L300 Then strRsl = SubOfLCV.L300.ToString
        Return strRsl
    End Function
    Public Function GetSubOfPCSQLValue(ByVal val As Short) As String
        Dim strRsl As String = ""
        If val = SubOfPC.StradaTriton Then strRsl = "STRADA%;TRITON%;CR%" ' "CR"
        'If val = SubOfPC.Lancer Then strRsl = "LANCER%"
        'If val = SubOfPC.MavenGrandis Then strRsl = "MAVEN%;GRANDIS%" ' "MAVEN;GRANDIS%"
        If val = SubOfPC.Lancer_Grandis_Maven Then strRsl = "%LANCER%;GRANDIS%;MAVEN%" ' "MAVEN;GRANDIS%"
        If val = SubOfPC.Pajero_PajeroSport Then strRsl = "%PAJERO%;QX%"
        If val = SubOfPC.OutlanderSport Then strRsl = "%OUTLANDER%"
        If val = SubOfPC.Mirage Then strRsl = "%MIRAGE%"
        If val = SubOfPC.Delica Then strRsl = "%Delica%"
        If val = SubOfPC.XPander Then strRsl = "%XPander%"

        ''-- add start rudi
        'If val = SubOfPC.All_New_StradaTriton Then strRsl = "ALL NEW STRADA TRITON%;ALL NEW TRITON%" ' "CR"
        'If val = SubOfPC.All_New_PajeroSport Then strRsl = "ALL NEW PAJERO%"
        ''-- add end rudi 2018-01-18

        Return strRsl
    End Function
    Public Function GetSubOfCVSQLValue(ByVal val As Short) As String
        Dim strRsl As String = ""
        If val = SubOfCV.FE Then strRsl = "FE%" '"Colt FE%"
        If val = SubOfCV.Fuso Then strRsl = "Fuso%" '"FM;FN"
        Return strRsl
    End Function
    Public Function GetSubOfLCVSQLValue(ByVal val As Short) As String
        Dim strRsl As String = ""
        If val = SubOfLCV.T120ss Then strRsl = "%T120SS%"
        If val = SubOfLCV.L300 Then strRsl = "L300%" '"Colt L300%"
        Return strRsl
    End Function
    Public Function GetSubOfPCList() As ArrayList
        Dim arlRsl As New ArrayList
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.StradaTriton), SubOfPC.StradaTriton))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Pajero_PajeroSport), SubOfPC.Pajero_PajeroSport))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.OutlanderSport), SubOfPC.OutlanderSport))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Mirage), SubOfPC.Mirage))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Delica), SubOfPC.Delica))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Lancer_Grandis_Maven), SubOfPC.Lancer_Grandis_Maven))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.XPander), SubOfPC.XPander))
        'arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Lancer), SubOfPC.Lancer))
        'arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.MavenGrandis), SubOfPC.MavenGrandis))

        ''-- add start rudi
        'arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.All_New_StradaTriton), SubOfPC.All_New_StradaTriton))
        'arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.All_New_PajeroSport), SubOfPC.All_New_PajeroSport))
        ''-- add end rudi 2018-01-18

        Return arlRsl
    End Function
    Public Function GetSubOfCVList() As ArrayList
        Dim arlRsl As New ArrayList
        arlRsl.Add(New ListItem(GetSubOfCVString(SubOfCV.FE), SubOfCV.FE))
        arlRsl.Add(New ListItem(GetSubOfCVString(SubOfCV.Fuso), SubOfCV.Fuso))
        Return arlRsl
    End Function
    Public Function GetSubOfLCVList() As ArrayList
        Dim arlRsl As New ArrayList
        arlRsl.Add(New ListItem(GetSubOfLCVString(SubOfLCV.T120ss), SubOfLCV.T120ss))
        arlRsl.Add(New ListItem(GetSubOfLCVString(SubOfLCV.L300), SubOfLCV.L300))
        Return arlRsl
    End Function
    Public Function GetSQLValue(ByVal pCategory As String, ByVal pSubCategory As Short) As String
        If pCategory.ToUpper = "pc".ToUpper Then
            Return GetSubOfPCSQLValue(pSubCategory)
        ElseIf pCategory.ToUpper = "cv".ToUpper Then
            Return GetSubOfCVSQLValue(pSubCategory)
        ElseIf pCategory.ToUpper = "lcv".ToUpper Then
            Return GetSubOfLCVSQLValue(pSubCategory)
        Else
            Return GetSubOfExcCVSQLValue(pSubCategory)
        End If
        Return ""
    End Function
    'Public Sub BindDdl(ByRef ddlSubCategory As DropDownList, Optional ByVal strCategoryCode As String = "")
    '    Dim arlItem As ArrayList

    '    If strCategoryCode.Trim.ToUpper = "PC" Then
    '        arlItem = GetSubOfPCList()
    '    ElseIf strCategoryCode.Trim.ToUpper = "CV" Then
    '        arlItem = GetSubOfCVList()
    '    ElseIf strCategoryCode.Trim.ToUpper = "LCV" Then
    '        arlItem = GetSubOfLCVList()
    '    End If
    '    ddlSubCategory.Items.Clear()
    '    ddlSubCategory.Items.Add(New ListItem("Silahkan pilih", -1))
    '    If Not IsNothing(arlItem) Then
    '        For Each li As ListItem In arlItem
    '            ddlSubCategory.Items.Add(li)
    '        Next
    '    End If
    'End Sub

    Private Function GetSubOfExcCVSQLValue(ByVal Val As Short) As String

        Dim strRsl As String = ""
        If Val = SubOfPC.StradaTriton Then strRsl = "STRADA TRITON%;TRITON%" ' "CR"
        'If val = SubOfPC.Lancer Then strRsl = "LANCER%"
        'If val = SubOfPC.MavenGrandis Then strRsl = "MAVEN%;GRANDIS%" ' "MAVEN;GRANDIS%"
        If Val = SubOfPC.Lancer_Grandis_Maven Then strRsl = "%LANCER%;GRANDIS%;MAVEN%" ' "MAVEN;GRANDIS%"
        If Val = SubOfPC.Pajero_PajeroSport Then strRsl = "PAJERO%"
        If Val = SubOfPC.OutlanderSport Then strRsl = "%OUTLANDER%"
        If Val = SubOfPC.Mirage Then strRsl = "%MIRAGE%"
        If Val = SubOfPC.Delica Then strRsl = "%Delica%"
        If Val = SubOfPC.XPander Then strRsl = "%XPander%"
        If Val = SubOfLCV.T120ss Then strRsl = "T120SS%"
        If Val = SubOfLCV.L300 Then strRsl = "L300%" '"Colt L300%"

        ''-- add start rudi
        'If val = SubOfPC.All_New_StradaTriton Then strRsl = "ALL NEW STRADA TRITON%;ALL NEW TRITON%" ' "CR"
        'If val = SubOfPC.All_New_PajeroSport Then strRsl = "ALL NEW PAJERO%"
        ''-- add end rudi 2018-01-18

        Return strRsl
    End Function

    Function GetSubOfExcCVList() As ArrayList
        Dim arlRsl As New ArrayList
        arlRsl.Add(New ListItem(GetSubOfLCVString(SubOfLCV.T120ss), SubOfLCV.T120ss))
        arlRsl.Add(New ListItem(GetSubOfLCVString(SubOfLCV.L300), SubOfLCV.L300))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.StradaTriton), SubOfPC.StradaTriton))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Pajero_PajeroSport), SubOfPC.Pajero_PajeroSport))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.OutlanderSport), SubOfPC.OutlanderSport))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Mirage), SubOfPC.Mirage))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Delica), SubOfPC.Delica))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.Lancer_Grandis_Maven), SubOfPC.Lancer_Grandis_Maven))
        arlRsl.Add(New ListItem(GetSubOfPCString(SubOfPC.XPander), SubOfPC.XPander))
        Return arlRsl
    End Function

End Class
