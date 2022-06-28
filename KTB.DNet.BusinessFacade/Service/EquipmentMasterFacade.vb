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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade

    Public Class EquipmentMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_EquipmentMasterMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_EquipmentMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(EquipmentMaster).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.EquipmentMaster))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As EquipmentMaster
            Return CType(m_EquipmentMasterMapper.Retrieve(ID), EquipmentMaster)
        End Function

        Public Function Retrieve(ByVal Code As String) As EquipmentMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EquipmentMaster), "EquipmentNumber", MatchType.Exact, Code))

            Dim EquipmentMasterColl As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(criterias)
            If (EquipmentMasterColl.Count > 0) Then
                Return CType(EquipmentMasterColl(0), EquipmentMaster)
            End If
            Return New EquipmentMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EquipmentMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EquipmentMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_EquipmentMasterMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EquipmentMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipmentMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EquipmentMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EquipmentMaster As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(criterias)
            Return _EquipmentMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentMasterColl As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EquipmentMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EquipmentMasterColl As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return EquipmentMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal AccType As Short = -1) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentMasterColl As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return EquipmentMasterColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal AccType As Short = -1) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EquipmentMaster), columnName, matchOperator, columnValue))
            
            Return m_EquipmentMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EquipmentMasterColl As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return EquipmentMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EquipmentMasterColl As ArrayList = m_EquipmentMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(EquipmentMaster), columnName, matchOperator, columnValue))
            Return EquipmentMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(EquipmentMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), columnName, matchOperator, columnValue))

            Return m_EquipmentMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Insert(ByVal objDomain As EquipmentMaster)
            Dim iReturn As Integer = -2
            Try
                m_EquipmentMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Sub Update(ByVal objDomain As EquipmentMaster)
            Try
                m_EquipmentMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "EquipmentMasterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EquipmentMaster), "EquipmentMasterCode", AggregateType.Count)
            Return CType(m_EquipmentMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Sub AddEquipment(ByVal objDomain As EquipmentMaster)
            Dim value As Short

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentMaster), "EquipmentNumber", MatchType.Exact, objDomain.EquipmentNumber))
            Dim agg As Aggregate = New Aggregate(GetType(EquipmentMaster), "EquipmentNumber", AggregateType.Count)
            value = CType(m_EquipmentMasterMapper.RetrieveScalar(agg, crit), Short)

            If value = 0 Then
                Me.Insert(objDomain)
            Else
                Dim _Eqold As EquipmentMaster
                _Eqold = Me.Retrieve(objDomain.EquipmentNumber)
                _Eqold.Description = objDomain.Description
                '_Eqold.EquipmentNumber = objDomain.EquipmentNumber
                _Eqold.Kind = objDomain.Kind
                '_Eqold.PhotoFileName = objDomain.PhotoFileName
                '_Eqold.PhotoPath = objDomain.PhotoPath
                '_Eqold.Price = objDomain.Price
                _Eqold.Status = objDomain.Status
                Me.Update(_Eqold)
            End If

        End Sub

        Public Function RetrieveBOMDetail(ByVal id As Integer) As DetailBOM
            Dim m_DetailBOMMapper As IMapper

            m_DetailBOMMapper = MapperFactory.GetInstance.GetMapper(GetType(DetailBOM).ToString)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DetailBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DetailBOM), "EquipmentMaster.ID", MatchType.Exact, id))

            Dim DetailBOMColl As ArrayList = m_DetailBOMMapper.RetrieveByCriteria(criterias)
            If (DetailBOMColl.Count > 0) Then
                Return CType(DetailBOMColl(0), DetailBOM)
            End If
            Return New DetailBOM
        End Function

        Public Function CekHeaderBOM(ByVal id As Integer) As HeaderBOM
            Dim value As Short
            Dim m_HeaderBOMMapper As IMapper

            m_HeaderBOMMapper = MapperFactory.GetInstance.GetMapper(GetType(HeaderBOM).ToString)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(HeaderBOM), "EquipmentMaster.ID", MatchType.Exact, id))

            'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "EquipmentMaster.ID", MatchType.Exact, id))
            'Dim agg As Aggregate = New Aggregate(GetType(HeaderBOM), "ID", AggregateType.Count)
            'value = CType(m_HeaderBOMMapper.RetrieveScalar(agg, crit), Short)
            Dim HeaderColl As ArrayList = m_HeaderBOMMapper.RetrieveByCriteria(criterias)
            If HeaderColl.Count > 0 Then
                Return HeaderColl(0)
            Else
                Return Nothing
            End If
            'Return CType(m_HeaderBOMMapper.RetrieveByCriteria(criterias)(0), HeaderBOM)

            'Return value
        End Function

#End Region

    End Class

End Namespace
