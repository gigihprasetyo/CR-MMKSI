
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitParameterHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 8:27:21
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

    Public Class BabitParameterHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitParameterHeader"
        Private m_UpdateStatement As String = "up_UpdateBabitParameterHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBabitParameterHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitParameterHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitParameterHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitParameterHeader As BabitParameterHeader = Nothing
            While dr.Read

                babitParameterHeader = Me.CreateObject(dr)

            End While

            Return babitParameterHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitParameterHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim babitParameterHeader As BabitParameterHeader = Me.CreateObject(dr)
                babitParameterHeaderList.Add(babitParameterHeader)
            End While

            Return babitParameterHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitParameterHeader As BabitParameterHeader = CType(obj, BabitParameterHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitParameterHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitParameterHeader As BabitParameterHeader = CType(obj, BabitParameterHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ParameterName", DbType.AnsiString, babitParameterHeader.ParameterName)
            DbCommandWrapper.AddInParameter("@IsMandatory", DbType.Boolean, babitParameterHeader.IsMandatory)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, babitParameterHeader.Status)
            DbCommandWrapper.AddInParameter("@ParameterCategory", DbType.Int32, babitParameterHeader.ParameterCategory)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitParameterHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitParameterHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitMasterEventTypeID", DbType.Int32, Me.GetRefObject(babitParameterHeader.BabitMasterEventType))

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

            Dim babitParameterHeader As BabitParameterHeader = CType(obj, BabitParameterHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitParameterHeader.ID)
            DbCommandWrapper.AddInParameter("@ParameterName", DbType.AnsiString, babitParameterHeader.ParameterName)
            DbCommandWrapper.AddInParameter("@IsMandatory", DbType.Boolean, babitParameterHeader.IsMandatory)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, babitParameterHeader.Status)
            DbCommandWrapper.AddInParameter("@ParameterCategory", DbType.Int32, babitParameterHeader.ParameterCategory)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitParameterHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitParameterHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitMasterEventTypeID", DbType.Int32, Me.GetRefObject(babitParameterHeader.BabitMasterEventType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitParameterHeader

            Dim babitParameterHeader As BabitParameterHeader = New BabitParameterHeader

            babitParameterHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParameterName")) Then babitParameterHeader.ParameterName = dr("ParameterName").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("BabitType")) Then babitParameterHeader.BabitType = dr("BabitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsMandatory")) Then babitParameterHeader.IsMandatory = CType(dr("IsMandatory"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitParameterHeader.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParameterCategory")) Then babitParameterHeader.ParameterCategory = CType(dr("ParameterCategory"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitParameterHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitParameterHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitParameterHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitParameterHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitParameterHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BabitMasterEventTypeID")) Then
                babitParameterHeader.BabitMasterEventType = New BabitMasterEventType(CType(dr("BabitMasterEventTypeID"), Short))
            End If

            Return babitParameterHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitParameterHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitParameterHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitParameterHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

