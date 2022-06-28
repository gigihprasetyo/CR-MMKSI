
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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 23/06/2020 - 10:24:28
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

    Public Class DiscountProposalProgramRegulerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DiscountProposalProgramRegulerMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DiscountProposalProgramRegulerMapper = MapperFactory.GetInstance.GetMapper(GetType(DiscountProposalProgramReguler).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DiscountProposalProgramReguler))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DiscountProposalProgramReguler
            Return CType(m_DiscountProposalProgramRegulerMapper.Retrieve(ID), DiscountProposalProgramReguler)
        End Function

        Public Function Retrieve(ByVal Code As String) As DiscountProposalProgramReguler
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "DiscountProposalProgramRegulerCode", MatchType.Exact, Code))

            Dim DiscountProposalProgramRegulerColl As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias)
            If (DiscountProposalProgramRegulerColl.Count > 0) Then
                Return CType(DiscountProposalProgramRegulerColl(0), DiscountProposalProgramReguler)
            End If
            Return New DiscountProposalProgramReguler
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DiscountProposalProgramRegulerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalProgramReguler), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalProgramRegulerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalProgramReguler), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalProgramRegulerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DiscountProposalProgramReguler As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias)
            Return _DiscountProposalProgramReguler
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalProgramReguler), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim DiscountProposalProgramRegulerColl As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DiscountProposalProgramRegulerColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalProgramRegulerColl As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DiscountProposalProgramRegulerColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DiscountProposalProgramReguler), SortColumn, sortDirection))
            Dim DiscountProposalProgramRegulerColl As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DiscountProposalProgramRegulerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DiscountProposalProgramRegulerColl As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DiscountProposalProgramRegulerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalProgramRegulerColl As ArrayList = m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), columnName, matchOperator, columnValue))
            Return DiscountProposalProgramRegulerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalProgramReguler), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), columnName, matchOperator, columnValue))

            Return m_DiscountProposalProgramRegulerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "DiscountProposalProgramRegulerCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DiscountProposalProgramReguler), "DiscountProposalProgramRegulerCode", AggregateType.Count)
            Return CType(m_DiscountProposalProgramRegulerMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DiscountProposalProgramReguler) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DiscountProposalProgramRegulerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DiscountProposalProgramReguler) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DiscountProposalProgramRegulerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DiscountProposalProgramReguler)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DiscountProposalProgramRegulerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DiscountProposalProgramReguler)
            Try
                m_DiscountProposalProgramRegulerMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function SearchByCriteria(ByVal kategoriProgram As Integer, ByVal kategoriKendaraan As Integer, ByVal subKategoriKendaraan As Integer, ByVal type As Integer) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim dtbs As New DataSet
            Dim strCommand As String = "SELECT a.ID as [ID], " _
                                       & "b.ParameterName [KategoriProgram], " _
                                       & "f.Name [Model], " _
                                       & "c.Description [Tipe], " _
                                       & "a.AssyYear [TahunPerakitan], " _
                                       & "a.ValidFrom [PeriodeMulaiBerlaku], " _
                                       & "a.DiscountAmount [JumlahDiskon] " _
                                       & "FROM DiscountProposalProgramReguler a " _
                                       & "JOIN DiscountProposalParameter b on b.ID = a.DiscountProposalParameterID and b.rowstatus = 0 " _
                                       & "JOIN VechileType c on c.ID = a.VechileTypeID and c.RowStatus = 0 " _
                                       & "JOIN VechileModel d on d.ID = c.ModelID and d.RowStatus = 0 " _
                                       & "JOIN SubCategoryVehicleToModel e on e.VechileModelID = d.ID and e.RowStatus = 0 " _
                                       & "JOIN SubCategoryVehicle f on f.ID = e.SubCategoryVehicleID and f.RowStatus = 0 " _
                                       & "JOIN Category g on g.ID = c.CategoryID " _
                                       & "WHERE " _
                                       & "a.rowstatus = 0 " _
                                       & If(kategoriProgram = -1, "", String.Format("and a.DiscountProposalParameterID={0} ", kategoriProgram)) _
                                       & If(kategoriKendaraan = -1, "", String.Format("and g.ID={0} ", kategoriKendaraan)) _
                                       & If(subKategoriKendaraan = -1, "", String.Format("and f.ID={0} ", subKategoriKendaraan)) _
                                       & If(type = -1, "", String.Format("and c.ID={0}", type)) _
                                       '& "and g.CategoryCode = 'PC' " _
            '& "and f.name = 'XPANDER' " _
            '& "and c.description = 'XPANDER 1.5L SPORT-L (4X2) M/T (NH20/NH21)' "

            dtbs = m_DiscountProposalProgramRegulerMapper.RetrieveDataSet(strCommand)
            For Each dr As DataRow In dtbs.Tables(0).Rows
                result.Add(dr)
            Next
            Return result
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is DiscountProposalProgramReguler) Then
                CType(InsertArg.DomainObject, DiscountProposalProgramReguler).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DiscountProposalProgramReguler).MarkLoaded()
            End If
        End Sub

        Public Function InsertTransaction(ByVal arrDiscountProposalProgramReguler As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If Not IsNothing(arrDiscountProposalProgramReguler) Then
                        If arrDiscountProposalProgramReguler.Count > 0 Then
                            For Each oDiscountProposalProgramReguler As DiscountProposalProgramReguler In arrDiscountProposalProgramReguler
                                m_TransactionManager.AddInsert(oDiscountProposalProgramReguler, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 7

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

        Function UpdateTransaction(arrDiscountProposalProgramReguler As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrDiscountProposalProgramReguler) Then
                        If arrDiscountProposalProgramReguler.Count > 0 Then
                            For Each item As DiscountProposalProgramReguler In arrDiscountProposalProgramReguler
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 7
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

#End Region

    End Class

End Namespace

