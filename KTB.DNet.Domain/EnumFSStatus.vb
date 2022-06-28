Namespace KTB.DNet.Domain

  Public Class EnumFSStatus

    Public Enum FSStatus
      Baru
      Rilis
      Proses
      Selesai
    End Enum

    Public Function RetrieveFSStatus() As ArrayList
      Dim al As New ArrayList

      Dim sts As EnumFS
      sts = New EnumFS(0, "Baru")
      al.Add(sts)
      sts = New EnumFS(1, "Rilis")
      al.Add(sts)
      sts = New EnumFS(2, "Proses")
      al.Add(sts)
      sts = New EnumFS(3, "Selesai")
      al.Add(sts)
      Return al
    End Function

  End Class

End Namespace

