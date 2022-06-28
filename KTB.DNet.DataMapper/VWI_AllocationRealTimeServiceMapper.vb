#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_AllocationRealTimeService Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:26:31 PM
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
    Public Class VWI_AllocationRealTimeServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_AllocationRealTimeService"
        Private m_UpdateStatement As String = "up_UpdateVWI_AllocationRealTimeService"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_AllocationRealTimeService"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_AllocationRealTimeServiceMapperList" '[up_RetrieveVWI_AllocationRealTimeServiceList]
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_AllocationRealTimeService"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_AllocationRealTimeService As VWI_AllocationRealTimeService = Nothing
            While dr.Read

                VWI_AllocationRealTimeService = Me.CreateObject(dr)

            End While

            Return VWI_AllocationRealTimeService

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_AllocationRealTimeServiceList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_AllocationRealTimeService As VWI_AllocationRealTimeService = Me.CreateObject(dr)
                VWI_AllocationRealTimeServiceList.Add(VWI_AllocationRealTimeService)
            End While

            Return VWI_AllocationRealTimeServiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_AllocationRealTimeService As VWI_AllocationRealTimeService = CType(obj, VWI_AllocationRealTimeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_AllocationRealTimeService.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_AllocationRealTimeService As VWI_AllocationRealTimeService = CType(obj, VWI_AllocationRealTimeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_AllocationRealTimeService.DealerCode)
            DbCommandWrapper.AddInParameter("@AlokasiStall", DbType.Int32, VWI_AllocationRealTimeService.AlokasiStall)
            DbCommandWrapper.AddInParameter("@CurrentStall", DbType.Int32, VWI_AllocationRealTimeService.CurrentStall)

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

            Dim VWI_AllocationRealTimeService As VWI_AllocationRealTimeService = CType(obj, VWI_AllocationRealTimeService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.Int32, VWI_AllocationRealTimeService.DealerCode)
            DbCommandWrapper.AddInParameter("@AlokasiStall", DbType.Int32, VWI_AllocationRealTimeService.AlokasiStall)
            DbCommandWrapper.AddInParameter("@CurrentStall", DbType.Int32, VWI_AllocationRealTimeService.CurrentStall)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_AllocationRealTimeService

            Dim VWI_AllocationRealTimeService As VWI_AllocationRealTimeService = New VWI_AllocationRealTimeService

            VWI_AllocationRealTimeService.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_AllocationRealTimeService.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AlokasiStall")) Then VWI_AllocationRealTimeService.AlokasiStall = CType(dr("AlokasiStall"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CurrentStall")) Then VWI_AllocationRealTimeService.CurrentStall = CType(dr("CurrentStall"), Integer)
            Return VWI_AllocationRealTimeService

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_AllocationRealTimeService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_AllocationRealTimeService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_AllocationRealTimeService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region
    End Class

End Namespace
