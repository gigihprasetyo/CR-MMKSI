#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_QuickProduct Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 18/04/2018 - 9:22:26
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

    Public Class VWI_QuickProductMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_QuickProduct"
        Private m_UpdateStatement As String = "up_UpdateVWI_QuickProduct"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_QuickProduct"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_QuickProductList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_QuickProduct"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_QuickProduct As VWI_QuickProduct = Nothing
            While dr.Read

                vWI_QuickProduct = Me.CreateObject(dr)

            End While

            Return vWI_QuickProduct

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_QuickProductList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_QuickProduct As VWI_QuickProduct = Me.CreateObject(dr)
                vWI_QuickProductList.Add(vWI_QuickProduct)
            End While

            Return vWI_QuickProductList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_QuickProduct As VWI_QuickProduct = CType(obj, VWI_QuickProduct)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, vWI_QuickProduct.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_QuickProduct As VWI_QuickProduct = CType(obj, VWI_QuickProduct)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, vWI_QuickProduct.VehicleType)
            DbCommandWrapper.AddInParameter("@VehicleDesc", DbType.AnsiString, vWI_QuickProduct.VehicleDesc)
            DbCommandWrapper.AddInParameter("@ProductCategory", DbType.AnsiString, vWI_QuickProduct.ProductCategory)
            DbCommandWrapper.AddInParameter("@VehicleCatDesc", DbType.AnsiString, vWI_QuickProduct.VehicleCatDesc)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vWI_QuickProduct.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorDescription", DbType.AnsiString, vWI_QuickProduct.ColorDescription)
            DbCommandWrapper.AddInParameter("@VehicleBrand", DbType.AnsiString, vWI_QuickProduct.VehicleBrand)
            DbCommandWrapper.AddInParameter("@VehicleModel_S1", DbType.AnsiString, vWI_QuickProduct.VehicleModel_S1)
            DbCommandWrapper.AddInParameter("@VehicleCategory_S2", DbType.AnsiString, vWI_QuickProduct.VehicleCategory_S2)
            DbCommandWrapper.AddInParameter("@ProductSegment_S3", DbType.AnsiString, vWI_QuickProduct.ProductSegment_S3)
            DbCommandWrapper.AddInParameter("@DriveSystem_S4", DbType.AnsiString, vWI_QuickProduct.DriveSystem_S4)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_QuickProduct.Status)


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

            Dim vWI_QuickProduct As VWI_QuickProduct = CType(obj, VWI_QuickProduct)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, vWI_QuickProduct.ID)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, vWI_QuickProduct.VehicleType)
            DbCommandWrapper.AddInParameter("@VehicleDesc", DbType.AnsiString, vWI_QuickProduct.VehicleDesc)
            DbCommandWrapper.AddInParameter("@ProductCategory", DbType.AnsiString, vWI_QuickProduct.ProductCategory)
            DbCommandWrapper.AddInParameter("@VehicleCatDesc", DbType.AnsiString, vWI_QuickProduct.VehicleCatDesc)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vWI_QuickProduct.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorDescription", DbType.AnsiString, vWI_QuickProduct.ColorDescription)
            DbCommandWrapper.AddInParameter("@VehicleBrand", DbType.AnsiString, vWI_QuickProduct.VehicleBrand)
            DbCommandWrapper.AddInParameter("@VehicleModel_S1", DbType.AnsiString, vWI_QuickProduct.VehicleModel_S1)
            DbCommandWrapper.AddInParameter("@VehicleCategory_S2", DbType.AnsiString, vWI_QuickProduct.VehicleCategory_S2)
            DbCommandWrapper.AddInParameter("@ProductSegment_S3", DbType.AnsiString, vWI_QuickProduct.ProductSegment_S3)
            DbCommandWrapper.AddInParameter("@DriveSystem_S4", DbType.AnsiString, vWI_QuickProduct.DriveSystem_S4)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_QuickProduct.Status)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_QuickProduct

            Dim vWI_QuickProduct As VWI_QuickProduct = New VWI_QuickProduct

            vWI_QuickProduct.ID = CType(dr("ID"), Long)
            'Dealer code show on cr dealer category
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_QuickProduct.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then vWI_QuickProduct.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleDesc")) Then vWI_QuickProduct.VehicleDesc = dr("VehicleDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategory")) Then vWI_QuickProduct.ProductCategory = dr("ProductCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCatDesc")) Then vWI_QuickProduct.VehicleCatDesc = dr("VehicleCatDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then vWI_QuickProduct.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorDescription")) Then vWI_QuickProduct.ColorDescription = dr("ColorDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleBrand")) Then vWI_QuickProduct.VehicleBrand = dr("VehicleBrand").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModel_S1")) Then vWI_QuickProduct.VehicleModel_S1 = dr("VehicleModel_S1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCategory_S2")) Then vWI_QuickProduct.VehicleCategory_S2 = dr("VehicleCategory_S2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductSegment_S3")) Then vWI_QuickProduct.ProductSegment_S3 = dr("ProductSegment_S3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystem_S4")) Then vWI_QuickProduct.DriveSystem_S4 = dr("DriveSystem_S4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VariantType")) Then vWI_QuickProduct.VariantType = dr("VariantType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransmitType")) Then vWI_QuickProduct.TransmitType = dr("TransmitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystemType")) Then vWI_QuickProduct.DriveSystemType = dr("DriveSystemType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpeedType")) Then vWI_QuickProduct.SpeedType = dr("SpeedType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FuelType")) Then vWI_QuickProduct.FuelType = dr("FuelType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialFlag")) Then vWI_QuickProduct.SpecialFlag = dr("SpecialFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_QuickProduct.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_QuickProduct.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ModelYear")) Then vWI_QuickProduct.ModelYear = CType(dr("ModelYear"), Integer)

            Return vWI_QuickProduct

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_QuickProduct) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_QuickProduct), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_QuickProduct).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


