#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExMappingtoFSKind Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/17/2020 - 5:19:26 PM
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

    Public Class MSPExMappingtoFSKindMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExMappingtoFSKind"
        Private m_UpdateStatement As String = "up_UpdateMSPExMappingtoFSKind"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExMappingtoFSKind"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExMappingtoFSKindList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExMappingtoFSKind"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExMappingtoFSKind As MSPExMappingtoFSKind = Nothing
            While dr.Read

                mSPExMappingtoFSKind = Me.CreateObject(dr)

            End While

            Return mSPExMappingtoFSKind

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExMappingtoFSKindList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExMappingtoFSKind As MSPExMappingtoFSKind = Me.CreateObject(dr)
                mSPExMappingtoFSKindList.Add(mSPExMappingtoFSKind)
            End While

            Return mSPExMappingtoFSKindList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExMappingtoFSKind As MSPExMappingtoFSKind = CType(obj, MSPExMappingtoFSKind)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExMappingtoFSKind.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExMappingtoFSKind As MSPExMappingtoFSKind = CType(obj, MSPExMappingtoFSKind)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExMappingtoFSKind.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPExMappingtoFSKind.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, Me.GetRefObject(mSPExMappingtoFSKind.FSKind))
            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(mSPExMappingtoFSKind.MSPExType))

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

            Dim mSPExMappingtoFSKind As MSPExMappingtoFSKind = CType(obj, MSPExMappingtoFSKind)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExMappingtoFSKind.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExMappingtoFSKind.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPExMappingtoFSKind.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, Me.GetRefObject(mSPExMappingtoFSKind.FSKind))
            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(mSPExMappingtoFSKind.MSPExType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExMappingtoFSKind

            Dim mSPExMappingtoFSKind As MSPExMappingtoFSKind = New MSPExMappingtoFSKind

            mSPExMappingtoFSKind.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPExMappingtoFSKind.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPExMappingtoFSKind.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPExMappingtoFSKind.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPExMappingtoFSKind.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPExMappingtoFSKind.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                mSPExMappingtoFSKind.FSKind = New FSKind(CType(dr("FSKindID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExTypeID")) Then
                mSPExMappingtoFSKind.MSPExType = New MSPExType(CType(dr("MSPExTypeID"), Integer))
            End If

            Return mSPExMappingtoFSKind

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExMappingtoFSKind) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExMappingtoFSKind), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExMappingtoFSKind).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
