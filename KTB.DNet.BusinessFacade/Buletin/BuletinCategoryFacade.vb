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
'// Copyright  2006
'// ---------------------
'// $History      : $
'// Generated on 1/6/2006 - 4:00:30 PM
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

Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Buletin

    Public Class BuletinCategoryFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BuletinCategoryMapper As IMapper
        Private m_BuletinDetailMapper As IMapper
        Private m_list As ArrayList
        Private m_TransactionManager As TransactionManager
       

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_list = New ArrayList
            Me.m_TransactionManager = New TransactionManager
            Me.m_BuletinCategoryMapper = MapperFactory.GetInstance.GetMapper(GetType(BuletinCategory).ToString)
            Me.m_BuletinDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BuletinDetail).ToString)
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BuletinCategory))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.BuletinDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BuletinCategory
            Return CType(m_BuletinCategoryMapper.Retrieve(ID), BuletinCategory)
        End Function

        Public Function Retrieve(ByVal Code As String) As BuletinCategory
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "Code", MatchType.Exact, Code))

            Dim BuletinCategoryColl As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(criterias)
            If (BuletinCategoryColl.Count > 0) Then
                Return CType(BuletinCategoryColl(0), BuletinCategory)
            End If
            Return New BuletinCategory
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BuletinCategoryMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Search.Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BuletinCategoryMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BuletinCategoryMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BuletinCategory As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(criterias)
            Return _BuletinCategory
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BuletinCategoryColl As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BuletinCategoryColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim BuletinCategoryColl As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BuletinCategoryColl
        End Function


        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BuletinCategoryColl As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BuletinCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BuletinCategoryColl As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BuletinCategory), columnName, matchOperator, columnValue))
            Return BuletinCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(Sort.SortDirection.ASC)) Then
                sortColl.Add(New Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), columnName, matchOperator, columnValue))

            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BuletinCategory), "Code", AggregateType.Count)
            Return CType(m_BuletinCategoryMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateCode(ByVal Code As String, ByVal topParent As Integer, ByVal parent As Integer, ByVal leveling As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "Code", MatchType.Exact, Code))
            crit.opAnd(New Criteria(GetType(BuletinCategory), "TopParent", MatchType.Exact, topParent))
            crit.opAnd(New Criteria(GetType(BuletinCategory), "Parent", MatchType.Exact, parent))
            crit.opAnd(New Criteria(GetType(BuletinCategory), "Leveling", MatchType.Exact, leveling))
            Dim agg As Aggregate = New Aggregate(GetType(BuletinCategory), "Code", AggregateType.Count)
            Return CType(m_BuletinCategoryMapper.RetrieveScalar(agg, crit), Integer)
            'Dim arr As ArrayList = m_BuletinCategoryMapper.RetrieveByCriteria(crit)
            'Return arr.Count
        End Function

        'Public Function Update(ByVal objDomain As BuletinCategory) As Integer
        '    Dim nResult As Integer = -1
        '    Try
        '        nResult = m_BuletinCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        nResult = -1
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        '    Return nResult
        'End Function

        Public Sub DeleteFromDB(ByVal objDomain As BuletinCategory)
            Try

                For Each item As BuletinDetail In objDomain.BuletinDetails
                    m_BuletinDetailMapper.Delete(item)
                Next

                objDomain.BuletinDetails.Clear()

                m_BuletinCategoryMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        'Public Function Insert(ByVal objDomain As BuletinCategory) As Integer
        '    Dim iReturn As Integer = -2
        '    Try
        '        m_BuletinCategoryMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try
        '    Return iReturn

        'End Function

        Public Function Insert(ByVal objDomain As BuletinCategory) As Integer
            Dim returnValue As Integer = -1             
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As BuletinDetail In objDomain.BuletinDetails
                        item.BuletinCategory = objDomain
                        m_TransactionManager.AddInsert(item, _user)
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.BuletinCategory) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BuletinCategory).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.BuletinCategory).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is BuletinDetail) Then
                CType(InsertArg.DomainObject, BuletinDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertBulletinCategoryWithID(ByVal objDomain As KTB.DNet.Domain.BuletinCategory) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If m_userPrincipal.Identity.Name = "" Then
                        _user = "SAP"
                    Else
                        _user = m_userPrincipal.Identity.Name
                    End If
                    For Each item As BuletinDetail In objDomain.BuletinDetails
                        item.BuletinCategory = objDomain
                        m_TransactionManager.AddInsert(item, _user)
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

            Dim objNewBulletin As BuletinCategory = Me.Retrieve(objDomain.ID)
            Dim newID As Integer = objDomain.ID
            objNewBulletin.Code = objNewBulletin.Code
            Me.Update(objNewBulletin)

            Return returnValue
        End Function


        Public Function UpdateTransaction(ByVal objDomainColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BuletinCategory In objDomainColl
                        For Each item1 As BuletinDetail In item.BuletinDetails
                            m_TransactionManager.AddUpdate(item1, m_userPrincipal.Identity.Name)
                        Next
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Function UpdateSingleDomainTransaction(ByVal objDomain As BuletinCategory) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As BuletinDetail In objDomain.BuletinDetails
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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

        Public Sub Delete(ByVal objDomain As BuletinCategory)
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BuletinCategoryMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


        Public Function Update(ByVal objDomain As KTB.DNet.Domain.BuletinCategory) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomain.BuletinDetails.Count > 0 Then
                        For Each objBuletinDetail As BuletinDetail In objDomain.BuletinDetails
                            objBuletinDetail.BuletinCategory = objDomain
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If
                            m_TransactionManager.AddUpdate(objBuletinDetail, _user)
                        Next
                    End If

                    For Each item As BuletinDetail In objDomain.BuletinDetails
                        item.BuletinCategory = objDomain
                        m_TransactionManager.AddInsert(item, _user)
                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function UpdatePassTop(ByVal objDomains As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomains.Count > 0 Then
                        For Each item As BuletinCategory In objDomains
                            ' If item.TotalQuantity > 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            '   End If

                        Next
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
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

#Region "Custom Method"

        Public Function PopulateListView(ByVal parentId As Integer, ByVal organizationType As Integer) As ArrayList
            For Each item As BuletinCategory In RetrieveByParentId(parentId, organizationType)
                m_list.Add(item)
                PopulateView(item.ID, organizationType)
            Next
            Return m_list
        End Function

        Private Sub PopulateView(ByVal parent As Integer, ByVal organizationType As Integer)
            For Each item As BuletinCategory In RetrieveByParentId(parent, organizationType)
                m_list.Add(item)
                PopulateView(item.ID, organizationType)
            Next
        End Sub

        Private Function RetrieveByParentId(ByVal parentId As Integer, ByVal organizationType As Integer) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            Dim sortColumn As String = "Leveling"
            Dim sortDirection As Sort.SortDirection = Sort.SortDirection.ASC
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Search.Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim sortColumn1 As String = "Code"
            Dim sortDirection1 As Sort.SortDirection = Sort.SortDirection.ASC
            If (Not IsNothing(sortColumn1)) And (Not IsNothing(sortDirection1)) Then
                sortColl.Add(New Search.Sort(GetType(BuletinCategory), sortColumn1, sortDirection1))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "Parent", MatchType.Exact, parentId))
            If organizationType = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(BuletinCategory), "Status", MatchType.Exact, CType(enumStatusBuletin.StatusBuletin.Aktif, Short)))
            End If
            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveByTOPParentId(ByVal parentId As Integer) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            Dim sortColumn As String = "Leveling"
            Dim sortDirection As Sort.SortDirection = Sort.SortDirection.ASC
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Search.Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "TopParent", MatchType.Exact, parentId))
            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveParentList(ByVal organizationType As Integer) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            Dim sortColumn As String = "Code"
            Dim sortDirection As Sort.SortDirection = Sort.SortDirection.ASC
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Search.Sort(GetType(BuletinCategory), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "TopParent", MatchType.Exact, 0))
            If organizationType = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(BuletinCategory), "Status", MatchType.Exact, CType(enumStatusBuletin.StatusBuletin.Aktif, Short)))
            End If
            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveSubParentList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "TopParent", MatchType.No, 0))
            Return m_BuletinCategoryMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveAllParentCategory(ByVal objBuletinCategory As BuletinCategory) As ArrayList
            Dim listParentCategory As ArrayList = New ArrayList
            While objBuletinCategory.Parent <> 0
                Dim objBulCat As BuletinCategory
                objBulCat = m_BuletinCategoryMapper.Retrieve(objBuletinCategory.Parent)
                listParentCategory.Add(objBulCat)
                objBuletinCategory = objBulCat
            End While
            Return listParentCategory
        End Function

        ''' <summary>
        ''' To get the sub parent list.
        ''' </summary>
        ''' <param name="objBuletinCategory">A BuletinCategory object, which is used to retrieve sub parent under this object.</param>
        ''' <param name="Retval">To get the sub parent list in ArrayList(BuletinCategory object).</param>
        Public Sub RetrieveAllSubParentCategory(ByVal objBuletinCategory As BuletinCategory, ByRef Retval As ArrayList)
            'Dim Retval As ArrayList = New ArrayList
            'Retval = New ArrayList

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "Parent", MatchType.Exact, objBuletinCategory.ID))
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(BuletinCategory), "Parent", MatchType.Exact, objBuletinCategory.ID))

            ' get the sub parent
            Dim listSubParentCategory As ArrayList = Me.Retrieve(criterias)

            ' if exist then do action
            If listSubParentCategory.Count > 0 Then
                For Each item As BuletinCategory In listSubParentCategory
                    ' added the sub parent category to reference variable
                    Retval.Add(item)

                    ' and do recall this methods, to check wether, there is sub parent or not
                    Me.RetrieveAllSubParentCategory(New KTB.DNet.BusinessFacade.Buletin.BuletinCategoryFacade(Me.m_userPrincipal).Retrieve(item.ID), Retval)
                Next
            End If

            'Return Retval
        End Sub

        ''' <summary>
        ''' To get the parent list up to top parent.
        ''' </summary>
        ''' <param name="objBuletinCategory">A BuletinCategory object, which is used to retrieve parent top of this object, including this object too.</param>
        ''' <param name="ListFromTop">Based on list from top parent to bottom or bottom to top.</param>
        ''' <param name="Retval">To get the parent list in ArrayList(BuletinCategory object).</param>
        Public Sub RetrieveAllParentUpToTopList(ByVal objBuletinCategory As BuletinCategory, ByVal ListFromTop As Boolean, ByRef Retval As ArrayList)

            If objBuletinCategory.Leveling > 0 Then
                If Not ListFromTop Then
                    '-- then added reference variable with objBuletinCategory
                    Retval.Add(objBuletinCategory)
                End If

                '-- then recall again this method until Leveling is 0.
                Me.RetrieveAllParentUpToTopList(New BuletinCategoryFacade(Me.m_userPrincipal).Retrieve(objBuletinCategory.Parent), ListFromTop, Retval)

                If ListFromTop Then
                    '-- then added reference variable with objBuletinCategory
                    Retval.Add(objBuletinCategory)
                End If
            End If

            'Return Retval
        End Sub

        Public Function RetrieveAllCategory(ByVal ParentID As Integer, ByVal ArlList As ArrayList) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BuletinCategory), "Parent", MatchType.Exact, ParentID))
            Dim AllCategoryID As ArrayList = Me.Retrieve(criterias)
            If AllCategoryID.Count > 0 Then
                For Each Item As BuletinCategory In AllCategoryID
                    ArlList.Add(Item)
                    Me.RetrieveAllCategory(Item.ID, ArlList)
                Next
            End If
            Return ArlList
        End Function
#End Region

    End Class

End Namespace

