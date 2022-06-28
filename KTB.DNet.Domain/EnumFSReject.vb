Namespace KTB.DNet.Domain

  Public Class EnumFSReject

    Public Enum FSReject
      Approve
      Disapprove
    End Enum

    Public Function RetrieveFSReject() As ArrayList
      Dim al As New ArrayList

      Dim sts As EnumFS
      sts = New EnumFS(0, "Disapprove")
      al.Add(sts)
      sts = New EnumFS(1, "Approve")
      al.Add(sts)

      Return al

    End Function

  End Class

End Namespace

