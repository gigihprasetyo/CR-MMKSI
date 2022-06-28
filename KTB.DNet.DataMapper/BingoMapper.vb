#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Bingo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2007 - 9:49:02 AM
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

    Public Class BingoMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

        Public Sub New(ByVal instanceName As String)
            Db = DatabaseFactory.CreateDatabase(instanceName)
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBingo"
        Private m_UpdateStatement As String = "up_UpdateBingo"
        Private m_RetrieveStatement As String = "up_RetrieveBingo"
        Private m_RetrieveListStatement As String = "up_RetrieveBingoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBingo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bingo As Bingo = Nothing
            While dr.Read

                bingo = Me.CreateObject(dr)

            End While

            Return bingo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bingoList As ArrayList = New ArrayList

            While dr.Read
                Dim bingo As Bingo = Me.CreateObject(dr)
                bingoList.Add(bingo)
            End While

            Return bingoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bingo As Bingo = CType(obj, Bingo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bingo.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bingo As Bingo = CType(obj, Bingo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, bingo.Status)
            DbCommandWrapper.AddInParameter("@ExpiredCount", DbType.Int32, bingo.ExpiredCount)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, bingo.SerialNumber)
            DbCommandWrapper.AddInParameter("@DimensiX", DbType.Int32, bingo.DimensiX)
            DbCommandWrapper.AddInParameter("@DimensiY", DbType.Int32, bingo.DimensiY)
            DbCommandWrapper.AddInParameter("@Handphone", DbType.AnsiString, bingo.Handphone)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bingo.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bingo.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim bingo As Bingo = CType(obj, Bingo)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bingo.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, bingo.Status)
            DbCommandWrapper.AddInParameter("@ExpiredCount", DbType.Int32, bingo.ExpiredCount)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, bingo.SerialNumber)
            DbCommandWrapper.AddInParameter("@DimensiX", DbType.Int32, bingo.DimensiX)
            DbCommandWrapper.AddInParameter("@DimensiY", DbType.Int32, bingo.DimensiY)
            DbCommandWrapper.AddInParameter("@Handphone", DbType.AnsiString, bingo.Handphone)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bingo.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bingo.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Bingo

            Dim bingo As Bingo = New Bingo

            bingo.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then bingo.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpiredCount")) Then bingo.ExpiredCount = CType(dr("ExpiredCount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SerialNumber")) Then bingo.SerialNumber = dr("SerialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DimensiX")) Then bingo.DimensiX = CType(dr("DimensiX"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DimensiY")) Then bingo.DimensiY = CType(dr("DimensiY"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Handphone")) Then bingo.Handphone = dr("Handphone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bingo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bingo.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bingo.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bingo.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bingo.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return bingo

        End Function

        Private Sub SetTableName()

            If Not (GetType(Bingo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Bingo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Bingo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

