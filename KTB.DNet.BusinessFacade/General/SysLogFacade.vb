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
'// Generated on 8/3/2005 - 3:58:00 PM
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
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Profile

#End Region

Namespace KTB.DNet.BusinessFacade.General
    Public Class SysLogFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_SyslogMapper As IMapper
        Private m_TransactionControlMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)


            Me.m_userPrincipal = userPrincipal
            Me.m_SyslogMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.SysLog).ToString)
            Me.m_TransactionControlMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.TransactionControl).ToString)
            Me.objTransactionManager = New TransactionManager

            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SysLog))

        End Sub

#End Region

#Region "Transaction Methods"
        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SysLog) As Integer
            Dim returnValue As Short = -2
            Try
                Dim ObjMapper As IMapper = m_SyslogMapper

                returnValue = ObjMapper.Insert(objDomain, m_userPrincipal.Identity.Name)

            Catch ex As Exception
                returnValue = -1
            End Try

            Return returnValue
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SysLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim list As ArrayList = m_SyslogMapper.RetrieveByCriteria(criterias)
            Return list
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SysLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim list As ArrayList = m_SyslogMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return list
        End Function

        Public Function Delete(ByVal objDomain As SysLog)
            m_SyslogMapper.Delete(objDomain)
        End Function
#End Region

    End Class
End Namespace