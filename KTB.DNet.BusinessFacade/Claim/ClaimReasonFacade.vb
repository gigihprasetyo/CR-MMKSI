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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/27/2007 - 9:51:42 AM
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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTb.DNet.BusinessFacade.Claim

    Public Class ClaimReasonFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ClaimReasonMapper As IMapper

        Private m_TransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ClaimReasonMapper = MapperFactory.GetInstance.GetMapper(GetType(ClaimReason).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ClaimReason
            Return CType(m_ClaimReasonMapper.Retrieve(ID), ClaimReason)
        End Function

        Public Function Retrieve(ByVal Code As String) As ClaimReason
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimReason), "ClaimReasonCode", MatchType.Exact, Code))

            Dim ClaimReasonColl As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias)
            If (ClaimReasonColl.Count > 0) Then
                Return CType(ClaimReasonColl(0), ClaimReason)
            End If
            Return New ClaimReason
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ClaimReasonMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ClaimReasonMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ClaimReasonMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimReason), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimReasonMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimReason), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimReasonMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ClaimReason As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias)
            Return _ClaimReason
        End Function

        Public Function RetrieveActiveListHeader() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimReason), "IsHeader", MatchType.Exact, "1"))
            Dim _ClaimReason As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias)
            Return _ClaimReason
        End Function

        Public Function RetrieveActiveListDetail() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimReason), "IsHeader", MatchType.Exact, "0"))
            Dim _ClaimReason As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias)
            Return _ClaimReason
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ClaimReasonColl As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimReason), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ClaimReasonColl As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimReason), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ClaimReasonMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ClaimReasonColl As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ClaimReasonColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ClaimReasonColl As ArrayList = m_ClaimReasonMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ClaimReason), columnName, matchOperator, columnValue))
            Return ClaimReasonColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ClaimReason), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), columnName, matchOperator, columnValue))

            Return m_ClaimReasonMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String, ByVal ID As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "Code", MatchType.Exact, Code))
            crit.opAnd(New Criteria(GetType(ClaimReason), "ID", MatchType.No, ID))
            Dim agg As Aggregate = New Aggregate(GetType(ClaimReason), "Code", AggregateType.Count)
            Return CType(m_ClaimReasonMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateReason(ByVal _reason As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "Reason", MatchType.Exact, _reason))
            crit.opAnd(New Criteria(GetType(ClaimReason), "IsHeader", MatchType.Exact, 0))

            Dim agg As Aggregate = New Aggregate(GetType(ClaimReason), "Reason", AggregateType.Count)
            Return CType(m_ClaimReasonMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As ClaimReason) As Integer
            Dim returnValue As Short = 1
            Try
                m_ClaimReasonMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                returnValue = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
                'Dim s As String = ex.Message
                'returnValue = -1
            End Try

            Return returnValue
        End Function

        Public Function InsertTransaction(ByVal objDomain As ClaimReason) As Integer
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

        Public Function Update(ByVal objDomain As ClaimReason) As Integer
            Dim nResult As Integer = -1
            Try
                Dim oCRinDB As ClaimReason = Me.m_ClaimReasonMapper.Retrieve(objDomain.ID)
                nResult = m_ClaimReasonMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                If nResult > 0 Then
                    Me.WriteHistory(objDomain, oCRinDB)
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

        Public Sub DeleteFromDB(ByVal objDomain As ClaimReason)
            Try
                m_ClaimReasonMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is ClaimReason) Then
                CType(InsertArg.DomainObject, ClaimReason).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ClaimReason).MarkLoaded()
            End If
        End Sub
#End Region

#Region "Custom Method"

        Private m_DataHistoryMapper As IMapper
        Private m_DataHistoryDetailMapper As IMapper

        Public Function WriteHistory(ByVal oCR As ClaimReason, ByVal oCRinDB As ClaimReason) As Integer
            Dim iResult As Integer = -1
            If oCR.ID > 0 Then

                If IsNothing(Me.m_DataHistoryMapper) Then
                    Me.m_DataHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(DataHistory).ToString)
                    Me.m_DataHistoryDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(DataHistoryDetail).ToString)
                End If


                'Dim oCRinDB As ClaimReason = Me.m_ClaimReasonMapper.Retrieve(oCR.ID)
                Dim oDH As New DataHistory
                Dim oDHD As DataHistoryDetail
                Dim aDHDs As New ArrayList
                Dim IsChanged As Boolean = False

                oDH.DataTable = "ClaimReason"
                oDH.DataID = oCR.ID

                If oCR.Code <> oCRinDB.Code Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "Code" : oDHD.OldValue = oCRinDB.Code : oDHD.NewValue = oCR.Code : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.IsHeader <> oCRinDB.IsHeader Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "IsHeader" : oDHD.OldValue = oCRinDB.IsHeader : oDHD.NewValue = oCR.IsHeader : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.Reason <> oCRinDB.Reason Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "Reason" : oDHD.OldValue = oCRinDB.Reason : oDHD.NewValue = oCR.Reason : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.TimeLimit <> oCRinDB.TimeLimit Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "TimeLimit" : oDHD.OldValue = oCRinDB.TimeLimit : oDHD.NewValue = oCR.TimeLimit : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.Status <> oCRinDB.Status Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "Status" : oDHD.OldValue = oCRinDB.Status : oDHD.NewValue = oCR.Status : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.incharge <> oCRinDB.incharge Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "InCharge" : oDHD.OldValue = oCRinDB.incharge : oDHD.NewValue = oCR.incharge : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.Prerequisite <> oCRinDB.Prerequisite Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "Prerequisite" : oDHD.OldValue = oCRinDB.Prerequisite : oDHD.NewValue = oCR.Prerequisite : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oCR.IsMandatoryUpload <> oCRinDB.IsMandatoryUpload Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "IsMandatoryUpload" : oDHD.OldValue = oCRinDB.IsMandatoryUpload : oDHD.NewValue = oCR.IsMandatoryUpload : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If IsChanged Then
                    Dim cDH As New CriteriaComposite(New Criteria(GetType(DataHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDHs As ArrayList

                    cDH.opAnd(New Criteria(GetType(DataHistory), "DataTable", MatchType.Exact, "ClaimReason"))
                    cDH.opAnd(New Criteria(GetType(DataHistory), "DataID", MatchType.Exact, oCRinDB.ID))
                    aDHs = Me.m_DataHistoryMapper.RetrieveByCriteria(cDH)
                    If aDHs.Count > 0 Then
                        oDH = CType(aDHs(0), DataHistory)
                    End If
                    If oDH.ID <= 0 Then
                        oDH.ID = Me.m_DataHistoryMapper.Insert(oDH, m_userPrincipal.Identity.Name)
                    End If
                    If oDH.ID > 0 Then
                        iResult = oDH.ID
                        For Each oDHDNew As DataHistoryDetail In aDHDs
                            oDHDNew.DataHistory = oDH
                            oDHD.ID = Me.m_DataHistoryDetailMapper.Insert(oDHDNew, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        iResult = -2
                    End If
                End If

            End If
            Return iResult
        End Function
#End Region

    End Class

End Namespace

