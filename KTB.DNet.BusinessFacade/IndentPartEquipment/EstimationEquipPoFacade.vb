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


    Public Class EstimationEquipPOFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EstimationEquipPOMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EstimationEquipPOMapper = MapperFactory.GetInstance.GetMapper(GetType(EstimationEquipPO).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(EstimationEquipPO))
            Me.DomainTypeCollection.Add(GetType(IndentPartPO))

        End Sub

#End Region

#Region "Retrieve"

        'Public Function RetrieveBySPPODetailIDandPartNumber(ByVal SPPODetailID As Integer, ByVal partNumber As String) As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(EstimationEquipPO), "SparePartPODetail", MatchType.Exact, SPPODetailID))
        '    Dim arlResult As ArrayList = Retrieve(criterias)
        '    Dim estdetf As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(m_userPrincipal)
        '    For Each objEqpPo As EstimationEquipPO In arlResult
        '        Dim objEstDet As EstimationEquipDetail = estdetf.Retrieve(objEqpPo.ID)
        '        if (objEstDet.SparePartMaster.PartNumber =
        '    Next
        'End Function

        Public Function RetrieveEstimationNumber(ByVal arlDetail As ArrayList) As String
            Dim estnums As String = ""
            Dim arlNumber As ArrayList = New ArrayList
            For Each item As IndentPartDetail In arlDetail
                Dim arlDetail2 As ArrayList = RetrieveByIndentPartDetailID(item.ID)
                If arlDetail2.Count > 0 Then
                    Dim detail As EstimationEquipPO = arlDetail2(0)
                    Dim number As String = detail.EstimationEquipDetail.EstimationEquipHeader.EstimationNumber
                    If (Not arlNumber.Contains(number)) Then
                        arlNumber.Add(number)
                        estnums += number + ";"
                    End If
                End If
            Next
            If (estnums <> "") Then
                estnums = estnums.Substring(0, estnums.Length - 1)
            End If
            Return estnums
        End Function

        'Public Function RetrieveEstimationQuipHeader(ByVal indentDetailID As Integer) As ArrayList
        '    Dim arlHeader As ArrayList = New ArrayList
        '    Dim arlDetail As ArrayList = RetrieveByIndentPartDetailID(indentDetailID)
        '    For Each item As EstimationEquipPO In arlDetail
        '        Dim header As EstimationEquipHeader = item.EstimationEquipDetail.EstimationEquipHeader
        '        If (Not arlHeader.Contains(header)) Then
        '            arlHeader.Add(header)
        '        End If
        '    Next
        '    Return arlHeader
        'End Function

        Public Function RetrieveByIndentPartDetailID(ByVal IndentDetailID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipPO), "IndentPartDetail", MatchType.Exact, IndentDetailID))
            Return Retrieve(criterias)
        End Function

        Public Function RetrieveByEstimationEquipDetailID(ByVal EstimationEquipDetailID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipPO), "EstimationEquipDetailID", MatchType.Exact, EstimationEquipDetailID))
            Return Retrieve(criterias)
        End Function

        Public Function RetrieveByEstimationEquipDetailIDIndentPODetailID(ByVal objEstimationEquipDetail As EstimationEquipDetail, ByVal IndentPartDetailID As Integer) As EstimationEquipPO
            For Each objEqpPO As EstimationEquipPO In objEstimationEquipDetail.EstimationEquipPO
                If (objEqpPO.IndentPartDetail.ID = IndentPartDetailID) Then
                    Return objEqpPO
                End If
            Next
            Return Nothing
        End Function

        Public Function RetrieveByIndentPartDetailIDEstimationEquipHeaderID(ByVal objIndentPartDetail As IndentPartDetail, ByVal intEstHId As Integer) As EstimationEquipPO
            Dim arlEstPo As ArrayList = RetrieveByIndentPartDetailID(objIndentPartDetail.ID)
            For Each objEqpPo As EstimationEquipPO In arlEstPo
                If (objEqpPo.EstimationEquipDetail.EstimationEquipHeader.ID = intEstHId) Then
                    Return objEqpPo
                End If
            Next
            Return Nothing
        End Function

        Public Function Retrieve(ByVal ID As Integer) As EstimationEquipPO
            Return CType(m_EstimationEquipPOMapper.Retrieve(ID), EstimationEquipPO)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EstimationEquipPOMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EstimationEquipPOMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EstimationEquipPOMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EstimationEquipPOMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EstimationEquipPOMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EstimationEquipPO As ArrayList = m_EstimationEquipPOMapper.RetrieveByCriteria(criterias)
            Return _EstimationEquipPO
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EstimationEquipPOColl As ArrayList = m_EstimationEquipPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EstimationEquipPOColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_EstimationEquipPOMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(EstimationEquipPO), SortColumn, sortDirection))

            Dim EstimationEquipPOColl As ArrayList = m_EstimationEquipPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EstimationEquipPOColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EstimationEquipPOColl As ArrayList = m_EstimationEquipPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EstimationEquipPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EstimationEquipPOColl As ArrayList = m_EstimationEquipPOMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EstimationEquipPO), columnName, matchOperator, columnValue))
            Return EstimationEquipPOColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EstimationEquipPO), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), columnName, matchOperator, columnValue))

            Return m_EstimationEquipPOMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As EstimationEquipPO) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_EstimationEquipPOMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
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
                        For Each objIPHH As EstimationEquipPO In arlIPH
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

        Public Function Update(ByVal objDomain As EstimationEquipPO) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EstimationEquipPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As EstimationEquipPO) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_EstimationEquipPOMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteFromDB(ByVal objDomain As EstimationEquipPO) As Integer
            Dim nResult As Integer = -1
            Try
                Return m_EstimationEquipPOMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function DeleteEstimationEquipPO(ByVal objDomain As KTB.DNet.Domain.EstimationEquipPO, ByVal arrIPPO As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPPO.Count > 0 Then
                        For Each objIPPO As EstimationEquipPO In arrIPPO
                            m_TransactionManager.AddDelete(objIPPO)
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

            If (TypeOf InsertArg.DomainObject Is EstimationEquipPO) Then
                CType(InsertArg.DomainObject, EstimationEquipPO).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EstimationEquipPO).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is EstimationEquipPO) Then
                CType(InsertArg.DomainObject, EstimationEquipPO).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

