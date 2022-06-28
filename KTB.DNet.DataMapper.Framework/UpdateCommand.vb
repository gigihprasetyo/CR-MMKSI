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
'// Generated on 7/26/2005 - 02:33:00 PM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Data

#End Region

Namespace KTB.DNet.DataMapper.Framework

    Public Class UpdateCommand
        Inherits AbstractTransactionCommand

        Private m_User As String = String.Empty

#Region "Constructors/Destructors/Finalizers"

        Public Sub New(ByVal domain As Object, ByVal user As String)

            Me.m_User = user
            Me.DomainObj = domain
            Me.Mapper = MapperFactory.GetInstance().GetMapper(Me.DomainObj.GetType().ToString())

        End Sub

#End Region

#Region "Public Methods"

        Public Overrides Function Execute(ByVal transaction As IDbTransaction) As Integer

            Me.Mapper.UseTransaction(transaction)
            Return Me.Mapper.Update(Me.DomainObj, m_User)

        End Function

#End Region

    End Class

End Namespace