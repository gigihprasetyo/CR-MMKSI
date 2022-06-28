Imports System.Web.UI.WebControls

Namespace KTB.DNet.Domain

    Public Class EnumStatusSPK
        Public Enum Status
            Awal = 0
            Tanda_Jadi = 1
            Lunas = 2
            Batal = 3
            Pending = 4
            Selesai = 5
            Closed = 6
            Indent = 7
            Pending_Konsumen = 8
            Tunggu_Unit = 9

            '-- Di tutup untuk CR Dummy Faktur
            'Tunggu_Unit_I
            'Tunggu_Unit_II
            'Tunggu_Unit_III
            'Tunggu_Unit_IV
            'Tunggu_Unit_V

            ''--start add rudi 2018-01-25
            'Tunggu_Unit_VI
            'Tunggu_Unit_VII
            ''--end
        End Enum

        'Private Shared _arrayListStatusSPK As ArrayList
        'Public Shared ReadOnly Property ArrayStatusSPK() 'perubahan akibat indent system - anh 201707
        '    Get
        '        If (_arrayListStatusSPK Is Nothing) Then
        '            _arrayListStatusSPK = New ArrayList
        '            Dim listItemStatus0 As New ListItem("Awal", 0)
        '            Dim listItemStatus1 As New ListItem("Tanda Jadi", 1)
        '            Dim listItemStatus2 As New ListItem("Lunas", 2)
        '            Dim listItemStatus3 As New ListItem("Batal", 3)
        '            Dim listItemStatus4 As New ListItem("Pending", 4)
        '            Dim listItemStatus5 As New ListItem("Selesai", 5)
        '            Dim listItemStatus6 As New ListItem("Closed", 6)
        '            Dim listItemStatus7 As New ListItem("Indent", 7)
        '            Dim listItemStatus8 As New ListItem("Pending Konsumen", 8)
        '            Dim listItemStatus9 As New ListItem("Tunggu Unit", 9)

        '            With _arrayListStatusSPK
        '                .Add(listItemStatus0)
        '                .Add(listItemStatus7)
        '                .Add(listItemStatus1)
        '                .Add(listItemStatus2)
        '                .Add(listItemStatus4)
        '                .Add(listItemStatus6)
        '                .Add(listItemStatus8)
        '                .Add(listItemStatus9)
        '                .Add(listItemStatus3)
        '                .Add(listItemStatus5)
        '            End With

        '        End If
        '        Return _arrayListStatusSPK
        '    End Get
        'End Property

        Public Shared Function RetrieveStatus() As ArrayList
            Dim arl As New ArrayList

            arl.Add(New EnumItem(CInt(Status.Awal), Status.Awal.ToString()))

            arl.Add(New EnumItem(CInt(Status.Pending_Konsumen), Status.Pending_Konsumen.ToString().Replace("_", " ")))
            arl.Add(New EnumItem(CInt(Status.Tunggu_Unit), Status.Tunggu_Unit.ToString().Replace("_", " ")))

            '-- Di tutup untuk CR Dummy Faktur
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_I), "Tunggu Unit (I)")) ' Status.Tunggu_Unit_I.ToString().Replace("_", "")))
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_II), "Tunggu Unit (II)")) ' Status.Tunggu_Unit_II.ToString().Replace("_", "")))
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_III), "Tunggu Unit (III)")) ' Status.Tunggu_Unit_III.ToString().Replace("_", "")))
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_IV), "Tunggu Unit (IV)")) ' Status.Tunggu_Unit_IV.ToString().Replace("_", "")))
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_V), "Tunggu Unit (V)")) ' Status.Tunggu_Unit_V.ToString().Replace("_", "")))

            ''--start add rudi 2018-01-25
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_VI), "Tunggu Unit (VI)")) ' Status.Tunggu_Unit_VI.ToString().Replace("_", "")))
            'arl.Add(New EnumItem(CInt(Status.Tunggu_Unit_VII), "Tunggu Unit (VII)")) ' Status.Tunggu_Unit_VII.ToString().Replace("_", "")))
            ''--end

            'arl.Add(New EnumItem(CInt(Status.Tanda_Jadi), Status.Tanda_Jadi.ToString().Replace("_", " ")))
            'arl.Add(New EnumItem(CInt(Status.Lunas), Status.Lunas.ToString()))
            arl.Add(New EnumItem(CInt(Status.Batal), Status.Batal.ToString()))
            'arl.Add(New EnumItem(CInt(Status.Pending), Status.Pending.ToString()))
            arl.Add(New EnumItem(CInt(Status.Selesai), Status.Selesai.ToString()))
            'arl.Add(New EnumItem(CInt(Status.Closed), Status.Closed.ToString()))
            'arl.Add(New EnumItem(CInt(Status.Indent), Status.Indent.ToString()))
            Return arl
        End Function

        Public Shared Function GetStringValueStatus(ByVal iStatus As Integer) As String
            Dim str As String = ""
            If iStatus = 0 Then str = "Awal"
            If iStatus = 1 Then str = "Tanda Jadi"
            If iStatus = 2 Then str = "Lunas"
            If iStatus = 3 Then str = "Batal"
            If iStatus = 4 Then str = "Pending"
            If iStatus = 5 Then str = "Selesai"
            If iStatus = 6 Then str = "Closed"
            If iStatus = 7 Then str = "Indent"
            If iStatus = 8 Then str = "Pending Konsumen"
            If iStatus = 9 Then str = "Tunggu Unit"

            '-- Di tutup untuk CR Dummy Faktur
            'If iStatus = 10 Then str = "Tunggu Unit (I)"
            'If iStatus = 11 Then str = "Tunggu Unit (II)"
            'If iStatus = 12 Then str = "Tunggu Unit (III)"
            'If iStatus = 13 Then str = "Tunggu Unit (IV)"
            'If iStatus = 14 Then str = "Tunggu Unit (V)"

            ''--start add rudi 2018-01-25
            'If iStatus = 15 Then str = "Tunggu Unit (VI)"
            'If iStatus = 16 Then str = "Tunggu Unit (VII)"
            ''--end

            Return str
        End Function

    End Class
End Namespace