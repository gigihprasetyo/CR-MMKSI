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
'// Generated on 8/01/2005 - 2:38:00 PM
'//
'// ===========================================================================		
#End Region

Imports System

Namespace KTB.DNet.Domain.Search

    <AttributeUsage(AttributeTargets.Property)> Public Class ColumnInfoAttribute
        Inherits Attribute

        Private _strColumnName As String
        Private _strFormatString As String

        Public Sub New(ByVal columnName As String, ByVal formatString As String)

            Me._strColumnName = columnName
            Me._strFormatString = formatString

        End Sub

        Public ReadOnly Property ColumnName() As String
            Get
                Return Me._strColumnName
            End Get
        End Property

        Public ReadOnly Property FormatString() As String
            Get
                Return Me._strFormatString
            End Get
        End Property

    End Class

End Namespace