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
Imports System.Data.SqlClient
Imports System.Collections.Generic

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartMasterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartMaster).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartMaster
            Return CType(m_SparePartMasterMapper.Retrieve(ID), SparePartMaster)
        End Function

        Public Function Retrieve(ByVal code As String) As SparePartMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, code))

            Dim partColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), SparePartMaster)
            End If
            Return New SparePartMaster
        End Function

        Public Function RetrieveByCodeAndType(ByVal code As String, ByVal typecode As String) As SparePartMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, code))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.Exact, typecode))
            Dim partColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), SparePartMaster)
            End If
            Return New SparePartMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(Optional ByVal companyCode As String = "") As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If companyCode.Trim.ToUpper <> "" Then
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "ProductCategory.Code", MatchType.Exact, companyCode.Trim.ToUpper))
            End If
            Dim _SparePartMaster As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias)
            Return _SparePartMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartMasterColl
        End Function

        Public Function RetrieveActiveListNonIndent(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal AccType As Short = -1, Optional ByVal intPQRHeaderID As Integer = 0) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            If AccType <> -1 Then
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "AccessoriesType", MatchType.Exact, AccType))
            Else
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
            End If
            If intPQRHeaderID > 0 Then
                Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
            End If

            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterColl
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal TypeCode As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.Exact, TypeCode))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Criterias.opAnd(New Criteria(GetType(SparePartMaster), "ProductCategory.ID", MatchType.No, 2))
            Criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))

            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return SparePartMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl)
            Return SparePartMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SparePartMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))
            Dim SparePartMasterColl As ArrayList = m_SparePartMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartMasterColl
        End Function

        Public Function RetrieveWithOneCriteriaNonIndent(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, Optional ByVal AccType As Short = -1, Optional ByVal intPQRHeaderID As Integer = 0) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            If AccType <> -1 Then
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "AccessoriesType", MatchType.Exact, AccType))
            Else
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.NotInSet, "'I','E','A'"))
            End If
            If intPQRHeaderID > 0 Then
                Dim strSQL As String = "SELECT Distinct SparePartMasterID FROM PQRPartsCode WHERE PQRHeaderID = " & intPQRHeaderID
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "ID", MatchType.InSet, "(" + strSQL + ")"))
            End If
            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))

            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal TypeCode As String) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "TypeCode", MatchType.Exact, TypeCode))
            'Start  :RemainModule-IndentPart:only active part
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ActiveStatus", MatchType.Exact, CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short)))
            'End    :RemainModule-IndentPart:only active part
            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteriaWarranty(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), "IsWarranty", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))

            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteriaExtModel(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))
            If ExtModel <> "" And Not ExtModel Is String.Empty Then
                Dim Models As String = "'" & ExtModel.Replace(";", "','") & "'"

                'criterias.opAnd(New Criteria(GetType(SparePartMaster), "ModelCode", matchOperator.Exact, ExtModel))
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "ModelCode", MatchType.InSet, "(" & Models & ")"))
            End If

            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteriaExtModelAllPages(ByVal ExtModel As String, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartMaster), columnName, matchOperator, columnValue))
            If ExtModel <> "" And Not ExtModel Is String.Empty Then
                Dim Models As String = "'" & ExtModel.Replace(";", "','") & "'"

                'criterias.opAnd(New Criteria(GetType(SparePartMaster), "ModelCode", matchOperator.Exact, ExtModel))
                criterias.opAnd(New Criteria(GetType(SparePartMaster), "ModelCode", MatchType.InSet, "(" & Models & ")"))
            End If

            Return m_SparePartMasterMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_SparePartMasterMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "SparePartMasterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SparePartMaster), "SparePartMasterCode", AggregateType.Count)
            Return CType(m_SparePartMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As SparePartMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SparePartMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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
                m_SparePartMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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
                m_SparePartMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(SparePartMaster)) Then
                CType(args.DomainObject, SparePartMaster).ID = args.ID
                CType(args.DomainObject, SparePartMaster).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(SparePartConversion)) Then
                CType(args.DomainObject, SparePartConversion).ID = args.ID
                CType(args.DomainObject, SparePartConversion).MarkLoaded()
            End If

        End Sub

        Public Function BatchUpdateWithTransactionManager(ByVal sparePartMasterList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each sparePartMaster As SparePartMaster In sparePartMasterList
                        If sparePartMaster.ID <> 0 Then
                            If (sparePartMaster.LastUpdateBy.ToLower <> "not update") Then
                                Me.m_TransactionManager.AddUpdate(sparePartMaster, m_userPrincipal.Identity.Name)
                            End If
                            sparePartMaster.MarkLoaded()
                        End If

                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = 1
                Catch ex As Exception
                    Throw ex
                    result = -1
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result

        End Function

        Public Function BatchInsertOrUpdateWithTransactionManager(ByVal sparePartMasterList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Me.SetTaskLocking()
                Try
                    For Each sparePartMaster As SparePartMaster In sparePartMasterList
                        If sparePartMaster.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(sparePartMaster, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(sparePartMaster, m_userPrincipal.Identity.Name)
                            sparePartMaster.MarkLoaded()
                        End If

                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = 1
                Catch ex As Exception
                    Throw ex
                    result = -1
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result

        End Function

        Public Function InsertWithTransactionManager(ByVal sparePartMaster As SparePartMaster, ByVal sparePartConversionList As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert vehicle Type
                    Me.m_TransactionManager.AddInsert(sparePartMaster, m_userPrincipal.Identity.Name)
                    ' add command to insert vehicle Color
                    For Each sparePartConversion As SparePartConversion In sparePartConversionList
                        'Me.m_TransactionManager.AddInsert(sparePartConversion, m_userPrincipal.Identity.Name)
                        'Me.m_TransactionManager.AddUpdate(sparePartMaster, m_userPrincipal.Identity.Name)
                        If sparePartConversion.ID = 0 Then
                            Me.m_TransactionManager.AddInsert(sparePartConversion, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(sparePartConversion, m_userPrincipal.Identity.Name)
                        End If
                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = sparePartMaster.ID
                    UpdateSparePartMasterTOP(result)
                    Return result
                Catch sqlException As SqlException
                    Throw sqlException
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return result

        End Function

        Public Function UpdateWithTransactionManager(ByVal sparePartMaster As SparePartMaster, ByVal sparePartConversionList As ArrayList) As Integer
            ' mark as loaded to prevent it loads from db
            sparePartMaster.MarkLoaded()
            For Each sparePartConversion As SparePartConversion In sparePartConversionList
                sparePartConversion.MarkLoaded()
            Next
            ' set default result
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert spare part conversion
                    For Each sparePartConversion As SparePartConversion In sparePartConversionList
                        If (sparePartConversion.ID <> 0) Then
                            If (sparePartConversion.LastUpdateBy.ToLower <> "not update") Then
                                m_TransactionManager.AddUpdate(sparePartConversion, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            m_TransactionManager.AddInsert(sparePartConversion, m_userPrincipal.Identity.Name)
                        End If

                        sparePartConversion.MarkLoaded()

                    Next
                    ' add command to update spare part master
                    If (sparePartMaster.LastUpdateBy.ToLower <> "not update") Then
                        m_TransactionManager.AddUpdate(sparePartMaster, m_userPrincipal.Identity.Name)
                    End If

                    m_TransactionManager.PerformTransaction()
                    result = sparePartMaster.ID
                    UpdateSparePartMasterTOP(result)
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveListSparepartModel(ByVal modelCode As String, Optional ByVal companyCode As String = "") As ArrayList
            Dim SpModelList As ArrayList = New SparePartMasterMapper().RetrieveSparpartModel(modelCode, companyCode)
            Return SpModelList
        End Function

        Private Sub UpdateSparePartMasterTOP(ByVal SPMID As Integer)

            Try
                Dim strSPName As String = "up_InsertSparePartMasterTOP_WS"
                Dim Param As New List(Of SqlClient.SqlParameter)

                Param.Add(New SqlClient.SqlParameter("@SparePartMasterID ", SPMID))
                Param.Add(New SqlClient.SqlParameter("@CreatedBy", m_userPrincipal.Identity.Name))

                m_SparePartMasterMapper.ExecuteSP(strSPName, New ArrayList(Param))
            Catch ex As Exception

            End Try
        End Sub
#End Region

    End Class

End Namespace



