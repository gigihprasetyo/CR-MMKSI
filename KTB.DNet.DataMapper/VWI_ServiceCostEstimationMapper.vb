#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceCostEstimation Objects Mapper.
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

    Public Class VWI_ServiceCostEstimationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_ServiceCostEstimation"
        Private m_UpdateStatement As String = "up_UpdateVWI_ServiceCostEstimation"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_ServiceCostEstimation"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ServiceCostEstimationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_ServiceCostEstimation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ServiceCostEstimation As VWI_ServiceCostEstimation = Nothing
            While dr.Read

                VWI_ServiceCostEstimation = Me.CreateObject(dr)

            End While

            Return VWI_ServiceCostEstimation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ServiceCostEstimationList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ServiceCostEstimation As VWI_ServiceCostEstimation = Me.CreateObject(dr)
                VWI_ServiceCostEstimationList.Add(VWI_ServiceCostEstimation)
            End While

            Return VWI_ServiceCostEstimationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceCostEstimation As VWI_ServiceCostEstimation = CType(obj, VWI_ServiceCostEstimation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceCostEstimation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceCostEstimation As VWI_ServiceCostEstimation = CType(obj, VWI_ServiceCostEstimation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ServiceType", DbType.Int32, VWI_ServiceCostEstimation.ServiceType)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, VWI_ServiceCostEstimation.KindCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, VWI_ServiceCostEstimation.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_ServiceCostEstimation.DealerCode)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, VWI_ServiceCostEstimation.Varian)
            DbCommandWrapper.AddInParameter("@JenisService", DbType.AnsiString, VWI_ServiceCostEstimation.JenisService)
            DbCommandWrapper.AddInParameter("@JenisKegiatan", DbType.AnsiString, VWI_ServiceCostEstimation.JenisKegiatan)
            DbCommandWrapper.AddInParameter("@JasaService", DbType.Decimal, VWI_ServiceCostEstimation.JasaService)
            DbCommandWrapper.AddInParameter("@Details", DbType.AnsiString, VWI_ServiceCostEstimation.Details)

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

            Dim VWI_ServiceCostEstimation As VWI_ServiceCostEstimation = CType(obj, VWI_ServiceCostEstimation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceCostEstimation.ID)
            DbCommandWrapper.AddInParameter("@ServiceType", DbType.Int32, VWI_ServiceCostEstimation.ServiceType)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, VWI_ServiceCostEstimation.KindCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, VWI_ServiceCostEstimation.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_ServiceCostEstimation.DealerCode)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, VWI_ServiceCostEstimation.Varian)
            DbCommandWrapper.AddInParameter("@JenisService", DbType.AnsiString, VWI_ServiceCostEstimation.JenisService)
            DbCommandWrapper.AddInParameter("@JenisKegiatan", DbType.AnsiString, VWI_ServiceCostEstimation.JenisKegiatan)
            DbCommandWrapper.AddInParameter("@JasaService", DbType.Decimal, VWI_ServiceCostEstimation.JasaService)
            DbCommandWrapper.AddInParameter("@Details", DbType.AnsiString, VWI_ServiceCostEstimation.Details)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ServiceCostEstimation

            Dim VWI_ServiceCostEstimation As VWI_ServiceCostEstimation = New VWI_ServiceCostEstimation

            VWI_ServiceCostEstimation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then VWI_ServiceCostEstimation.ServiceType = CType(dr("ServiceType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then VWI_ServiceCostEstimation.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then VWI_ServiceCostEstimation.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_ServiceCostEstimation.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then VWI_ServiceCostEstimation.Varian = dr("Varian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JenisService")) Then VWI_ServiceCostEstimation.JenisService = dr("JenisService").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JenisKegiatan")) Then VWI_ServiceCostEstimation.JenisKegiatan = dr("JenisKegiatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JasaService")) Then VWI_ServiceCostEstimation.JasaService = CType(dr("JasaService"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Details")) Then VWI_ServiceCostEstimation.Details = dr("Details").ToString
            Return VWI_ServiceCostEstimation

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ServiceCostEstimation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ServiceCostEstimation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ServiceCostEstimation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
