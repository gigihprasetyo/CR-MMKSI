#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_WarrantyActivation Objects Mapper.
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

    Public Class V_WarrantyActivationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_WarrantyActivation"
        Private m_UpdateStatement As String = "up_UpdateV_WarrantyActivation"
        Private m_RetrieveStatement As String = "up_RetrieveV_WarrantyActivation"
        Private m_RetrieveListStatement As String = "up_RetrieveV_WarrantyActivationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_WarrantyActivation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_WarrantyActivation As V_WarrantyActivation = Nothing
            While dr.Read

                V_WarrantyActivation = Me.CreateObject(dr)

            End While

            Return V_WarrantyActivation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_WarrantyActivationList As ArrayList = New ArrayList

            While dr.Read
                Dim V_WarrantyActivation As V_WarrantyActivation = Me.CreateObject(dr)
                V_WarrantyActivationList.Add(V_WarrantyActivation)
            End While

            Return V_WarrantyActivationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_WarrantyActivation As V_WarrantyActivation = CType(obj, V_WarrantyActivation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_WarrantyActivation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_WarrantyActivation As V_WarrantyActivation = CType(obj, V_WarrantyActivation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, V_WarrantyActivation.ChassisNumber)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, V_WarrantyActivation.SoldDealerID)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, V_WarrantyActivation.CustomerName)
            DbCommandWrapper.AddInParameter("@PDIID", DbType.Int32, V_WarrantyActivation.PDIID)
            DbCommandWrapper.AddInParameter("@PDIStatus", DbType.AnsiString, V_WarrantyActivation.PDIStatus)
            DbCommandWrapper.AddInParameter("@PDIDate", DbType.DateTime, V_WarrantyActivation.PDIDate)
            DbCommandWrapper.AddInParameter("@PDICertificateFile", DbType.AnsiString, V_WarrantyActivation.PDICertificateFile)
            DbCommandWrapper.AddInParameter("@WarantyActivationID", DbType.Int32, V_WarrantyActivation.WarantyActivationID)
            DbCommandWrapper.AddInParameter("@WADate", DbType.DateTime, V_WarrantyActivation.WADate)
            DbCommandWrapper.AddInParameter("@WaCertificateFile", DbType.AnsiString, V_WarrantyActivation.WaCertificateFile)
            DbCommandWrapper.AddInParameter("@WARequestor", DbType.AnsiString, V_WarrantyActivation.WARequestor)
            DbCommandWrapper.AddInParameter("@SPKDealerCode", DbType.AnsiString, V_WarrantyActivation.SPKDealerCode)
            DbCommandWrapper.AddInParameter("@SPKDealerName", DbType.AnsiString, V_WarrantyActivation.SPKDealerName)
            DbCommandWrapper.AddInParameter("@DSFilePath", DbType.AnsiString, V_WarrantyActivation.DSFilePath)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, V_WarrantyActivation.PKTDate)
            DbCommandWrapper.AddInParameter("@WaStatus", DbType.Int32, V_WarrantyActivation.WaStatus)
            DbCommandWrapper.AddInParameter("@WaStatusDesc", DbType.AnsiString, V_WarrantyActivation.WaStatusDesc)
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, V_WarrantyActivation.DealerGroupID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, V_WarrantyActivation.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, V_WarrantyActivation.SalesmanName)
            DbCommandWrapper.AddInParameter("@PilotingWarranty", DbType.AnsiString, V_WarrantyActivation.PilotingWarranty)
            DbCommandWrapper.AddInParameter("@LastUpdateTimePKT", DbType.DateTime, V_WarrantyActivation.LastUpdateTimePKT)
            DbCommandWrapper.AddInParameter("@LastUpdateTimePDI", DbType.DateTime, V_WarrantyActivation.LastUpdateTimePDI)

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

            Dim V_WarrantyActivation As V_WarrantyActivation = CType(obj, V_WarrantyActivation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, V_WarrantyActivation.ChassisNumber)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, V_WarrantyActivation.SoldDealerID)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, V_WarrantyActivation.CustomerName)
            DbCommandWrapper.AddInParameter("@PDIID", DbType.Int32, V_WarrantyActivation.PDIID)
            DbCommandWrapper.AddInParameter("@PDIStatus", DbType.AnsiString, V_WarrantyActivation.PDIStatus)
            DbCommandWrapper.AddInParameter("@PDIDate", DbType.DateTime, V_WarrantyActivation.PDIDate)
            DbCommandWrapper.AddInParameter("@PDICertificateFile", DbType.AnsiString, V_WarrantyActivation.PDICertificateFile)
            DbCommandWrapper.AddInParameter("@WarantyActivationID", DbType.Int32, V_WarrantyActivation.WarantyActivationID)
            DbCommandWrapper.AddInParameter("@WADate", DbType.DateTime, V_WarrantyActivation.WADate)
            DbCommandWrapper.AddInParameter("@WaCertificateFile", DbType.AnsiString, V_WarrantyActivation.WaCertificateFile)
            DbCommandWrapper.AddInParameter("@WARequestor", DbType.AnsiString, V_WarrantyActivation.WARequestor)
            DbCommandWrapper.AddInParameter("@SPKDealerCode", DbType.AnsiString, V_WarrantyActivation.SPKDealerCode)
            DbCommandWrapper.AddInParameter("@SPKDealerName", DbType.AnsiString, V_WarrantyActivation.SPKDealerName)
            DbCommandWrapper.AddInParameter("@DSFilePath", DbType.AnsiString, V_WarrantyActivation.DSFilePath)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, V_WarrantyActivation.PKTDate)
            DbCommandWrapper.AddInParameter("@WaStatus", DbType.Int32, V_WarrantyActivation.WaStatus)
            DbCommandWrapper.AddInParameter("@WaStatusDesc", DbType.AnsiString, V_WarrantyActivation.WaStatusDesc)
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, V_WarrantyActivation.DealerGroupID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, V_WarrantyActivation.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, V_WarrantyActivation.SalesmanName)
            DbCommandWrapper.AddInParameter("@PilotingWarranty", DbType.AnsiString, V_WarrantyActivation.PilotingWarranty)
            DbCommandWrapper.AddInParameter("@LastUpdateTimePKT", DbType.DateTime, V_WarrantyActivation.LastUpdateTimePKT)
            DbCommandWrapper.AddInParameter("@LastUpdateTimePDI", DbType.DateTime, V_WarrantyActivation.LastUpdateTimePDI)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_WarrantyActivation

            Dim V_WarrantyActivation As V_WarrantyActivation = New V_WarrantyActivation

            V_WarrantyActivation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then V_WarrantyActivation.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then V_WarrantyActivation.SoldDealerID = CType(dr("SoldDealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then V_WarrantyActivation.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIID")) Then V_WarrantyActivation.PDIID = CType(dr("PDIID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PDIStatus")) Then V_WarrantyActivation.PDIStatus = dr("PDIStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIDate")) Then V_WarrantyActivation.PDIDate = CType(dr("PDIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PDICertificateFile")) Then V_WarrantyActivation.PDICertificateFile = dr("PDICertificateFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WarantyActivationID")) Then V_WarrantyActivation.WarantyActivationID = CType(dr("WarantyActivationID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WADate")) Then V_WarrantyActivation.WADate = CType(dr("WADate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WaCertificateFile")) Then V_WarrantyActivation.WaCertificateFile = dr("WaCertificateFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WARequestor")) Then V_WarrantyActivation.WARequestor = dr("WARequestor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDealerCode")) Then V_WarrantyActivation.SPKDealerCode = dr("SPKDealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDealerName")) Then V_WarrantyActivation.SPKDealerName = dr("SPKDealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DSFilePath")) Then V_WarrantyActivation.DSFilePath = dr("DSFilePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDate")) Then V_WarrantyActivation.PKTDate = CType(dr("PKTDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WaStatus")) Then V_WarrantyActivation.WaStatus = CType(dr("WaStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WaStatusDesc")) Then V_WarrantyActivation.WaStatusDesc = dr("WaStatusDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupID")) Then V_WarrantyActivation.DealerGroupID = CType(dr("DealerGroupID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then V_WarrantyActivation.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanName")) Then V_WarrantyActivation.SalesmanName = dr("SalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PilotingWarranty")) Then V_WarrantyActivation.PilotingWarranty = dr("PilotingWarranty").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTimePKT")) Then V_WarrantyActivation.LastUpdateTimePKT = CType(dr("LastUpdateTimePKT"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTimePDI")) Then V_WarrantyActivation.LastUpdateTimePDI = CType(dr("LastUpdateTimePDI"), DateTime)
            Return V_WarrantyActivation

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_WarrantyActivation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_WarrantyActivation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_WarrantyActivation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
