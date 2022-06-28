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
Namespace KTB.DNet.BusinessFacade.General
    Public Class AnnouncementDocFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AnnouncementDocMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_AnnouncementDocMapper = MapperFactory.GetInstance.GetMapper(GetType(AnnouncementDoc).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AnnouncementDoc
            Return CType(m_AnnouncementDocMapper.Retrieve(ID), AnnouncementDoc)
        End Function

        Public Function Retrieve(ByVal Code As String) As AnnouncementDoc
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AnnouncementDoc), "Code", MatchType.Exact, Code))

            Dim AnnouncementDocColl As ArrayList = m_AnnouncementDocMapper.RetrieveByCriteria(criterias)
            If (AnnouncementDocColl.Count > 0) Then
                Return CType(AnnouncementDocColl(0), AnnouncementDoc)
            End If
            Return New AnnouncementDoc
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AnnouncementDocMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AnnouncementDocMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AnnouncementDocMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnouncementDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AnnouncementDocMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnouncementDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AnnouncementDocMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AnnouncementDoc As ArrayList = m_AnnouncementDocMapper.RetrieveByCriteria(criterias)
            Return _AnnouncementDoc
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AnnouncementDocColl As ArrayList = m_AnnouncementDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AnnouncementDocColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnouncementDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AnnouncementDocColl As ArrayList = m_AnnouncementDocMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AnnouncementDocColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AnnouncementDocColl As ArrayList = m_AnnouncementDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return AnnouncementDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AnnouncementDocColl As ArrayList = m_AnnouncementDocMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AnnouncementDoc), columnName, matchOperator, columnValue))
            Return AnnouncementDocColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnouncementDoc), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), columnName, matchOperator, columnValue))

            Return m_AnnouncementDocMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is AnnouncementDoc) Then
                CType(InsertArg.DomainObject, AnnouncementDoc).ID = InsertArg.ID
                CType(InsertArg.DomainObject, AnnouncementDoc).MarkLoaded()
            End If
        End Sub


        Public Function InsertTransaction(ByVal objAnnouncementDoc As AnnouncementDoc) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objAnnouncementDoc, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objAnnouncementDoc.ID
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

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(AnnouncementDoc), "Code", AggregateType.Count)
            Return CType(m_AnnouncementDocMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateManualName(ByVal manualName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnouncementDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crit.opAnd(new Criteria
        End Function

        Public Function Update(ByVal objDomain As AnnouncementDoc) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AnnouncementDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As AnnouncementDoc) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = DBRowStatus.Deleted
                nResult = m_AnnouncementDocMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

#End Region

#Region "Custom Method"

#End Region
    End Class
End Namespace

