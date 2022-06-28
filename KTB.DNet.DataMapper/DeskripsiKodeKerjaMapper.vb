
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DeskripsiKodeKerja Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/19/2006 - 11:46:15 AM
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

    Public Class DeskripsiKodeKerjaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDeskripsiKodeKerja"
        Private m_UpdateStatement As String = "up_UpdateDeskripsiKodeKerja"
        Private m_RetrieveStatement As String = "up_RetrieveDeskripsiKodeKerja"
        Private m_RetrieveListStatement As String = "up_RetrieveDeskripsiKodeKerjaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDeskripsiKodeKerja"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim deskripsiKodeKerja As DeskripsiKodeKerja = Nothing
            While dr.Read

                deskripsiKodeKerja = Me.CreateObject(dr)

            End While

            Return deskripsiKodeKerja

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim deskripsiKodeKerjaList As ArrayList = New ArrayList

            While dr.Read
                Dim deskripsiKodeKerja As DeskripsiKodeKerja = Me.CreateObject(dr)
                deskripsiKodeKerjaList.Add(deskripsiKodeKerja)
            End While

            Return deskripsiKodeKerjaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim deskripsiKodeKerja As DeskripsiKodeKerja = CType(obj, DeskripsiKodeKerja)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, deskripsiKodeKerja.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim deskripsiKodeKerja As DeskripsiKodeKerja = CType(obj, DeskripsiKodeKerja)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@KodeKerja", DbType.AnsiString, deskripsiKodeKerja.KodeKerja)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, deskripsiKodeKerja.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, deskripsiKodeKerja.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, deskripsiKodeKerja.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, deskripsiKodeKerja.LastUpdateBy)
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

            Dim deskripsiKodeKerja As DeskripsiKodeKerja = CType(obj, DeskripsiKodeKerja)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, deskripsiKodeKerja.ID)
            DbCommandWrapper.AddInParameter("@KodeKerja", DbType.AnsiString, deskripsiKodeKerja.KodeKerja)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, deskripsiKodeKerja.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, deskripsiKodeKerja.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, deskripsiKodeKerja.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, deskripsiKodeKerja.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DeskripsiKodeKerja

            Dim deskripsiKodeKerja As DeskripsiKodeKerja = New DeskripsiKodeKerja

            deskripsiKodeKerja.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeKerja")) Then deskripsiKodeKerja.KodeKerja = dr("KodeKerja").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then deskripsiKodeKerja.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then deskripsiKodeKerja.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then deskripsiKodeKerja.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then deskripsiKodeKerja.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then deskripsiKodeKerja.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then deskripsiKodeKerja.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then deskripsiKodeKerja.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return deskripsiKodeKerja

        End Function

        Private Sub SetTableName()

            If Not (GetType(DeskripsiKodeKerja) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DeskripsiKodeKerja), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DeskripsiKodeKerja).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

