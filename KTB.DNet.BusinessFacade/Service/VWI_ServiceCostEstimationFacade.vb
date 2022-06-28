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
    Public Class VWI_ServiceCostEstimationFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_VWI_ServiceCostEstimationMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VWI_ServiceCostEstimationMapper = MapperFactory.GetInstance().GetMapper(GetType(VWI_ServiceCostEstimation).ToString)
            Me.DomainTypeCollection.Add(GetType(VWI_ServiceCostEstimation))
        End Sub

#End Region

#Region "Retrieve"


        Public Function Retrieve(ByVal ID As Integer) As VWI_ServiceCostEstimation
            Return CType(m_VWI_ServiceCostEstimationMapper.Retrieve(ID), VWI_ServiceCostEstimation)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_ServiceCostEstimation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VWI_ServiceCostEstimationMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_ServiceCostEstimation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_ServiceCostEstimationMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_ServiceCostEstimation), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_ServiceCostEstimationMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VWI_ServiceCostEstimationList As ArrayList = m_VWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VWI_ServiceCostEstimationList
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim VWI_ServiceCostEstimationList As ArrayList = m_VWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias)

            Return VWI_ServiceCostEstimationList
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Dim VWI_ServiceCostEstimationList As ArrayList = m_VWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias, sorts)

            Return VWI_ServiceCostEstimationList
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
