#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class VWI_AllocationRealTimeServiceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_VWI_AllocationRealTimeServiceMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VWI_AllocationRealTimeServiceMapper = MapperFactory.GetInstance().GetMapper(GetType(VWI_AllocationRealTimeService).ToString)
            Me.DomainTypeCollection.Add(GetType(VWI_AllocationRealTimeService))
        End Sub

#End Region

#Region "Retrieve"


        Public Function Retrieve(ByVal ID As Integer) As VWI_AllocationRealTimeService
            Return CType(m_VWI_AllocationRealTimeServiceMapper.Retrieve(ID), VWI_AllocationRealTimeService)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VWI_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VWI_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VWI_AllocationRealTimeServiceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_AllocationRealTimeServiceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_AllocationRealTimeService), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_AllocationRealTimeServiceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VWI_AllocationRealTimeServiceList As ArrayList = m_VWI_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VWI_AllocationRealTimeServiceList
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim VWI_AllocationRealTimeServiceList As ArrayList = m_VWI_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias)

            Return VWI_AllocationRealTimeServiceList
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Dim VWI_AllocationRealTimeServiceList As ArrayList = m_VWI_AllocationRealTimeServiceMapper.RetrieveByCriteria(criterias, sorts)

            Return VWI_AllocationRealTimeServiceList
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"

#End Region
    End Class

End Namespace
