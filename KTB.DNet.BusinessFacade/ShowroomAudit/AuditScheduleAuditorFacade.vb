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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/27/2007 - 1:17:22 PM
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
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.ShowroomAudit

    Public Class AuditScheduleAuditorFacade
        Inherits AbstractFacade


#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AuditScheduleAuditorMapper As IMapper

        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_AuditScheduleAuditorMapper = MapperFactory.GetInstance.GetMapper(GetType(AuditScheduleAuditor).ToString)
        End Sub
#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As AuditScheduleAuditor
            Return CType(m_AuditScheduleAuditorMapper.Retrieve(ID), AuditScheduleAuditor)
        End Function

        'Public Function Retrieve(ByVal Code As String) As AuditScheduleAuditor
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleAuditor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "EventRequestNo", MatchType.Exact, Code))

        '    Dim arr As ArrayList = m_AuditScheduleAuditorMapper.RetrieveByCriteria(criterias)
        '    If (arr.Count > 0) Then
        '        Return CType(arr(0), EventInfo)
        '    End If
        '    Return New EventInfo
        'End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AuditScheduleAuditorMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AuditScheduleAuditor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AuditScheduleAuditorColl As ArrayList = m_AuditScheduleAuditorMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AuditScheduleAuditorColl
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Public Function Insert(ByVal objDomain As AuditScheduleAuditor) As Integer
            Dim iReturn As Integer = -2
            Try
                m_AuditScheduleAuditorMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As AuditScheduleAuditor)
            Try
                m_AuditScheduleAuditorMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As AuditScheduleAuditor) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AuditScheduleAuditorMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function



#End Region

#Region "Custom Method"
        'Public Function ValidateItem(ByVal _scheduleID As Integer, ByVal _Dealer As String, ByVal _Type As Integer) As AuditScheduleAuditor
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleAuditor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "AuditSchedule.ID ", MatchType.Exact, _scheduleID))
        '    criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "Dealer.DealerCode", MatchType.Exact, _Dealer))
        '    criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "AuditorType", MatchType.Exact, _Type))

        '    Dim arlAuditor As ArrayList = m_AuditScheduleAuditorMapper.RetrieveByCriteria(criterias)
        '    If arlAuditor.Count > 0 Then
        '        Return CType(arlAuditor(0), AuditScheduleAuditor)
        '    Else
        '        Return Nothing
        '    End If
        'End Function

#End Region

    End Class
End Namespace