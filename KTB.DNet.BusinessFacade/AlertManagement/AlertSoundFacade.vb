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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:43:31 PM
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

Imports KTB.DNet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region
Namespace KTB.DNet.BusinessFacade.AlertManagement
    Public Class AlertSoundFacade
        Inherits AbstractFacade
#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AlertSoundMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal

            Me.m_AlertSoundMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.AlertSound).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AlertSound))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AlertSound
            Return CType(m_AlertSoundMapper.Retrieve(ID), AlertSound)
        End Function

        Public Function RetrieveActiveList(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AlertSound), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AlertSoundMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region
    End Class
End Namespace