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
'// Generated on 8/3/2005 - 10:53:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Public Class SPFacade
    Inherits AbstractFacade


    Private _Mapper As IMapper

    Public Sub New(ByVal userPrincipal As IPrincipal)
        Me._Mapper = MapperFactory.GetInstance().GetMapper(GetType(SP).ToString)
    End Sub

    Public Function ExecuteSP(ByVal SQL As String) As Boolean
        If SQL.IndexOf("exec ") < 0 Then
            SQL = SQL.Replace("'", "''")
            SQL = "exec SPHelper '" & SQL & "'"
        End If
        Return Me._Mapper.ExecuteSP(SQL)
    End Function
End Class
