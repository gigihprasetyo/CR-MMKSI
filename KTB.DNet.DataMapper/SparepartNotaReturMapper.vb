#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SparepartNotaRetur Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/15/2021 - 6:14:15 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class SparepartNotaReturMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparepartNotaRetur"
        Private m_UpdateStatement As String = "up_UpdateSparepartNotaRetur"
        Private m_RetrieveStatement As String = "up_RetrieveSparepartNotaRetur"
        Private m_RetrieveListStatement As String = "up_RetrieveSparepartNotaReturList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparepartNotaRetur"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparepartNotaRetur As SparepartNotaRetur = Nothing
            While dr.Read

                sparepartNotaRetur = Me.CreateObject(dr)

            End While

            Return sparepartNotaRetur

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparepartNotaReturList As ArrayList = New ArrayList

            While dr.Read
                Dim sparepartNotaRetur As SparepartNotaRetur = Me.CreateObject(dr)
                sparepartNotaReturList.Add(sparepartNotaRetur)
            End While

            Return sparepartNotaReturList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparepartNotaRetur As SparepartNotaRetur = CType(obj, SparepartNotaRetur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparepartNotaRetur.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparepartNotaRetur As SparepartNotaRetur = CType(obj, SparepartNotaRetur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NoDoc", DbType.AnsiString, sparepartNotaRetur.NoDoc)
            DbCommandWrapper.AddInParameter("@TypeDoc", DbType.Int16, sparepartNotaRetur.TypeDoc)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, sparepartNotaRetur.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, sparepartNotaRetur.PeriodeYear)
            DbCommandWrapper.AddInParameter("@FileNamePath", DbType.AnsiString, sparepartNotaRetur.FileNamePath)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparepartNotaRetur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparepartNotaRetur.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sparepartNotaRetur.LastUpdateTime)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparepartNotaRetur.Dealer))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparepartNotaRetur As SparepartNotaRetur = CType(obj, SparepartNotaRetur)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparepartNotaRetur.ID)
            DbCommandWrapper.AddInParameter("@NoDoc", DbType.AnsiString, sparepartNotaRetur.NoDoc)
            DbCommandWrapper.AddInParameter("@TypeDoc", DbType.Int16, sparepartNotaRetur.TypeDoc)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, sparepartNotaRetur.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, sparepartNotaRetur.PeriodeYear)
            DbCommandWrapper.AddInParameter("@FileNamePath", DbType.AnsiString, sparepartNotaRetur.FileNamePath)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparepartNotaRetur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparepartNotaRetur.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparepartNotaRetur.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sparepartNotaRetur.LastUpdateTime)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparepartNotaRetur.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparepartNotaRetur

            Dim sparepartNotaRetur As SparepartNotaRetur = New SparepartNotaRetur

            sparepartNotaRetur.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoDoc")) Then sparepartNotaRetur.NoDoc = dr("NoDoc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TypeDoc")) Then sparepartNotaRetur.TypeDoc = CType(dr("TypeDoc"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeMonth")) Then sparepartNotaRetur.PeriodeMonth = CType(dr("PeriodeMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeYear")) Then sparepartNotaRetur.PeriodeYear = CType(dr("PeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePath")) Then sparepartNotaRetur.FileNamePath = dr("FileNamePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparepartNotaRetur.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparepartNotaRetur.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparepartNotaRetur.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparepartNotaRetur.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparepartNotaRetur.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparepartNotaRetur.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return sparepartNotaRetur

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparepartNotaRetur) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparepartNotaRetur), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparepartNotaRetur).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
