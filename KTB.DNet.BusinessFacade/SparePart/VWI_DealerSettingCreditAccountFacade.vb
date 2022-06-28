
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:43:51 AM
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

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class VWI_DealerSettingCreditAccountFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_VWI_DealerSettingCreditAccountMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_VWI_DealerSettingCreditAccountMapper = MapperFactory.GetInstance.GetMapper(GetType(VWI_DealerSettingCreditAccount).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(VWI_DealerSettingCreditAccount))

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VWI_DealerSettingCreditAccount
            Return CType(m_VWI_DealerSettingCreditAccountMapper.Retrieve(ID), VWI_DealerSettingCreditAccount)
        End Function

        Public Function Retrieve(ByVal DONumber As String) As VWI_DealerSettingCreditAccount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VWI_DealerSettingCreditAccount), "DONumber", MatchType.Exact, DONumber))
            Dim arlDO As ArrayList = m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias)
            If arlDO.Count > 0 Then
                Return CType(arlDO(0), VWI_DealerSettingCreditAccount)
            End If
            Return Nothing
        End Function


        Public Function RetrieveByDealerCode(ByVal DealerCode As String) As VWI_DealerSettingCreditAccount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VWI_DealerSettingCreditAccount), "DealerCode", MatchType.Exact, DealerCode))
            Dim arlDO As ArrayList = m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias)
            If arlDO.Count > 0 Then
                Return CType(arlDO(0), VWI_DealerSettingCreditAccount)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VWI_DealerSettingCreditAccountMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_DealerSettingCreditAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_DealerSettingCreditAccountMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_DealerSettingCreditAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_DealerSettingCreditAccountMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _VWI_DealerSettingCreditAccount As ArrayList = m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias)
            Return _VWI_DealerSettingCreditAccount
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VWI_DealerSettingCreditAccountColl As ArrayList = m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VWI_DealerSettingCreditAccountColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VWI_DealerSettingCreditAccountColl As ArrayList = m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VWI_DealerSettingCreditAccountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VWI_DealerSettingCreditAccountColl As ArrayList = m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(VWI_DealerSettingCreditAccount), columnName, matchOperator, columnValue))
            Return VWI_DealerSettingCreditAccountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_DealerSettingCreditAccount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), columnName, matchOperator, columnValue))

            Return m_VWI_DealerSettingCreditAccountMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As VWI_DealerSettingCreditAccount) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_VWI_DealerSettingCreditAccountMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VWI_DealerSettingCreditAccount) As Integer
            Dim nResult As Integer = -1
            'Dim isChange As New IsChangeFacade
            'Try

            '    'farid additional 20180316

            '    Dim criSPDO As New CriteriaComposite(New Criteria(GetType(VWI_DealerSettingCreditAccount), "Dealer.ID", MatchType.Exact, CType(objDomain.Dealer.ID, Integer)))
            '    criSPDO.opAnd(New Criteria(GetType(VWI_DealerSettingCreditAccount), "DONumber", MatchType.Exact, CType(objDomain.DONumber, String)))
            '    criSPDO.opAnd(New Criteria(GetType(VWI_DealerSettingCreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            '    Dim arlSPDO As New ArrayList
            '    arlSPDO = New VWI_DealerSettingCreditAccount(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criSPDO)

            '    Dim objSPDO_DB As New VWI_DealerSettingCreditAccount
            '    objSPDO_DB = CType(arlSPDO.Item(0), VWI_DealerSettingCreditAccount)

            '    If isChange.ISchangeVWI_DealerSettingCreditAccount(objDomain, objSPDO_DB) Then
            '        nResult = m_VWI_DealerSettingCreditAccountMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            '    End If

            'Catch ex As Exception
            '    nResult = -1
            '    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            '    If rethrow Then
            '        Throw
            '    End If
            'End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VWI_DealerSettingCreditAccount)
            Dim nResult As Integer = -1

        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As VWI_DealerSettingCreditAccount)
            Try
                m_VWI_DealerSettingCreditAccountMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"


#End Region

#Region "Customs"


#End Region

    End Class

End Namespace

