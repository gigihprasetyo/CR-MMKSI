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
'// Generated on 8/2/2007 - 1:07:49 PM
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
Imports KTB.DNET.BusinessFacade.Salesman
Imports KTB.DNET.BusinessFacade.General

#End Region

Namespace KTB.DNET.BusinessFacade.Salesman
    Public Class SalesmanUniformOrderDetailFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanUniformOrderDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanUniformOrderDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanUniformOrderDetail).ToString)

        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal Code As Integer) As SalesmanUniformOrderDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformAssigned.ID", MatchType.Exact, Code))

            Dim SalesmanUniformOrderDetailColl As ArrayList = m_SalesmanUniformOrderDetailMapper.RetrieveByCriteria(criterias)
            If (SalesmanUniformOrderDetailColl.Count > 0) Then
                Return CType(SalesmanUniformOrderDetailColl(0), SalesmanUniformOrderDetail)
            End If
            Return New SalesmanUniformOrderDetail

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanUniformOrderDetailMapper.RetrieveByCriteria(criterias)
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateItem(ByVal nID As Integer, ByVal UniformSize As Byte) As SalesmanUniformOrderDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformOrderHeader.ID ", MatchType.Exact, nID))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "UniformSize", MatchType.Exact, UniformSize))
            Dim arlIPDetail As ArrayList = m_SalesmanUniformOrderDetailMapper.RetrieveByCriteria(criterias)
            If arlIPDetail.Count > 0 Then
                Return CType(arlIPDetail(0), SalesmanUniformOrderDetail)
            Else
                Return Nothing
            End If
        End Function

        Public Function Insert(ByVal objDomain As SalesmanUniformOrderDetail) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SalesmanUniformOrderDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Insert(ByVal ArrToInsert As ArrayList) As Integer
            Dim nResult As Integer = -1
            Try
                For Each objDetail As SalesmanUniformOrderDetail In ArrToInsert
                    nResult = m_SalesmanUniformOrderDetailMapper.Insert(objDetail, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Update(ByVal objDomain As SalesmanUniformOrderDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanUniformOrderDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesmanUniformOrderDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SalesmanUniformOrderDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanUniformOrderDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanUniformOrderDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal ArrTodelete As ArrayList) As Integer
            Dim nResult As Integer = -1
            Try
                For Each objDetailOrig As SalesmanUniformOrderDetail In ArrTodelete
                    nResult = objDetailOrig.RowStatus = CType(DBRowStatus.Deleted, Short)
                    m_SalesmanUniformOrderDetailMapper.Update(objDetailOrig, m_userPrincipal.Identity.Name)
                Next
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SalesmanUniformOrderHeader) Then
                CType(InsertArg.DomainObject, SalesmanUniformOrderHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SalesmanUniformOrderHeader).MarkLoaded()

            End If
        End Sub
#End Region
    End Class

End Namespace

