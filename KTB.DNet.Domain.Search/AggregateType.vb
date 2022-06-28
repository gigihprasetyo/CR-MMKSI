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
'// Generated on 8/01/2005 - 2:24:00 PM
'//
'// ===========================================================================		
#End Region

Imports System

Namespace KTB.DNet.Domain.Search

    Public Enum AggregateType

        Avg = 0
        Count
        Sum
        Max
        Min
        Distinct  '24/jul/2007  Deddy H     penambahan enum, keperluan distinct value

    End Enum

End Namespace