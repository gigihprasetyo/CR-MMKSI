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
'// Generated on 9/26/2005 - 2:38:25 PM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.DataMapper.Framework
Imports KTB.DNET.DataMapper
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartMasterTOPFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartMasterTOPMapper As IMapper

        '  Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartMasterTOPMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartMasterTOP).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartMasterTOP
            Return CType(m_SparePartMasterTOPMapper.Retrieve(ID), SparePartMasterTOP)
        End Function

        Public Function RetrieveBySPIDandTypeTOPID(ByVal spID As Integer, ByVal TypeTOPId As Integer) As SparePartMasterTOP
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "SparePartMaster.ID", MatchType.Exact, spID))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "SparePartPOTypeTOP.ID", MatchType.Exact, TypeTOPId))

            Dim partColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), SparePartMasterTOP)
            End If
            Return New SparePartMasterTOP
        End Function

        Public Function Retrieve(ByVal code As String) As SparePartMasterTOP
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "PartNumber", MatchType.Exact, code))

            Dim partColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), SparePartMasterTOP)
            End If
            Return New SparePartMasterTOP
        End Function

        Public Function RetrieveByCodeAndType(ByVal code As String, ByVal typecode As String) As SparePartMasterTOP
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "PartNumber", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "TypeCode", MatchType.Exact, typecode))
            Dim partColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), SparePartMasterTOP)
            End If
            Return New SparePartMasterTOP
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartMasterTOPMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterTOPMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterTOPMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If companyCode.Trim.ToUpper <> "" Then
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ProductCategory.Code", MatchType.Exact, companyCode.Trim.ToUpper))
            End If
            Dim _SparePartMasterTOP As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias)
            Return _SparePartMasterTOP
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveActiveListNonIndent(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal AccType As Short = -1) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            If AccType <> -1 Then
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "AccessoriesType", MatchType.Exact, AccType))
            Else
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
            End If
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterTOPColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal TypeCode As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "TypeCode", MatchType.Exact, TypeCode))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterTOPColl
        End Function



        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl)
            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), columnName, matchOperator, columnValue))
            Dim SparePartMasterTOPColl As ArrayList = m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartMasterTOPColl
        End Function

        Public Function RetrieveWithOneCriteriaNonIndent(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal AccType As Short = -1) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), columnName, matchOperator, columnValue))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            If AccType <> -1 Then
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "AccessoriesType", MatchType.Exact, AccType))
            Else
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
            End If
            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), columnName, matchOperator, columnValue))

            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal TypeCode As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), columnName, matchOperator, columnValue))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "TypeCode", MatchType.Exact, TypeCode))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteriaExtModel(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), columnName, matchOperator, columnValue))
            If ExtModel <> "" And Not ExtModel Is String.Empty Then
                Dim Models As String = "'" & ExtModel.Replace(";", "','") & "'"

                'criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ModelCode", matchOperator.Exact, ExtModel))
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ModelCode", MatchType.InSet, "(" & Models & ")"))
            End If

            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteriaExtModelAllPages(ByVal ExtModel As String, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMasterTOP), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), columnName, matchOperator, columnValue))
            If ExtModel <> "" And Not ExtModel Is String.Empty Then
                Dim Models As String = "'" & ExtModel.Replace(";", "','") & "'"

                'criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ModelCode", matchOperator.Exact, ExtModel))
                criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "ModelCode", MatchType.InSet, "(" & Models & ")"))
            End If

            Return m_SparePartMasterTOPMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_SparePartMasterTOPMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMasterTOP), "SparePartMasterTOPCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartMasterTOP), "SparePartMasterTOPCode", AggregateType.Count)
            Return CType(m_SparePartMasterTOPMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartMasterTOP) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SparePartMasterTOPMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartMasterTOP) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartMasterTOPMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPO)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartMasterTOPMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SparePartPO)
            Try
                m_SparePartMasterTOPMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


#End Region

#Region "Custom Method"
        'Public Function RetrieveListSparepartModel(ByVal modelCode As String, Optional ByVal companyCode As String = "") As ArrayList
        '    Dim SpModelList As ArrayList = New SparePartMasterTOPMapper().RetrieveSparpartModel(modelCode, companyCode)
        '    Return SpModelList
        'End Function
#End Region

    End Class

End Namespace



