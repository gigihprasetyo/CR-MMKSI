
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
'// Generated on 11/10/2020 - 12:13:32 PM
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

    Public Class MSPExRegistrationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MSPExRegistrationMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MSPExRegistrationMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPExRegistration).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(MSPCustomer))


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MSPExRegistration
            Return CType(m_MSPExRegistrationMapper.Retrieve(ID), MSPExRegistration)
        End Function

        Public Function RetrieveByChassisNumber(ByVal Code As String) As MSPExRegistration
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, Code))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))

            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl)
            If (MSPExRegistrationColl.Count > 0) Then
                Return CType(MSPExRegistrationColl(0), MSPExRegistration)
            End If
            Return New MSPExRegistration
        End Function

        Public Function RetrieveByCNumberAndNotRNumber(ByVal ChassisNumber As String, ByVal RegNumber As String) As MSPExRegistration
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, ChassisNumber))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "RegNumber", MatchType.No, RegNumber))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))

            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl)
            If (MSPExRegistrationColl.Count > 0) Then
                Return CType(MSPExRegistrationColl(0), MSPExRegistration)
            End If
            Return New MSPExRegistration
        End Function

        Public Function RetrieveArrChassisNumber(ByVal Code As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "Status", MatchType.Exact, CType(EnumMSPEx.MSPExStatus.Selesai, Short)))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))

            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl)
            If MSPExRegistrationColl.Count > 0 Then
                Return MSPExRegistrationColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveByRegNumber(ByVal Code As String) As MSPExRegistration
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), "RegNumber", MatchType.Exact, Code))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.DESC))

            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl)
            If (MSPExRegistrationColl.Count > 0) Then
                Return CType(MSPExRegistrationColl(0), MSPExRegistration)
            End If
            Return New MSPExRegistration
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias)
            If MSPExRegistrationColl.Count > 0 Then
                Return MSPExRegistrationColl
            End If
            Return New ArrayList
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sorts)
            If MSPExRegistrationColl.Count > 0 Then
                Return MSPExRegistrationColl
            End If
            Return New ArrayList
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MSPExRegistrationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPExRegistrationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPExRegistrationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPExRegistration As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias)
            Return _MSPExRegistration
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MSPExRegistrationColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MSPExRegistration), SortColumn, sortDirection))
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPExRegistrationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortColl As SortCollection) As ArrayList
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl)
            Return MSPExRegistrationColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPExRegistrationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExRegistrationColl As ArrayList = m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPExRegistration), columnName, matchOperator, columnValue))
            Return MSPExRegistrationColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExRegistration), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), columnName, matchOperator, columnValue))

            Return m_MSPExRegistrationMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveSP(ByVal strSQL As String) As ArrayList
            Dim ret As ArrayList = m_MSPExRegistrationMapper.RetrieveSP(strSQL)
            Return ret
        End Function

        Public Function RetrieveSpDS(str As String) As DataSet
            Return m_MSPExRegistrationMapper.RetrieveDataSet(str)
        End Function

        Public Function RetrieveCountMaxPM(ByVal MSPExTypeID As Integer) As Integer
            Dim count As Integer = 0
            'Dim arrResult As ArrayList = m_MSPExRegistrationMapper.RetrieveSP("SP_MSPExGetCountMaxPM @chassis='" & chassisNo & "'")
            Dim res As DataSet = m_MSPExRegistrationMapper.RetrieveDataSet("SP_MSPExGetCountMaxPM '" & MSPExTypeID & "'")
            If res.Tables.Count > 0 Then
                Dim tb As DataTable = res.Tables(0)
                If tb.Rows.Count > 0 Then
                    If Not IsDBNull(tb.Rows(0)(0)) Then
                        count = CInt(tb.Rows(0)(0))
                    End If
                End If
            End If

            Return count
        End Function

        Public Function RetrieveCountHistoryService(ByVal ChassisMasterID As Integer, ByVal CreatedTime As DateTime) As Integer
            Dim count As Integer = 0, by
            'Dim res As DataSet = m_MSPExRegistrationMapper.RetrieveDataSet("SP_MSPExGetCountHistoryService @chassis='" & chassisNo & "', @mspExRegID=" & mspExRegID.ToString())
            Dim res As DataSet = m_MSPExRegistrationMapper.RetrieveDataSet("SP_MSPExGetCountHistoryService '" & ChassisMasterID & "', '" & CreatedTime.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            If res.Tables.Count > 0 Then
                Dim tb As DataTable = res.Tables(0)
                If tb.Rows.Count > 0 Then
                    If Not IsDBNull(tb.Rows(0)(0)) Then
                        count = CInt(tb.Rows(0)(0))
                    End If
                End If
            End If

            Return count
        End Function

        Public Function RetrieveCountFS(ByVal MSPExTypeID As Integer, ByVal ChassisMasterID As Integer, ByVal CreatedTime As Date, ByVal ValidDateTo As Date) As Integer
            Dim count As Integer = 0, by
            'Dim res As DataSet = m_MSPExRegistrationMapper.RetrieveDataSet("SP_MSPExGetCountFS @chassis='" & chassisNo & "', @mspExRegID=" & mspExRegID)
            Dim res As DataSet = m_MSPExRegistrationMapper.RetrieveDataSet("SP_MSPExGetCountFS '" & MSPExTypeID.ToString() & "', '" & ChassisMasterID.ToString() & "', '" & CreatedTime.ToString("yyyy/MM/dd") & "', '" & ValidDateTo.ToString("yyyy/MM/dd") & "'")
            If res.Tables.Count > 0 Then
                Dim tb As DataTable = res.Tables(0)
                If tb.Rows.Count > 0 Then
                    If Not IsDBNull(tb.Rows(0)(0)) Then
                        count = CInt(tb.Rows(0)(0))
                    End If
                End If
            End If

            Return count
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "MSPExRegistrationCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MSPExRegistration), "MSPExRegistrationCode", AggregateType.Count)
            Return CType(m_MSPExRegistrationMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As MSPExRegistration) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MSPExRegistrationMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MSPExRegistration) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MSPExRegistrationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MSPExRegistration)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_MSPExRegistrationMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPExRegistration)
            Try
                m_MSPExRegistrationMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is MSPCustomer) Then
                CType(InsertArg.DomainObject, MSPCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, MSPCustomer).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is MSPExRegistration) Then
                CType(InsertArg.DomainObject, MSPExRegistration).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal objMSPCustomer As MSPCustomer, ByVal objMSPExRegistration As MSPExRegistration) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objMSPCustomer, m_userPrincipal.Identity.Name)

                    objMSPExRegistration.MSPCustomer = objMSPCustomer
                    m_TransactionManager.AddInsert(objMSPExRegistration, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objMSPExRegistration.ID
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

        Public Function UpdateTransaction(ByVal objMSPCustomer As MSPCustomer, ByVal objMSPExRegistration As MSPExRegistration) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddUpdate(objMSPCustomer, m_userPrincipal.Identity.Name)

                    objMSPExRegistration.MSPCustomer = objMSPCustomer
                    m_TransactionManager.AddUpdate(objMSPExRegistration, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objMSPExRegistration.ID
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

        Public Function UpdateRegUploadedToSAP(ByVal ListWSCHeader As ArrayList) As Integer
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim nCounter As Integer
                    For nCounter = 0 To ListWSCHeader.Count - 1
                        Dim objMSPExRegistration As MSPExRegistration
                        objMSPExRegistration = CType(ListWSCHeader.Item(nCounter), MSPExRegistration)
                        m_TransactionManager.AddUpdate(objMSPExRegistration, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    nReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return nReturnValue
        End Function


        Public Function DoRetrieveDataset(ByVal strSql As String) As DataSet
            Dim ds As New DataSet()
            ds = m_MSPExRegistrationMapper.RetrieveDataSet(strSql)
            Return ds
        End Function

        Public Function RetrieveDataTable(str As String) As DataTable
            Dim arr As DataSet
            arr = m_MSPExRegistrationMapper.RetrieveDataSet(str)

            If arr.Tables.Count > 0 Then
                Return arr.Tables(0)
            Else
                Return Nothing
            End If
        End Function
#End Region

    End Class

End Namespace

