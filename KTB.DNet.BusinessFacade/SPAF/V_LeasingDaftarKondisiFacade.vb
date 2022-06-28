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
'// PURPOSE       : Facade for Page Leasing - Daftar Kondisi.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2009 
'// ---------------------
'// $History      : $
'// Generated on 8/14/2009 - 11:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.SPAF
    Public Class V_LeasingDaftarKondisiFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_V_LeasingDaftarKondisiMapper As IMapper
        Private m_ConditionMaster As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_LeasingDaftarKondisiMapper = MapperFactory.GetInstance().GetMapper(GetType(V_LeasingDaftarKondisi).ToString)
            Me.m_ConditionMaster = MapperFactory.GetInstance().GetMapper(GetType(ConditionMaster).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As V_LeasingDaftarKondisi
            Return CType(m_V_LeasingDaftarKondisiMapper.Retrieve(ID), V_LeasingDaftarKondisi)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_LeasingDaftarKondisiMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarKondisiMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarKondisiMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _V_LeasingDaftarKondisi As ArrayList = m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias)
            Return _V_LeasingDaftarKondisi
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_LeasingDaftarKondisi), columnName, matchOperator, columnValue))
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarKondisi), columnName, matchOperator, columnValue))

            Return m_V_LeasingDaftarKondisiMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objCollection As ArrayList) As Integer
            Dim iReturn As Integer = -1
            If MyBase.IsTaskFree Then
                Try
                    MyBase.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    For Each objDomain As V_LeasingDaftarKondisi In objCollection
                        Dim objMaster As ConditionMaster = MappingToConditionMaster(objDomain)
                        objMaster.AssistFee = objMaster.RetailPrice * (objMaster.SPAF / 100)
                        If isExist(objMaster) Then
                            m_TransactionManager.AddUpdate(objMaster, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(objMaster, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objCollection.Count
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    MyBase.RemoveTaskLocking()
                End Try
            End If
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As V_LeasingDaftarKondisi) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ConditionMaster.Update(MappingToConditionMaster(objDomain), m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As V_LeasingDaftarKondisi)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_ConditionMaster.Update(MappingToConditionMaster(objDomain), m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As V_LeasingDaftarKondisi) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_ConditionMaster.Delete(MappingToConditionMaster(objDomain))
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"
        Public Function isExist(ByVal objDomain As ConditionMaster) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ConditionMaster), "VechileType.ID", MatchType.Exact, objDomain.VechileType.ID))
            criterias.opAnd(New Criteria(GetType(ConditionMaster), "ValidFrom", MatchType.Exact, objDomain.ValidFrom))
            Dim _ConditionMaster As ArrayList = m_ConditionMaster.RetrieveByCriteria(criterias)

            If _ConditionMaster.Count > 0 Then
                Return True
            Else
                Return False
            End If

        End Function
        Private Function MappingToConditionMaster(ByVal objDomain As V_LeasingDaftarKondisi) As ConditionMaster
            Dim objRealDomain As New ConditionMaster
            objRealDomain.AssistFee = objDomain.AssistFee
            objRealDomain.BasePrice = objDomain.BasePrice
            objRealDomain.CreatedBy = objDomain.CreatedBy
            objRealDomain.CreatedTime = objDomain.CreatedTime
            objRealDomain.DocumentType = objDomain.DocumentType
            objRealDomain.ErrorMessage = objDomain.ErrorMessage
            objRealDomain.ID = objDomain.ID
            objRealDomain.LastUpdateBy = objDomain.LastUpdateBy
            objRealDomain.LastUpdateTime = objDomain.LastUpdateTime
            objRealDomain.PPhPercent = objDomain.PPhPercent
            objRealDomain.RetailPrice = objDomain.RetailPrice
            objRealDomain.RowStatus = objDomain.RowStatus
            objRealDomain.SPAF = objDomain.SPAF
            objRealDomain.Subsidi = objDomain.Subsidi
            objRealDomain.ValidFrom = objDomain.ValidFrom
            objRealDomain.VechileType = objDomain.VechileType
            Return objRealDomain
        End Function
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
