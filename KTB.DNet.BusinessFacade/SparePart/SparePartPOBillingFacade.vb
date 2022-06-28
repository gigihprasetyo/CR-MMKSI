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
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartPOBillingFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPOBillingMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPOBillingMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPOBillingRecap).ToString)
            Me.m_TransactionManager = New TransactionManager

            Me.DomainTypeCollection.Add(GetType(SparePartPOBillingRecap))

        End Sub

#End Region

#Region "Retrieve"
        Public Function Retrieve(ByVal ID As Integer) As SparePartPOBillingRecap
            Return m_SparePartPOBillingMapper.Retrieve(ID)
        End Function

        Public Function Retrieve(ByVal crit As CriteriaComposite) As ArrayList
            Return m_SparePartPOBillingMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveActiveListByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(SparePartPOBillingRecap), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOBillingMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Decimal
            Dim returnValue As Decimal = 0
            Try
                returnValue = CType(m_SparePartPOBillingMapper.RetrieveScalar(aggr, crit), Decimal)
            Catch ex As Exception
                returnValue = 0
            End Try
            Return returnValue
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal List As ArrayList) As String
            Dim returnValue As String = ""
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each bill As SparePartPOBillingRecap In List
                        m_TransactionManager.AddInsert(bill, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    returnValue = ex.Message
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

        Public Function IsExist(ByVal obj As SparePartPOBillingRecap) As Integer
            Dim returnVal As Integer = 0
            Dim aggr As Aggregate = New Aggregate(GetType(SparePartPOBillingRecap), "ID", AggregateType.Count)
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPOBillingRecap), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(SparePartPOBillingRecap), "BillingNumber", MatchType.Exact, obj.BillingNumber))

            Dim list As ArrayList = m_SparePartPOBillingMapper.RetrieveByCriteria(crit)
            If (Not IsNothing(list)) AndAlso list.Count > 0 Then
                returnVal = CType(list.Item(0), SparePartPOBillingRecap).ID
            End If
            Return returnVal
        End Function

        Public Function InsertOrUpdate(ByVal List As ArrayList) As String
            Dim returnValue As String = ""
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each bill As SparePartPOBillingRecap In List
                        bill.ID = IsExist(bill)
                        If bill.ID > 0 Then
                            m_TransactionManager.AddUpdate(bill, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(bill, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    returnValue = ex.Message
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

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
