
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 15/05/2019 - 7:54:46
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
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitEventProposalDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitEventProposalDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitEventProposalDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitEventProposalDetail).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(BabitEventProposalDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitEventProposalDetail
            Return CType(m_BabitEventProposalDetailMapper.Retrieve(ID), BabitEventProposalDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitEventProposalDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitEventProposalDetailCode", MatchType.Exact, Code))

            Dim BabitEventProposalDetailColl As ArrayList = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias)
            If (BabitEventProposalDetailColl.Count > 0) Then
                Return CType(BabitEventProposalDetailColl(0), BabitEventProposalDetail)
            End If
            Return New BabitEventProposalDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitEventProposalDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventProposalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitEventProposalDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventProposalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitEventProposalDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitEventProposalDetail As ArrayList = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias)
            Return _BabitEventProposalDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitEventProposalDetailColl As ArrayList = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitEventProposalDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitEventProposalDetail), SortColumn, sortDirection))
            Dim BabitEventProposalDetailColl As ArrayList = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitEventProposalDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitEventProposalDetailColl As ArrayList = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitEventProposalDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitEventProposalDetailColl As ArrayList = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitEventProposalDetail), columnName, matchOperator, columnValue))
            Return BabitEventProposalDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitEventProposalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), columnName, matchOperator, columnValue))

            Return m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "BabitEventProposalDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitEventProposalDetail), "BabitEventProposalDetailCode", AggregateType.Count)
            Return CType(m_BabitEventProposalDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitEventProposalDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitEventProposalDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitEventProposalDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitEventProposalDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitEventProposalDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitEventProposalDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitEventProposalDetail)
            Try
                m_BabitEventProposalDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveDataBabitEventProposalDetail(ByVal intBabitEventProposalHeaderID As Integer, ByVal strTypeCode As String, ByVal strCategory As String, ByVal strValueCode As String) As ArrayList
            Dim strSql As String = String.Empty
            strSql = "select ID from BabitParameterDetail "
            strSql += "where BabitParameterHeaderID in ( "
            strSql += "select ID from BabitParameterHeader "
            strSql += "where BabitMasterEventTypeID in ( "
            strSql += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "') "
            strSql += "and ParameterCategory in ( "
            strSql += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode= '" & strValueCode & "') "
            strSql += ") "

            Dim arlEventProposalDtl As New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitEventProposalHeader.ID", MatchType.Exact, intBabitEventProposalHeaderID))
            criterias.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(BabitEventProposalDetail), "BabitParameterDetail.BabitParameterHeader.Status", MatchType.Exact, 1))
            arlEventProposalDtl = m_BabitEventProposalDetailMapper.RetrieveByCriteria(criterias)

            Return arlEventProposalDtl
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is BabitEventProposalHeader) Then
                CType(InsertArg.DomainObject, BabitEventProposalHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, BabitEventProposalHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is BabitEventProposalDetail) Then
                CType(InsertArg.DomainObject, BabitEventProposalDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objBabitEventProposalHeader As BabitEventProposalHeader, ByVal arrBabitEventProposalDetail As ArrayList, ByVal arlEventProposalDoc As ArrayList, ByVal arlEventProposalAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objBabitEventProposalHeader, m_userPrincipal.Identity.Name)

                    If arlEventProposalAct.Count > 0 Then
                        For Each oActivity As BabitEventProposalDetail In arlEventProposalAct
                            oActivity.BabitEventProposalHeader = objBabitEventProposalHeader
                            m_TransactionManager.AddInsert(oActivity, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arlEventProposalDoc.Count > 0 Then
                        For Each oDocument As BabitEventProposalDocument In arlEventProposalDoc
                            oDocument.BabitEventProposalHeader = objBabitEventProposalHeader
                            m_TransactionManager.AddInsert(oDocument, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrBabitEventProposalDetail.Count > 0 Then
                        For Each oBabitEventProposalDetail As BabitEventProposalDetail In arrBabitEventProposalDetail
                            oBabitEventProposalDetail.BabitEventProposalHeader = objBabitEventProposalHeader
                            m_TransactionManager.AddInsert(oBabitEventProposalDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objBabitEventProposalHeader.ID
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

        Public Function UpdateTransaction(ByVal objBabitEventProposalHeader As BabitEventProposalHeader, ByVal arrEventProposalDetail As ArrayList, ByVal arrDeletedEventProposalDetail As ArrayList, ByVal arlEventProposalDoc As ArrayList, ByVal arlDeleteEventProposalDoc As ArrayList, ByVal arlEventProposalAct As ArrayList, ByVal arlDeleteEventProposalAct As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeletedEventProposalDetail.Count > 0 Then
                        For Each item As BabitEventProposalDetail In arrDeletedEventProposalDetail
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arrEventProposalDetail.Count > 0 Then
                        For Each item As BabitEventProposalDetail In arrEventProposalDetail
                            item.BabitEventProposalHeader = objBabitEventProposalHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arlDeleteEventProposalDoc.Count > 0 Then
                        For Each item As BabitEventProposalDocument In arlDeleteEventProposalDoc
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arlEventProposalDoc.Count > 0 Then
                        For Each item As BabitEventProposalDocument In arlEventProposalDoc
                            item.BabitEventProposalHeader = objBabitEventProposalHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arlDeleteEventProposalAct.Count > 0 Then
                        For Each item As BabitEventProposalDetail In arlDeleteEventProposalAct
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    If arlEventProposalAct.Count > 0 Then
                        For Each item As BabitEventProposalDetail In arlEventProposalAct
                            item.BabitEventProposalHeader = objBabitEventProposalHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objBabitEventProposalHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objBabitEventProposalHeader.ID
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

