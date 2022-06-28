
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ATPHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 5/29/2012 - 11:23:30 AM
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

    Public Class ATPHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertATPHistory"
        Private m_UpdateStatement As String = "up_UpdateATPHistory"
        Private m_RetrieveStatement As String = "up_RetrieveATPHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveATPHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteATPHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim aTPHistory As ATPHistory = Nothing
            While dr.Read

                aTPHistory = Me.CreateObject(dr)

            End While

            Return aTPHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim aTPHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim aTPHistory As ATPHistory = Me.CreateObject(dr)
                aTPHistoryList.Add(aTPHistory)
            End While

            Return aTPHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aTPHistory As ATPHistory = CType(obj, ATPHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aTPHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aTPHistory As ATPHistory = CType(obj, ATPHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AllocationDate", DbType.DateTime, aTPHistory.AllocationDate)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, aTPHistory.MaterialNumber)
            DbCommandWrapper.AddInParameter("@PODetailID", DbType.Int32, aTPHistory.PODetailID)
            DbCommandWrapper.AddInParameter("@StokATP", DbType.Int32, aTPHistory.StokATP)
            DbCommandWrapper.AddInParameter("@StokSebelum", DbType.Int32, aTPHistory.StokSebelum)
            DbCommandWrapper.AddInParameter("@StokSesudah", DbType.Int32, aTPHistory.StokSesudah)
            DBCommandWrapper.AddInParameter("@DownloadedTime", DbType.DateTime, aTPHistory.DownloadedTime)
            DBCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, aTPHistory.ProductionYear)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aTPHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, aTPHistory.LastUpdateBy)
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

            Dim aTPHistory As ATPHistory = CType(obj, ATPHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aTPHistory.ID)
            DbCommandWrapper.AddInParameter("@AllocationDate", DbType.DateTime, aTPHistory.AllocationDate)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, aTPHistory.MaterialNumber)
            DbCommandWrapper.AddInParameter("@PODetailID", DbType.Int32, aTPHistory.PODetailID)
            DbCommandWrapper.AddInParameter("@StokATP", DbType.Int32, aTPHistory.StokATP)
            DbCommandWrapper.AddInParameter("@StokSebelum", DbType.Int32, aTPHistory.StokSebelum)
            DbCommandWrapper.AddInParameter("@StokSesudah", DbType.Int32, aTPHistory.StokSesudah)
            DBCommandWrapper.AddInParameter("@DownloadedTime", DbType.DateTime, aTPHistory.DownloadedTime)
            DBCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, aTPHistory.ProductionYear)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aTPHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, aTPHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ATPHistory

            Dim aTPHistory As ATPHistory = New ATPHistory

            aTPHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationDate")) Then aTPHistory.AllocationDate = CType(dr("AllocationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then aTPHistory.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODetailID")) Then aTPHistory.PODetailID = CType(dr("PODetailID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StokATP")) Then aTPHistory.StokATP = CType(dr("StokATP"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StokSebelum")) Then aTPHistory.StokSebelum = CType(dr("StokSebelum"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StokSesudah")) Then aTPHistory.StokSesudah = CType(dr("StokSesudah"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadedTime")) Then aTPHistory.DownloadedTime = CType(dr("DownloadedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then aTPHistory.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then aTPHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then aTPHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then aTPHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then aTPHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then aTPHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return aTPHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(ATPHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ATPHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ATPHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

