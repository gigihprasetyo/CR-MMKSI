Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumStopMark
        Public Enum StopMark
            'A = 0
            'B = 1
            'D = 2
            'E = 3
            'I = 4
            K = 5
            'M = 6
            'N = 7
            'P = 8
            'S = 9
            'W = 10
            'W1 = 11
            'W2 = 12
            'W3 = 13
            'W4 = 14
            'W5 = 15
            'W6 = 16
            X = 17
            'Y = 18

        End Enum


        Public Shared Function GetStringValue(ByVal pStopMark As Short) As String
            Dim str As String = "Tidak Terpenuhi"
            'If pStopMark = StopMark.A Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.B Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.D Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.E Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.I Then str = "Tidak Dialokasi"
            If pStopMark = StopMark.K Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.M Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.N Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.P Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.S Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W1 Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W2 Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W3 Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W4 Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W5 Then str = "Tidak Dialokasi"
            'If pStopMark = StopMark.W6 Then str = "Tidak Dialokasi"
            If pStopMark = StopMark.X Then str = "Tidak Diproduksi"
            'If pStopMark = StopMark.Y Then str = "Tidak Dialokasi"

            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sStopMark As String) As Short
            Dim Rsl As Short = 0
            'If sStopMark.ToUpper = "Accessories".ToUpper Then Rsl = StopMark.A
            'If sStopMark.ToUpper = "Back Order".ToUpper Then Rsl = StopMark.B
            'If sStopMark.ToUpper = "Domestic".ToUpper Then Rsl = StopMark.D
            'If sStopMark.ToUpper = "Tools".ToUpper Then Rsl = StopMark.E
            'If sStopMark.ToUpper = "Parts".ToUpper Then Rsl = StopMark.I
            If sStopMark.ToUpper = "Tidak Dialokasi".ToUpper Then Rsl = StopMark.K
            'If sStopMark.ToUpper = "Technical".ToUpper Then Rsl = StopMark.M
            'If sStopMark.ToUpper = "Import".ToUpper Then Rsl = StopMark.N
            'If sStopMark.ToUpper = "Data Control".ToUpper Then Rsl = StopMark.P
            'If sStopMark.ToUpper = "Import".ToUpper Then Rsl = StopMark.S
            'If sStopMark.ToUpper = "Data Control".ToUpper Then Rsl = StopMark.W
            'If sStopMark.ToUpper = "X 10".ToUpper Then Rsl = StopMark.W1
            'If sStopMark.ToUpper = "X 15".ToUpper Then Rsl = StopMark.W2
            'If sStopMark.ToUpper = "X 18".ToUpper Then Rsl = StopMark.W3
            'If sStopMark.ToUpper = "X 20".ToUpper Then Rsl = StopMark.W4
            'If sStopMark.ToUpper = "X 50".ToUpper Then Rsl = StopMark.W5
            'If sStopMark.ToUpper = "X 100".ToUpper Then Rsl = StopMark.W6
            If sStopMark.ToUpper = "Tidak Diproduksi".ToUpper Then Rsl = StopMark.X
            'If sStopMark.ToUpper = "Domestic".ToUpper Then Rsl = StopMark.Y

            Return Rsl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList

            arl.Add(New ListItem("Tidak Dialokasi", StopMark.K))
            arl.Add(New ListItem("Tidak Diproduksi", StopMark.X))

            Return arl
        End Function

    End Class
End Namespace

