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

Namespace KTB.DNet.BusinessFacade.Training
    Public Class VWI_ServiceAdvisorFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_VWI_ServiceAdvisorMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VWI_ServiceAdvisorMapper = MapperFactory.GetInstance().GetMapper(GetType(VWI_ServiceAdvisor).ToString)
            Me.DomainTypeCollection.Add(GetType(VWI_ServiceAdvisor))
        End Sub

#End Region

#Region "Retrieve"


        Public Function Retrieve(ByVal ID As Integer) As VWI_ServiceAdvisor
            Return CType(m_VWI_ServiceAdvisorMapper.Retrieve(ID), VWI_ServiceAdvisor)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VWI_ServiceAdvisorMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VWI_ServiceAdvisorMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_ServiceAdvisor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_ServiceAdvisorMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VWI_ServiceAdvisorMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_ServiceAdvisor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_ServiceAdvisorMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VWI_ServiceAdvisor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VWI_ServiceAdvisorMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VWI_ServiceAdvisorList As ArrayList = m_VWI_ServiceAdvisorMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VWI_ServiceAdvisorList
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim VWI_ServiceAdvisorList As ArrayList = m_VWI_ServiceAdvisorMapper.RetrieveByCriteria(criterias)

            Return VWI_ServiceAdvisorList
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Dim VWI_ServiceAdvisorList As ArrayList = m_VWI_ServiceAdvisorMapper.RetrieveByCriteria(criterias, sorts)

            Return VWI_ServiceAdvisorList
        End Function

#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
