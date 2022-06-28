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
'// Generated on 8/01/2005 - 4:25:00 PM
'//
'// ===========================================================================		
#End Region

Imports System

Namespace KTB.DNet.Domain.Search

    <AttributeUsage(AttributeTargets.Property)> _
    Public Class RelationInfoAttribute
        Inherits Attribute

        Private _strPrimaryKeyTable As String
        Private _strPrimaryKeyColumn As String
        Private _strForeignKeyTable As String
        Private _strForeignKeyColumn As String

        Public Sub New(ByVal primaryKeyTable As String, ByVal primaryKeyColumn As String, ByVal foreignKeyTable As String, ByVal foreignKeyColumn As String)

            Me._strPrimaryKeyTable = primaryKeyTable
            Me._strPrimaryKeyColumn = primaryKeyColumn
            Me._strForeignKeyTable = foreignKeyTable
            Me._strForeignKeyColumn = foreignKeyColumn

        End Sub

        Public ReadOnly Property PrimaryKeyTable() As String
            Get
                Return Me._strPrimaryKeyTable
            End Get
        End Property

        Public ReadOnly Property PrimaryKeyColumn() As String
            Get
                Return Me._strPrimaryKeyColumn
            End Get
        End Property

        Public ReadOnly Property ForeignKeyTable() As String
            Get
                Return Me._strForeignKeyTable
            End Get
        End Property

        Public ReadOnly Property ForeignKeyColumn() As String
            Get
                Return Me._strForeignKeyColumn
            End Get
        End Property

    End Class

End Namespace