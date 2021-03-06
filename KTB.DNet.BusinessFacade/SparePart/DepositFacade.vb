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

    Public Class DepositFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_DepositMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DepositMapper = MapperFactory.GetInstance().GetMapper(GetType(Deposit).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(Deposit))
            Me.DomainTypeCollection.Add(GetType(DepositLine))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As Deposit
            Return CType(m_DepositMapper.Retrieve(ID), Deposit)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DepositMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_DepositMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

        Public Function Retrieve(ByVal Code As String) As Deposit
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Deposit), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Deposit), "ClaimNumber", MatchType.Exact, Code))

            Dim DepositColl As ArrayList = m_DepositMapper.RetrieveByCriteria(criterias)
            If (DepositColl.Count > 0) Then
                Return CType(DepositColl(0), Deposit)
            End If
            Return New Deposit
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is Deposit) Then
                CType(InsertArg.DomainObject, Deposit).ID = InsertArg.ID
                CType(InsertArg.DomainObject, Deposit).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is DepositLine) Then
                CType(InsertArg.DomainObject, DepositLine).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As Deposit
            Dim DepositColl As ArrayList = m_DepositMapper.RetrieveByCriteria(criterias)
            If DepositColl.Count > 0 Then
                Return CType(DepositColl(0), Deposit)
            Else
                Return Nothing
            End If
        End Function

        Public Function Insert(ByVal oDeposit As Deposit) As Integer
            Dim returnValue As Integer = -1

            'If Me.IsTaskFree() Then 'To view error on parser log
            Try
                Me.SetTaskLocking()

                '-- Check to see if the Deposit header already exists
                If Not isExistDepositHead(oDeposit) Then

                    '-- Insert this deposit along with its lines
                    m_TransactionManager.AddInsert(oDeposit, m_userPrincipal.Identity.Name)
                    For Each item As DepositLine In oDeposit.DepositLines
                        item.Deposit = oDeposit
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next
                Else
                    '-- The Deposit header already exists
                    Dim IsDepositLineEmpty As Boolean = IIf(oDeposit.DepositLines.Count = 0, True, False)


                    '-- Retrieve and assign Deposit header's ID first
                    AssignDepositHeaderID(oDeposit)

                    '-- Insert or update Deposit lines
                    InsertOrUpdateDepositLines(oDeposit, IsDepositLineEmpty)

                    '-- Update Deposit header
                    m_TransactionManager.AddUpdate(oDeposit, m_userPrincipal.Identity.Name)

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
            'End If

            Return returnValue
        End Function

        Private Function isExistDepositHead(ByVal oDeposit As Deposit) As Boolean
            '-- Check to see if the Deposit header already exists

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Deposit), "Dealer.DealerCode", MatchType.Exact, oDeposit.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(Deposit), "Period", MatchType.Exact, oDeposit.Period))
            Dim _depositList As ArrayList = New DepositFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            Return _depositList.Count <> 0
        End Function

        Public Sub AssignDepositHeaderID(ByVal oDeposit As Deposit)
            '-- Retrieve and assign Deposit header's ID first

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Deposit), "Dealer.DealerCode", MatchType.Exact, oDeposit.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(Deposit), "Period", MatchType.Exact, oDeposit.Period))
            Dim _depositList As ArrayList = New DepositFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

            oDeposit.ID = CType(_depositList(0), Deposit).ID  '-- Assign Deposit header's ID
            oDeposit.CreatedBy = CType(_depositList(0), Deposit).CreatedBy

        End Sub

        Private Sub InsertOrUpdateDepositLines(ByVal oDeposit As Deposit, Optional ByVal IsDepositLineEmpty As Boolean = False)
            '-- Insert or update Deposit lines

            '-- Firstly, delete unmatched old records
            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "Deposit.Dealer.DealerCode", MatchType.Exact, oDeposit.Dealer.DealerCode))
            'criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, oDeposit.Period))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "Deposit.ID", MatchType.Exact, oDeposit.ID))
            Dim _DepositLines As ArrayList = New DepositLineFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)

            For Each oDepositLine As DepositLine In _DepositLines
                '  If Not isExistsIn(oDepositLine, oDeposit) Then
                '-- Delete Deposit line
                oDepositLine.RowStatus = DBRowStatus.Deleted  '-- Delete status
                'oDepositLine.CreatedBy = m_userPrincipal.Identity.Name
                'm_TransactionManager.AddUpdate(oDepositLine, m_userPrincipal.Identity.Name)
                m_TransactionManager.AddDelete(oDepositLine)
                '  End If
            Next

            If IsDepositLineEmpty = False Then
                For Each oDepositLine As DepositLine In oDeposit.DepositLines
                    oDepositLine.Deposit = oDeposit  '-- Its Deposit header

                    ' If isExistDepositLine(oDepositLine) Then
                    '-- Set its ID and its parents' objects
                    'SetDepositLineID(oDepositLine)

                    '-- Update Deposit line

                    ' m_TransactionManager.AddUpdate(oDepositLine, m_userPrincipal.Identity.Name)

                    ' Else
                    '-- Insert Deposit line
                    m_TransactionManager.AddInsert(oDepositLine, m_userPrincipal.Identity.Name)

                    'End If
                Next
            End If
            '-- Secondly, insert new or update old records
           

        End Sub

        Private Function isExistsIn(ByVal oDepositLine As DepositLine, ByVal oDeposit As Deposit) As Boolean
            '-- Return True if oDepositLine is in oDeposit, otherwise return False

            Dim bExists As Boolean = False

            For Each item As DepositLine In oDeposit.DepositLines
                If oDepositLine.Deposit.Dealer.DealerCode = item.Deposit.Dealer.DealerCode And _
                   oDepositLine.Deposit.Period = item.Deposit.Period And _
                   oDepositLine.DocumentNo = item.DocumentNo And _
                   ((oDepositLine.Credit > 0 And item.Credit > 0) Or (oDepositLine.Debit > 0 And item.Debit > 0)) Then

                    bExists = True
                    Exit For
                End If
            Next

            Return bExists
        End Function

        Private Function isExistDepositLine(ByVal oDepositLine As DepositLine) As Boolean
            '-- Check to see if the Deposit line already exists

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "Deposit.Dealer.DealerCode", MatchType.Exact, oDepositLine.Deposit.Dealer.DealerCode))
            'criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, oDepositLine.Deposit.Period))
            'criterias.opAnd(New Criteria(GetType(DepositLine), "DocumentNo", MatchType.Exact, oDepositLine.DocumentNo))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "Deposit.ID", MatchType.Exact, oDepositLine.Deposit.ID))
            criterias.opAnd(New Criteria(GetType(DepositLine), "DocumentNo", MatchType.Exact, oDepositLine.DocumentNo))

            If oDepositLine.Credit > 0 Then
                criterias.opAnd(New Criteria(GetType(DepositLine), "Credit", MatchType.Greater, 0))

            Else
                If oDepositLine.Debit > 0 Then
                    criterias.opAnd(New Criteria(GetType(DepositLine), "Debit", MatchType.Greater, 0))

                End If

            End If


            Dim _DepositLines As ArrayList = New DepositLineFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            Return _DepositLines.Count <> 0

        End Function

        Private Sub SetDepositLineID(ByVal oDepositLine As DepositLine)
            '-- Set its ID and its parents' objects

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "Deposit.Dealer.DealerCode", MatchType.Exact, oDepositLine.Deposit.Dealer.DealerCode))
            'criterias.opAnd(New Criteria(GetType(DepositLine), "Deposit.Period", MatchType.Exact, oDepositLine.Deposit.Period))
            'criterias.opAnd(New Criteria(GetType(DepositLine), "DocumentNo", MatchType.Exact, oDepositLine.DocumentNo))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositLine), "Deposit.ID", MatchType.Exact, oDepositLine.Deposit.ID))
            criterias.opAnd(New Criteria(GetType(DepositLine), "DocumentNo", MatchType.Exact, oDepositLine.DocumentNo))

            If oDepositLine.Credit > 0 Then
                criterias.opAnd(New Criteria(GetType(DepositLine), "Credit", MatchType.Greater, 0))

            Else
                If oDepositLine.Debit > 0 Then
                    criterias.opAnd(New Criteria(GetType(DepositLine), "Debit", MatchType.Greater, 0))

                End If

            End If

            Dim oDepositLines As ArrayList = New DepositLineFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias)
            oDepositLine.ID = oDepositLines(0).ID
            oDepositLine.CreatedBy = CType(oDepositLines(0), DepositLine).CreatedBy
            '-- Set its ID

            'Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Deposit), "Dealer.DealerCode", MatchType.Exact, oDepositLine.Deposit.Dealer.DealerCode))
            'criterias2.opAnd(New Criteria(GetType(Deposit), "Period", MatchType.Exact, oDepositLine.Deposit.Period))
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Deposit), "ID", MatchType.Exact, oDepositLine.Deposit.ID))
            Dim oDeposits As ArrayList = New DepositFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(criterias2)
            oDepositLine.Deposit = oDeposits(0)  '-- Set its Deposit header

        End Sub

        Public Function Delete(ByVal objDomain As Deposit) As Integer
            Dim returnValue As Integer = -1

            If Me.IsTaskFree() Then
                Try
                    Me.SetTaskLocking()
                    Dim ObjMapper As IMapper

                    For Each item As DepositLine In objDomain.DepositLines
                        item.Deposit = objDomain
                        m_TransactionManager.AddDelete(item)
                    Next
                    m_TransactionManager.AddDelete(objDomain)

                    m_TransactionManager.PerformTransaction()
                    returnValue = 0

                Catch ex As Exception
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

    End Class

End Namespace