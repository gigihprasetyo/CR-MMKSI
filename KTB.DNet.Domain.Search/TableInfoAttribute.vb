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
'// Generated on 8/01/2005 - 4:44:00 PM
'//
'// ===========================================================================		
#End Region

Imports System

Namespace KTB.DNet.Domain.Search

    <AttributeUsage(AttributeTargets.Class)> _
    Public Class TableInfoAttribute
        Inherits Attribute

        Private _strTableName As String

        Public Sub New(ByVal tableName As String)

            Me._strTableName = tableName

        End Sub

        Public ReadOnly Property TableName() As String
            Get
                Return Me._strTableName
            End Get
        End Property

    End Class

End Namespace