#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRDamageCodeBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/26/2007 - 1:20:48 PM
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

    Public Class PQRDamageCodeBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRDamageCodeBB"
        Private m_UpdateStatement As String = "up_UpdatePQRDamageCodeBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRDamageCodeBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRDamageCodeBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRDamageCodeBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRDamageCodeBB As PQRDamageCodeBB = Nothing
            While dr.Read

                PQRDamageCodeBB = Me.CreateObject(dr)

            End While

            Return PQRDamageCodeBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRDamageCodeBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRDamageCodeBB As PQRDamageCodeBB = Me.CreateObject(dr)
                PQRDamageCodeBBList.Add(PQRDamageCodeBB)
            End While

            Return PQRDamageCodeBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRDamageCodeBB As PQRDamageCodeBB = CType(obj, PQRDamageCodeBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRDamageCodeBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRDamageCodeBB As PQRDamageCodeBB = CType(obj, PQRDamageCodeBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, PQRDamageCodeBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastModifiedBy", DbType.AnsiString, PQRDamageCodeBB.LastModifiedBy)
            'DbCommandWrapper.AddInParameter("@LastModifiedTime", DbType.AnsiString, PQRDamageCodeBB.LastModifiedTime)

            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRDamageCodeBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@DeskripsiKodePosisiID", DbType.Int32, Me.GetRefObject(PQRDamageCodeBB.DeskripsiKodePosisi))

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

            Dim PQRDamageCodeBB As PQRDamageCodeBB = CType(obj, PQRDamageCodeBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRDamageCodeBB.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, PQRDamageCodeBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRDamageCodeBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastModifiedBy", DbType.AnsiString, PQRDamageCodeBB.LastModifiedBy)
            'DbCommandWrapper.AddInParameter("@LastModifiedTime", DbType.AnsiString, PQRDamageCodeBB.LastModifiedTime)


            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRDamageCodeBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@DeskripsiKodePosisiID", DbType.Int32, Me.GetRefObject(PQRDamageCodeBB.DeskripsiKodePosisi))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRDamageCodeBB

            Dim PQRDamageCodeBB As PQRDamageCodeBB = New PQRDamageCodeBB

            PQRDamageCodeBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRDamageCodeBB.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRDamageCodeBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRDamageCodeBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastModifiedBy")) Then PQRDamageCodeBB.LastModifiedBy = dr("LastModifiedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastModifiedTime")) Then PQRDamageCodeBB.LastModifiedTime = dr("LastModifiedTime").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) Then
                PQRDamageCodeBB.PQRHeaderBB = New PQRHeaderBB(CType(dr("PQRHeaderBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DeskripsiKodePosisiID")) Then
                PQRDamageCodeBB.DeskripsiKodePosisi = New DeskripsiKodePosisi(CType(dr("DeskripsiKodePosisiID"), Integer))
            End If

            Return PQRDamageCodeBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRDamageCodeBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRDamageCodeBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRDamageCodeBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

