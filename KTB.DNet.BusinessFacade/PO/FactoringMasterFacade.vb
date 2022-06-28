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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.PO
    Public Class FactoringMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FactoringMasterMapper As IMapper
        Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_V_DraftPOTotalDetailMapper As IMapper

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_FactoringMasterMapper = MapperFactory.GetInstance().GetMapper(GetType(FactoringMaster).ToString)
            Me.m_V_DraftPOTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_DraftPOTotalDetail).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FactoringMaster
            Return CType(m_FactoringMasterMapper.Retrieve(ID), FactoringMaster)
        End Function
        Public Function Retrieve(ByVal ID As String) As FactoringMaster
            Return CType(m_FactoringMasterMapper.Retrieve(Convert.ToInt32(ID)), FactoringMaster)
        End Function
        Public Function Retrieve_(ByVal CreditAccount As String) As FactoringMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.Exact, CreditAccount))

            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias)
            If (FactoringMasterColl.Count > 0) Then
                Return CType(FactoringMasterColl(0), FactoringMaster)
            End If
            Return New FactoringMaster
        End Function

        Public Function Retrieve(ByVal PC As ProductCategory, ByVal CreditAccount As String) As FactoringMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.Exact, CreditAccount))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), "ProductCategory.ID", MatchType.Exact, pc.id))

            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias)
            If (FactoringMasterColl.Count > 0) Then
                Return CType(FactoringMasterColl(0), FactoringMaster)
            End If
            Return New FactoringMaster
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FactoringMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FactoringMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FactoringMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FactoringMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FactoringMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FactoringMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FactoringMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("FactoringMasterCode")) Then
                sortColl.Add(New Sort(GetType(FactoringMaster), "FactoringMasterCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim _FactoringMaster As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias, sortColl)
            Return _FactoringMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FactoringMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FactoringMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), columnName, matchOperator, columnValue))
            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FactoringMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FactoringMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), columnName, matchOperator, columnValue))

            Return m_FactoringMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FactoringMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FactoringMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FactoringMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FactoringMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FactoringMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FactoringMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As FactoringMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FactoringMasterMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FactoringMaster)
            Try
                m_FactoringMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "FactoringMasterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(FactoringMaster), "FactoringMasterCode", AggregateType.Count)

            Return CType(m_FactoringMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FactoringMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FactoringMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FactoringMasterColl
        End Function

        Public Function GetCeilingComponent_(ByVal CreditAccount As String, Optional ByVal oFMUploaded As FactoringMaster = Nothing) As ArrayList
            Dim Outstanding As Decimal = 0
            Dim ProposedPO As Decimal = 0
            Dim LiquefiedPO As Decimal = 0
            Dim User As System.Security.Principal.IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oPOHFac As POHeaderFacade = New POHeaderFacade(User)
            Dim cPOH As CriteriaComposite
            Dim aPOH As New ArrayList
            Dim aResult As New ArrayList
            Dim Sql As String = ""
            Dim oFM As FactoringMaster
            Dim rsl As Object
            Dim dUploadDate As Date

            oFM = New FactoringMasterFacade(User).Retrieve(CreditAccount)
            dUploadDate = oFM.LastUploadedTime '  oFM.LastUpdateTime
            'Ceiling Factoring Master
            If Not isnothing(oFMUploaded) Then
                oFM = oFMUploaded
            End If

            aResult.Add(oFM.FactoringCeiling)
            aResult.Add(oFM.GiroTolakan)
            Outstanding = oFM.Outstanding

            'Outstanding
            'cPOH = New CriteriaComposite(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "IsFactoring", MatchType.Exact, 1))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "Status", MatchType.Exact, CType(enumStatusPO.Status.Selesai, Short)))
            'Sql = "select count(*) from DailyPayment dp " & _
            '    " where dp.POID=V_POTotalDetail.ID and dp.RowStatus=0 " & _
            '    " and dp.RejectStatus=0 and dp.IsCleared=0 and dp.IsReversed=0 and dp.EffectiveDate>'" & Now.ToString("yyyy.MM.dd") & "'"
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.No, "(" & Sql & ")"))

            'Dim agg As Aggregate = New Aggregate(GetType(V_POTotalDetail), "TotalDetail", AggregateType.Sum)
            'rsl = m_V_POTotalDetailMapper.RetrieveScalar(agg, cPOH)
            'Outstanding = IIf(rsl Is DBNull.Value, 0, rsl)

            aResult.Add(Outstanding)

            'Proposed PO
            cPOH = New CriteriaComposite(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "IsFactoring", MatchType.Exact, 1))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Short).ToString & "," & CType(enumStatusPO.Status.Konfirmasi, Short).ToString & "," & CType(enumStatusPO.Status.Rilis, Short).ToString & "," & CType(enumStatusPO.Status.Setuju, Short).ToString & "," & CType(enumStatusPO.Status.Selesai, Short).ToString & ")"))
            Dim sqlHavingGyro As String
            sqlHavingGyro = " select count(dp.ID) from DailyPayment dp with (nolock) inner join FactoringMaster fm on fm.CreditAccount=V_POTotalDetail.CreditAccount "
            sqlHavingGyro &= " where dp.POID = V_POTotalDetail.ID And dp.Status = " & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short).ToString
            sqlHavingGyro &= " and dp.CessieID>0 and dp.CessieTime<=fm.LastUploadedTime "
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, "(" & sqlHavingGyro & ")"))
            Dim agg2 As Aggregate = New Aggregate(GetType(V_POTotalDetail), "TotalDetail", AggregateType.Sum)
            rsl = m_V_POTotalDetailMapper.RetrieveScalar(agg2, cPOH)
            ProposedPO = IIf(rsl Is DBNull.Value, 0, rsl)
            aResult.Add(ProposedPO)


            'LiquefiedPO
            'cPOH = New CriteriaComposite(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "IsFactoring", MatchType.Exact, 1))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "Status", MatchType.Exact, CType(enumStatusPO.Status.Selesai, Short)))
            'Sql = "select count(*) from DailyPayment dp " & _
            '    " where dp.POID=V_POTotalDetail.ID and dp.RowStatus=0 " & _
            '    " and dp.RejectStatus=0 and dp.IsCleared=0 and dp.IsReversed=0 and dp.EffectiveDate='" & Now.ToString("yyyy.MM.dd") & "'"
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.No, "(" & Sql & ")"))

            'Dim agg3 As Aggregate = New Aggregate(GetType(V_POTotalDetail), "TotalDetail", AggregateType.Sum)
            'rsl = m_V_POTotalDetailMapper.RetrieveScalar(agg3, cPOH)
            'LiquefiedPO = IIf(rsl Is DBNull.Value, 0, rsl)
            aResult.Add(LiquefiedPO)


            Return aResult
        End Function

        Public Function GetCeilingComponent(ByVal PC As productCategory, ByVal CreditAccount As String, Optional ByVal oFMUploaded As FactoringMaster = Nothing) As ArrayList
            Dim Outstanding As Decimal = 0
            Dim ProposedPO As Decimal = 0
            Dim LiquefiedPO As Decimal = 0
            Dim User As System.Security.Principal.IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oPOHFac As POHeaderFacade = New POHeaderFacade(User)
            Dim cPOH As CriteriaComposite
            Dim aPOH As New ArrayList
            Dim aResult As New ArrayList
            Dim Sql As String = ""
            Dim oFM As FactoringMaster
            Dim rsl As Object
            Dim dUploadDate As Date
            Dim ProposedPODraft As Decimal = 0
            Dim rsl2 As Object

            oFM = New FactoringMasterFacade(User).Retrieve(PC, CreditAccount)
            dUploadDate = oFM.LastUploadedTime '  oFM.LastUpdateTime
            'Ceiling Factoring Master
            If Not isnothing(oFMUploaded) Then
                oFM = oFMUploaded
            End If

            aResult.Add(oFM.FactoringCeiling)
            aResult.Add(oFM.GiroTolakan)
            Outstanding = oFM.Outstanding

            'Outstanding
            'cPOH = New CriteriaComposite(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "IsFactoring", MatchType.Exact, 1))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "Status", MatchType.Exact, CType(enumStatusPO.Status.Selesai, Short)))
            'Sql = "select count(*) from DailyPayment dp " & _
            '    " where dp.POID=V_POTotalDetail.ID and dp.RowStatus=0 " & _
            '    " and dp.RejectStatus=0 and dp.IsCleared=0 and dp.IsReversed=0 and dp.EffectiveDate>'" & Now.ToString("yyyy.MM.dd") & "'"
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.No, "(" & Sql & ")"))

            'Dim agg As Aggregate = New Aggregate(GetType(V_POTotalDetail), "TotalDetail", AggregateType.Sum)
            'rsl = m_V_POTotalDetailMapper.RetrieveScalar(agg, cPOH)
            'Outstanding = IIf(rsl Is DBNull.Value, 0, rsl)

            aResult.Add(Outstanding)

            'Proposed PO
            cPOH = New CriteriaComposite(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "ProductCategory.ID", MatchType.Exact, PC.ID))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "IsFactoring", MatchType.Exact, 1))
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Short).ToString & "," & CType(enumStatusPO.Status.Konfirmasi, Short).ToString & "," & CType(enumStatusPO.Status.Rilis, Short).ToString & "," & CType(enumStatusPO.Status.Setuju, Short).ToString & "," & CType(enumStatusPO.Status.Selesai, Short).ToString & ")"))
            Dim sqlHavingGyro As String
            sqlHavingGyro = " select count(dp.ID) from DailyPayment dp with (nolock) inner join FactoringMaster fm on fm.CreditAccount=V_POTotalDetail.CreditAccount "
            sqlHavingGyro &= " where dp.POID = V_POTotalDetail.ID And dp.Status = " & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short).ToString
            sqlHavingGyro &= " and dp.CessieID>0 and dp.CessieTime<=fm.LastUploadedTime "
            cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, "(" & sqlHavingGyro & ")"))
            Dim agg2 As Aggregate = New Aggregate(GetType(V_POTotalDetail), "TotalDetail", AggregateType.Sum)
            rsl = m_V_POTotalDetailMapper.RetrieveScalar(agg2, cPOH)
            ProposedPO = IIf(rsl Is DBNull.Value, 0, rsl)

            cPOH = New CriteriaComposite(New Criteria(GetType(V_DraftPOTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cPOH.opAnd(New Criteria(GetType(V_DraftPOTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            cPOH.opAnd(New Criteria(GetType(V_DraftPOTotalDetail), "ProductCategory.ID", MatchType.Exact, PC.ID))
            cPOH.opAnd(New Criteria(GetType(V_DraftPOTotalDetail), "IsFactoring", MatchType.Exact, 1))
            cPOH.opAnd(New Criteria(GetType(V_DraftPOTotalDetail), "Status", MatchType.InSet, "(" & CType(enumStatusPO.StatusDraftPO.Baru, Short).ToString & ")"))
            Dim agg3 As Aggregate = New Aggregate(GetType(V_DraftPOTotalDetail), "TotalDetail", AggregateType.Sum)
            rsl2 = m_V_DraftPOTotalDetailMapper.RetrieveScalar(agg3, cPOH)
            ProposedPODraft = IIf(rsl2 Is DBNull.Value, 0, rsl2)

            aResult.Add(ProposedPO + ProposedPODraft)


            'LiquefiedPO
            'cPOH = New CriteriaComposite(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "CreditAccount", MatchType.Exact, CreditAccount))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "IsFactoring", MatchType.Exact, 1))
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "Status", MatchType.Exact, CType(enumStatusPO.Status.Selesai, Short)))
            'Sql = "select count(*) from DailyPayment dp " & _
            '    " where dp.POID=V_POTotalDetail.ID and dp.RowStatus=0 " & _
            '    " and dp.RejectStatus=0 and dp.IsCleared=0 and dp.IsReversed=0 and dp.EffectiveDate='" & Now.ToString("yyyy.MM.dd") & "'"
            'cPOH.opAnd(New Criteria(GetType(V_POTotalDetail), "RowStatus", MatchType.No, "(" & Sql & ")"))

            'Dim agg3 As Aggregate = New Aggregate(GetType(V_POTotalDetail), "TotalDetail", AggregateType.Sum)
            'rsl = m_V_POTotalDetailMapper.RetrieveScalar(agg3, cPOH)
            'LiquefiedPO = IIf(rsl Is DBNull.Value, 0, rsl)
            aResult.Add(LiquefiedPO)


            Return aResult
        End Function


        Public Function GetAvailableCeiling_(ByVal CreditAccount As String, ByVal Ceiling As Decimal, ByVal Outstanding As Decimal, ByVal ProposedPO As Decimal, ByVal LiquifiedPO As Decimal, Optional ByVal oFMUploaded As FactoringMaster = Nothing) As Decimal
            'Start  :Available Ceiling will be calculated outside DNet System
            Dim User As System.Security.Principal.IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oFM As FactoringMaster

            If Not IsNothing(oFMUploaded) Then
                oFM = oFMUploaded
            Else
                oFM = New FactoringMasterFacade(User).Retrieve(CreditAccount)
            End If

            Return oFM.AvailableCeiling - ProposedPO
            'End    :Available Ceiling will be calculated outside DNet System

            Return (Ceiling - Outstanding) - ProposedPO + LiquifiedPO
        End Function

        Public Function GetAvailableCeiling(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal Ceiling As Decimal, ByVal Outstanding As Decimal, ByVal ProposedPO As Decimal, ByVal LiquifiedPO As Decimal, Optional ByVal oFMUploaded As FactoringMaster = Nothing) As Decimal
            'Start  :Available Ceiling will be calculated outside DNet System
            Dim User As System.Security.Principal.IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim oFM As FactoringMaster

            If Not IsNothing(oFMUploaded) Then
                oFM = oFMUploaded
            Else
                oFM = New FactoringMasterFacade(User).Retrieve(PC, CreditAccount)
            End If

            Return oFM.AvailableCeiling - ProposedPO
            'End    :Available Ceiling will be calculated outside DNet System

            Return (Ceiling - Outstanding) - ProposedPO + LiquifiedPO
        End Function


        Public Function IsAllowedToProposePO_(ByVal CreditAccount As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.Exact, CreditAccount))

            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias)
            If (FactoringMasterColl.Count > 0) Then
                If CType(FactoringMasterColl(0), FactoringMaster).Status = enumFactoringStatus.FactoringStatus.Active Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function IsAllowedToProposePO(ByVal PC As ProductCategory, ByVal CreditAccount As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.Exact, CreditAccount))
            criterias.opAnd(New Criteria(GetType(FactoringMaster), "ProductCategory.ID", MatchType.Exact, PC.ID))

            Dim FactoringMasterColl As ArrayList = m_FactoringMasterMapper.RetrieveByCriteria(criterias)
            If (FactoringMasterColl.Count > 0) Then
                If CType(FactoringMasterColl(0), FactoringMaster).Status = enumFactoringStatus.FactoringStatus.Active Then
                    Return True
                End If
            End If
            Return False
        End Function

#End Region

    End Class

End Namespace
