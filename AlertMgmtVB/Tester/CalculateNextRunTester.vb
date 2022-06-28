Imports System.Collections
Imports System.Threading

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.AlertManagement

Public Interface ITestClass
    Sub Run()
End Interface

Public Class CalculateNextRunTester
    Implements ITestClass


    Public Sub Run() Implements ITestClass.Run

    End Sub

    'Jangan lupa update dengan yg terbaru di MainModule
    Private Function CalculateNextRun(ByVal currentDateTime As DateTime, ByVal AlertFrequency As Integer, ByVal AlertFrequencyType As Integer) As DateTime

        Select Case CType(AlertFrequencyType, EnumAlertManagement.AlertMediaType)
            Case EnumAlertManagement.AlertMediaType.Menit
                Return currentDateTime.AddMinutes(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Jam
                Return currentDateTime.AddHours(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Hari
                Return currentDateTime.AddDays(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Minggu
                Return currentDateTime.AddDays(AlertFrequency * 7)
            Case EnumAlertManagement.AlertMediaType.Bulan
                Return currentDateTime.AddMonths(AlertFrequency)
            Case EnumAlertManagement.AlertMediaType.Tahun
                Return currentDateTime.AddYears(AlertFrequency)
            Case Else
                Throw New Exception("Invalid Alert frequency type.")
        End Select

    End Function
End Class

Class NextRunTestData
    Public CurrentDateTime As DateTime
    Public AlertFrequency As Integer
    Public AlertFrequencyType As Integer
End Class