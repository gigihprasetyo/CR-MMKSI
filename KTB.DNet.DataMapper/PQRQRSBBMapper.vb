#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRQRSBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2007 - 8:20:27 AM
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

    Public Class PQRQRSBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRQRSBB"
        Private m_UpdateStatement As String = "up_UpdatePQRQRSBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRQRSBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRQRSBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRQRSBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRQRSBB As PQRQRSBB = Nothing
            While dr.Read

                PQRQRSBB = Me.CreateObject(dr)

            End While

            Return PQRQRSBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRQRSBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRQRSBB As PQRQRSBB = Me.CreateObject(dr)
                PQRQRSBBList.Add(PQRQRSBB)
            End While

            Return PQRQRSBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRQRSBB As PQRQRSBB = CType(obj, PQRQRSBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRQRSBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRQRSBB As PQRQRSBB = CType(obj, PQRQRSBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@TglKerusakan", DbType.DateTime, PQRQRSBB.TglKerusakan)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, PQRQRSBB.Note)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.Int32, PQRQRSBB.Odometer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, PQRQRSBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, PQRQRSBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRQRSBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(PQRQRSBB.ChassisMasterBB))

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

            Dim PQRQRSBB As PQRQRSBB = CType(obj, PQRQRSBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRQRSBB.ID)
            DbCommandWrapper.AddInParameter("@TglKerusakan", DbType.DateTime, PQRQRSBB.TglKerusakan)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, PQRQRSBB.Note)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.Int32, PQRQRSBB.Odometer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, PQRQRSBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRQRSBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRQRSBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(PQRQRSBB.ChassisMasterBB))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRQRSBB

            Dim PQRQRSBB As PQRQRSBB = New PQRQRSBB

            PQRQRSBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TglKerusakan")) Then PQRQRSBB.TglKerusakan = CType(dr("TglKerusakan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then PQRQRSBB.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Odometer")) Then PQRQRSBB.Odometer = CType(dr("Odometer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRQRSBB.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRQRSBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRQRSBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then PQRQRSBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then PQRQRSBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) Then
                PQRQRSBB.PQRHeaderBB = New PQRHeaderBB(CType(dr("PQRHeaderBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterBBID")) Then
                PQRQRSBB.ChassisMasterBB = New ChassisMasterBB(CType(dr("ChassisMasterBBID"), Integer))
            End If

            Return PQRQRSBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRQRSBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRQRSBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRQRSBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

