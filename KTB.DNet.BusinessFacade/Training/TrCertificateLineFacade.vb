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

Namespace KTB.DNet.BusinessFacade.Training
    Public Class TrCertificateLineFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_TrCertificateLineMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private objTransactionManager As TransactionManager


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_TrCertificateLineMapper = MapperFactory.GetInstance().GetMapper(GetType(TrCertificateLine).ToString)
            Me.objTransactionManager = New TransactionManager
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As TrCertificateLine
            Return CType(m_TrCertificateLineMapper.Retrieve(ID), TrCertificateLine)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_TrCertificateLineMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_TrCertificateLineMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_TrCertificateLineMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCertificateLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCertificateLineMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCertificateLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCertificateLineMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _TrCertificateLine As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(criterias)
            Return _TrCertificateLine
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim TrCertificateLineColl As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrCertificateLineColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCertificateLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim TrCertificateLineColl As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return TrCertificateLineColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCertificateLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_TrCertificateLineMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCertificateLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_TrCertificateLineMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim TrCertificateLineColl As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrCertificateLineColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCertificateLine), columnName, matchOperator, columnValue))
            Dim TrCertificateLineColl As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return TrCertificateLineColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(TrCertificateLine), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), columnName, matchOperator, columnValue))

            Return m_TrCertificateLineMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function InsertCertificateLinePerClassReg(ByVal arrCertificateLine As ArrayList, ByVal ObjTrClassRegistration As TrClassRegistration) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrCertificateLine.Count > 0 Then
                        For Each objCertificateLine As TrCertificateLine In arrCertificateLine
                            objTransactionManager.AddInsert(objCertificateLine, m_userPrincipal.Identity.Name)
                        Next

                        For Each objCertificateLine As TrCertificateLine In arrCertificateLine
                            If objCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                                If Right(objCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "00" Then
                                    objCertificateLine.TrClassRegistration.InitialTest = objCertificateLine.NumTestResult
                                    If Not IsNothing(ObjTrClassRegistration) Then
                                        ObjTrClassRegistration.InitialTest = objCertificateLine.NumTestResult
                                        ObjTrClassRegistration.LastUpdateBy = m_userPrincipal.Identity.Name
                                        objTransactionManager.AddUpdate(ObjTrClassRegistration, m_userPrincipal.Identity.Name)
                                        Exit For
                                    End If
                                End If
                            End If

                        Next


                        For Each objCertificateLine As TrCertificateLine In arrCertificateLine
                            If objCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                                If Right(objCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "99" Then
                                    objCertificateLine.TrClassRegistration.FinalTest = objCertificateLine.NumTestResult
                                    If Not IsNothing(ObjTrClassRegistration) Then
                                        ObjTrClassRegistration.FinalTest = objCertificateLine.NumTestResult
                                        ObjTrClassRegistration.LastUpdateBy = m_userPrincipal.Identity.Name
                                        objTransactionManager.AddUpdate(ObjTrClassRegistration, m_userPrincipal.Identity.Name)
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    objTransactionManager.PerformTransaction()
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

        Public Function UpdateCertificateLinePerClassReg(ByVal arrCertificateLine As ArrayList, ByVal ObjTrClassRegistration As TrClassRegistration) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrCertificateLine.Count > 0 Then
                        For Each objCertificateLine As TrCertificateLine In arrCertificateLine
                            objTransactionManager.AddUpdate(objCertificateLine, m_userPrincipal.Identity.Name)
                        Next

                        For Each objCertificateLine As TrCertificateLine In arrCertificateLine
                            If objCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                                If Right(objCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "00" Then
                                    objCertificateLine.TrClassRegistration.InitialTest = objCertificateLine.NumTestResult
                                    If Not IsNothing(ObjTrClassRegistration) Then
                                        ObjTrClassRegistration.InitialTest = objCertificateLine.NumTestResult
                                        ObjTrClassRegistration.LastUpdateBy = m_userPrincipal.Identity.Name
                                        objTransactionManager.AddUpdate(ObjTrClassRegistration, m_userPrincipal.Identity.Name)
                                        Exit For
                                    End If
                                End If
                            End If

                        Next


                        For Each objCertificateLine As TrCertificateLine In arrCertificateLine
                            If objCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                                If Right(objCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "99" Then
                                    objCertificateLine.TrClassRegistration.FinalTest = objCertificateLine.NumTestResult
                                    If Not IsNothing(ObjTrClassRegistration) Then
                                        ObjTrClassRegistration.FinalTest = objCertificateLine.NumTestResult
                                        ObjTrClassRegistration.LastUpdateBy = m_userPrincipal.Identity.Name
                                        objTransactionManager.AddUpdate(ObjTrClassRegistration, m_userPrincipal.Identity.Name)
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    objTransactionManager.PerformTransaction()
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



        Public Function UpdateCertificateLineClassReg(ByVal ObjCertificateLine As TrCertificateLine, ByVal ObjTrClassRegistration As TrClassRegistration) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If Not IsNothing(ObjCertificateLine) Then
                        If ObjCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                            If Right(ObjCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "00" Then
                                ObjCertificateLine.TrClassRegistration.InitialTest = ObjCertificateLine.NumTestResult
                                If Not IsNothing(ObjTrClassRegistration) Then
                                    ObjTrClassRegistration.InitialTest = ObjCertificateLine.NumTestResult
                                    ObjTrClassRegistration.LastUpdateBy = m_userPrincipal.Identity.Name
                                    objTransactionManager.AddUpdate(ObjTrClassRegistration, m_userPrincipal.Identity.Name)
                                End If
                            End If
                            If Right(ObjCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "99" Then
                                ObjCertificateLine.TrClassRegistration.FinalTest = ObjCertificateLine.NumTestResult
                                If Not IsNothing(ObjTrClassRegistration) Then
                                    ObjTrClassRegistration.FinalTest = ObjCertificateLine.NumTestResult
                                    ObjTrClassRegistration.LastUpdateBy = m_userPrincipal.Identity.Name
                                    objTransactionManager.AddUpdate(ObjTrClassRegistration, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        End If
                        objTransactionManager.AddUpdate(ObjCertificateLine, m_userPrincipal.Identity.Name)
                    End If
                    objTransactionManager.PerformTransaction()
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


        Public Function Insert(ByVal objDomain As TrCertificateLine) As Integer
            Dim iReturn As Integer = -2
            Try
                m_TrCertificateLineMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As TrCertificateLine) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_TrCertificateLineMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As TrCertificateLine)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_TrCertificateLineMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As TrCertificateLine) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_TrCertificateLineMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal strTrCertificateLineCode As String, ByVal strType As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "TrClassRegistration.RegistrationCode", MatchType.Exact, strTrCertificateLineCode))
            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.ID", MatchType.Exact, strType))

            Dim agg As Aggregate = New Aggregate(GetType(TrCertificateLine), "ID", AggregateType.Count)
            Return CType(m_TrCertificateLineMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveTrCertificateLine(ByVal nTrCourseEvalID As Integer, ByVal strRegCode As String) As TrCertificateLine
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.ID", MatchType.Exact, nTrCourseEvalID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.RegistrationCode", MatchType.Exact, strRegCode))
            Dim ArrTransactionControl As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(criterias)
            If ArrTransactionControl.Count > 0 Then
                Return ArrTransactionControl(0)
            End If
            Return Nothing
        End Function
        Public Function RetrieveTrCertificateLine2(ByVal nTrCourseEvalID As Integer, ByVal strRegCode As String) As TrCertificateLine
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.ID", MatchType.Exact, nTrCourseEvalID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, strRegCode))
            Dim ArrTransactionControl As ArrayList = m_TrCertificateLineMapper.RetrieveByCriteria(criterias)
            If ArrTransactionControl.Count > 0 Then
                Return ArrTransactionControl(0)
            End If
            Return Nothing
        End Function
#End Region

    End Class

End Namespace
