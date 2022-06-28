#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRPartsCodeBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/25/2007 - 9:56:44 AM
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

    Public Class PQRPartsCodeBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRPartsCodeBB"
        Private m_UpdateStatement As String = "up_UpdatePQRPartsCodeBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRPartsCodeBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRPartsCodeBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRPartsCodeBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRPartsCodeBB As PQRPartsCodeBB = Nothing
            While dr.Read

                PQRPartsCodeBB = Me.CreateObject(dr)

            End While

            Return PQRPartsCodeBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRPartsCodeBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRPartsCodeBB As PQRPartsCodeBB = Me.CreateObject(dr)
                PQRPartsCodeBBList.Add(PQRPartsCodeBB)
            End While

            Return PQRPartsCodeBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRPartsCodeBB As PQRPartsCodeBB = CType(obj, PQRPartsCodeBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRPartsCodeBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRPartsCodeBB As PQRPartsCodeBB = CType(obj, PQRPartsCodeBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, PQRPartsCodeBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastModifiedBy", DbType.AnsiString, PQRPartsCodeBB.LastModifiedBy)
            DbCommandWrapper.AddInParameter("@LastModifiedTime", DbType.AnsiString, PQRPartsCodeBB.LastModifiedTime)

            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRPartsCodeBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(PQRPartsCodeBB.SparePartMaster))

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

            Dim PQRPartsCodeBB As PQRPartsCodeBB = CType(obj, PQRPartsCodeBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRPartsCodeBB.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, PQRPartsCodeBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRPartsCodeBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastModifiedBy", DbType.AnsiString, PQRPartsCodeBB.LastModifiedBy)
            DbCommandWrapper.AddInParameter("@LastModifiedTime", DbType.AnsiString, PQRPartsCodeBB.LastModifiedTime)


            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRPartsCodeBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(PQRPartsCodeBB.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRPartsCodeBB

            Dim PQRPartsCodeBB As PQRPartsCodeBB = New PQRPartsCodeBB

            PQRPartsCodeBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRPartsCodeBB.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRPartsCodeBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRPartsCodeBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastModifiedBy")) Then PQRPartsCodeBB.LastModifiedBy = dr("LastModifiedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastModifiedTime")) Then PQRPartsCodeBB.LastModifiedTime = dr("LastModifiedTime").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) Then
                PQRPartsCodeBB.PQRHeaderBB = New PQRHeaderBB(CType(dr("PQRHeaderBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                PQRPartsCodeBB.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return PQRPartsCodeBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRPartsCodeBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRPartsCodeBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRPartsCodeBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

