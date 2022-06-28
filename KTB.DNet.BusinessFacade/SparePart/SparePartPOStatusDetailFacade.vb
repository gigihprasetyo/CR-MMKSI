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

    Public Class SparePartPOStatusDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPOStatusDetailMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPOStatusDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPOStatusDetail).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf TransactionManager_Insert)

            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SparePartPOStatus))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SparePartPOStatusDetail))

        End Sub

#End Region

#Region "Transaction/Other Public Method"

        Private Sub TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SparePartPOStatus) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.SparePartPOStatus).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SparePartPOStatus).MarkLoaded()

            End If

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SparePartPOStatusDetail
            Return CType(m_SparePartPOStatusDetailMapper.Retrieve(ID), SparePartPOStatusDetail)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Decimal
            Dim returnValue As Decimal = 0
            returnValue = m_SparePartPOStatusDetailMapper.RetrieveScalar(aggr, crit)
            Return returnValue
        End Function

        Public Function RetrieveList(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPOStatusDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPOStatusDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPPODetailsColl As ArrayList = m_SparePartPOStatusDetailMapper.RetrieveByCriteria(Criterias, sortColl)
            Return SPPODetailsColl
        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(SparePartPOStatusDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPOStatusDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
#End Region


    End Class

End Namespace

