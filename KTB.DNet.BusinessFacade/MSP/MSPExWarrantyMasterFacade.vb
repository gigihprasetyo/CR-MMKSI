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


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class MSPExWarrantyMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MSPExWarrantyMasterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MSPExWarrantyMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(MSPExWarrantyMaster).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MSPExWarrantyMaster
            Return CType(m_MSPExWarrantyMasterMapper.Retrieve(ID), MSPExWarrantyMaster)
        End Function

        Public Function Retrieve(ByVal oMSPExType As MSPExType, ByVal StartDate As Date, ByVal EndDate As Date) As MSPExWarrantyMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExWarrantyMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExWarrantyMaster), "MSPExType.ID", MatchType.Exact, oMSPExType.ID))
            criterias.opAnd(New Criteria(GetType(MSPExWarrantyMaster), "StartDate", MatchType.LesserOrEqual, StartDate))
            criterias.opAnd(New Criteria(GetType(MSPExWarrantyMaster), "EndDate", MatchType.GreaterOrEqual, EndDate))

            Dim MSPExWarrantyMasterColl As ArrayList = m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias)
            If (MSPExWarrantyMasterColl.Count > 0) Then
                Return CType(MSPExWarrantyMasterColl(0), MSPExWarrantyMaster)
            End If
            Return New MSPExWarrantyMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MSPExWarrantyMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExWarrantyMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPExWarrantyMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExWarrantyMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MSPExWarrantyMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExWarrantyMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MSPExWarrantyMaster As ArrayList = m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias)
            Return _MSPExWarrantyMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExWarrantyMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExWarrantyMasterColl As ArrayList = m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MSPExWarrantyMasterColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MSPExWarrantyMaster), SortColumn, sortDirection))
            Dim MSPExWarrantyMasterColl As ArrayList = m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MSPExWarrantyMasterColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MSPExWarrantyMasterColl As ArrayList = m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MSPExWarrantyMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExWarrantyMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MSPExWarrantyMasterColl As ArrayList = m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MSPExWarrantyMaster), columnName, matchOperator, columnValue))
            Return MSPExWarrantyMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MSPExWarrantyMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExWarrantyMaster), columnName, matchOperator, columnValue))

            Return m_MSPExWarrantyMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExWarrantyMaster), "MSPExWarrantyMasterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MSPExWarrantyMaster), "MSPExWarrantyMasterCode", AggregateType.Count)
            Return CType(m_MSPExWarrantyMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As MSPExWarrantyMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_MSPExWarrantyMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As MSPExWarrantyMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MSPExWarrantyMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As MSPExWarrantyMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MSPExWarrantyMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MSPExWarrantyMaster)
            Try
                m_MSPExWarrantyMasterMapper.Delete(objDomain)
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

    End Class

End Namespace
