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
'// Author Name   : Ariwibawa
'// PURPOSE       : Facade for Page Event - Parameter Event
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 11:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.Event
    Public Class EventProposalHistoryAgreementFacade
        Inherits AbstractFacade

#Region "Private Variables"
        Private m_EventProposalHistoryMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EventProposalHistoryMapper = MapperFactory.GetInstance().GetMapper(GetType(EventProposalHistoryAgreement).ToString)
        End Sub
#End Region

#Region "Retrieve"
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, _
            ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EventProposalHistoryAgreement), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return Me.m_EventProposalHistoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, _
                pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As EventParameter) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = Me.m_EventProposalHistoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn
        End Function
#End Region

#Region "Custom Method"
        Private Function GenerateDateCriteria(ByVal nDate As Date, ByVal startDate As Boolean) As DateTime
            Dim Hour As Integer
            Dim Minute As Integer
            Dim Second As Integer
            If startDate Then
                Hour = 0
                Minute = 0
                Second = 0
            Else
                Hour = 23
                Minute = 59
                Second = 59
            End If
            Return New DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second)
        End Function
#End Region

    End Class
End Namespace