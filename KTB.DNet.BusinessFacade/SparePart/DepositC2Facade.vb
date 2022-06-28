#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class DepositC2Facade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_DepositC2Mapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositC2Mapper = MapperFactory.GetInstance().GetMapper(GetType(DepositC2).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(DepositC2))
            Me.DomainTypeCollection.Add(GetType(DepositC2Line))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DepositC2
            Return CType(m_DepositC2Mapper.Retrieve(ID), DepositC2)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositC2Mapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_DepositC2Mapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function Retrieve(ByVal Code As String) As DepositC2
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositC2), "ClaimNumber", MatchType.Exact, Code))

            Dim DepositC2Coll As ArrayList = m_DepositC2Mapper.RetrieveByCriteria(criterias)
            If (DepositC2Coll.Count > 0) Then
                Return CType(DepositC2Coll(0), DepositC2)
            End If
            Return New DepositC2
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is DepositC2) Then
                CType(InsertArg.DomainObject, DepositC2).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DepositC2).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositC2Line) Then
                CType(InsertArg.DomainObject, DepositC2Line).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As DepositC2
            Dim DepositC2Coll As ArrayList = m_DepositC2Mapper.RetrieveByCriteria(criterias, sorts)
            If DepositC2Coll.Count > 0 Then
                Return CType(DepositC2Coll(0), DepositC2)
            Else
                Return Nothing
            End If
        End Function

        Public Function Insert(ByVal oDepositC2 As DepositC2) As Integer
            Dim returnValue As Integer = -1

            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()

                    '-- Check to see if the DepositC2 header already exists
                    If Not isExistDepositC2Head(oDepositC2) Then

                        '-- Insert this deposit along with its lines
                        m_TransactionManager.AddInsert(oDepositC2, m_userPrincipal.Identity.Name)
                        For Each item As DepositC2Line In oDepositC2.DepositC2Lines
                            item.DepositC2 = oDepositC2
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        '-- The DepositC2 header already exists

                        '-- Retrieve and assign DepositC2 header's ID first
                        AssignDepositC2HeaderID(oDepositC2)

                        '-- Insert or update DepositC2 lines
                        InsertOrUpdateDepositC2Lines(oDepositC2)

                        '-- Update DepositC2 header
                        m_TransactionManager.AddUpdate(oDepositC2, m_userPrincipal.Identity.Name)

                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = 0

                Catch ex As Exception
                    If ExceptionPolicy.HandleException(ex, "Domain Policy") Then
                        Throw
                    End If

                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If

            Return returnValue
        End Function

        Private Function isExistDepositC2Head(ByVal oDepositC2 As DepositC2) As Boolean
            '-- Check to see if the DepositC2 header already exists

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2), "Dealer.DealerCode", MatchType.Exact, oDepositC2.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(DepositC2), "Period", MatchType.Exact, oDepositC2.Period))
            Dim _depositList As ArrayList = New DepositC2Facade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            Return _depositList.Count <> 0
        End Function

        Public Sub AssignDepositC2HeaderID(ByVal oDepositC2 As DepositC2)
            '-- Retrieve and assign DepositC2 header's ID first

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2), "Dealer.DealerCode", MatchType.Exact, oDepositC2.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(DepositC2), "Period", MatchType.Exact, oDepositC2.Period))
            Dim _depositList As ArrayList = New DepositC2Facade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            oDepositC2.ID = CType(_depositList(0), DepositC2).ID  '-- Assign DepositC2 header's ID
            oDepositC2.CreatedBy = CType(_depositList(0), DepositC2).CreatedBy

        End Sub

        Private Sub InsertOrUpdateDepositC2Lines(ByVal oDepositC2 As DepositC2)
            '-- Insert or update DepositC2 lines

            '-- Firstly, delete unmatched old records
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2Line), "DepositC2.Dealer.DealerCode", MatchType.Exact, oDepositC2.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DepositC2.Period", MatchType.Exact, oDepositC2.Period))
            Dim _DepositC2Lines As ArrayList = New DepositC2LineFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            For Each oDepositC2Line As DepositC2Line In _DepositC2Lines
                If Not isExistsIn(oDepositC2Line, oDepositC2) Then
                    '-- Delete DepositC2 line
                    oDepositC2Line.RowStatus = DBRowStatus.Deleted  '-- Delete status
                    oDepositC2Line.CreatedBy = oDepositC2.CreatedBy
                    m_TransactionManager.AddUpdate(oDepositC2Line, m_userPrincipal.Identity.Name)
                End If
            Next

            '-- Secondly, insert new or update old records
            For Each oDepositC2Line As DepositC2Line In oDepositC2.DepositC2Lines
                oDepositC2Line.DepositC2 = oDepositC2  '-- Its DepositC2 header

                If isExistDepositC2Line(oDepositC2Line) Then
                    '-- Set its ID and its parents' objects
                    SetDepositC2LineID(oDepositC2Line)

                    '-- Update DepositC2 line
                    oDepositC2Line.CreatedBy = oDepositC2.CreatedBy
                    m_TransactionManager.AddUpdate(oDepositC2Line, m_userPrincipal.Identity.Name)

                Else
                    '-- Insert DepositC2 line
                    m_TransactionManager.AddInsert(oDepositC2Line, m_userPrincipal.Identity.Name)

                End If
            Next

        End Sub

        Private Function isExistsIn(ByVal oDepositC2Line As DepositC2Line, ByVal oDepositC2 As DepositC2) As Boolean
            '-- Return True if oDepositC2Line is in oDepositC2, otherwise return False

            Dim bExists As Boolean = False

            For Each item As DepositC2Line In oDepositC2.DepositC2Lines
                If oDepositC2Line.DepositC2.Dealer.DealerCode = item.DepositC2.Dealer.DealerCode And _
                   oDepositC2Line.DepositC2.Period = item.DepositC2.Period And _
                   oDepositC2Line.DocumentNo = item.DocumentNo Then

                    bExists = True
                    Exit For
                End If
            Next

            Return bExists
        End Function

        Private Function isExistDepositC2Line(ByVal oDepositC2Line As DepositC2Line) As Boolean
            '-- Check to see if the DepositC2 line already exists

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2Line), "DepositC2.Dealer.DealerCode", MatchType.Exact, oDepositC2Line.DepositC2.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DepositC2.Period", MatchType.Exact, oDepositC2Line.DepositC2.Period))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DocumentNo", MatchType.Exact, oDepositC2Line.DocumentNo))

            Dim _DepositC2Lines As ArrayList = New DepositC2LineFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            Return _DepositC2Lines.Count <> 0

        End Function

        Private Sub SetDepositC2LineID(ByVal oDepositC2Line As DepositC2Line)
            '-- Set its ID and its parents' objects

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2Line), "DepositC2.Dealer.DealerCode", MatchType.Exact, oDepositC2Line.DepositC2.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DepositC2.Period", MatchType.Exact, oDepositC2Line.DepositC2.Period))
            criterias.opAnd(New Criteria(GetType(DepositC2Line), "DocumentNo", MatchType.Exact, oDepositC2Line.DocumentNo))
            Dim oDepositC2Lines As ArrayList = New DepositC2LineFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            oDepositC2Line.ID = oDepositC2Lines(0).ID  '-- Set its ID

            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositC2), "Dealer.DealerCode", MatchType.Exact, oDepositC2Line.DepositC2.Dealer.DealerCode))
            criterias2.opAnd(New Criteria(GetType(DepositC2), "Period", MatchType.Exact, oDepositC2Line.DepositC2.Period))
            Dim oDepositC2s As ArrayList = New DepositC2Facade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias2)
            oDepositC2Line.DepositC2 = oDepositC2s(0)  '-- Set its DepositC2 header
            oDepositC2Line.DepositC2.CreatedBy = m_userPrincipal.Identity.Name
        End Sub

#End Region

    End Class

End Namespace
