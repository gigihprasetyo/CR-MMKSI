
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ServiceHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/08/2018 - 9:55:43
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

    Public Class VWI_ServiceHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_ServiceHistory"
        Private m_UpdateStatement As String = "up_UpdateVWI_ServiceHistory"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_ServiceHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ServiceHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_ServiceHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ServiceHistory As VWI_ServiceHistory = Nothing
            While dr.Read

                VWI_ServiceHistory = Me.CreateObject(dr)

            End While

            Return VWI_ServiceHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ServiceHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ServiceHistory As VWI_ServiceHistory = Me.CreateObject(dr)
                VWI_ServiceHistoryList.Add(VWI_ServiceHistory)
            End While

            Return VWI_ServiceHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceHistory As VWI_ServiceHistory = CType(obj, VWI_ServiceHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, VWI_ServiceHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceHistory As VWI_ServiceHistory = CType(obj, VWI_ServiceHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, VWI_ServiceHistory.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@KodeChassis", DbType.AnsiString, VWI_ServiceHistory.KodeChassis)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, VWI_ServiceHistory.PKTDate)
            DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.Object, VWI_ServiceHistory.TglBukaTransaksi)
            DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.Object, VWI_ServiceHistory.TglTutupTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Object, VWI_ServiceHistory.WaktuMasuk)
            DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Object, VWI_ServiceHistory.WaktuKeluar)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_ServiceHistory.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, VWI_ServiceHistory.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryCode", DbType.AnsiString, VWI_ServiceHistory.WorkOrderCategoryCode)
            DbCommandWrapper.AddInParameter("@KMService", DbType.Int32, VWI_ServiceHistory.KMService)


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

            Dim VWI_ServiceHistory As VWI_ServiceHistory = CType(obj, VWI_ServiceHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, VWI_ServiceHistory.ID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, VWI_ServiceHistory.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@KodeChassis", DbType.AnsiString, VWI_ServiceHistory.KodeChassis)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, VWI_ServiceHistory.PKTDate)
            DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.Object, VWI_ServiceHistory.TglBukaTransaksi)
            DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.Object, VWI_ServiceHistory.TglTutupTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Object, VWI_ServiceHistory.WaktuMasuk)
            DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Object, VWI_ServiceHistory.WaktuKeluar)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_ServiceHistory.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, VWI_ServiceHistory.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryCode", DbType.AnsiString, VWI_ServiceHistory.WorkOrderCategoryCode)
            DbCommandWrapper.AddInParameter("@KMService", DbType.Int32, VWI_ServiceHistory.KMService)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ServiceHistory

            Dim VWI_ServiceHistory As VWI_ServiceHistory = New VWI_ServiceHistory

            'VWI_ServiceHistory.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then VWI_ServiceHistory.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeChassis")) Then VWI_ServiceHistory.KodeChassis = dr("KodeChassis").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDate")) Then VWI_ServiceHistory.PKTDate = CType(dr("PKTDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TglBukaTransaksi")) Then VWI_ServiceHistory.TglBukaTransaksi = CType(dr("TglBukaTransaksi"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("TglTutupTransaksi")) Then VWI_ServiceHistory.TglTutupTransaksi = CType(dr("TglTutupTransaksi"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("WaktuMasuk")) Then VWI_ServiceHistory.WaktuMasuk = CType(dr("WaktuMasuk"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("WaktuKeluar")) Then VWI_ServiceHistory.WaktuKeluar = CType(dr("WaktuKeluar"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_ServiceHistory.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then VWI_ServiceHistory.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderType")) Then VWI_ServiceHistory.WorkOrderType = dr("WorkOrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategoryCode")) Then VWI_ServiceHistory.WorkOrderCategoryCode = dr("WorkOrderCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KMService")) Then VWI_ServiceHistory.KMService = CType(dr("KMService"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoWorkOrder")) Then VWI_ServiceHistory.NoWorkOrder = dr("NoWorkOrder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_ServiceHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePlaceCode")) Then VWI_ServiceHistory.ServicePlaceCode = dr("ServicePlaceCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeCode")) Then VWI_ServiceHistory.ServiceTypeCode = dr("ServiceTypeCode").ToString
            Return VWI_ServiceHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ServiceHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ServiceHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ServiceHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

