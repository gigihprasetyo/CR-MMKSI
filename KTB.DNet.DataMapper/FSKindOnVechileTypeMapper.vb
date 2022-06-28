#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FSKindOnVechileType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 2/24/2006 - 4:17:15 PM
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

    Public Class FSKindOnVechileTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFSKindOnVechileType"
        Private m_UpdateStatement As String = "up_UpdateFSKindOnVechileType"
        Private m_RetrieveStatement As String = "up_RetrieveFSKindOnVechileType"
        Private m_RetrieveListStatement As String = "up_RetrieveFSKindOnVechileTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFSKindOnVechileType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fSKindOnVechileType As FSKindOnVechileType = Nothing
            While dr.Read

                fSKindOnVechileType = Me.CreateObject(dr)

            End While

            Return fSKindOnVechileType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fSKindOnVechileTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim fSKindOnVechileType As FSKindOnVechileType = Me.CreateObject(dr)
                fSKindOnVechileTypeList.Add(fSKindOnVechileType)
            End While

            Return fSKindOnVechileTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fSKindOnVechileType As FSKindOnVechileType = CType(obj, FSKindOnVechileType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fSKindOnVechileType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fSKindOnVechileType As FSKindOnVechileType = CType(obj, FSKindOnVechileType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@FSType", DbType.AnsiString, fSKindOnVechileType.FSType)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int32, fSKindOnVechileType.Duration)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, fSKindOnVechileType.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fSKindOnVechileType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fSKindOnVechileType.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, Me.GetRefObject(fSKindOnVechileType.FSKind))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(fSKindOnVechileType.VechileType))

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

            Dim fSKindOnVechileType As FSKindOnVechileType = CType(obj, FSKindOnVechileType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fSKindOnVechileType.ID)
            DbCommandWrapper.AddInParameter("@FSType", DbType.AnsiString, fSKindOnVechileType.FSType)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int32, fSKindOnVechileType.Duration)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, fSKindOnVechileType.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fSKindOnVechileType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fSKindOnVechileType.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, Me.GetRefObject(fSKindOnVechileType.FSKind))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(fSKindOnVechileType.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FSKindOnVechileType

            Dim fSKindOnVechileType As FSKindOnVechileType = New FSKindOnVechileType

            fSKindOnVechileType.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FSType")) Then fSKindOnVechileType.FSType = dr("FSType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then fSKindOnVechileType.Duration = CType(dr("Duration"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then fSKindOnVechileType.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fSKindOnVechileType.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fSKindOnVechileType.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fSKindOnVechileType.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fSKindOnVechileType.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fSKindOnVechileType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                fSKindOnVechileType.FSKind = New FSKind(CType(dr("FSKindID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                fSKindOnVechileType.VechileType = New VechileType(CType(dr("VechileTypeID"), Integer))
            End If

            Return fSKindOnVechileType

        End Function

        Private Sub SetTableName()

            If Not (GetType(FSKindOnVechileType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FSKindOnVechileType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FSKindOnVechileType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

