Namespace KTB.DNet.Domain
    'updated 
    '03122020 by irfan add extended
    Public Class EnumFSKind

        Private Const RegularText As String = "Regular"
        Private Const CampaignText As String = "Campaign"
        Private Const LaborText As String = "Labor"
        Private Const MaintenanceText As String = "Maintenance"
        Private Const SilverText As String = "Maintenance Silver"
        Private Const GoldText As String = "Maintenance Gold"
        Private Const DiamondText As String = "Maintenance Diamond"
        Private Const Extended2Text As String = "Extended 2xPM"
        Private Const Extended4Text As String = "Extended 4xPM"
        Private Const Extended6Text As String = "Extended 6xPM"
        Private Const NWENGINEPACKAGE As String = "NON WORKSHOP - ENGINE PACKAGE"
        Private Const NWFULLPACKAGE As String = "NON WORKSHOP - FULL PACKAGE"
        Private Const NWUNDERCARRIAGE As String = "NON WORKSHOP - UNDER CARRIAGE"

        Public Enum FSType
            Regular = 0
            Campaign = 1
            Labor = 2
            Maintenance = 3
            Silver = 4
            Gold = 5
            Diamond = 6
            Extended2 = 7
            Extended4 = 8
            Extended6 = 9
            NWENGINEPACKAGE = 10
            NWFULLPACKAGE = 11
            NWUNDERCARRIAGE = 12
        End Enum

        Public Function RetrieveFSType() As ArrayList
            Dim al As New ArrayList

            Dim fsType As EnumFSType
            fsType = New EnumFSType(0, RegularText)
            al.Add(fsType)
            fsType = New EnumFSType(1, CampaignText)
            al.Add(fsType)
            fsType = New EnumFSType(2, LaborText)
            al.Add(fsType)
            fsType = New EnumFSType(3, MaintenanceText)
            al.Add(fsType)
            fsType = New EnumFSType(4, SilverText)
            al.Add(fsType)
            fsType = New EnumFSType(5, GoldText)
            al.Add(fsType)
            fsType = New EnumFSType(6, DiamondText)
            al.Add(fsType)
            fsType = New EnumFSType(7, Extended2Text)
            al.Add(fsType)
            fsType = New EnumFSType(8, Extended4Text)
            al.Add(fsType)
            fsType = New EnumFSType(9, Extended6Text)
            al.Add(fsType)
            fsType = New EnumFSType(10, NWENGINEPACKAGE)
            al.Add(fsType)
            fsType = New EnumFSType(11, NWFULLPACKAGE)
            al.Add(fsType)
            fsType = New EnumFSType(12, NWUNDERCARRIAGE)
            al.Add(fsType)
            Return al
        End Function

        Public Function TypeByIndex(ByVal index As Integer) As String
            Select Case index
                Case FSType.Regular
                    Return RegularText
                Case FSType.Campaign
                    Return CampaignText
                Case FSType.Labor
                    Return LaborText
                Case FSType.Maintenance
                    Return MaintenanceText
                Case FSType.Silver
                    Return SilverText
                Case FSType.Gold
                    Return GoldText
                Case FSType.Diamond
                    Return DiamondText
                Case FSType.Extended2
                    Return Extended2Text
                Case FSType.Extended4
                    Return Extended4Text
                Case FSType.Extended6
                    Return Extended6Text
                Case FSType.NWENGINEPACKAGE
                    Return NWENGINEPACKAGE
                Case FSType.NWFULLPACKAGE
                    Return NWFULLPACKAGE
                Case FSType.NWUNDERCARRIAGE
                    Return NWUNDERCARRIAGE
                Case Else
                    Return String.Empty
            End Select
        End Function
    End Class

    Public Class EnumFSType

        Private _p1 As Integer
        Private _regularText As String

        Sub New(p1 As Integer, RegularText As String)
            _p1 = p1
            _regularText = RegularText
        End Sub

        Public Property ValTitle() As Integer
            Get
                Return _p1
            End Get
            Set(value As Integer)
                _p1 = value
            End Set
        End Property

        Property NameTitle() As String
            Get
                Return _regularText
            End Get
            Set(value As String)
                _regularText = value
            End Set
        End Property

    End Class

End Namespace
