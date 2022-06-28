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
'// Generated on 7/16/2007 - 11:09:45 AM
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

Namespace KTB.DNet.BusinessFacade.Claim
    Public Class ClaimDealerAdditionalFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerAdditionalMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DealerAdditionalMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerAdditional).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As DealerAdditional
            Return CType(m_DealerAdditionalMapper.Retrieve(ID), DealerAdditional)
        End Function
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerAdditionalMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
     ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerAdditional), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerAdditionalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortdirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(SortColumn)) And (Not IsNothing(SortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerAdditional), SortColumn, sortdirection))
            Else
                sortColl = Nothing
            End If

            Dim DealerAdditionalColl As ArrayList = m_DealerAdditionalMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerAdditionalColl
        End Function


#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As DealerAdditional) As Integer
            Dim iReturn As Integer = -2
            Try
                m_DealerAdditionalMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Sub DeleteFromDB(ByVal objDomain As DealerAdditional)
            Try
                m_DealerAdditionalMapper.Delete(objDomain)

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Update(ByVal objDomain As DealerAdditional) As Integer
            Dim nResult As Integer = -1
            Try
                Dim oDAinDB As DealerAdditional = Me.m_DealerAdditionalMapper.Retrieve(objDomain.ID)
                nResult = m_DealerAdditionalMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                If nResult > 0 Then
                    Me.WriteHistory(objDomain, oDAinDB)
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

        Public Function ValidateCode(ByVal DealerID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "Dealer.ID", MatchType.Exact, DealerID))
            Dim agg As Aggregate = New Aggregate(GetType(DealerAdditional), "ID", AggregateType.Count)
            Return CType(m_DealerAdditionalMapper.RetrieveScalar(agg, crit), Integer)
        End Function
        Public Function ValidateCodeETA(ByVal DealerID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "Dealer.ID", MatchType.Exact, DealerID))
            crit.opAnd(New Criteria(GetType(DealerAdditional), "ClaimETA", MatchType.No, "0"))
            Dim agg As Aggregate = New Aggregate(GetType(DealerAdditional), "ID", AggregateType.Count)
            Return CType(m_DealerAdditionalMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Private m_DataHistoryMapper As IMapper
        Private m_DataHistoryDetailMapper As IMapper

        Public Function WriteHistory(ByVal oDA As DealerAdditional, ByVal oDAinDB As DealerAdditional) As Integer
            Dim iResult As Integer = -1
            If oDA.ID > 0 Then

                If IsNothing(Me.m_DataHistoryMapper) Then
                    Me.m_DataHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(DataHistory).ToString)
                    Me.m_DataHistoryDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(DataHistoryDetail).ToString)
                End If


                'Dim oDAinDB As DealerAdditional = Me.m_DealerAdditionalMapper.Retrieve(oDA.ID)
                Dim oDH As New DataHistory
                Dim oDHD As DataHistoryDetail
                Dim aDHDs As New ArrayList
                Dim IsChanged As Boolean = False

                oDH.DataTable = "DealerAdditional"
                oDH.DataID = oDA.ID

                If oDA.Dealer.ID <> oDAinDB.Dealer.ID Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "DealerID" : oDHD.OldValue = oDAinDB.Dealer.ID : oDHD.NewValue = oDA.Dealer.ID : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If oDA.ClaimETA <> oDAinDB.ClaimETA Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "ClaimETA" : oDHD.OldValue = oDAinDB.ClaimETA : oDHD.NewValue = oDA.ClaimETA : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If IsChanged Then
                    Dim cDH As New CriteriaComposite(New Criteria(GetType(DataHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDHs As ArrayList

                    cDH.opAnd(New Criteria(GetType(DataHistory), "DataTable", MatchType.Exact, "DealerAdditional"))
                    cDH.opAnd(New Criteria(GetType(DataHistory), "DataID", MatchType.Exact, oDAinDB.ID))
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

