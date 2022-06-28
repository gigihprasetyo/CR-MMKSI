#Region "imports library namespace"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
#End Region

#Region "imports custom namespace"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class SPKChassisFacade
        Inherits AbstractFacade

#Region "private variables"
        Private m_SPKChassisMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPKChassisMapper = MapperFactory.GetInstance().GetMapper(GetType(SPKChassis).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKChassis))
        End Sub
#End Region

#Region "retrieve"
        Public Function Retrieve(ByVal ID As Integer) As SPKChassis
            Return CType(m_SPKChassisMapper.Retrieve(ID), SPKChassis)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKChassisMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKChassisMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKChassisMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPKChassisMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKChassisMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKChassisMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _arrayResult As ArrayList = m_SPKChassisMapper.RetrieveByCriteria(criterias)
            Return _arrayResult
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKChassisColl As ArrayList = m_SPKChassisMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPKChassisColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPKChassisColl As ArrayList = m_SPKChassisMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPKChassisColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_SPKChassisMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKChassisMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SPLColl As ArrayList = m_SPKChassisMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPLColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKChassis), columnName, matchOperator, columnValue))
            Dim SPKChassisColl As ArrayList = m_SPKChassisMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPKChassisColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKChassis), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), columnName, matchOperator, columnValue))

            Return m_SPKChassisMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByChassisID(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKChassis), "ChassisMaster.ID", MatchType.Exact, ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(SPKChassis), "MatchingDate", Sort.SortDirection.DESC))
            sortColl.Add(New Sort(GetType(SPKChassis), "CreatedTime", Sort.SortDirection.DESC))

            Dim List As ArrayList
            List = Retrieve(criterias, sortColl)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetriveByListSPKDetailID(ByVal spkDetailIDList As String)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKChassis), "SPKDetail.ID", MatchType.InSet, spkDetailIDList))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(SPKChassis), "MatchingDate", Sort.SortDirection.DESC))
            sortColl.Add(New Sort(GetType(SPKChassis), "CreatedTime", Sort.SortDirection.DESC))

            Dim List As ArrayList
            List = Retrieve(criterias, sortColl)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveBySPKDetailID(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKChassis), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKChassis), "SPKDetail.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_SPKChassisMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveTotalMathedSPKDetail(ByVal spkDetailID As Integer) As Integer
            Dim spName As String = "up_RetrieveTotalMatchedSPKDetailSPKChassis"
            Dim param As New ArrayList
            Dim sl As New SqlClient.SqlParameter
            sl.ParameterName = "@spkDetailID"

            sl.Value = spkDetailID
            param.Add(sl)
            Dim listOfMatchedSPKChassis As ArrayList = m_SPKChassisMapper.RetrieveSP(spName, param)
            If Not IsNothing(listOfMatchedSPKChassis) Then
                Return listOfMatchedSPKChassis.Count
            End If
            Return 0

        End Function

#End Region

#Region "trans/other public method"
        'aa
#Region "Insert"
        Public Function Insert(ByVal objDomain As SPKChassis) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SPKChassisMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function
#End Region
#Region "Update"
        Public Function Update(ByVal objDomain As SPKChassis) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPKChassisMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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
#Region "Delete"
        Public Sub Delete(ByVal objDomain As SPKChassis)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPKChassisMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SPKChassis) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SPKChassisMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function
#End Region

#End Region

#Region "custom method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SPKChassis) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKChassis).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKChassis).MarkLoaded()
            End If
        End Sub

        Public Function Insert(ByVal objDomain As SPKChassis, ByVal objOldDomain As SPKChassis) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddUpdate(objOldDomain, _user)
                m_TransactionManager.AddInsert(objDomain, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function
#End Region


    End Class

End Namespace

