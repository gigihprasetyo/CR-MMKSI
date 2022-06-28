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
'// Generated on 8/2/2007 - 12:59:07 PM
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

Namespace KTB.DNET.BusinessFacade.IndentPartEquipment


    Public Class EstimationEquipDetailFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EstimationEquipDetailMapper As IMapper
        Private m_EstimationEquipHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EstimationEquipDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(EstimationEquipDetail).ToString)
            Me.m_EstimationEquipHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(EstimationEquipHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EstimationEquipDetail))
            Me.DomainTypeCollection.Add(GetType(IndentPartDetail))


        End Sub

#End Region

#Region "Retrieve"

        'Public Function RetrievePOHeader(ByVal ID As Integer) As IndentPartPOHeader
        '    Return CType(m_IndentPartPOHeaderMapper.Retrieve(ID), IndentPartPOHeader)
        'End Function

        Public Function RetrieveByEstimationNumberAndPartNumber(ByVal estimationNumber As String, ByVal partNumber As String) As EstimationEquipDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "EstimationNumber", MatchType.Exact, estimationNumber))
            Dim objHeaderColl As ArrayList = m_EstimationEquipHeaderMapper.RetrieveByCriteria(criterias)

            If (Not IsNothing(objHeaderColl)) Then
                Dim objHeader As EstimationEquipHeader = CType(objHeaderColl(0), EstimationEquipHeader)
                Dim intSparePartMasterID As Integer = 0
                For Each objDetail As EstimationEquipDetail In objHeader.EstimationEquipDetails
                    If (objDetail.SparePartMaster.PartNumber = partNumber) Then
                        intSparePartMasterID = objDetail.ID
                        Return objDetail
                    End If
                Next
            End If

            Return Nothing
        End Function

        Public Function Retrieve(ByVal ID As Integer) As EstimationEquipDetail
            Return CType(m_EstimationEquipDetailMapper.Retrieve(ID), EstimationEquipDetail)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EstimationEquipDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EstimationEquipDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EstimationEquipDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EstimationEquipDetail As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias)
            Return _EstimationEquipDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EstimationEquipDetailColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EstimationEquipDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EstimationEquipDetail), SortColumn, sortDirection))

            Dim EstimationEquipDetailColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EstimationEquipDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EstimationEquipDetailColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EstimationEquipDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EstimationEquipDetailColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EstimationEquipDetail), columnName, matchOperator, columnValue))
            Return EstimationEquipDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), columnName, matchOperator, columnValue))

            Return m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "EstimationEquipDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EstimationEquipDetail), "EstimationEquipDetailCode", AggregateType.Count)
            Return CType(m_EstimationEquipDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As EstimationEquipDetail) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EstimationEquipDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal arlIPH As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlIPH.Count > 0 Then
                        For Each objIPHH As EstimationEquipDetail In arlIPH
                            m_TransactionManager.AddUpdate(objIPHH, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function Update(ByVal objDomain As EstimationEquipDetail) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EstimationEquipDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As EstimationEquipDetail) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_EstimationEquipDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EstimationEquipDetail) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EstimationEquipDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteEstimationEquipDetail(ByVal objDomain As KTB.DNet.Domain.EstimationEquipDetail, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As EstimationEquipDetail In arrIPDetail
                            m_TransactionManager.AddDelete(objIPDetail)
                        Next
                    End If

                    m_TransactionManager.AddDelete(objDomain)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is EstimationEquipDetail) Then
                CType(InsertArg.DomainObject, EstimationEquipDetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EstimationEquipDetail).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EstimationEquipDetail) Then
                CType(InsertArg.DomainObject, EstimationEquipDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"
        Public Function ValidateItem(ByVal nPOID As Integer, ByVal strPartNumber As String) As IndentPartDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID ", MatchType.Exact, nPOID))
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "SparePartMaster.PartNumber", MatchType.Exact, strPartNumber))
            Dim arlIPDetail As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias)
            If arlIPDetail.Count > 0 Then
                Return CType(arlIPDetail(0), IndentPartDetail)

            Else
                Return Nothing
            End If
        End Function

        Public Function RetrieveByHID(ByVal EquipHID As Integer) As EstimationEquipDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipDetail), "EstimationEquipHeader.ID", MatchType.Exact, EquipHID))

            Dim EstimationEquipDetailColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias)
            If (EstimationEquipDetailColl.Count > 0) Then
                Return CType(EstimationEquipDetailColl(0), EstimationEquipDetail)
            End If
            Return New EstimationEquipDetail
        End Function

        Public Function RetrieveByHIDList(ByVal EquipHID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipDetail), "EstimationEquipHeader.ID", MatchType.Exact, EquipHID))

            Dim EstimationEquipDetailColl As ArrayList = m_EstimationEquipDetailMapper.RetrieveByCriteria(criterias)
            Return EstimationEquipDetailColl
        End Function

#End Region

    End Class

End Namespace

