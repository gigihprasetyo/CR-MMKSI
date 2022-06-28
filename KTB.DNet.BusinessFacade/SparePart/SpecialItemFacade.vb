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
    Public Class SpecialItemFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SpecialItemDetailMapper As IMapper
        Private m_SpecialItemHeaderMapper As IMapper
        Private m_SpecialItemPackageMapper As IMapper
        Private m_SpecialItemGroupMapper As IMapper
        Private m_SpecialItemPartMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SpecialItemDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SpecialItemDetail).ToString)
            Me.m_SpecialItemHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(SpecialItemHeader).ToString)
            Me.m_SpecialItemPackageMapper = MapperFactory.GetInstance.GetMapper(GetType(SpecialItemPackage).ToString)
            Me.m_SpecialItemGroupMapper = MapperFactory.GetInstance.GetMapper(GetType(VIEW_ExtMaterialGroup).ToString)
            Me.m_SpecialItemPartMapper = MapperFactory.GetInstance.GetMapper(GetType(VIEW_PartNumberByExtMaterialGroup).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

            Me.DomainTypeCollection.Add(GetType(SpecialItemDetail))
            Me.DomainTypeCollection.Add(GetType(SpecialItemHeader))
            Me.DomainTypeCollection.Add(GetType(SpecialItemPackage))

        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveSIHeader(ByVal Month As Integer, ByVal Year As Integer, ByVal reference As String) As SpecialItemHeader
            'todo
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemHeader), "MonthPeriode", MatchType.Exact, Month))
            criterias.opAnd(New Criteria(GetType(SpecialItemHeader), "YearPeriode", MatchType.Exact, Year))
            criterias.opAnd(New Criteria(GetType(SpecialItemHeader), "Reference", MatchType.Exact, reference.Trim))
            Dim SIHeaders As ArrayList = m_SpecialItemHeaderMapper.RetrieveByCriteria(criterias)
            If SIHeaders.Count > 0 Then
                Return CType(SIHeaders(0), SpecialItemHeader)
            End If
            Return Nothing
        End Function

        Public Function RetrieveSpecialItemDetail(ByVal ID As Integer) As SpecialItemDetail
            Return CType(m_SpecialItemDetailMapper.Retrieve(ID), SpecialItemDetail)
        End Function

        Public Function RetrieveSpecialItemHeader(ByVal ID As Integer) As SpecialItemHeader
            Return CType(m_SpecialItemHeaderMapper.Retrieve(ID), SpecialItemHeader)
        End Function

        Public Function RetrieveSpecialItemDetailList(ByVal criteria As CriteriaComposite) As ArrayList
            'Dim crit As criteria = New criteria( _
            '    GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short))
            'criteria.opAnd(crit)

            'criteria.opAnd(New criteria(GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_SpecialItemDetailMapper.RetrieveByCriteria(criteria)
        End Function

        Public Function RetrieveSpecialItemDetailList(ByVal criteria As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) And Not sortColumn = "" Then
                sortColl.Add(New Sort(GetType(SpecialItemDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SpecialItemDetailColl As ArrayList = m_SpecialItemDetailMapper.RetrieveByCriteria(criteria, sortColl, pageNumber, pageSize, totalRow)

            Return SpecialItemDetailColl
        End Function

        Public Function RetrieveSpecialItemDetailListByID(ByVal IDSpecialItemDetail As Integer) As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria( _
                GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(SpecialItemDetail), "ID", MatchType.Exact, IDSpecialItemDetail))
            Return m_SpecialItemDetailMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveSpecialItemDetailList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria( _
                GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_SpecialItemDetailMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveSpecialItemGroupList(ByVal nMonth As Integer, ByVal nYear As Integer) As ArrayList
            'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria( _
            '    GetType(VIEW_ExtMaterialGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VIEW_ExtMaterialGroup), "MonthPeriode", MatchType.Exact, nMonth))
            crit.opAnd(New Criteria(GetType(VIEW_ExtMaterialGroup), "YearPeriode", MatchType.Exact, nYear))
            Return m_SpecialItemGroupMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveSpecialItemPartList(ByVal nMonth As Integer, ByVal nYear As Integer) As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VIEW_PartNumberByExtMaterialGroup), "MonthPeriode", MatchType.Exact, nMonth))
            crit.opAnd(New Criteria(GetType(VIEW_PartNumberByExtMaterialGroup), "YearPeriode", MatchType.Exact, nYear))
            Return m_SpecialItemPartMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveSpecialItemPartList(ByVal nMonth As Integer, ByVal nYear As Integer, ByVal MtrGroup As String) As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VIEW_PartNumberByExtMaterialGroup), "MonthPeriode", MatchType.Exact, nMonth))
            crit.opAnd(New Criteria(GetType(VIEW_PartNumberByExtMaterialGroup), "YearPeriode", MatchType.Exact, nYear))
            crit.opAnd(New Criteria(GetType(VIEW_PartNumberByExtMaterialGroup), "ExtMaterialGroup", MatchType.Exact, MtrGroup))
            Return m_SpecialItemPartMapper.RetrieveByCriteria(crit)
        End Function
#End Region

#Region "Transaction/Other Public Method"

#End Region

#Region "Custom Method"

        Public Function InsertSPSI(ByVal siHeader As SpecialItemHeader) As Integer
            '-- Insert Sparepart special item header, details, and packages

            Dim retValue As Integer = -1  '-- Assume not successful at first

            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()

                    DeleteSpecItem(siHeader)  '-- Firstly, delete SI header & its children if any
                    InsertSpecItem(siHeader)  '-- Secondly, insert SI header & its children

                    m_TransactionManager.PerformTransaction()
                    retValue = 1  '-- Successfully update sparepart SI

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return retValue  '-- Return status

        End Function

        Public Function UpdateSpecialItemHeader(ByVal objDomain As SpecialItemHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SpecialItemHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateSpecialItemDetail(ByVal objDomain As SpecialItemDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SpecialItemDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateSpecialItemPackage(ByVal objDomain As SpecialItemPackage) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SpecialItemPackageMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Function CountDetailInHeader(ByVal IDHeader As Integer) As Integer
            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SpecialItemDetail), "SpecialItemHeader", MatchType.Exact, IDHeader))
            criteria.opAnd(New criteria(GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim tempArrList As ArrayList = Me.RetrieveSpecialItemDetailList(criteria)
            Return tempArrList.Count
        End Function

        Private Function CountDetail(ByVal IDDetail As Integer) As Integer
            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SpecialItemDetail), "ID", MatchType.Exact, IDDetail))
            criteria.opAnd(New criteria(GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim tempArrList As ArrayList = Me.RetrieveSpecialItemDetailList(criteria)
            Return tempArrList.Count
        End Function

        Public Function DeleteSpecialItemDetail(ByVal objDomain As SpecialItemDetail) As Integer
            Dim nResult As Integer = -1
            Dim IDHeader As Integer
            Dim objHeader As SpecialItemHeader = New SpecialItemHeader

            If CountDetail(objDomain.ID) <> 0 Then
                Try
                    objHeader = Me.RetrieveSpecialItemHeader(objDomain.SpecialItemHeader.ID)
                    For Each ItemPackage As SpecialItemPackage In objDomain.SpecialItemPackages
                        ItemPackage.RowStatus = CType(DBRowStatus.Deleted, Short)
                        Me.UpdateSpecialItemPackage(ItemPackage)
                    Next
                    objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Me.UpdateSpecialItemDetail(objDomain)

                    If Me.CountDetailInHeader(objHeader.ID) < 1 Then
                        objHeader.RowStatus = CType(DBRowStatus.Deleted, Short)
                        Me.UpdateSpecialItemHeader(objHeader)
                    End If

                    nResult = 1
                Catch ex As Exception
                    nResult = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
            Else
                nResult = -1
            End If

            Return nResult
        End Function

        Private Sub DeleteSpecItem(ByVal oHeader As SpecialItemHeader)
            '-- Delete special item header & its children from database

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemHeader), "MonthPeriode", MatchType.Exact, oHeader.MonthPeriode))
            criterias.opAnd(New Criteria(GetType(SpecialItemHeader), "YearPeriode", MatchType.Exact, oHeader.YearPeriode))
            criterias.opAnd(New Criteria(GetType(SpecialItemHeader), "Reference", MatchType.Exact, oHeader.Reference.Trim))

            Dim SIHeaders As ArrayList = m_SpecialItemHeaderMapper.RetrieveByCriteria(criterias)

            For Each siHeader As SpecialItemHeader In SIHeaders

                Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemDetail), "SpecialItemHeader.ID", MatchType.Exact, siHeader.ID))
                Dim SIDetails As ArrayList = m_SpecialItemDetailMapper.RetrieveByCriteria(criterias2)

                '-- Delete details one-by-one
                For Each siDetail As SpecialItemDetail In SIDetails

                    Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemPackage), "SpecialItemDetail.ID", MatchType.Exact, siDetail.ID))
                    Dim SIPackages As ArrayList = m_SpecialItemPackageMapper.RetrieveByCriteria(criterias3)

                    '-- Delete packages one-by-one
                    For Each siPackage As SpecialItemPackage In SIPackages

                        m_TransactionManager.AddDelete(siPackage)  '-- Delete each package
                    Next

                    m_TransactionManager.AddDelete(siDetail)  '-- Delete each detail
                Next

                m_TransactionManager.AddDelete(siHeader)  '-- Delete header
            Next

        End Sub

        Public Function GetSpecialItemHeaderByPeriod(ByVal year As Integer, ByVal month As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemHeader), "MonthPeriode", MatchType.Exact, month))
            criterias.opAnd(New Criteria(GetType(SpecialItemHeader), "YearPeriode", MatchType.Exact, year))
            Dim SIHeaders As ArrayList = m_SpecialItemHeaderMapper.RetrieveByCriteria(criterias)
            Return SIHeaders
        End Function

        Private Sub InsertSpecItem(ByVal siHeader As SpecialItemHeader)
            '-- Insert special item header & its children

            m_TransactionManager.AddInsert(siHeader, m_userPrincipal.Identity.Name)  '-- Insert header

            '-- Insert details one-by-one
            For Each siDetail As SpecialItemDetail In siHeader.SpecialItemDetails

                siDetail.SpecialItemHeader = siHeader
                m_TransactionManager.AddInsert(siDetail, m_userPrincipal.Identity.Name)  '-- Insert each detail

                '-- Insert packages one-by-one
                For Each siPackage As SpecialItemPackage In siDetail.SpecialItemPackages

                    siPackage.SpecialItemDetail = siDetail
                    m_TransactionManager.AddInsert(siPackage, m_userPrincipal.Identity.Name)  '-- Insert each package
                Next
            Next

        End Sub

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SpecialItemHeader) Then
                CType(InsertArg.DomainObject, SpecialItemHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SpecialItemHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is SpecialItemDetail) Then
                CType(InsertArg.DomainObject, SpecialItemDetail).ID = InsertArg.ID
            End If

        End Sub

#End Region

    End Class

End Namespace
