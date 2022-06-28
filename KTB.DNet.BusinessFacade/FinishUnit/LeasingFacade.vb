
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
'// Generated on 3/2/2018 - 1:45:08 PM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class LeasingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_LeasingMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_LeasingMapper = MapperFactory.GetInstance.GetMapper(GetType(Leasing).ToString)

            Me.m_TransactionManager = New TransactionManager

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Leasing
            Return CType(m_LeasingMapper.Retrieve(ID), Leasing)
        End Function


        Public Function GetLeasing(ByVal LeasingCode As String) As KTB.DNet.Domain.Leasing
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Leasing), "LeasingCode", MatchType.Exact, LeasingCode))
                Dim poColl As ArrayList = m_LeasingMapper.RetrieveByCriteria(criterias)
                If (poColl.Count > 0) Then
                    Return CType(poColl(0), KTB.DNet.Domain.Leasing)
                End If
                Return New KTB.DNet.Domain.Leasing
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return Nothing
        End Function




        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LeasingMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LeasingMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_LeasingMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Leasing), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LeasingMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Leasing), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LeasingMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Leasing As ArrayList = m_LeasingMapper.RetrieveByCriteria(criterias)
            Return _Leasing
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LeasingColl As ArrayList = m_LeasingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LeasingColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LeasingColl As ArrayList = m_LeasingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return LeasingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LeasingColl As ArrayList = m_LeasingMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(Leasing), columnName, matchOperator, columnValue))
            Return LeasingColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Leasing), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), columnName, matchOperator, columnValue))

            Return m_LeasingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As Leasing) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_LeasingMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Leasing) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_LeasingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As Leasing)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LeasingMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As Leasing)
            Try
                m_LeasingMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function ValidateLeasing(ByVal Leasing As Leasing) As Leasing
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(KTB.DNET.Domain.Leasing), "LeasingCode", MatchType.Exact, Leasing.LeasingCode))
            Dim arlLeasing As ArrayList = m_LeasingMapper.RetrieveByCriteria(criteria)
            If arlLeasing.Count > 0 Then
                Return CType(arlLeasing(0), Leasing)
            End If
            Return Nothing

        End Function


        Public Function InsertFromWebSevice(ByVal Leasing As Leasing) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    Dim Leasing_old As Leasing = ValidateLeasing(Leasing)
                    If IsNothing(Leasing_old) Then

                        m_TransactionManager.AddInsert(Leasing, m_userPrincipal.Identity.Name)
                    Else
                        Leasing_old.LeasingGroupName = Leasing.LeasingGroupName
                        Leasing_old.LeasingCode = Leasing.LeasingCode
                        Leasing_old.LeasingName = Leasing.LeasingName
                        Leasing_old.Alamat = Leasing.Alamat
                        Leasing_old.PostalCode = Leasing.PostalCode
                        Leasing_old.City = Leasing.City
                        Leasing_old.Province = Leasing.Province
                        Leasing_old.ContactPerson = Leasing.ContactPerson
                        Leasing_old.PhoneNo = Leasing.PhoneNo
                        Leasing_old.HP = Leasing.HP
                        Leasing_old.Email = Leasing.Email

                        m_TransactionManager.AddUpdate(Leasing_old, m_userPrincipal.Identity.Name)

                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    'Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

    End Class

End Namespace

