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
'// Generated on 7/22/2005 - 02:10:00 PM
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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper.Framework

    Public MustInherit Class AbstractMapper
        Implements IMapper

#Region "Private Variables"

        Private m_IsTransactional As Boolean
        Private m_DbTransaction As IDbTransaction
        Private m_MapperExceptionPolicy As String = "Mapper Policy"
        Private _Db As Database
        Private _DbCommandWrapper As DbCommandWrapper
        Private _instance As String
#End Region

#Region "Protected and Methods"
        'TODO: Encapsulate with protected properties

        Protected MustOverride Function GetInsertParameter(ByVal obj As Object, ByVal user As String) As DbCommandWrapper
        Protected MustOverride Function GetRetrieveParameter(ByVal id As Integer) As DbCommandWrapper
        Protected MustOverride Function GetRetrieveListParameter() As DbCommandWrapper
        Protected MustOverride Function GetRetrieveCommand() As DbCommandWrapper
        Protected MustOverride Function GetPagingRetrieveCommand() As DbCommandWrapper
        Protected MustOverride Function GetUpdateParameter(ByVal obj As Object, ByVal user As String) As DbCommandWrapper
        Protected MustOverride Function GetDeleteParameter(ByVal obj As Object) As DbCommandWrapper
        Protected MustOverride Function GetNewID(ByVal dbCommandWrapper As DbCommandWrapper) As Integer
        Protected MustOverride Function DoRetrieve(ByVal dr As IDataReader) As Object
        Protected MustOverride Function DoRetrieveList(ByVal dr As IDataReader) As ArrayList

        Protected m_TableName As String = String.Empty

#End Region

#Region "Public Methods"

        Public Function Insert(ByVal obj As Object, ByVal User As String) As Integer Implements IMapper.Insert
            Dim iReturn As Integer = -1
            Try
                DbCommandWrapper = GetInsertParameter(obj, User)
                If (Me.ExecuteNonQuery(DbCommandWrapper) > 0) Then

                    iReturn = GetNewID(DbCommandWrapper)
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
            End Try
            Return iReturn
        End Function

        Public Function Retrieve(ByVal id As Integer) As Object Implements IMapper.Retrieve
            Dim dr As IDataReader = Nothing
            Dim domainObject As Object = Nothing
            Try
                DbCommandWrapper = GetRetrieveParameter(id)
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObject = DoRetrieve(dr)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObject
        End Function

        Public Function RetrieveList() As ArrayList Implements IMapper.RetrieveList
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing
            Try
                DbCommandWrapper = GetRetrieveListParameter()
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso (Not (dr.IsClosed)) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function RetrieveList(ByVal Sorts As ICollection) As ArrayList Implements IMapper.RetrieveList
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing
            Try
                DbCommandWrapper = GetRetrieveCommand()
                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString()
                For Each obj As Object In Sorts
                    Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause()
                    For Each joinClause As String In joinClauses
                        sqlQuery += joinClause
                    Next
                Next

                sqlQuery += " ORDER BY " + CType(Sorts, Object).ToString()
                If (sqlQuery.EndsWith(" ORDER BY ")) Then
                    sqlQuery = sqlQuery.Replace(" ORDER BY ", "")
                End If

                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function RetrieveList(ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList Implements IMapper.RetrieveList
            Dim strPK As String
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing
            Try
                DbCommandWrapper = GetPagingRetrieveCommand()
                DbCommandWrapper.AddInParameter("@PageSize", DbType.Int16, pageSize)
                DbCommandWrapper.AddInParameter("@PageNumber", DbType.Int16, pageNumber)
                If (Not IsNothing(Sorts)) Then
                    '//Addes Join Clause
                    Dim strJoinClause As String = String.Empty
                    For Each obj As Object In Sorts
                        Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause
                        For Each joinClause As String In joinClauses
                            strJoinClause += joinClause
                        Next
                    Next
                    DbCommandWrapper.AddInParameter("@Filter", DbType.String, strJoinClause)
                    DbCommandWrapper.AddInParameter("@Sort", DbType.String, CType(Sorts, Object).ToString())
                End If
                strPK = DbCommandWrapper.GetParameterValue("@PK").ToString()
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
                DbCommandWrapper = GetRetrieveCommand()

                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString()
                sqlQuery = sqlQuery.Replace(m_TableName + ".*", " Count(" + m_TableName + "." + strPK + ") ")
                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                totalRow = CType(Db.ExecuteScalar(DbCommandWrapper), Integer)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If (rethrow) Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function RetrieveByCriteria(ByVal Criterias As ICriteria) As ArrayList Implements IMapper.RetrieveByCriteria
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing
            Try
                DbCommandWrapper = GetRetrieveCommand()

                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString
                sqlQuery += " with (nolock) " 'Add by Firman

                sqlQuery += Criterias.ToString

                If (sqlQuery.EndsWith(" WHERE ")) Then
                    sqlQuery = sqlQuery.Replace(" WHERE ", "")
                End If

                

                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal Sorts As ICollection) As ArrayList Implements IMapper.RetrieveByCriteria
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing

            Try
                DbCommandWrapper = GetRetrieveCommand()
                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString
                If (Not IsNothing(Criterias)) Then
                    sqlQuery += " with (nolock) " 'Add by Firman
                    sqlQuery += Criterias.ToString
                    If (sqlQuery.EndsWith(" WHERE ")) Then
                        sqlQuery = sqlQuery.Replace(" WHERE ", "")
                    End If
                End If

                If (Not IsNothing(Sorts)) Then
                    For Each obj As Object In Sorts
                        Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause()
                        For Each joinClause As String In joinClauses
                            If (sqlQuery.IndexOf(joinClause) = -1) Then
                                sqlQuery = sqlQuery.Insert(sqlQuery.IndexOf(" WHERE "), joinClause)
                            End If
                        Next
                    Next
                    sqlQuery += " ORDER BY " + CType(Sorts, Object).ToString()
                    If (sqlQuery.EndsWith(" ORDER BY ")) Then
                        sqlQuery = sqlQuery.Replace(" ORDER BY ", "")
                    End If
                End If

                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList Implements IMapper.RetrieveByCriteria
            Dim strPK As String
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing
            Try
                DbCommandWrapper = GetPagingRetrieveCommand()
                DbCommandWrapper.AddInParameter("@PageSize", DbType.Int16, pageSize)
                DbCommandWrapper.AddInParameter("@PageNumber", DbType.Int16, pageNumber)
                If (Not IsNothing(Criterias)) Then
                    DbCommandWrapper.AddInParameter("@Filter", DbType.String, Criterias.ToString())
                End If
                If (Not IsNothing(Sorts)) Then
                    Dim strFilter As String = CType(DbCommandWrapper.GetParameterValue("@Filter"), String)
                    For Each obj As Object In Sorts
                        Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause()
                        For Each joinClause As String In joinClauses
                            If (strFilter.IndexOf(joinClause) = -1) Then
                                strFilter = strFilter.Insert(strFilter.IndexOf(" WHERE "), joinClause)
                            End If
                        Next
                    Next
                    DbCommandWrapper.SetParameterValue("@Filter", strFilter)
                    DbCommandWrapper.AddInParameter("@Sort", DbType.String, CType(Sorts, Object).ToString())
                End If
                strPK = DbCommandWrapper.GetParameterValue("@PK").ToString()
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
                DbCommandWrapper = GetRetrieveCommand()
                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString()
                sqlQuery = sqlQuery.Replace(m_TableName + ".*", " Count(" + m_TableName + "." + strPK + ") ")
                If (Not IsNothing(Criterias)) Then
                    sqlQuery += " with (nolock) " 'Add by Firman
                    sqlQuery += Criterias.ToString()
                End If
                If (sqlQuery.EndsWith(" WHERE ")) Then
                    sqlQuery = sqlQuery.Replace(" WHERE ", "")
                End If
                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                totalRow = CType(Db.ExecuteScalar(DbCommandWrapper), Integer)
            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso (Not dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function Update(ByVal obj As Object, ByVal User As String) As Integer Implements IMapper.Update
            Dim iReturn As Integer = -1
            Try
                DbCommandWrapper = GetUpdateParameter(obj, User)
                iReturn = Me.ExecuteNonQuery(DbCommandWrapper)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
            End Try
            Return iReturn
        End Function

        Public Function Delete(ByVal obj As Object) As Integer Implements IMapper.Delete

            Dim iReturn As Integer = -1

            Try

                DbCommandWrapper = GetDeleteParameter(obj)
                iReturn = Me.ExecuteNonQuery(DbCommandWrapper)

            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)

                If rethrow Then
                    Throw
                End If

            Finally

            End Try

            Return iReturn
        End Function

        Public Sub UseTransaction(ByVal transaction As IDbTransaction) Implements IMapper.UseTransaction
            Me.m_DbTransaction = transaction
            Me.m_IsTransactional = True
        End Sub

        '//TODO: Rubah sqlQuery = sqlQuery.Replace("*",aggregation.ToString());

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Object Implements IMapper.RetrieveScalar

            Dim objResult As Object = Nothing

            Try

                DbCommandWrapper = GetRetrieveCommand()

                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString

                sqlQuery = sqlQuery.Replace(m_TableName + ".*", aggregation.ToString())
                sqlQuery += " with (nolock) " 'Add by Firman
                sqlQuery += criterias.ToString()

                If (sqlQuery.EndsWith(" WHERE ")) Then
                    sqlQuery = sqlQuery.Replace(" WHERE ", "")
                End If

                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)

                objResult = Db.ExecuteScalar(DbCommandWrapper)

            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)

                If rethrow Then
                    Throw
                End If

            Finally

            End Try

            Return objResult
        End Function

        '//TODO: RUbah sqlQuery = sqlQuery.Replace("*",aggregation.ToString());
        Public Function RetrieveScalar(ByVal aggregation As IAggregate) As Object Implements IMapper.RetrieveScalar

            Dim objResult As Object = Nothing

            Try

                DbCommandWrapper = GetRetrieveCommand()

                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString()

                sqlQuery = sqlQuery.Replace(m_TableName + ".*", aggregation.ToString())

                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)

                objResult = Db.ExecuteScalar(DbCommandWrapper)

            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)

                If rethrow Then

                    Throw

                End If

            Finally

            End Try

            Return objResult
        End Function
        '--add by dk 050930
        Protected Function GetRefObject(ByVal obj As Object) As Object
            If IsNothing(obj) Then
                obj = CType(DBNull.Value, Object)
            Else
                obj = CType(obj.ID, Object)
            End If
            Return obj
        End Function

        Public Function RetrieveSP(ByVal Sql As String) As ArrayList Implements IMapper.RetrieveSP
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim dtr As IDataReader '  SqlClient.SqlDataReader
            Dim arlResult As ArrayList = New ArrayList
            Dim con As SqlClient.SqlConnection

            Try
                con = Db.GetConnection
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Sql
                cmd.CommandTimeout = 600 '120 'in seconds
                dtr = cmd.ExecuteReader(CommandBehavior.Default)
                arlResult = DoRetrieveList(dtr)

            Catch ex As Exception
                arlResult = New ArrayList
            Finally
                If (Not IsNothing(dtr)) AndAlso Not (dtr.IsClosed) Then
                    dtr.Close()
                End If
                con.Close()
                con.Dispose()
            End Try
            Return arlResult
        End Function

        Public Function RetrieveSP(ByVal Sql As String, ByVal ParamList As ArrayList) As ArrayList Implements IMapper.RetrieveSP
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim dtr As IDataReader '  SqlClient.SqlDataReader
            Dim arlResult As ArrayList = New ArrayList
            Dim con As SqlClient.SqlConnection

            Try
                con = Db.GetConnection
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = Sql
                cmd.CommandTimeout = 600 '120 'in seconds

                If Not IsNothing(ParamList) AndAlso ParamList.Count > 0 Then
                    For Each par As SqlClient.SqlParameter In ParamList
                        cmd.Parameters.Add(par)
                    Next
                End If
                dtr = cmd.ExecuteReader(CommandBehavior.Default)
                arlResult = DoRetrieveList(dtr)

            Catch ex As Exception
                arlResult = New ArrayList
            Finally
                If (Not IsNothing(dtr)) AndAlso Not (dtr.IsClosed) Then
                    dtr.Close()
                End If
                con.Close()
                con.Dispose()
            End Try
            Return arlResult
        End Function



        Public Function ExecuteSP(ByVal SQL As String) As Boolean Implements IMapper.ExecuteSP
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim dtr As SqlClient.SqlDataReader
            Dim arlResult As ArrayList = New ArrayList
            Dim con As SqlClient.SqlConnection
            Dim iResult As Integer = 0

            Try
                con = Db.GetConnection
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = SQL
                cmd.CommandTimeout = 180 'in seconds
                iResult = cmd.ExecuteScalar()
            Catch ex As Exception
                iResult = 0
            Finally
                If Not IsNothing(con) Then
                    Try
                        con.Close()
                        con.Dispose()
                    Catch ex As Exception

                    End Try

                End If
            End Try
            Return (iResult = 1)
        End Function


        Public Function ExecuteSP(ByVal Sql As String, ByVal ParamList As ArrayList) As Boolean Implements IMapper.ExecuteSP
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim dtr As SqlClient.SqlDataReader
            Dim arlResult As ArrayList = New ArrayList
            Dim con As SqlClient.SqlConnection
            Dim iResult As Integer = 0

            Try
                con = Db.GetConnection
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure

                cmd.CommandText = Sql
                cmd.CommandTimeout = 180 'in seconds
                If Not IsNothing(ParamList) AndAlso ParamList.Count > 0 Then
                    For Each par As SqlClient.SqlParameter In ParamList
                        cmd.Parameters.Add(par)
                    Next
                End If

                iResult = cmd.ExecuteNonQuery()
            Catch ex As Exception
                iResult = 0
            Finally
                If Not IsNothing(con) Then
                    Try
                        con.Close()
                        con.Dispose()
                    Catch ex As Exception

                    End Try

                End If
            End Try
            Return (iResult = 1)
        End Function



        Public Function RetrieveDataSet(ByVal Sql As String) As DataSet Implements IMapper.RetrieveDataSet
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim con As SqlClient.SqlConnection
            Dim adp As SqlClient.SqlDataAdapter
            Dim ds As New DataSet

            Try
                con = Db.GetConnection
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Sql
                cmd.CommandTimeout = 600 '120 'in seconds
                adp = New SqlClient.SqlDataAdapter(cmd)
                adp.Fill(ds)
            Catch ex As Exception
                Dim s As String = ex.Message
            Finally
                con.Close()
                con.Dispose()
            End Try
            Return ds
        End Function
        Public Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal timeout As Integer) As ArrayList Implements IMapper.RetrieveByCriteria
            Dim strPK As String
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing

            Try
                DbCommandWrapper = GetPagingRetrieveCommand()
                DbCommandWrapper.CommandTimeout = timeout

                DbCommandWrapper.AddInParameter("@PageSize", DbType.Int16, pageSize)
                DbCommandWrapper.AddInParameter("@PageNumber", DbType.Int16, pageNumber)
                If (Not IsNothing(Criterias)) Then
                    DbCommandWrapper.AddInParameter("@Filter", DbType.String, Criterias.ToString())
                End If
                If (Not IsNothing(Sorts)) Then
                    Dim strFilter As String = CType(DbCommandWrapper.GetParameterValue("@Filter"), String)
                    For Each obj As Object In Sorts
                        Dim joinClauses As System.Collections.Specialized.StringCollection = CType(obj, Sort).GetJoinClause()
                        For Each joinClause As String In joinClauses
                            If (strFilter.IndexOf(joinClause) = -1) Then
                                strFilter = strFilter.Insert(strFilter.IndexOf(" WHERE "), joinClause)
                            End If
                        Next
                    Next
                    DbCommandWrapper.SetParameterValue("@Filter", strFilter)
                    DbCommandWrapper.AddInParameter("@Sort", DbType.String, CType(Sorts, Object).ToString())
                End If
                strPK = DbCommandWrapper.GetParameterValue("@PK").ToString()
                dr = Db.ExecuteReader(DbCommandWrapper)
                domainObjects = DoRetrieveList(dr)
                DbCommandWrapper = GetRetrieveCommand()
                DbCommandWrapper.CommandTimeout = timeout

                Dim sqlQuery As String = DbCommandWrapper.GetParameterValue("@sqlQuery").ToString()
                sqlQuery = sqlQuery.Replace(m_TableName + ".*", " Count(" + m_TableName + "." + strPK + ") ")
                If (Not IsNothing(Criterias)) Then
                    sqlQuery += " with (nolock) " 'Add by Firman
                    sqlQuery += Criterias.ToString()
                End If
                If (sqlQuery.EndsWith(" WHERE ")) Then
                    sqlQuery = sqlQuery.Replace(" WHERE ", "")
                End If
                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                totalRow = CType(Db.ExecuteScalar(DbCommandWrapper), Integer)
            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso (Not dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function

        Public Function RetrieveDataSet(ByVal Sql As String, ByVal ParamList As ArrayList) As DataSet Implements IMapper.RetrieveDataSet
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim con As SqlClient.SqlConnection
            Dim adp As SqlClient.SqlDataAdapter
            Dim ds As New DataSet

            Try
                con = Db.GetConnection
                con.Open()
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = Sql
                cmd.CommandTimeout = 600 '120 'in seconds
                If Not IsNothing(ParamList) AndAlso ParamList.Count > 0 Then
                    For Each par As SqlClient.SqlParameter In ParamList
                        cmd.Parameters.Add(par)
                    Next
                End If
                adp = New SqlClient.SqlDataAdapter(cmd)
                adp.Fill(ds)
            Catch ex As Exception
                Dim s As String = ex.Message
            Finally
                con.Close()
                con.Dispose()
            End Try
            Return ds
        End Function


        ''' <summary>
        ''' Custom Retrieve Paging using SP
        ''' </summary>
        ''' <param name="spName"></param>
        ''' <param name="paramSql"></param>
        ''' <param name="pageNumber"></param>
        ''' <param name="pageSize"></param>
        ''' <param name="totalRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function RetrieveCustomPagingBySP(ByVal spName As String, ByVal paramSql As ArrayList, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList Implements IMapper.RetrieveCustomPagingBySP
            Dim strPK As String
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = Nothing
            Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
            Dim con As SqlClient.SqlConnection

            Try
                Try
                    con = Db.GetConnection
                    con.Open()
                    cmd.Connection = con
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = spName
                    cmd.CommandTimeout = 600 '120 'in seconds
                    If Not IsNothing(paramSql) AndAlso paramSql.Count > 0 Then
                        For Each par As SqlClient.SqlParameter In paramSql
                            cmd.Parameters.Add(par)
                        Next
                    End If
                    dr = cmd.ExecuteReader()

                    'retrieve list
                    domainObjects = DoRetrieveList(dr)

                    'retrieve Total
                    dr.NextResult()

                    While dr.Read()
                        'Write logic to process data for the second result.
                        totalRow = CInt(dr(0))
                    End While

                Catch ex As Exception
                    Dim s As String = ex.Message
                Finally
                    If (Not IsNothing(dr)) AndAlso (Not dr.IsClosed) Then
                        dr.Close()
                    End If
                    con.Close()
                    con.Dispose()
                End Try
            Catch ex As Exception

                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, m_MapperExceptionPolicy)
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso (Not dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function
#End Region

#Region "Private Methods"

        Private Function ExecuteNonQuery(ByVal DbCommandWrapper As DBCommandWrapper) As Integer

            If (Me.m_IsTransactional) Then

                Db.ExecuteNonQuery(DbCommandWrapper, Me.m_DbTransaction)

            Else

                Db.ExecuteNonQuery(DbCommandWrapper)

            End If

            Return DbCommandWrapper.RowsAffected

        End Function

#End Region

#Region "Public Properties"

        Protected Property Db() As Database
            Get
                Return _Db
            End Get
            Set(ByVal Value As Database)
                _Db = Value
            End Set
        End Property

        Protected Property DbCommandWrapper() As DBCommandWrapper
            Get
                Return _DbCommandWrapper
            End Get
            Set(ByVal Value As DBCommandWrapper)
                _DbCommandWrapper = Value
            End Set
        End Property

        Protected Property Instance() As String
            Get
                Return _instance
            End Get
            Set(ByVal Value As String)
                _instance = Value
            End Set
        End Property

#End Region
    End Class

End Namespace