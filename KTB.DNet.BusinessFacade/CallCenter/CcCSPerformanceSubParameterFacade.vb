
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 3:19:44 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.CallCenter

    Public Class CcCSPerformanceSubParameterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CcCSPerformanceSubParameterMapper As IMapper
        Private ID_Insert As Integer
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CcCSPerformanceSubParameterMapper = MapperFactory.GetInstance.GetMapper(GetType(CcCSPerformanceSubParameter).ToString)
            m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CcCSPerformanceSubParameter
            Return CType(m_CcCSPerformanceSubParameterMapper.Retrieve(ID), CcCSPerformanceSubParameter)
        End Function

        Public Function Retrieve(ByVal code As String) As Integer
            Dim nreturn As Integer
            Dim cri As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cri.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "Code", MatchType.Exact, code))

            nreturn = CType((m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(cri)).Item(0), CcCSPerformanceSubParameter).ID
            Return nreturn
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CcCSPerformanceSubParameterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceSubParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcCSPerformanceSubParameterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceSubParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcCSPerformanceSubParameterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim CSPMasterColl As ArrayList = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias)
            Return CSPMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
          ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceSubParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CSPMasterColl As ArrayList = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CSPMasterColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CcCSPerformanceSubParameter As ArrayList = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias)
            Return _CcCSPerformanceSubParameter
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcCSPerformanceSubParameterColl As ArrayList = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CcCSPerformanceSubParameterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CcCSPerformanceSubParameterColl As ArrayList = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CcCSPerformanceSubParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcCSPerformanceSubParameterColl As ArrayList = m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), columnName, matchOperator, columnValue))
            Return CcCSPerformanceSubParameterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceSubParameter), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameter), columnName, matchOperator, columnValue))

            Return m_CcCSPerformanceSubParameterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As CcCSPerformanceSubParameter) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For Each Attribute As CcCSPerformanceSubParameterAttribute In objDomain.ListOfAttribute
                        Attribute.CcCSPerformanceSubParameter = objDomain
                        m_TransactionManager.AddInsert(Attribute, m_userPrincipal.Identity.Name)
                    Next


                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue

        End Function

        Public Function Update(ByVal objDomain As CcCSPerformanceSubParameter) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim listNewAttribute As List(Of CcCSPerformanceSubParameterAttribute) = objDomain.ListOfAttribute.Cast(Of CcCSPerformanceSubParameterAttribute).ToList()
                    Dim listOldAttribute As List(Of CcCSPerformanceSubParameterAttribute) = GetOldListAttribute(objDomain.ID)

                    For Each oldAttribute As CcCSPerformanceSubParameterAttribute In listOldAttribute
                        Dim newAttribute As CcCSPerformanceSubParameterAttribute = listNewAttribute.FirstOrDefault(Function(x) x.CcAttribute.ID = oldAttribute.CcAttribute.ID)

                        If Not newAttribute Is Nothing Then
                            oldAttribute.RowStatus = 0
                            oldAttribute.MinimumScore = newAttribute.MinimumScore
                            oldAttribute.CcPeriodFrom = newAttribute.CcPeriodFrom
                            oldAttribute.CcPeriodTo = newAttribute.CcPeriodTo
                            listNewAttribute.Remove(newAttribute)
                        Else
                            oldAttribute.RowStatus = -1
                        End If

                        m_TransactionManager.AddUpdate(oldAttribute, m_userPrincipal.Identity.Name)

                    Next

                    For Each newAttribute As CcCSPerformanceSubParameterAttribute In listNewAttribute
                        newAttribute.CcCSPerformanceSubParameter = objDomain
                        m_TransactionManager.AddInsert(newAttribute, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue


        End Function

        Public Sub Delete(ByVal objDomain As CcCSPerformanceSubParameter)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CcCSPerformanceSubParameterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As CcCSPerformanceSubParameter)
            Try
                m_CcCSPerformanceSubParameterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.CcCSPerformanceSubParameter) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.CcCSPerformanceSubParameter).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.CcCSPerformanceSubParameter).MarkLoaded()
                ID_Insert = InsertArg.ID
            End If
        End Sub

        Private Function GetOldListAttribute(ByVal subID As Integer) As List(Of CcCSPerformanceSubParameterAttribute)
            Dim result As New List(Of CcCSPerformanceSubParameterAttribute)
            Dim attFacade As CcCSPerformanceSubParameterAttributeFacade = New CcCSPerformanceSubParameterAttributeFacade(m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameterAttribute), "CcCSPerformanceSubParameter.ID", MatchType.Exact, subID))

            Dim arlAttribute As ArrayList = attFacade.Retrieve(criterias)

            If arlAttribute.Count > 0 Then
                result = arlAttribute.Cast(Of CcCSPerformanceSubParameterAttribute).ToList()
            End If
            Return result
        End Function
#End Region

    End Class

End Namespace

