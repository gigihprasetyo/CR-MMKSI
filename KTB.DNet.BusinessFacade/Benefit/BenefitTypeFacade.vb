
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:49:35 AM
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
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitTypeMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitTypeMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitType).ToString)

            
            Me.m_TransactionManager = New TransactionManager
            '  AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Short) As BenefitType
            Return CType(m_BenefitTypeMapper.Retrieve(ID), BenefitType)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitType), "BenefitTypeCode", MatchType.Exact, Code))

            Dim BenefitTypeColl As ArrayList = m_BenefitTypeMapper.RetrieveByCriteria(criterias)
            If (BenefitTypeColl.Count > 0) Then
                Return CType(BenefitTypeColl(0), BenefitType)
            End If
            Return New BenefitType
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitTypeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitType), "Status", MatchType.Exact, 0))
            Dim _BenefitType As ArrayList = m_BenefitTypeMapper.RetrieveByCriteria(criterias)
            Return _BenefitType
        End Function

        Public Function RetrieveActiveList(ByVal leasingCompanyID As Integer) As ArrayList
            Dim sqlCmd As String = "select distinct b.BenefitTypeID from  BenefitMasterDetail b "
            sqlCmd += "inner join BenefitMasterHeader c on c.ID = b.BenefitMasterHeaderID "
            sqlCmd += "inner join BenefitMasterLeasing d on d.BenefitMasterDetailID = b.ID "
            sqlCmd += "inner join LeasingCompany e on e.ID = d.LeasingCompanyID "
            sqlCmd += "where 1=1 "
            sqlCmd += "and b.RowStatus = 0 "
            sqlCmd += "and c.RowStatus = 0 "
            sqlCmd += "and d.RowStatus = 0 "
            sqlCmd += "and e.RowStatus = 0 "
            sqlCmd += String.Format("and e.ID = {0}", leasingCompanyID)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitType), "LeasingBox", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(BenefitType), "ID", MatchType.InSet, "(" & sqlCmd & ")"))
            Dim _BenefitType As ArrayList = m_BenefitTypeMapper.RetrieveByCriteria(criterias)
            Return _BenefitType
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitTypeColl As ArrayList = m_BenefitTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitTypeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitTypeColl As ArrayList = m_BenefitTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitTypeColl As ArrayList = m_BenefitTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitType), columnName, matchOperator, columnValue))
            Return BenefitTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), columnName, matchOperator, columnValue))

            Return m_BenefitTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function





        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.BenefitType) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.BenefitType) As Integer
            Dim nUpdatedRow As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    nUpdatedRow = objDomain.ID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If
           
            Return nUpdatedRow
        End Function

        Public Function UpdateForDelete(ByVal objDomain As KTB.DNet.Domain.BenefitType) As Integer
            Dim nUpdatedRow As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    '  objDomain.RowStatus = -1
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    nUpdatedRow = objDomain.ID
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return nUpdatedRow
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "BenefitTypeCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitType), "BenefitTypeCode", AggregateType.Count)
            Return CType(m_BenefitTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        'Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
        '    If (TypeOf InsertArg.DomainObject Is ClaimStatusHistory) Then
        '        CType(InsertArg.DomainObject, ClaimStatusHistory).ID = InsertArg.ID
        '        CType(InsertArg.DomainObject, ClaimStatusHistory).MarkLoaded()
        '        'ElseIf (TypeOf InsertArg.DomainObject Is ClaimDetail) Then
        '        '    CType(InsertArg.DomainObject, ClaimDetail).ID = InsertArg.ID
        '    End If
        'End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

