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
'// Generated on 7/26/2005 - 09:28:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.Data

#End Region

Namespace KTB.DNet.DataMapper.Framework

    Public Interface ITransactionCommand

        Function Execute(ByVal tran As IDbTransaction) As Integer

    End Interface

End Namespace