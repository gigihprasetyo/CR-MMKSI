 
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
'// Generated on 11/09/2005 - 9:04:49 AM
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
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class BOMMaintenanceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BOMMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_BOMMapper = MapperFactory.GetInstance.GetMapper(GetType(HeaderBOM).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.HeaderBOM))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.DetailBOM))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As HeaderBOM
            Return CType(m_BOMMapper.Retrieve(ID), HeaderBOM)
        End Function

        Public Function Retrieve(ByVal Code As String) As HeaderBOM
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(HeaderBOM), "EquipmentMaster.EquipmentNumber", MatchType.Exact, Code))

            Dim HeaderBOMColl As ArrayList = m_BOMMapper.RetrieveByCriteria(criterias)
            If (HeaderBOMColl.Count > 0) Then
                Return CType(HeaderBOMColl(0), HeaderBOM)
            End If
            Return New HeaderBOM
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BOMMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BOMMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BOMMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(HeaderBOM), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BOMMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(HeaderBOM), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BOMMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BOMHeaders As ArrayList = m_BOMMapper.RetrieveByCriteria(criterias)
            Return _BOMHeaders
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BOMHeaderColl As ArrayList = m_BOMMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BOMHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BOMHeaderColl As ArrayList = m_BOMMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BOMHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BOMHeaderColl As ArrayList = m_BOMMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(HeaderBOM), columnName, matchOperator, columnValue))
            Return BOMHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(HeaderBOM), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), columnName, matchOperator, columnValue))

            Return m_BOMMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.HeaderBOM) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                 
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As DetailBOM In objDomain.DetailBOMs
                        item.HeaderBOM = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
                    End If
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.HeaderBOM) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim eqNumber As String = objDomain.EquipmentMaster.EquipmentNumber
                    If Not IsBOMUsedTransaction(eqNumber) Then
                        Dim oldBOM As HeaderBOM = Retrieve(eqNumber)
                        If oldBOM.ID > 0 Then
                            For Each item As DetailBOM In oldBOM.DetailBOMs
                                m_TransactionManager.AddDelete(item)
                            Next
                            m_TransactionManager.AddDelete(oldBOM)
                        End If

                        m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                        For Each item As DetailBOM In objDomain.DetailBOMs
                            item.HeaderBOM = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                        If performTransaction Then
                            m_TransactionManager.PerformTransaction()
                            returnValue = objDomain.ID
                        End If
                    Else
                        Throw New Exception("Error Uplpad, BOM " & eqNumber & " already used in Transaction ")
                    End If
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

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.HeaderBOM) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.HeaderBOM).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.HeaderBOM).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DetailBOM) Then

                CType(InsertArg.DomainObject, DetailBOM).ID = InsertArg.ID

            End If

        End Sub


#End Region

#Region "Custom Method"

        Public Function IsBOMUsedTransaction(ByVal equipmentNumber As String) As Boolean
            If New HelperFacade(System.Threading.Thread.CurrentPrincipal, GetType(EquipmentSalesDetail)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(EquipmentSalesDetail), equipmentNumber), CreateAggreateForCheckRecord(GetType(EquipmentSalesDetail))) Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
       ByVal equipmentNumber As String) As CriteriaComposite
            Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(DomainType, "EquipmentMaster.EquipmentNumber", MatchType.Exact, equipmentNumber))
            Return criterias
        End Function

        Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
            Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
            Return aggregates
        End Function

#End Region

    End Class

End Namespace
