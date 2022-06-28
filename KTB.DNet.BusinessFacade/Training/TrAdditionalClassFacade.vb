#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region


#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrAdditionalClassFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrAdditionalClassMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_TrAdditionalClassMapper = MapperFactory.GetInstance.GetMapper(GetType(TrAdditionalClass).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrAdditionalClass
            Return CType(m_TrAdditionalClassMapper.Retrieve(ID), TrAdditionalClass)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrAdditionalClass
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), "ClassCode", MatchType.Exact, Code))

            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias)
            If (TrAdditionalClassColl.Count > 0) Then
                Return CType(TrAdditionalClassColl(0), TrAdditionalClass)
            End If
            Return New TrAdditionalClass
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrAdditionalClassMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrAdditionalClassMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrAdditionalClassMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrAdditionalClassMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrAdditionalClass As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias)
            Return _TrAdditionalClass
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrAdditionalClassColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_TrAdditionalClassMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrAdditionalClassColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrAdditionalClassColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrAdditionalClassColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrAdditionalClassColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrAdditionalClassColl As ArrayList = m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrAdditionalClass), columnName, matchOperator, columnValue))
            Return TrAdditionalClassColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrAdditionalClass), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(TrAdditionalClass), columnName, matchOperator, columnValue))

            Return m_TrAdditionalClassMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "ClassCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrAdditionalClass), "ClassCode", AggregateType.Count)
            Return CType(m_TrAdditionalClassMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As TrAdditionalClass) As String
            Dim returnValue As String = String.Empty
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    'objDomain.ClassCode = GetClassCode(objDomain.ClassType)
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ClassCode
                    End If

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        returnValue = -1
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return objDomain.ClassCode

        End Function

        Public Function InsertTransaction(ByVal objDomain As TrAdditionalClass) As Integer
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
        Public Function Update(ByVal objDomain As TrAdditionalClass) As Integer

            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                
                    Dim oldClass As TrAdditionalClass = Retrieve(objDomain.ID)
                    If objDomain.Status <> oldClass.Status Then
                        Dim history As New StatusChangeHistory
                        history.DocumentType = LookUp.DocumentType.TrAdditionalClass
                        history.DocumentRegNumber = objDomain.ID
                        history.OldStatus = oldClass.Status
                        history.NewStatus = objDomain.Status
                        history.RowStatus = 0
                        m_TransactionManager.AddInsert(history, m_userPrincipal.Identity.Name)
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)


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

        Public Sub DeleteFromDB(ByVal objDomain As TrAdditionalClass)
            Try
                m_TrAdditionalClassMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is TrAdditionalClass) Then
                CType(InsertArg.DomainObject, TrAdditionalClass).ID = InsertArg.ID
                CType(InsertArg.DomainObject, TrAdditionalClass).MarkLoaded()

            End If
        End Sub

#End Region

#Region "Custom Method"
        Public Function GetClassCode(ByVal tipeKelas As Integer) As String
            Dim result As String = String.Empty
            Dim prefix As String = String.Empty
            Dim lastId As Integer = 0

            If tipeKelas = CInt(EnumTrClass.EnumClassType.INHOUSE_TRAINING) Then
                prefix = "IHT" & DateTime.Now.ToString("yy")
                lastId = GetLastId(prefix) + 1
                result = prefix & lastId.ToString().PadLeft(4, "0")
            ElseIf tipeKelas = CInt(EnumTrClass.EnumClassType.FLEET_TRAINING) Then
                prefix = "FT" & DateTime.Now.ToString("yy")
                lastId = GetLastId(prefix) + 1
                result = prefix & lastId.ToString().PadLeft(4, "0")
            End If

            Return result
        End Function

        Private Function GetLastId(ByVal prefix As String) As Integer
            Dim result As Integer = 0
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrAdditionalClass), "ClassCode", MatchType.StartsWith, prefix))
            Dim arl As ArrayList = RetrieveActiveList(criterias, "CreatedTime", Sort.SortDirection.DESC)

            If arl.Count > 0 Then
                Try
                    Dim lastData As TrAdditionalClass = CType(arl(0), TrAdditionalClass)
                    result = Convert.ToInt32(Right(lastData.ClassCode, 4))
                Catch ex As Exception
                    result = 0
                End Try
            End If

            Return result
        End Function
#End Region

    End Class
End Namespace

