Imports System.Collections.Generic

Public Class BabitMarBox
    Public Class Metadata
        Public Property user_agent As String
        Public Property platform As String
        Public Property referer As String
        Public Property network_id As String
        Public Property browser As String
    End Class

    Public Class Field
        Public Property id As String
        Public Property type As String
        Public Property ref As String
    End Class

    Public Class Answer
        Public Property field As Field
        Public Property type As String
        Public Property text As String
        Public Property [date] As DateTime?
        Public Property number As Integer?
    End Class

    Public Class Item
        Public Property landing_id As String
        Public Property token As String
        Public Property response_id As String
        Public Property landed_at As DateTime
        Public Property submitted_at As DateTime
        Public Property metadata As Metadata
        Public Property answers As List(Of Answer)
    End Class

    Public Class RootObject
        Public Property total_items As Integer
        Public Property page_count As Integer
        Public Property items As List(Of Item)
    End Class
End Class
