Namespace KTB.DNet.Domain
    Public Class EnumDealerTittle
        Public Enum DealerTittle
            DEALER
            KTB
            KTB_DEALER
            LEASING
            KTB_LEASING
            DEALER_LEASING
            KTB_DEALER_LEASING
        End Enum

        Public Function RetrieveTitle() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTitle
            sts = New EnumTitle(0, "DEALER")
            al.Add(sts)
            sts = New EnumTitle(1, "KTB")
            al.Add(sts)
            sts = New EnumTitle(3, "LEASING")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveTitle(ByVal companyCode As String) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTitle
            sts = New EnumTitle(0, "DEALER")
            al.Add(sts)
            If companyCode = "MFTBC" Then
                sts = New EnumTitle(1, "KTB")
            Else
                sts = New EnumTitle(1, "MKS")
            End If
            al.Add(sts)
            sts = New EnumTitle(3, "LEASING")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveTitleForPrivilege() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTitle
            sts = New EnumTitle(0, "DEALER")
            al.Add(sts)
            sts = New EnumTitle(1, "KTB")
            al.Add(sts)
            sts = New EnumTitle(2, "KTB_DEALER")
            al.Add(sts)
            sts = New EnumTitle(3, "LEASING")
            al.Add(sts)
            sts = New EnumTitle(4, "KTB_LEASING")
            al.Add(sts)
            sts = New EnumTitle(5, "DEALER_LEASING")
            al.Add(sts)
            sts = New EnumTitle(6, "KTB_DEALER_LEASING")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveTitleForPrivilege(ByVal companyCode As String) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTitle
            If companyCode = "MFTBC" Then
                sts = New EnumTitle(0, "DEALER")
                al.Add(sts)
                sts = New EnumTitle(1, "KTB")
                al.Add(sts)
                sts = New EnumTitle(2, "KTB_DEALER")
                al.Add(sts)
                sts = New EnumTitle(3, "LEASING")
                al.Add(sts)
                sts = New EnumTitle(4, "KTB_LEASING")
                al.Add(sts)
                sts = New EnumTitle(5, "DEALER_LEASING")
                al.Add(sts)
                sts = New EnumTitle(6, "KTB_DEALER_LEASING")
                al.Add(sts)
            Else
                sts = New EnumTitle(0, "DEALER")
                al.Add(sts)
                sts = New EnumTitle(1, "MKS")
                al.Add(sts)
                sts = New EnumTitle(2, "MKS_DEALER")
                al.Add(sts)
                sts = New EnumTitle(3, "LEASING")
                al.Add(sts)
                sts = New EnumTitle(4, "MKS_LEASING")
                al.Add(sts)
                sts = New EnumTitle(5, "DEALER_LEASING")
                al.Add(sts)
                sts = New EnumTitle(6, "MKS_DEALER_LEASING")
                al.Add(sts)
            End If
            
            Return al
        End Function

        Public Shared Function DealerTitleGetStringValue(ByVal iType As Integer) As String
            Dim str As String = ""
            If iType = 0 Then str = "DEALER"
            If iType = 1 Then str = "KTB"
            If iType = 2 Then str = "KTB - DEALER"
            If iType = 3 Then str = "LEASING"
            If iType = 4 Then str = "KTB - LEASING"
            If iType = 5 Then str = "DEALER - LEASING"
            If iType = 6 Then str = "KTB - DEALER - LEASING"
            Return str
        End Function

        Public Shared Function DealerTitleGetStringValue(ByVal iType As Integer, ByVal companyCode As String) As String
            Dim str As String = ""
            If companyCode = "MFTBC" Then
                If iType = 0 Then str = "DEALER"
                If iType = 1 Then str = "KTB"
                If iType = 2 Then str = "KTB - DEALER"
                If iType = 3 Then str = "LEASING"
                If iType = 4 Then str = "KTB - LEASING"
                If iType = 5 Then str = "DEALER - LEASING"
                If iType = 6 Then str = "KTB - DEALER - LEASING"
            Else
                If iType = 0 Then str = "DEALER"
                If iType = 1 Then str = "MKS"
                If iType = 2 Then str = "MKS - DEALER"
                If iType = 3 Then str = "LEASING"
                If iType = 4 Then str = "MKS - LEASING"
                If iType = 5 Then str = "DEALER - LEASING"
                If iType = 6 Then str = "MKS - DEALER - LEASING"
            End If
            
            Return str
        End Function

        Public Shared Function RetrieveTitleForShowroomAudit() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTitle
            sts = New EnumTitle(0, "DEALER")
            al.Add(sts)
            sts = New EnumTitle(1, "KTB")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveTitleForShowroomAudit(ByVal companyCode As String) As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumTitle
            sts = New EnumTitle(0, "DEALER")
            al.Add(sts)
            If companyCode = "MFTBC" Then
                sts = New EnumTitle(1, "KTB")
            Else
                sts = New EnumTitle(1, "MKS")
            End If
            al.Add(sts)
            Return al
        End Function


    End Class

    Public Class EnumTitle
        Private _val As Integer
        Private _Name As String


        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValTitle() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTitle() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

End Namespace
