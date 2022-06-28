#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRDamageCode Objects Mapper.
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

    Public Class PQRDamageCodeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRDamageCode"
        Private m_UpdateStatement As String = "up_UpdatePQRDamageCode"
        Private m_RetrieveStatement As String = "up_RetrievePQRDamageCode"
        Private m_RetrieveListStatement As String = "up_RetrievePQRDamageCodeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRDamageCode"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pQRDamageCode As PQRDamageCode = Nothing
            While dr.Read

                pQRDamageCode = Me.CreateObject(dr)

            End While

            Return pQRDamageCode

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pQRDamageCodeList As ArrayList = New ArrayList

            While dr.Read
                Dim pQRDamageCode As PQRDamageCode = Me.CreateObject(dr)
                pQRDamageCodeList.Add(pQRDamageCode)
            End While

            Return pQRDamageCodeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pQRDamageCode As PQRDamageCode = CType(obj, PQRDamageCode)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pQRDamageCode.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pQRDamageCode As PQRDamageCode = CType(obj, PQRDamageCode)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, pQRDamageCode.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastModifiedBy", DbType.AnsiString, pQRDamageCode.LastModifiedBy)
            DbCommandWrapper.AddInParameter("@LastModifiedTime", DbType.AnsiString, pQRDamageCode.LastModifiedTime)

            DbCommandWrapper.AddInParameter("@PQRHeaderID", DbType.Int32, Me.GetRefObject(pQRDamageCode.PQRHeader))
            DbCommandWrapper.AddInParameter("@DeskripsiKodePosisiID", DbType.Int32, Me.GetRefObject(pQRDamageCode.DeskripsiKodePosisi))

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

            Dim pQRDamageCode As PQRDamageCode = CType(obj, PQRDamageCode)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pQRDamageCode.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, pQRDamageCode.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pQRDamageCode.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastModifiedBy", DbType.AnsiString, pQRDamageCode.LastModifiedBy)
            DbCommandWrapper.AddInParameter("@LastModifiedTime", DbType.AnsiString, pQRDamageCode.LastModifiedTime)


            DbCommandWrapper.AddInParameter("@PQRHeaderID", DbType.Int32, Me.GetRefObject(pQRDamageCode.PQRHeader))
            DbCommandWrapper.AddInParameter("@DeskripsiKodePosisiID", DbType.Int32, Me.GetRefObject(pQRDamageCode.DeskripsiKodePosisi))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRDamageCode

            Dim pQRDamageCode As PQRDamageCode = New PQRDamageCode

            pQRDamageCode.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pQRDamageCode.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pQRDamageCode.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pQRDamageCode.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastModifiedBy")) Then pQRDamageCode.LastModifiedBy = dr("LastModifiedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastModifiedTime")) Then pQRDamageCode.LastModifiedTime = dr("LastModifiedTime").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderID")) Then
                pQRDamageCode.PQRHeader = New PQRHeader(CType(dr("PQRHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DeskripsiKodePosisiID")) Then
                pQRDamageCode.DeskripsiKodePosisi = New DeskripsiKodePosisi(CType(dr("DeskripsiKodePosisiID"), Integer))
            End If

            Return pQRDamageCode

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRDamageCode) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRDamageCode), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRDamageCode).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

