Namespace KTB.DNet.Domain

    Public Class EnumClaimStatusDetail


        Public Enum ClaimStatusDetail
            Baru
            Ditolak
            Retur
            Ditagih
            Dipenuhi

        End Enum

        Public Enum ClaimStatusDetailKTB
            Baru
            Ditolak
            Retur_Asuransi
            Retur_NonAsuransi
            Ditagih
            Dipenuhi

        End Enum


        Public Function RetrieveStatus() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumClaimStatusDetailProp
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetail.Baru, ClaimStatusDetail.Baru.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetail.Ditolak, ClaimStatusDetail.Ditolak.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetail.Retur, ClaimStatusDetail.Retur.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetail.Ditagih, ClaimStatusDetail.Ditagih.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetail.Ditagih, ClaimStatusDetail.Dipenuhi.ToString)
            al.Add(sts)

            Return al

        End Function

        Public Function RetrieveStatusKTB() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumClaimStatusDetailProp
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetailKTB.Baru, ClaimStatusDetailKTB.Baru.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetailKTB.Ditolak, ClaimStatusDetailKTB.Ditolak.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetailKTB.Retur_Asuransi, ClaimStatusDetailKTB.Retur_Asuransi.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetailKTB.Retur_NonAsuransi, ClaimStatusDetailKTB.Retur_NonAsuransi.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetailKTB.Ditagih, ClaimStatusDetailKTB.Ditagih.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusDetailProp(ClaimStatusDetailKTB.Dipenuhi, ClaimStatusDetailKTB.Dipenuhi.ToString)
            al.Add(sts)

            Return al

        End Function

    End Class

    Public Class EnumClaimStatusDetailProp
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace

