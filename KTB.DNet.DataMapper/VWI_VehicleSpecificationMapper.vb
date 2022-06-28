
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_VehicleSpecification Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 27/07/2018 - 10:39:19
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

    Public Class VWI_VehicleSpecificationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_VehicleSpecification"
        Private m_UpdateStatement As String = "up_UpdateVWI_VehicleSpecification"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_VehicleSpecification"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_VehicleSpecificationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_VehicleSpecification"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_VehicleSpecification As VWI_VehicleSpecification = Nothing
            While dr.Read

                vWI_VehicleSpecification = Me.CreateObject(dr)

            End While

            Return vWI_VehicleSpecification

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_VehicleSpecificationList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_VehicleSpecification As VWI_VehicleSpecification = Me.CreateObject(dr)
                vWI_VehicleSpecificationList.Add(vWI_VehicleSpecification)
            End While

            Return vWI_VehicleSpecificationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_VehicleSpecification As VWI_VehicleSpecification = CType(obj, VWI_VehicleSpecification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_VehicleSpecification.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_VehicleSpecification As VWI_VehicleSpecification = CType(obj, VWI_VehicleSpecification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@VehicleCategory_S1", DbType.AnsiString, vWI_VehicleSpecification.VehicleCategory_S1)
            DbCommandWrapper.AddInParameter("@ClassificationNumber", DbType.AnsiString, vWI_VehicleSpecification.ClassificationNumber)
            DbCommandWrapper.AddInParameter("@VehicleDesc", DbType.AnsiString, vWI_VehicleSpecification.VehicleDesc)
            DbCommandWrapper.AddInParameter("@ProductCategory", DbType.AnsiString, vWI_VehicleSpecification.ProductCategory)
            DbCommandWrapper.AddInParameter("@VehicleCatDesc", DbType.AnsiString, vWI_VehicleSpecification.VehicleCatDesc)
            DbCommandWrapper.AddInParameter("@VehicleBrand", DbType.AnsiString, vWI_VehicleSpecification.VehicleBrand)
            DbCommandWrapper.AddInParameter("@SpeedType", DbType.AnsiString, vWI_VehicleSpecification.SpeedType)
            DbCommandWrapper.AddInParameter("@FuelType", DbType.AnsiString, vWI_VehicleSpecification.FuelType)
            DbCommandWrapper.AddInParameter("@Transmition", DbType.AnsiString, vWI_VehicleSpecification.Transmition)
            DbCommandWrapper.AddInParameter("@Drivesystem", DbType.AnsiString, vWI_VehicleSpecification.Drivesystem)
            DbCommandWrapper.AddInParameter("@SegmentType", DbType.AnsiString, vWI_VehicleSpecification.SegmentType)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_VehicleSpecification.Status)


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

            Dim vWI_VehicleSpecification As VWI_VehicleSpecification = CType(obj, VWI_VehicleSpecification)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_VehicleSpecification.ID)
            DbCommandWrapper.AddInParameter("@VehicleCategory_S1", DbType.AnsiString, vWI_VehicleSpecification.VehicleCategory_S1)
            DbCommandWrapper.AddInParameter("@ClassificationNumber", DbType.AnsiString, vWI_VehicleSpecification.ClassificationNumber)
            DbCommandWrapper.AddInParameter("@VehicleDesc", DbType.AnsiString, vWI_VehicleSpecification.VehicleDesc)
            DbCommandWrapper.AddInParameter("@ProductCategory", DbType.AnsiString, vWI_VehicleSpecification.ProductCategory)
            DbCommandWrapper.AddInParameter("@VehicleCatDesc", DbType.AnsiString, vWI_VehicleSpecification.VehicleCatDesc)
            DbCommandWrapper.AddInParameter("@VehicleBrand", DbType.AnsiString, vWI_VehicleSpecification.VehicleBrand)
            DbCommandWrapper.AddInParameter("@SpeedType", DbType.AnsiString, vWI_VehicleSpecification.SpeedType)
            DbCommandWrapper.AddInParameter("@FuelType", DbType.AnsiString, vWI_VehicleSpecification.FuelType)
            DbCommandWrapper.AddInParameter("@Transmition", DbType.AnsiString, vWI_VehicleSpecification.Transmition)
            DbCommandWrapper.AddInParameter("@Drivesystem", DbType.AnsiString, vWI_VehicleSpecification.Drivesystem)
            DbCommandWrapper.AddInParameter("@SegmentType", DbType.AnsiString, vWI_VehicleSpecification.SegmentType)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_VehicleSpecification.Status)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_VehicleSpecification

            Dim vWI_VehicleSpecification As VWI_VehicleSpecification = New VWI_VehicleSpecification

            'vWI_VehicleSpecification.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCategory_S1")) Then vWI_VehicleSpecification.VehicleCategory_S1 = dr("VehicleCategory_S1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassificationNumber")) Then vWI_VehicleSpecification.ClassificationNumber = dr("ClassificationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleDesc")) Then vWI_VehicleSpecification.VehicleDesc = dr("VehicleDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategory")) Then vWI_VehicleSpecification.ProductCategory = dr("ProductCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCatDesc")) Then vWI_VehicleSpecification.VehicleCatDesc = dr("VehicleCatDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleBrand")) Then vWI_VehicleSpecification.VehicleBrand = dr("VehicleBrand").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpeedType")) Then vWI_VehicleSpecification.SpeedType = dr("SpeedType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FuelType")) Then vWI_VehicleSpecification.FuelType = dr("FuelType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Transmition")) Then vWI_VehicleSpecification.Transmition = dr("Transmition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Drivesystem")) Then vWI_VehicleSpecification.Drivesystem = dr("Drivesystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SegmentType")) Then vWI_VehicleSpecification.SegmentType = dr("SegmentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_VehicleSpecification.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_VehicleSpecification.Status = CType(dr("Status"), Integer)

            Return vWI_VehicleSpecification

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_VehicleSpecification) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_VehicleSpecification), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_VehicleSpecification).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

