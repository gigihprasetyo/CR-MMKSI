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
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.General
    Public Class PPhFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_PPhMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PPhMapper = MapperFactory.GetInstance().GetMapper(GetType(PPh).ToString)
        End Sub
#End Region

#Region "Retrieve"
        Public Function RetrievePPh() As Decimal
            Dim crits As New CriteriaComposite(New Criteria(GetType(PPh), "RowStatus", CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(PPh), "ID", Sort.SortDirection.DESC))
            Dim arr As ArrayList = Me.m_PPhMapper.RetrieveByCriteria(crits, sortColl)
            If arr.Count > 0 Then
                Return DirectCast(arr(0), PPh).Value
            End If
            Return 0
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal PPhValue As Decimal) As Integer
            Dim iReturn As Integer = 1
            Try
                Dim objDomain As New PPh
                objDomain.ChangeDate = DateTime.Now
                objDomain.Value = PPhValue
                Me.m_PPhMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
