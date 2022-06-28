
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
'// Generated on 09/12/2019 - 14:28:30
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
Imports System.Collections.Generic


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class DSFLeasingClaimFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DSFLeasingClaimMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DSFLeasingClaimMapper = MapperFactory.GetInstance.GetMapper(GetType(DSFLeasingClaim).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DSFLeasingClaimDocument))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DSFLeasingClaim
            Return CType(m_DSFLeasingClaimMapper.Retrieve(ID), DSFLeasingClaim)
        End Function

        Public Function Retrieve(ByVal Code As String) As DSFLeasingClaim
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "DSFLeasingClaimCode", MatchType.Exact, Code))

            Dim DSFLeasingClaimColl As ArrayList = m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias)
            If (DSFLeasingClaimColl.Count > 0) Then
                Return CType(DSFLeasingClaimColl(0), DSFLeasingClaim)
            End If
            Return New DSFLeasingClaim
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DSFLeasingClaimMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DSFLeasingClaim), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DSFLeasingClaimMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DSFLeasingClaim), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DSFLeasingClaimMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DSFLeasingClaim As ArrayList = m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias)
            Return _DSFLeasingClaim
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DSFLeasingClaimColl As ArrayList = m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DSFLeasingClaimColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DSFLeasingClaim), SortColumn, sortDirection))
            Dim DSFLeasingClaimColl As ArrayList = m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DSFLeasingClaimColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DSFLeasingClaimColl As ArrayList = m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DSFLeasingClaimColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DSFLeasingClaimColl As ArrayList = m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), columnName, matchOperator, columnValue))
            Return DSFLeasingClaimColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DSFLeasingClaim), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), columnName, matchOperator, columnValue))

            Return m_DSFLeasingClaimMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "DSFLeasingClaimCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DSFLeasingClaim), "DSFLeasingClaimCode", AggregateType.Count)
            Return CType(m_DSFLeasingClaimMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DSFLeasingClaim) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DSFLeasingClaimMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DSFLeasingClaim) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DSFLeasingClaimMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DSFLeasingClaim)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DSFLeasingClaimMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DSFLeasingClaim)
            Try
                m_DSFLeasingClaimMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is DSFLeasingClaim) Then
                CType(InsertArg.DomainObject, DSFLeasingClaim).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DSFLeasingClaim).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DSFLeasingClaimDocument) Then
                CType(InsertArg.DomainObject, DSFLeasingClaimDocument).ID = InsertArg.ID
            End If
        End Sub

        Public Function InsertTransaction(ByVal dataForSave As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If Not IsNothing(dataForSave) Then
                        If dataForSave.Count > 0 Then
                            For Each data As DSFLeasingClaim In dataForSave
                                m_TransactionManager.AddInsert(data, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    m_TransactionManager.PerformTransaction()
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

        Function UpdateStatusTransaction(arrDSFLeasingClaim As ArrayList, arrBenefitClaimDeducted2 As ArrayList, arrBenefitClaimDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrBenefitClaimDeducted2) Then
                        If arrBenefitClaimDeducted2.Count > 0 Then
                            For Each item As BenefitClaimDeducted In arrBenefitClaimDeducted2
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If Not IsNothing(arrBenefitClaimDetail) Then
                        If arrBenefitClaimDetail.Count > 0 Then
                            For Each item As BenefitClaimDetails In arrBenefitClaimDetail
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    'If Not IsNothing(arrDSFLeasingClaim) Then
                    '    If arrDSFLeasingClaim.Count > 0 Then
                    '        For Each item As DSFLeasingClaim In arrDSFLeasingClaim
                    '            If item.Status = 5 Then
                    '                For Each itemDoc As DSFLeasingClaimDocument In item.DSFLeasingClaimDocuments
                    '                    itemDoc.SourceData = 0
                    '                    If itemDoc.ID <> 0 Then
                    '                        m_TransactionManager.AddUpdate(itemDoc, m_userPrincipal.Identity.Name)
                    '                    Else
                    '                        m_TransactionManager.AddInsert(itemDoc, m_userPrincipal.Identity.Name)
                    '                    End If
                    '                Next
                    '            End If
                    '        Next
                    '    End If
                    'End If

                    If Not IsNothing(arrDSFLeasingClaim) Then
                        If arrDSFLeasingClaim.Count > 0 Then
                            For Each item As DSFLeasingClaim In arrDSFLeasingClaim
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

        Public Function RetrieveUploadRemark(chassisNumber As String, engineNumber As String) As String
            Dim result As String = String.Empty
            Dim dtbs As New DataSet
            Dim strSPName As String = "up_GetRemarkDSFClaimUpload  "
            Dim Param As New List(Of SqlClient.SqlParameter)

            Param.Add(New SqlClient.SqlParameter("@chassisNumber ", chassisNumber))
            Param.Add(New SqlClient.SqlParameter("@EngineNumber", engineNumber))

            dtbs = m_DSFLeasingClaimMapper.RetrieveDataSet(strSPName, New ArrayList(Param))

            If dtbs.Tables.Count > 0 Then
                If dtbs.Tables(1).Rows.Count > 0 Then
                    result = dtbs.Tables(1).Rows(0)(0).ToString() + ";" + dtbs.Tables(1).Rows(0)(1).ToString()
                    Return result
                Else
                    Return String.Empty
                End If

            Else
                Return String.Empty
            End If

        End Function

#End Region

    End Class

End Namespace

