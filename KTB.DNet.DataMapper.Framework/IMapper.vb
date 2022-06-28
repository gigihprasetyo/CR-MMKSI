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
'// Generated on 7/22/2005 - 11:00:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"

Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports KTB.DNet.Domain.Search

#End Region

Namespace KTB.DNet.DataMapper.Framework

    Public Interface IMapper

        Function Insert(ByVal obj As Object, ByVal User As String) As Integer
        Function Update(ByVal obj As Object, ByVal User As String) As Integer
        Function Retrieve(ByVal id As Integer) As Object
        Function RetrieveList() As ArrayList
        Function RetrieveList(ByVal sortCollection As ICollection) As ArrayList
        Function RetrieveList(ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Function RetrieveByCriteria(ByVal Criterias As ICriteria) As ArrayList
        Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal sortCollection As ICollection) As ArrayList
        Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
        Function RetrieveCustomPagingBySP(ByVal spName As String, ByVal paramSql As ArrayList, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList

        Function Delete(ByVal obj As Object) As Integer
        Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Object
        Function RetrieveScalar(ByVal aggregation As IAggregate) As Object

        Function RetrieveSP(ByVal Sql As String) As ArrayList
        Function RetrieveSP(ByVal Sql As String, ByVal param As ArrayList) As ArrayList


        Function RetrieveDataSet(ByVal Sql As String) As DataSet
        Function RetrieveDataSet(ByVal Sql As String, ByVal param As ArrayList) As DataSet

        Sub UseTransaction(ByVal tran As IDbTransaction)

        Function ExecuteSP(ByVal SQL As String) As Boolean
        Function ExecuteSP(ByVal Sql As String, ByVal param As ArrayList) As Boolean
        Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal timeout As Integer) As ArrayList
    End Interface

End Namespace