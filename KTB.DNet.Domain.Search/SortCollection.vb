#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/01/2005 - 4:39:00 PM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Collections
Imports System.Text

#End Region

Namespace KTB.DNet.Domain.Search

    Public Class SortCollection
        Inherits ArrayList

#Region "Public Methods"

        Public Overrides Function ToString() As String

            Dim sb As StringBuilder = New StringBuilder

            '== kalau pakai ini retrieve untuk sorting error : Index was out of range
            'For i As Integer = 0 To Count

            '== di modifikasi (count) ---> (count-1)
            For i As Integer = 0 To Count - 1

                sb.Append((CType(Me(i), Sort)).ToString() + " ,")

            Next

            Return sb.ToString(0, sb.Length - 1)

        End Function

#End Region

    End Class

End Namespace