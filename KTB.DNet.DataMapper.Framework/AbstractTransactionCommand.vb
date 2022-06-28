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
'// Generated on 7/26/2005 - 09:24:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Data

#End Region

Namespace KTB.DNet.DataMapper.Framework

    Public MustInherit Class AbstractTransactionCommand
        Implements ITransactionCommand

#Region "Protected Variables"

        Private _DomainObj As Object
        Private _Mapper As IMapper

#End Region

#Region "Protected Properties"

        Public Property DomainObj() As Object
            Get
                Return Me._DomainObj
            End Get
            Set(ByVal Value As Object)
                Me._DomainObj = Value
            End Set
        End Property

        Protected Property Mapper() As IMapper
            Get
                Return Me._Mapper
            End Get
            Set(ByVal Value As IMapper)
                Me._Mapper = Value
            End Set
        End Property

#End Region

#Region "Public Methods"

        Public MustOverride Function Execute(ByVal tran As IDbTransaction) As Integer Implements ITransactionCommand.Execute

#End Region

    End Class

End Namespace