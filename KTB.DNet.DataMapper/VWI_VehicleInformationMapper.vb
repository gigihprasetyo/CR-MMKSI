
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_VehicleInformation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 01/10/2018 - 15:18:58
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

    Public Class VWI_VehicleInformationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_VehicleInformation"
        Private m_UpdateStatement As String = "up_UpdateVWI_VehicleInformation"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_VehicleInformation"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_VehicleInformationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_VehicleInformation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_VehicleInformation As VWI_VehicleInformation = Nothing
            While dr.Read

                VWI_VehicleInformation = Me.CreateObject(dr)

            End While

            Return VWI_VehicleInformation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_VehicleInformationList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_VehicleInformation As VWI_VehicleInformation = Me.CreateObject(dr)
                VWI_VehicleInformationList.Add(VWI_VehicleInformation)
            End While

            Return VWI_VehicleInformationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_VehicleInformation As VWI_VehicleInformation = CType(obj, VWI_VehicleInformation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_VehicleInformation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_VehicleInformation As VWI_VehicleInformation = CType(obj, VWI_VehicleInformation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_VehicleInformation.ChassisNumber)
            DbCommandWrapper.AddInParameter("@IsBB", DbType.Int32, VWI_VehicleInformation.IsBB)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, VWI_VehicleInformation.CategoryCode)
            DbCommandWrapper.AddInParameter("@CategoryDesc", DbType.AnsiString, VWI_VehicleInformation.CategoryDesc)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, VWI_VehicleInformation.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorIndName", DbType.AnsiString, VWI_VehicleInformation.ColorIndName)
            DbCommandWrapper.AddInParameter("@ColorEngName", DbType.AnsiString, VWI_VehicleInformation.ColorEngName)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, VWI_VehicleInformation.MaterialDescription)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, VWI_VehicleInformation.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, VWI_VehicleInformation.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@ModelSearchTerm1", DbType.AnsiString, VWI_VehicleInformation.ModelSearchTerm1)
            DbCommandWrapper.AddInParameter("@ModelSearchTerm2", DbType.AnsiString, VWI_VehicleInformation.ModelSearchTerm2)
            DbCommandWrapper.AddInParameter("@SegmentType", DbType.AnsiString, VWI_VehicleInformation.SegmentType)
            DbCommandWrapper.AddInParameter("@FuelType", DbType.AnsiString, VWI_VehicleInformation.FuelType)
            DbCommandWrapper.AddInParameter("@TransmitType", DbType.AnsiString, VWI_VehicleInformation.TransmitType)
            DbCommandWrapper.AddInParameter("@DriveSystemType", DbType.AnsiString, VWI_VehicleInformation.DriveSystemType)
            DbCommandWrapper.AddInParameter("@VariantType", DbType.AnsiString, VWI_VehicleInformation.VariantType)
            DbCommandWrapper.AddInParameter("@VehicleBrand", DbType.AnsiString, VWI_VehicleInformation.VehicleBrand)
            DbCommandWrapper.AddInParameter("@SpeedType", DbType.AnsiString, VWI_VehicleInformation.SpeedType)
            DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, VWI_VehicleInformation.VehicleKindID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, VWI_VehicleInformation.Code)
            DbCommandWrapper.AddInParameter("@VehicleKindDesc", DbType.AnsiString, VWI_VehicleInformation.VehicleKindDesc)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, VWI_VehicleInformation.SoldDealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_VehicleInformation.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_VehicleInformation.DealerName)
            'DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, VWI_VehicleInformation.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, VWI_VehicleInformation.SerialNumber)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, VWI_VehicleInformation.ProductionYear)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, VWI_VehicleInformation.FleetCode)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, VWI_VehicleInformation.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, VWI_VehicleInformation.FakturDate)
            DbCommandWrapper.AddInParameter("@FSExtended", DbType.AnsiString, VWI_VehicleInformation.FSExtended)
            DbCommandWrapper.AddInParameter("@FSProgram", DbType.AnsiString, VWI_VehicleInformation.FSProgram)

            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, VWI_VehicleInformation.PKTDate)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@WSCDuration", DbType.Int32, VWI_VehicleInformation.WSCDuration)
            DbCommandWrapper.AddInParameter("@WSCStart", DbType.DateTime, VWI_VehicleInformation.WSCStart)
            DbCommandWrapper.AddInParameter("@WSCEnd", DbType.DateTime, VWI_VehicleInformation.WSCEnd)
            DbCommandWrapper.AddInParameter("@WebModel", DbType.AnsiString, VWI_VehicleInformation.WebModel)
            DbCommandWrapper.AddInParameter("@WebVariant", DbType.AnsiString, VWI_VehicleInformation.WebVariant)
            DbCommandWrapper.AddInParameter("@ColorWeb", DbType.AnsiString, VWI_VehicleInformation.ColorWeb)
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

            Dim VWI_VehicleInformation As VWI_VehicleInformation = CType(obj, VWI_VehicleInformation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_VehicleInformation.ID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_VehicleInformation.ChassisNumber)
            DbCommandWrapper.AddInParameter("@IsBB", DbType.Int32, VWI_VehicleInformation.IsBB)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, VWI_VehicleInformation.CategoryCode)
            DbCommandWrapper.AddInParameter("@CategoryDesc", DbType.AnsiString, VWI_VehicleInformation.CategoryDesc)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, VWI_VehicleInformation.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorIndName", DbType.AnsiString, VWI_VehicleInformation.ColorIndName)
            DbCommandWrapper.AddInParameter("@ColorEngName", DbType.AnsiString, VWI_VehicleInformation.ColorEngName)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, VWI_VehicleInformation.MaterialDescription)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, VWI_VehicleInformation.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, VWI_VehicleInformation.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@ModelSearchTerm1", DbType.AnsiString, VWI_VehicleInformation.ModelSearchTerm1)
            DbCommandWrapper.AddInParameter("@ModelSearchTerm2", DbType.AnsiString, VWI_VehicleInformation.ModelSearchTerm2)
            DbCommandWrapper.AddInParameter("@SegmentType", DbType.AnsiString, VWI_VehicleInformation.SegmentType)
            DbCommandWrapper.AddInParameter("@FuelType", DbType.AnsiString, VWI_VehicleInformation.FuelType)
            DbCommandWrapper.AddInParameter("@TransmitType", DbType.AnsiString, VWI_VehicleInformation.TransmitType)
            DbCommandWrapper.AddInParameter("@DriveSystemType", DbType.AnsiString, VWI_VehicleInformation.DriveSystemType)
            DbCommandWrapper.AddInParameter("@VariantType", DbType.AnsiString, VWI_VehicleInformation.VariantType)
            DbCommandWrapper.AddInParameter("@VehicleBrand", DbType.AnsiString, VWI_VehicleInformation.VehicleBrand)
            DbCommandWrapper.AddInParameter("@SpeedType", DbType.AnsiString, VWI_VehicleInformation.SpeedType)
            DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, VWI_VehicleInformation.VehicleKindID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, VWI_VehicleInformation.Code)
            DbCommandWrapper.AddInParameter("@VehicleKindDesc", DbType.AnsiString, VWI_VehicleInformation.VehicleKindDesc)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, VWI_VehicleInformation.SoldDealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_VehicleInformation.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_VehicleInformation.DealerName)
            'DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, VWI_VehicleInformation.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, VWI_VehicleInformation.SerialNumber)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, VWI_VehicleInformation.ProductionYear)
            DbCommandWrapper.AddInParameter("@FleetCode", DbType.AnsiString, VWI_VehicleInformation.FleetCode)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, VWI_VehicleInformation.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, VWI_VehicleInformation.FakturDate)
            DbCommandWrapper.AddInParameter("@FSExtended", DbType.AnsiString, VWI_VehicleInformation.FSExtended)
            DbCommandWrapper.AddInParameter("@FSProgram", DbType.AnsiString, VWI_VehicleInformation.FSProgram)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, VWI_VehicleInformation.PKTDate)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@WSCDuration", DbType.Int32, VWI_VehicleInformation.WSCDuration)
            DbCommandWrapper.AddInParameter("@WSCStart", DbType.DateTime, VWI_VehicleInformation.WSCStart)
            DbCommandWrapper.AddInParameter("@WSCEnd", DbType.DateTime, VWI_VehicleInformation.WSCEnd)
            DbCommandWrapper.AddInParameter("@WebModel", DbType.AnsiString, VWI_VehicleInformation.WebModel)
            DbCommandWrapper.AddInParameter("@WebVariant", DbType.AnsiString, VWI_VehicleInformation.WebVariant)
            DbCommandWrapper.AddInParameter("@ColorWeb", DbType.AnsiString, VWI_VehicleInformation.ColorWeb)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_VehicleInformation

            Dim VWI_VehicleInformation As VWI_VehicleInformation = New VWI_VehicleInformation

            VWI_VehicleInformation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then VWI_VehicleInformation.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsBB")) Then VWI_VehicleInformation.IsBB = CType(dr("IsBB"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then VWI_VehicleInformation.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryDesc")) Then VWI_VehicleInformation.CategoryDesc = dr("CategoryDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then VWI_VehicleInformation.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorIndName")) Then VWI_VehicleInformation.ColorIndName = dr("ColorIndName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorEngName")) Then VWI_VehicleInformation.ColorEngName = dr("ColorEngName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then VWI_VehicleInformation.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then VWI_VehicleInformation.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then VWI_VehicleInformation.VehicleTypeDesc = dr("VehicleTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModelSearchTerm1")) Then VWI_VehicleInformation.ModelSearchTerm1 = dr("ModelSearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModelSearchTerm2")) Then VWI_VehicleInformation.ModelSearchTerm2 = dr("ModelSearchTerm2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SegmentType")) Then VWI_VehicleInformation.SegmentType = dr("SegmentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FuelType")) Then VWI_VehicleInformation.FuelType = dr("FuelType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransmitType")) Then VWI_VehicleInformation.TransmitType = dr("TransmitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystemType")) Then VWI_VehicleInformation.DriveSystemType = dr("DriveSystemType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VariantType")) Then VWI_VehicleInformation.VariantType = dr("VariantType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleBrand")) Then VWI_VehicleInformation.VehicleBrand = dr("VehicleBrand").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpeedType")) Then VWI_VehicleInformation.SpeedType = dr("SpeedType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then VWI_VehicleInformation.VehicleKindID = CType(dr("VehicleKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then VWI_VehicleInformation.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindDesc")) Then VWI_VehicleInformation.VehicleKindDesc = dr("VehicleKindDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then VWI_VehicleInformation.SoldDealerID = CType(dr("SoldDealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_VehicleInformation.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_VehicleInformation.DealerName = dr("DealerName").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then VWI_VehicleInformation.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SerialNumber")) Then VWI_VehicleInformation.SerialNumber = dr("SerialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then VWI_VehicleInformation.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCode")) Then VWI_VehicleInformation.FleetCode = dr("FleetCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then VWI_VehicleInformation.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then VWI_VehicleInformation.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FSExtended")) Then VWI_VehicleInformation.FSExtended = dr("FSExtended").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FSProgram")) Then VWI_VehicleInformation.FSProgram = dr("FSProgram").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDate")) Then VWI_VehicleInformation.PKTDate = CType(dr("PKTDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_VehicleInformation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("WSCDuration")) Then VWI_VehicleInformation.WSCDuration = CType(dr("WSCDuration"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCStart")) Then VWI_VehicleInformation.WSCStart = CType(dr("WSCStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WSCEnd")) Then VWI_VehicleInformation.WSCEnd = CType(dr("WSCEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WebModel")) Then VWI_VehicleInformation.WebModel = dr("WebModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WebVariant")) Then VWI_VehicleInformation.WebVariant = dr("WebVariant").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorWeb")) Then VWI_VehicleInformation.ColorWeb = dr("ColorWeb").ToString

            Return VWI_VehicleInformation

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_VehicleInformation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_VehicleInformation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_VehicleInformation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

