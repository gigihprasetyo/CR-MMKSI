
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
'// Copyright  2015
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:55:11 AM
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

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Benefit

    Public Class BenefitMasterDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BenefitMasterDetailMapper As IMapper


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BenefitMasterDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(BenefitMasterDetail).ToString)



        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BenefitMasterDetail
            Return CType(m_BenefitMasterDetailMapper.Retrieve(ID), BenefitMasterDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As BenefitMasterDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "BenefitMasterDetailCode", MatchType.Exact, Code))

            Dim BenefitMasterDetailColl As ArrayList = m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias)
            If (BenefitMasterDetailColl.Count > 0) Then
                Return CType(BenefitMasterDetailColl(0), BenefitMasterDetail)
            End If
            Return New BenefitMasterDetail
        End Function

       

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BenefitMasterDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitMasterDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitMasterDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitMasterDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BenefitMasterDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BenefitMasterDetail As ArrayList = m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias)
            Return _BenefitMasterDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitMasterDetailColl As ArrayList = m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BenefitMasterDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BenefitMasterDetailColl As ArrayList = m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BenefitMasterDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BenefitMasterDetailColl As ArrayList = m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), columnName, matchOperator, columnValue))
            Return BenefitMasterDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(BenefitMasterDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), columnName, matchOperator, columnValue))

            Return m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "BenefitMasterDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BenefitMasterDetail), "BenefitMasterDetailCode", AggregateType.Count)
            Return CType(m_BenefitMasterDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function CheckTransaction(ByVal BenefitType As BenefitType, ByVal AssyYear As Integer, _
                                         ByVal StartDate As Date, ByVal EndDate As Date, _
                                         ByVal VechileTypeID As String, ByVal leasingcompanyid As String, Optional ByVal listDealer As ArrayList = Nothing) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "BenefitMasterHeader.Status", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            'If cbDateClaim.Checked = True Then
            '    If CInt(icDateClaim.Value.ToString("yyyy")) > 1900 Then
            '        Dim tgl As New DateTime(CInt(icDateClaim.Value.Year), CInt(icDateClaim.Value.Month), CInt(icDateClaim.Value.Day), 0, 0, 0)
            '        criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimDate", MatchType.Exact, Format(tgl, "yyyy-MM-dd HH:mm:ss")))
            '    End If
            'End If

            Dim strSql As String = ""
            strSql += " select bmd.id from BenefitMasterDetail bmd"
            If Not listDealer Is Nothing AndAlso listDealer.Count > 0 Then
                Dim StrDealerID As String = ""

                For Each ObjDealer As Dealer In listDealer
                    If StrDealerID = "" Then
                        StrDealerID += ObjDealer.ID.ToString()
                    Else
                        StrDealerID += "," + ObjDealer.ID.ToString()
                    End If

                Next

                strSql += " inner join (select BenefitMasterDealer.BenefitMasterHeaderID FROM BenefitMasterDealer WHERE BenefitMasterDealer.ROwStatus=0  AND BenefitMasterDealer.DealerID IN (" + StrDealerID + ") GROUP BY BenefitMasterDealer.BenefitMasterHeaderID ) X ON X.BenefitMasterHeaderID = bmd.BenefitMasterHeaderID "
            End If
            strSql += " inner join BenefitMasterVehicleType bmv on bmd.ID = bmv.BenefitMasterDetailID and bmv.RowStatus = 0"
         


            strSql += " left join BenefitMasterLeasing bml on bmd.ID = bml.BenefitMasterDetailID and bml.RowStatus = 0"
            strSql += " left join VechileType vt on bmv.VechileTypeID = vt.ID and vt.RowStatus = 0"
            strSql += " where bmd.RowStatus = 0"
            strSql += " and BenefitTypeID = " & BenefitType.ID
            If BenefitType.AssyYearBox = 1 Then
                strSql += " and AssyYear = '" & AssyYear & "'"
            End If
            'strSql += " and ((FakturValidationStart >= '" & Format(StartDate, "yyyy-MM-dd HH:mm:ss") & "' and FakturValidationEnd <= '" & Format(StartDate, "yyyy-MM-dd HH:mm:ss") & "') or (FakturValidationStart >= '" & Format(EndDate, "yyyy-MM-dd HH:mm:ss") & "' and FakturValidationEnd <= '" & Format(EndDate, "yyyy-MM-dd HH:mm:ss") & "'))"

            If BenefitType.EventValidation = 1 Then
                strSql += " and (('" & Format(StartDate, "yyyy-MM-dd HH:mm:ss") & "' between FakturOpenStart  and FakturOpenEnd ) or "
                strSql += "  ('" & Format(EndDate, "yyyy-MM-dd HH:mm:ss") & "' between FakturOpenStart  and FakturOpenEnd ))"
            Else
                strSql += " and (('" & Format(StartDate, "yyyy-MM-dd HH:mm:ss") & "' between FakturValidationStart  and FakturValidationEnd ) or "
                strSql += "  ('" & Format(EndDate, "yyyy-MM-dd HH:mm:ss") & "' between FakturValidationStart  and FakturValidationEnd ))"
            End If

            
            strSql += " and VechileTypecode in ('" & VechileTypeID.Replace(";", "','") & "')"
            If Not leasingcompanyid = "" Then
                strSql += " and leasingcompanyid in ('" & leasingcompanyid.Replace(";", "','") & "')"
            End If
            criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "ID", MatchType.InSet, "(" & strSql & ")"))



            Dim _BenefitMasterDetail As ArrayList = m_BenefitMasterDetailMapper.RetrieveByCriteria(criterias)
            Return _BenefitMasterDetail
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

