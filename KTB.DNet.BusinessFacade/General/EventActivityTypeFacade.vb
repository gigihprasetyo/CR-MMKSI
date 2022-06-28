#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2009

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
'// Copyright © 2009
'// ---------------------
'// $History      : $
'// Generated on 8/25/2009 - 10:53:00 AM
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
    Public Class EventActivityTypeFacade

#Region "Private Variables"
        Private m_EventActivityTypeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EventActivityTypeMapper = MapperFactory.GetInstance().GetMapper(GetType(EventActivityType).ToString)
        End Sub
#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As EventActivityType
            Return CType(m_EventActivityTypeMapper.Retrieve(ID), EventActivityType)
        End Function
        Public Function RetrieveActiveList(ByVal GroupCode As EnumEventActivityType.EventActivityTypeGroupCode) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventActivityType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(EventActivityType), "EventActivityTypeName", Sort.SortDirection.ASC))
            criterias.opAnd(New Criteria(GetType(EventActivityType), "EventActivityTypeGroupCode", GroupCode.ToString))
            Dim lists As ArrayList = m_EventActivityTypeMapper.RetrieveByCriteria(criterias, sorts)
            Return lists
        End Function
        Public Function RetrieveCarDefault() As EventActivityType
            Dim criterias As New CriteriaComposite(New Criteria(GetType(EventActivityType), "RowStatus", _
                CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventActivityType), "EventActivityTypeGroupCode", _
                EnumEventActivityType.EventActivityTypeGroupCode.CAR.ToString))
            Return m_EventActivityTypeMapper.RetrieveByCriteria(criterias)(0)
        End Function
#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace