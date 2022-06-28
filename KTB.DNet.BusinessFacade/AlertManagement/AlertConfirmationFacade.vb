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
    Public Class AlertConfirmationFacade
        Inherits AbstractFacade
#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AlertConfirmationMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal

            Me.m_AlertConfirmationMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.AlertConfirmation).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AlertConfirmation))
        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As AlertConfirmation
            Return CType(m_AlertConfirmationMapper.Retrieve(ID), AlertConfirmation)
        End Function

        Public Function RetrieveByDealer(ByVal DealerID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertConfirmation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AlertConfirmation), "Dealer.ID", MatchType.Exact, DealerID))
            Dim result As ArrayList = m_AlertConfirmationMapper.RetrieveByCriteria(criterias)
            Return result
        End Function

        Public Function RetrieveByAlertDealer(ByVal AlertMasterID As Integer, ByVal DealerID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertConfirmation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AlertConfirmation), "DealerID", MatchType.Exact, DealerID))
            criterias.opAnd(New Criteria(GetType(AlertConfirmation), "AlertMaster.ID", MatchType.Exact, AlertMasterID))
            Dim result As ArrayList = m_AlertConfirmationMapper.RetrieveByCriteria(criterias)
            Return result
        End Function
#End Region
#Region "Transaction"
        Public Function Insert(ByVal objAlertConfirmation As KTB.DNet.Domain.AlertConfirmation) As Integer
            Dim nInsertedRow As Integer = -1
            Try
                nInsertedRow = m_AlertConfirmationMapper.Insert(objAlertConfirmation, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return ex.Message
            End Try
            Return nInsertedRow
        End Function

        Public Sub Update(ByVal objDomain As AlertConfirmation)
            Try
                m_AlertConfirmationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Invoicelicy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As AlertConfirmation)
            Try
                m_AlertConfirmationMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region
    End Class
End Namespace