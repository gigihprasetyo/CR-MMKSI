#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCertificateConfig Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/24/2019 - 11:15:06 AM
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

    Public Class TrCertificateConfigMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrCertificateConfig"
        Private m_UpdateStatement As String = "up_UpdateTrCertificateConfig"
        Private m_RetrieveStatement As String = "up_RetrieveTrCertificateConfig"
        Private m_RetrieveListStatement As String = "up_RetrieveTrCertificateConfigList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrCertificateConfig"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trCertificateConfig As TrCertificateConfig = Nothing
            While dr.Read

                trCertificateConfig = Me.CreateObject(dr)

            End While

            Return trCertificateConfig

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trCertificateConfigList As ArrayList = New ArrayList

            While dr.Read
                Dim trCertificateConfig As TrCertificateConfig = Me.CreateObject(dr)
                trCertificateConfigList.Add(trCertificateConfig)
            End While

            Return trCertificateConfigList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCertificateConfig As TrCertificateConfig = CType(obj, TrCertificateConfig)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCertificateConfig.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trCertificateConfig As TrCertificateConfig = CType(obj, TrCertificateConfig)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCertificateConfig.Description)
            DbCommandWrapper.AddInParameter("@NamaTTD", DbType.AnsiString, trCertificateConfig.NamaTTD)
            DbCommandWrapper.AddInParameter("@JabatanTTD", DbType.AnsiString, trCertificateConfig.JabatanTTD)
            DbCommandWrapper.AddInParameter("@PathTTD", DbType.AnsiString, trCertificateConfig.PathTTD)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCertificateConfig.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trCertificateConfig.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trCertificateConfig.LastUpdateBy)
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

            Dim trCertificateConfig As TrCertificateConfig = CType(obj, TrCertificateConfig)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trCertificateConfig.ID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trCertificateConfig.Description)
            DbCommandWrapper.AddInParameter("@NamaTTD", DbType.AnsiString, trCertificateConfig.NamaTTD)
            DbCommandWrapper.AddInParameter("@JabatanTTD", DbType.AnsiString, trCertificateConfig.JabatanTTD)
            DbCommandWrapper.AddInParameter("@PathTTD", DbType.AnsiString, trCertificateConfig.PathTTD)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trCertificateConfig.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trCertificateConfig.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trCertificateConfig.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrCertificateConfig

            Dim trCertificateConfig As TrCertificateConfig = New TrCertificateConfig

            trCertificateConfig.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trCertificateConfig.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NamaTTD")) Then trCertificateConfig.NamaTTD = dr("NamaTTD").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JabatanTTD")) Then trCertificateConfig.JabatanTTD = dr("JabatanTTD").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PathTTD")) Then trCertificateConfig.PathTTD = dr("PathTTD").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trCertificateConfig.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trCertificateConfig.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trCertificateConfig.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trCertificateConfig.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trCertificateConfig.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trCertificateConfig.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trCertificateConfig

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrCertificateConfig) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrCertificateConfig), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrCertificateConfig).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
