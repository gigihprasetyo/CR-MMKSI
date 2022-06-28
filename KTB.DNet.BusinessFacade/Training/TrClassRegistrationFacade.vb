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
'// Generated on 11/14/2005 - 10:42:45 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrClassRegistrationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TrClassRegistrationMapper As IMapper
        Private m_TrBookingCourseMapper As IMapper
        Private m_TrBookingClassMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private ID_Insert As Integer

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrClassRegistrationMapper = MapperFactory.GetInstance.GetMapper(GetType(TrClassRegistration).ToString)
            Me.m_TrBookingCourseMapper = MapperFactory.GetInstance.GetMapper(GetType(TrBookingCourse).ToString)
            Me.m_TrBookingClassMapper = MapperFactory.GetInstance.GetMapper(GetType(TrBookingClass).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.TrClassRegistration))

            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrClassRegistration
            Return CType(m_TrClassRegistrationMapper.Retrieve(ID), TrClassRegistration)
        End Function

        Public Function Retrieve(ByVal Code As String) As TrClassRegistration
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "RegistrationCode", MatchType.Exact, Code))

            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias)
            If (TrClassRegistrationColl.Count > 0) Then
                Return CType(TrClassRegistrationColl(0), TrClassRegistration)
            End If
            Return New TrClassRegistration
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrClassRegistrationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrClassRegistrationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrClassRegistrationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrClassRegistrationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveCustomPagingSP(ByVal tahun As Integer, ByVal bulan As Integer, ByVal classID As Integer, _
                                   ByVal dealerID As Integer, ByVal areaID As Integer, ByVal pageNumber As Integer, ByVal pagesize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim paramSQL As ArrayList = New ArrayList()
            Dim par As SqlClient.SqlParameter = New SqlClient.SqlParameter("@year", tahun)
            paramSQL.Add(par)

            Dim par2 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Month", bulan)
            paramSQL.Add(par2)

            Dim par3 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@DealerID", dealerID)
            paramSQL.Add(par3)

            Dim par4 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@Area", areaID)
            paramSQL.Add(par4)

            Dim par5 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@ClassID", classID)
            paramSQL.Add(par5)

            Return m_TrClassRegistrationMapper.RetrieveCustomPagingBySP("GetTrainingReminder", paramSQL, pageNumber, pagesize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrClassRegistration As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias)
            Return _TrClassRegistration
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Try
                Return CType(m_TrClassRegistrationMapper.RetrieveScalar(aggregation, criterias), Integer)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Function RetrieveRegActiveList() As ArrayList

            'Dim status As Integer = CInt(New EnumTrClassRegistration().DataStatusType.Register)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, status))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.Category", MatchType.Exact, CInt(EnumTrClass.EnumTrClassCategory.NEW_MODEL)))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.FinishDate", MatchType.Lesser, New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)))
            Dim _TrClassRegistration As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias)
            Return _TrClassRegistration
        End Function

        'Public Sub RunClassRegServices()
        '    Dim collTrClassReg As ArrayList = RetrieveRegActiveList()
        '    For Each item As TrClassRegistration In collTrClassReg
        '        'Comment Disini untuk konfirmasi
        '        'item.TrClass.TrCourse.Category = CInt(EnumTrClass.EnumTrClassCategory.NEW_MODEL)
        '        'If item.TrClass.TrCourse.Category = CInt(EnumTrClass.EnumTrClassCategory.NEW_MODEL) Then
        '        '    item.Status = CInt(New EnumTrClassRegistration().DataStatusType.Pass)
        '        '    Me.Update(item)
        '        'End If
        '        'If item.TrClass.TrCourse.Category = CInt(EnumTrClass.EnumTrClassCategory.NEW_MODEL) Then
        '        '    item.Status = CInt(New EnumTrClassRegistration().DataStatusType.Pass)
        '        '    Me.Update(item)
        '        'End If
        '    Next
        'End Sub

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As ICriteria) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), columnName, matchOperator, columnValue))
            Return TrClassRegistrationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), columnName, matchOperator, columnValue))

            Return m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteria2(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal columnValue2 As Integer) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), columnName, matchOperator, columnValue))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, columnValue2))

            Return m_TrClassRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TrClassRegistration) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_TrClassRegistrationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrClassRegistration) As Integer
            Dim nResult As Integer = -1
            Try
                If objDomain.RowStatus = CType(DBRowStatus.Deleted, Short) Then
                    nResult = m_TrClassRegistrationMapper.Delete(objDomain)
                Else
                    nResult = m_TrClassRegistrationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                End If

            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrClassRegistration)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrClassRegistrationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TrClassRegistration)
            Try
                m_TrClassRegistrationMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Function IsClassRegAvalible(ByVal objClassReg As TrClassRegistration) As Boolean
            Dim strIn As String = "(" & CType(EnumTrClassRegistration.DataStatusType.Pass, Integer).ToString() + ", " + _
            CType(EnumTrClassRegistration.DataStatusType.Register, Integer).ToString() + ")"

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, objClassReg.TrTrainee.ID))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.ID", MatchType.Exact, objClassReg.TrClass.TrCourse.ID))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.InSet, strIn))
            Dim TrClassRegistrationColl As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias)
            If (TrClassRegistrationColl.Count > 0) Then
                Return True
            End If
            Return False
        End Function

        Public Function Insert(ByVal ClassRegInsertColl As ArrayList, _
            ByVal ClassRegCancelColl As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    For x As Integer = 0 To ClassRegInsertColl.Count - 1
                        Dim item As TrClassRegistration = _
                            CType(ClassRegInsertColl(x), TrClassRegistration)
                        'TODO before insert Cek udah ada atau belum
                        If Not IsClassRegAvalible(item) Then
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    If ClassRegCancelColl.Count > 0 Then
                        For y As Integer = 0 To ClassRegCancelColl.Count - 1
                            Dim item As TrClassRegistration = _
                                CType(ClassRegCancelColl(y), TrClassRegistration)

                            m_TransactionManager.AddDelete(item)
                        Next
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                    End If
                    returnValue = 1
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
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "TrClassRegistrationCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(TrClassRegistration), "TrClassRegistrationCode", AggregateType.Count)
            Return CType(m_TrClassRegistrationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrClassRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim Coll As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return Coll
        End Function

        Public Function UpdateTrClassRegistrationCollection(ByVal arrTrClassReg As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrTrClassReg.Count > 0 Then
                        For Each objTrClassReg As TrClassRegistration In arrTrClassReg
                            objTrClassReg.LastUpdateBy = m_userPrincipal.Identity.Name
                            m_TransactionManager.AddUpdate(objTrClassReg, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 0

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
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.TrClassRegistration) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TrClassRegistration).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TrClassRegistration).MarkLoaded()
                ID_Insert = InsertArg.ID
            End If
        End Sub

        Public Function SaveSuggestion(ByVal classData As TrClass, ByVal listTrBookingCourse As List(Of TrBookingCourse)) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each data As TrBookingCourse In listTrBookingCourse
                        Dim classRegis As TrClassRegistration = MappingClassRegistrationFromBookingCourse(classData, data)

                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classRegis.TrClass.ID))
                        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, classRegis.TrTrainee.ID))
                        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, Short)))
                        Dim tcr As ArrayList = m_TrClassRegistrationMapper.RetrieveByCriteria(criterias)
                        If tcr.Count.Equals(0) Then
                            m_TransactionManager.AddInsert(classRegis, m_userPrincipal.Identity.Name)
                        Else
                            classRegis.ID = CType(tcr(0), TrClassRegistration).ID
                        End If

                        Dim bookingClass As TrBookingClass = New TrBookingClass()
                        bookingClass.TrBookingCourse = data
                        bookingClass.TrClass = classData
                        bookingClass.TrClassRegistration = classRegis
                        bookingClass.Status = 1
                        bookingClass.RowStatus = 0

                        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClass.ID", MatchType.Exact, classRegis.TrClass.ID))
                        criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, classRegis.ID))
                        criteria.opAnd(New Criteria(GetType(TrBookingClass), "Status", MatchType.Exact, "1"))
                        Dim arrBC As ArrayList = m_TrBookingClassMapper.RetrieveByCriteria(criteria)
                        If arrBC.Count.Equals(0) Then
                            m_TransactionManager.AddInsert(bookingClass, m_userPrincipal.Identity.Name)
                        End If

                        data.TrClassRegistration = classRegis
                        m_TransactionManager.AddUpdate(data, m_userPrincipal.Identity.Name)

                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = ID_Insert
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
            Return returnValue
        End Function

        Private Function MappingClassRegistrationFromBookingCourse(ByVal classData As TrClass, ByVal data As TrBookingCourse) As TrClassRegistration
            Dim result As New TrClassRegistration

            result.TrTrainee = data.TrTrainee
            result.TrClass = classData
            result.Dealer = data.Dealer
            result.RegistrationDate = DateTime.Now
            result.Status = "0"
            result.EntryType = 0
            result.RowStatus = 0

            Return result
        End Function

#End Region

       

    End Class

End Namespace

