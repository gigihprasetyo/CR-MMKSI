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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

#End Region

Namespace KTB.DNet.BusinessFacade.Transfer
    Public Class vw_TransferPaymentFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TransferPaymentMapper As IMapper
        Private m_TransferPaymentDetailMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

        Private m_TransactionManager As TransactionManager
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_TransferPaymentMapper = MapperFactory.GetInstance().GetMapper(GetType(vw_TransferPayment).ToString)
            Me.m_TransferPaymentDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(TransferPaymentDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TransferPayment
            Return CType(m_TransferPaymentMapper.Retrieve(ID), TransferPayment)
        End Function

        Public Function Retrieve(ByVal TransferPaymentCode As String) As TransferPayment
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(vw_TransferPayment), "RegNumber", MatchType.Exact, TransferPaymentCode))

            Dim TransferPaymentColl As ArrayList = m_TransferPaymentMapper.RetrieveByCriteria(criterias)
            If (TransferPaymentColl.Count > 0) Then
                Return CType(TransferPaymentColl(0), TransferPayment)
            End If
            Return New TransferPayment
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TransferPaymentMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TransferPaymentMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(vw_TransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferPaymentMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(vw_TransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferPaymentMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("TransferPaymentCode")) Then
                sortColl.Add(New Sort(GetType(vw_TransferPayment), "TransferPaymentCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _TransferPayment As ArrayList = m_TransferPaymentMapper.RetrieveByCriteria(criterias, sortColl)
            Return _TransferPayment
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TransferPaymentColl As ArrayList = m_TransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferPaymentColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TransferPaymentColl As ArrayList = m_TransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(vw_TransferPayment), columnName, matchOperator, columnValue))
            Dim TransferPaymentColl As ArrayList = m_TransferPaymentMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TransferPaymentColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(vw_TransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), columnName, matchOperator, columnValue))

            Return m_TransferPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(vw_TransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TransferPaymentMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As TransferPayment) As Integer
            Dim iReturn As Integer = -2
            Try
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
                        For Each item As TransferPaymentDetail In objDomain.TransferPaymentDetails
                            item.TransferPayment = objDomain
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

                iReturn = returnValue
                'iReturn = m_TransferPaymentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.TransferPayment) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TransferPayment).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.TransferPayment).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is TransferPaymentDetail) Then
                CType(InsertArg.DomainObject, TransferPaymentDetail).ID = InsertArg.ID
            End If
        End Sub

        Public Function Update(ByVal objDomain As TransferPayment) As Integer


            Dim iReturn As Integer = -2
            Try
                Dim returnValue As Integer = -1
                Dim _user As String
                If (Me.IsTaskFree()) Then
                    Try
                        Me.SetTaskLocking()
                        Dim performTransaction As Boolean = True
                        Dim ObjMapper As IMapper
                        m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                        If m_userPrincipal.Identity.Name = "" Then
                            _user = "SAP"
                        Else
                            _user = m_userPrincipal.Identity.Name
                        End If
                        For Each item As TransferPaymentDetail In objDomain.TransferPaymentDetails
                            item.TransferPayment = objDomain
                            m_TransactionManager.AddUpdate(item, _user)
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

                iReturn = returnValue
                'iReturn = m_TransferPaymentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function



        'New
        Public Function Update(ByVal objDomain As TransferPayment, ByVal arrdetailNew As ArrayList, ByVal arrDetailOld As ArrayList) As Integer


            Dim iReturn As Integer = -2
            Try
                Dim returnValue As Integer = -1
                Dim _user As String
                If (Me.IsTaskFree()) Then
                    Try
                        Me.SetTaskLocking()
                        Dim performTransaction As Boolean = True
                        Dim ObjMapper As IMapper
                        'm_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                        m_TransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                        If m_userPrincipal.Identity.Name = "" Then
                            _user = "SAP"
                        Else
                            _user = m_userPrincipal.Identity.Name
                        End If
                        Dim ifExist = False
                        'remark all for deleted where not exist in new
                        For Each item As TransferPaymentDetail In arrDetailOld
                            ifExist = False
                            For Each objNewDetail As TransferPaymentDetail In arrdetailNew
                                If item.SalesOrder.ID = objNewDetail.SalesOrder.ID Then
                                    ifExist = True
                                    Exit For
                                End If
                            Next
                            If Not ifExist Then
                                item.TransferPayment = objDomain
                                item.RowStatus = CInt(DBRowStatus.Deleted)
                                m_TransferPaymentDetailMapper.Update(item, _user)
                            End If

                        Next


                        'insert for new SO
                        For Each item As TransferPaymentDetail In arrdetailNew
                            ifExist = False
                            For Each objNewDetail As TransferPaymentDetail In arrDetailOld
                                If item.SalesOrder.ID = objNewDetail.SalesOrder.ID Then
                                    ifExist = True
                                    Exit For
                                End If
                            Next
                            If Not ifExist Then
                                item.TransferPayment = objDomain
                                item.RowStatus = CInt(DBRowStatus.Active)
                                m_TransferPaymentDetailMapper.Insert(item, _user)
                            End If

                        Next

                        If performTransaction Then

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

                iReturn = returnValue
                'iReturn = m_TransferPaymentMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function UpdateSimple(ByVal objDomain As TransferPayment) As Integer
            Dim nResult As Integer = -1
            Try

                nResult = m_TransferPaymentMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                If nResult > 0 Then
                    nResult = objDomain.ID
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

        Public Sub Delete(ByVal objDomain As TransferPayment)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TransferPaymentMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As TransferPayment)
            Try
                m_TransferPaymentMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(vw_TransferPayment), "TransferPaymentCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(vw_TransferPayment), "TransferPaymentCode", AggregateType.Count)

            Return CType(m_TransferPaymentMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TransferPaymentMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(vw_TransferPayment), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TransferPaymentColl As ArrayList = m_TransferPaymentMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TransferPaymentColl
        End Function

#End Region

    End Class

End Namespace
