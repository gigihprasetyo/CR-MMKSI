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
'// Generated on 7/26/2005 - 11:32:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Data
Imports System.Collections

#End Region

#Region "Custom Namespace Imports"

Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.DataMapper.Framework

    Public Class TransactionManager

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()

        End Sub

#End Region

#Region "Private Variables"

        Private m_CommandList As ArrayList = New ArrayList
        Private m_MapperExceptionPolicy As String = "Mapper Policy"

#End Region

#Region "Public Methods"

        Public Sub AddInsert(ByVal domain As Object, ByVal user As String)
            Dim cmd As ITransactionCommand = New InsertCommand(domain, user)
            Me.m_CommandList.Add(cmd)
        End Sub

        Public Sub AddUpdate(ByVal domain As Object, ByVal user As String)
            Dim cmd As ITransactionCommand = New UpdateCommand(domain, user)
            Me.m_CommandList.Add(cmd)
        End Sub

        Public Sub AddDelete(ByVal domain As Object)
            Dim cmd As ITransactionCommand = New DeleteCommand(domain)
            Me.m_CommandList.Add(cmd)
        End Sub

        Public Sub PerformTransaction()
            Dim Db As Database = DatabaseFactory.CreateDatabase
            Dim connection As IDbConnection = Db.GetConnection

            If (connection.State = ConnectionState.Closed) Then
                connection.Open()
            End If

            Dim transaction As IDbTransaction = connection.BeginTransaction
            Try
                Dim enumerator As IEnumerator = Me.m_CommandList.GetEnumerator
                While (enumerator.MoveNext())
                    Dim cmd As ITransactionCommand = CType(enumerator.Current, ITransactionCommand)
                    Dim iReturn As Integer = cmd.Execute(transaction)

                    '//EventHandler to Capture Return ID
                    If TypeOf cmd Is InsertCommand Then
                        OnInsert(New OnInsertArgs(iReturn, (CType(cmd, InsertCommand)).DomainObj))
                    ElseIf (TypeOf cmd Is UpdateCommand) Then
                        OnUpdate(New OnUpdateArgs(iReturn, (CType(cmd, UpdateCommand)).DomainObj))
                    End If
                End While
                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (connection.State = ConnectionState.Open) Then
                    connection.Close()
                End If
            End Try

        End Sub

        Public Event Insert As OnInsertEventHandler
        Public Event Update As OnUpdateEventHandler

        Protected Overridable Sub OnInsert(ByVal insertArg As OnInsertArgs)
            'Dim handler As OnInsertEventHandler = CType(events(eventcommand), OnInsertEventHandler)
            ' If Not IsNothing(handler) Then
            RaiseEvent Insert(Me, insertArg)
            'End If
        End Sub

        Protected Overridable Sub OnUpdate(ByVal updateArg As OnUpdateArgs)
            'If Not IsNothing(Update) Then
            RaiseEvent Update(Me, updateArg)
            'End If
        End Sub

#End Region


        Public Class OnInsertArgs
            Inherits EventArgs

            Private _ID As Integer
            Private _DomainObject As Object

            Public Sub New(ByVal pID As Integer, ByVal pDomainObject As Object)
                _ID = pID
                If Not IsNothing(pDomainObject) Then
                    _DomainObject = pDomainObject
                End If
            End Sub

            Public ReadOnly Property DomainObject() As Object
                Get
                    Return _DomainObject
                End Get
            End Property

            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property

        End Class

        Public Class OnUpdateArgs
            Inherits EventArgs

            Private _ID As Integer
            Private _DomainObject As Object

            Public Sub New(ByVal pID As Integer, ByVal pDomainObject As Object)
                _ID = pID

                If Not IsNothing(pDomainObject) Then
                    _DomainObject = pDomainObject
                End If
            End Sub

            Public ReadOnly Property DomainObject() As Object
                Get
                    Return _DomainObject
                End Get
            End Property

            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property

        End Class

        Public Delegate Sub OnInsertEventHandler(ByVal sender As Object, ByVal InsertArg As OnInsertArgs)
        Public Delegate Sub OnUpdateEventHandler(ByVal sender As Object, ByVal UpdateArg As OnUpdateArgs)

    End Class

End Namespace