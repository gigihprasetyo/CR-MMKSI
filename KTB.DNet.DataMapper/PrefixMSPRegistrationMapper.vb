#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PrefixMSPRegistration Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 6/23/2021 - 8:35:48 AM
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

    Public Class PrefixMSPRegistrationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPrefixMSPRegistration"
        Private m_UpdateStatement As String = "up_UpdatePrefixMSPRegistration"
        Private m_RetrieveStatement As String = "up_RetrievePrefixMSPRegistration"
        Private m_RetrieveListStatement As String = "up_RetrievePrefixMSPRegistrationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePrefixMSPRegistration"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim prefixMSPRegistration As PrefixMSPRegistration = Nothing
            While dr.Read

                prefixMSPRegistration = Me.CreateObject(dr)

            End While

            Return prefixMSPRegistration

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim prefixMSPRegistrationList As ArrayList = New ArrayList

            While dr.Read
                Dim prefixMSPRegistration As PrefixMSPRegistration = Me.CreateObject(dr)
                prefixMSPRegistrationList.Add(prefixMSPRegistration)
            End While

            Return prefixMSPRegistrationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim prefixMSPRegistration As PrefixMSPRegistration = CType(obj, PrefixMSPRegistration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, prefixMSPRegistration.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim prefixMSPRegistration As PrefixMSPRegistration = CType(obj, PrefixMSPRegistration)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ProgramName", DbType.AnsiString, prefixMSPRegistration.ProgramName)
            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, prefixMSPRegistration.MSPExTypeID)
            DbCommandWrapper.AddInParameter("@Prefix", DbType.AnsiString, prefixMSPRegistration.Prefix)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, prefixMSPRegistration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, prefixMSPRegistration.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, prefixMSPRegistration.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(prefixMSPRegistration.MSPExType))

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

            Dim prefixMSPRegistration As PrefixMSPRegistration = CType(obj, PrefixMSPRegistration)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, prefixMSPRegistration.ID)
            DbCommandWrapper.AddInParameter("@ProgramName", DbType.AnsiString, prefixMSPRegistration.ProgramName)
            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, prefixMSPRegistration.MSPExTypeID)
            DbCommandWrapper.AddInParameter("@Prefix", DbType.AnsiString, prefixMSPRegistration.Prefix)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, prefixMSPRegistration.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, prefixMSPRegistration.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, prefixMSPRegistration.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, prefixMSPRegistration.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(prefixMSPRegistration.MSPExType))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PrefixMSPRegistration

            Dim prefixMSPRegistration As PrefixMSPRegistration = New PrefixMSPRegistration

            prefixMSPRegistration.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProgramName")) Then prefixMSPRegistration.ProgramName = dr("ProgramName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExTypeID")) Then prefixMSPRegistration.MSPExTypeID = CType(dr("MSPExTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Prefix")) Then prefixMSPRegistration.Prefix = dr("Prefix").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then prefixMSPRegistration.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then prefixMSPRegistration.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then prefixMSPRegistration.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then prefixMSPRegistration.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then prefixMSPRegistration.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MSPExTypeID")) Then
                prefixMSPRegistration.MSPExType = New MSPExType(CType(dr("MSPExTypeID"), Integer))
            End If

            Return prefixMSPRegistration

        End Function

        Private Sub SetTableName()

            If Not (GetType(PrefixMSPRegistration) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PrefixMSPRegistration), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PrefixMSPRegistration).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
