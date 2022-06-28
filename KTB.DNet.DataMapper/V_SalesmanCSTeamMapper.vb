
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanCSTeam Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/9/2011 - 11:00:26 AM
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

    Public Class V_SalesmanCSTeamMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SalesmanCSTeam"
        Private m_UpdateStatement As String = "up_UpdateV_SalesmanCSTeam"
        Private m_RetrieveStatement As String = "up_RetrieveV_SalesmanCSTeam"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SalesmanCSTeamList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SalesmanCSTeam"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_SalesmanCSTeam As V_SalesmanCSTeam = Nothing
            While dr.Read

                V_SalesmanCSTeam = Me.CreateObject(dr)

            End While

            Return V_SalesmanCSTeam

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_SalesmanCSTeamList As ArrayList = New ArrayList

            While dr.Read
                Dim V_SalesmanCSTeam As V_SalesmanCSTeam = Me.CreateObject(dr)
                V_SalesmanCSTeamList.Add(V_SalesmanCSTeam)
            End While

            Return V_SalesmanCSTeamList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_SalesmanCSTeam As V_SalesmanCSTeam = CType(obj, V_SalesmanCSTeam)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, V_SalesmanCSTeam.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_SalesmanCSTeam As V_SalesmanCSTeam = CType(obj, V_SalesmanCSTeam)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 2)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, V_SalesmanCSTeam.DealerId)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, V_SalesmanCSTeam.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, V_SalesmanCSTeam.Name)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, V_SalesmanCSTeam.Image)
            DbCommandWrapper.AddInParameter("@PlaceOfBirth", DbType.AnsiString, V_SalesmanCSTeam.PlaceOfBirth)
            DbCommandWrapper.AddInParameter("@DateOfBirth", DbType.DateTime, V_SalesmanCSTeam.DateOfBirth)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, V_SalesmanCSTeam.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, V_SalesmanCSTeam.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, V_SalesmanCSTeam.City)
            DbCommandWrapper.AddInParameter("@ShopSiteNumber", DbType.Int32, V_SalesmanCSTeam.ShopSiteNumber)
            DbCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, V_SalesmanCSTeam.HireDate)
            DbCommandWrapper.AddInParameter("@SalesmanAreaId", DbType.Int32, V_SalesmanCSTeam.SalesmanAreaId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Main", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Main)
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, V_SalesmanCSTeam.SalesmanLevelID)
            DbCommandWrapper.AddInParameter("@JobPositionId_Second", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Second)
            DbCommandWrapper.AddInParameter("@JobPositionId_Third", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Third)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, V_SalesmanCSTeam.LeaderId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Leader", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Leader)
            DbCommandWrapper.AddInParameter("@RegisterStatus", DbType.AnsiStringFixedLength, V_SalesmanCSTeam.RegisterStatus)
            DbCommandWrapper.AddInParameter("@MarriedStatus", DbType.AnsiStringFixedLength, V_SalesmanCSTeam.MarriedStatus)
            DbCommandWrapper.AddInParameter("@ResignDate", DbType.DateTime, V_SalesmanCSTeam.ResignDate)
            DbCommandWrapper.AddInParameter("@ResignReason", DbType.AnsiString, V_SalesmanCSTeam.ResignReason)
            DbCommandWrapper.AddInParameter("@SalesIndicator", DbType.Byte, V_SalesmanCSTeam.SalesIndicator)
            DbCommandWrapper.AddInParameter("@SalesUnitIndicator", DbType.Byte, V_SalesmanCSTeam.SalesUnitIndicator)
            DbCommandWrapper.AddInParameter("@MechanicIndicator", DbType.Byte, V_SalesmanCSTeam.MechanicIndicator)
            DbCommandWrapper.AddInParameter("@SparePartIndicator", DbType.Byte, V_SalesmanCSTeam.SparePartIndicator)
            DbCommandWrapper.AddInParameter("@SPAdminIndicator", DbType.Byte, V_SalesmanCSTeam.SPAdminIndicator)
            DbCommandWrapper.AddInParameter("@SPWareHouseIndicator", DbType.Byte, V_SalesmanCSTeam.SPWareHouseIndicator)
            DbCommandWrapper.AddInParameter("@SPCounterIndicator", DbType.Byte, V_SalesmanCSTeam.SPCounterIndicator)
            DbCommandWrapper.AddInParameter("@SPSalesIndicator", DbType.Byte, V_SalesmanCSTeam.SPSalesIndicator)
            DbCommandWrapper.AddInParameter("@SPCSIndicator", DbType.Byte, V_SalesmanCSTeam.SPCSIndicator)
            DbCommandWrapper.AddInParameter("@IsRequestID", DbType.Byte, V_SalesmanCSTeam.IsRequestID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, V_SalesmanCSTeam.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_SalesmanCSTeam.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_SalesmanCSTeam.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, V_SalesmanCSTeam.DealerCode)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, V_SalesmanCSTeam.ProvinceName)
            DbCommandWrapper.AddInParameter("@DivisiID", DbType.Int32, V_SalesmanCSTeam.DivisiID)
            DbCommandWrapper.AddInParameter("@DivisiName", DbType.AnsiString, V_SalesmanCSTeam.DivisiName)
            DbCommandWrapper.AddInParameter("@PosisiID", DbType.Int32, V_SalesmanCSTeam.PosisiID)
            DbCommandWrapper.AddInParameter("@PosisiName", DbType.AnsiString, V_SalesmanCSTeam.PosisiName)
            DbCommandWrapper.AddInParameter("@LevelID", DbType.Int32, V_SalesmanCSTeam.LevelID)
            DbCommandWrapper.AddInParameter("@LevelName", DbType.AnsiString, V_SalesmanCSTeam.LevelName)
            DbCommandWrapper.AddInParameter("@Salary", DbType.Currency, V_SalesmanCSTeam.Salary)
            DbCommandWrapper.AddInParameter("@LeaderCode", DbType.AnsiString, V_SalesmanCSTeam.LeaderCode)
            DbCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, V_SalesmanCSTeam.LeaderName)
            DbCommandWrapper.AddInParameter("@AreaDesc", DbType.AnsiString, V_SalesmanCSTeam.AreaDesc)
            DbCommandWrapper.AddInParameter("@PENDIDIKAN", DbType.AnsiString, V_SalesmanCSTeam.PENDIDIKAN)
            DbCommandWrapper.AddInParameter("@EMAIL", DbType.AnsiString, V_SalesmanCSTeam.EMAIL)
            DbCommandWrapper.AddInParameter("@NO_HP", DbType.AnsiString, V_SalesmanCSTeam.NO_HP)
            DbCommandWrapper.AddInParameter("@NOKTP", DbType.AnsiString, V_SalesmanCSTeam.NOKTP)


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

            Dim V_SalesmanCSTeam As V_SalesmanCSTeam = CType(obj, V_SalesmanCSTeam)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_SalesmanCSTeam.ID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, V_SalesmanCSTeam.DealerId)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, V_SalesmanCSTeam.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, V_SalesmanCSTeam.Name)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, V_SalesmanCSTeam.Image)
            DbCommandWrapper.AddInParameter("@PlaceOfBirth", DbType.AnsiString, V_SalesmanCSTeam.PlaceOfBirth)
            DbCommandWrapper.AddInParameter("@DateOfBirth", DbType.DateTime, V_SalesmanCSTeam.DateOfBirth)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, V_SalesmanCSTeam.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, V_SalesmanCSTeam.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, V_SalesmanCSTeam.City)
            DbCommandWrapper.AddInParameter("@ShopSiteNumber", DbType.Int32, V_SalesmanCSTeam.ShopSiteNumber)
            DbCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, V_SalesmanCSTeam.HireDate)
            DbCommandWrapper.AddInParameter("@SalesmanAreaId", DbType.Int32, V_SalesmanCSTeam.SalesmanAreaId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Main", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Main)
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, V_SalesmanCSTeam.SalesmanLevelID)
            DbCommandWrapper.AddInParameter("@JobPositionId_Second", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Second)
            DbCommandWrapper.AddInParameter("@JobPositionId_Third", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Third)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, V_SalesmanCSTeam.LeaderId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Leader", DbType.Int32, V_SalesmanCSTeam.JobPositionId_Leader)
            DbCommandWrapper.AddInParameter("@RegisterStatus", DbType.AnsiStringFixedLength, V_SalesmanCSTeam.RegisterStatus)
            DbCommandWrapper.AddInParameter("@MarriedStatus", DbType.AnsiStringFixedLength, V_SalesmanCSTeam.MarriedStatus)
            DbCommandWrapper.AddInParameter("@ResignDate", DbType.DateTime, V_SalesmanCSTeam.ResignDate)
            DbCommandWrapper.AddInParameter("@ResignReason", DbType.AnsiString, V_SalesmanCSTeam.ResignReason)
            DbCommandWrapper.AddInParameter("@SalesIndicator", DbType.Byte, V_SalesmanCSTeam.SalesIndicator)
            DbCommandWrapper.AddInParameter("@SalesUnitIndicator", DbType.Byte, V_SalesmanCSTeam.SalesUnitIndicator)
            DbCommandWrapper.AddInParameter("@MechanicIndicator", DbType.Byte, V_SalesmanCSTeam.MechanicIndicator)
            DbCommandWrapper.AddInParameter("@SparePartIndicator", DbType.Byte, V_SalesmanCSTeam.SparePartIndicator)
            DbCommandWrapper.AddInParameter("@SPAdminIndicator", DbType.Byte, V_SalesmanCSTeam.SPAdminIndicator)
            DbCommandWrapper.AddInParameter("@SPWareHouseIndicator", DbType.Byte, V_SalesmanCSTeam.SPWareHouseIndicator)
            DbCommandWrapper.AddInParameter("@SPCounterIndicator", DbType.Byte, V_SalesmanCSTeam.SPCounterIndicator)
            DbCommandWrapper.AddInParameter("@SPSalesIndicator", DbType.Byte, V_SalesmanCSTeam.SPSalesIndicator)
            DbCommandWrapper.AddInParameter("@SPCSIndicator", DbType.Byte, V_SalesmanCSTeam.SPCSIndicator)
            DbCommandWrapper.AddInParameter("@IsRequestID", DbType.Byte, V_SalesmanCSTeam.IsRequestID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, V_SalesmanCSTeam.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_SalesmanCSTeam.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, V_SalesmanCSTeam.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, V_SalesmanCSTeam.DealerCode)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, V_SalesmanCSTeam.ProvinceName)
            DbCommandWrapper.AddInParameter("@DivisiID", DbType.Int32, V_SalesmanCSTeam.DivisiID)
            DbCommandWrapper.AddInParameter("@DivisiName", DbType.AnsiString, V_SalesmanCSTeam.DivisiName)
            DbCommandWrapper.AddInParameter("@PosisiID", DbType.Int32, V_SalesmanCSTeam.PosisiID)
            DbCommandWrapper.AddInParameter("@PosisiName", DbType.AnsiString, V_SalesmanCSTeam.PosisiName)
            DbCommandWrapper.AddInParameter("@LevelID", DbType.Int32, V_SalesmanCSTeam.LevelID)
            DbCommandWrapper.AddInParameter("@LevelName", DbType.AnsiString, V_SalesmanCSTeam.LevelName)
            DbCommandWrapper.AddInParameter("@Salary", DbType.Currency, V_SalesmanCSTeam.Salary)
            DbCommandWrapper.AddInParameter("@LeaderCode", DbType.AnsiString, V_SalesmanCSTeam.LeaderCode)
            DbCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, V_SalesmanCSTeam.LeaderName)
            DbCommandWrapper.AddInParameter("@AreaDesc", DbType.AnsiString, V_SalesmanCSTeam.AreaDesc)
            DbCommandWrapper.AddInParameter("@PENDIDIKAN", DbType.AnsiString, V_SalesmanCSTeam.PENDIDIKAN)
            DbCommandWrapper.AddInParameter("@EMAIL", DbType.AnsiString, V_SalesmanCSTeam.EMAIL)
            DbCommandWrapper.AddInParameter("@NO_HP", DbType.AnsiString, V_SalesmanCSTeam.NO_HP)
            DbCommandWrapper.AddInParameter("@NOKTP", DbType.AnsiString, V_SalesmanCSTeam.NOKTP)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SalesmanCSTeam

            Dim V_SalesmanCSTeam As V_SalesmanCSTeam = New V_SalesmanCSTeam

            V_SalesmanCSTeam.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then V_SalesmanCSTeam.DealerId = CType(dr("DealerId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then V_SalesmanCSTeam.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then V_SalesmanCSTeam.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then V_SalesmanCSTeam.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) Then V_SalesmanCSTeam.PlaceOfBirth = dr("PlaceOfBirth").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) Then V_SalesmanCSTeam.DateOfBirth = CType(dr("DateOfBirth"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then V_SalesmanCSTeam.Gender = CType(dr("Gender"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then V_SalesmanCSTeam.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then V_SalesmanCSTeam.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ShopSiteNumber")) Then V_SalesmanCSTeam.ShopSiteNumber = CType(dr("ShopSiteNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("HireDate")) Then V_SalesmanCSTeam.HireDate = CType(dr("HireDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanAreaId")) Then V_SalesmanCSTeam.SalesmanAreaId = CType(dr("SalesmanAreaId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Main")) Then V_SalesmanCSTeam.JobPositionId_Main = CType(dr("JobPositionId_Main"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevelID")) Then V_SalesmanCSTeam.SalesmanLevelID = CType(dr("SalesmanLevelID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Second")) Then V_SalesmanCSTeam.JobPositionId_Second = CType(dr("JobPositionId_Second"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Third")) Then V_SalesmanCSTeam.JobPositionId_Third = CType(dr("JobPositionId_Third"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderId")) Then V_SalesmanCSTeam.LeaderId = CType(dr("LeaderId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Leader")) Then V_SalesmanCSTeam.JobPositionId_Leader = CType(dr("JobPositionId_Leader"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegisterStatus")) Then V_SalesmanCSTeam.RegisterStatus = dr("RegisterStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MarriedStatus")) Then V_SalesmanCSTeam.MarriedStatus = dr("MarriedStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResignDate")) Then V_SalesmanCSTeam.ResignDate = CType(dr("ResignDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignReason")) Then V_SalesmanCSTeam.ResignReason = dr("ResignReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesIndicator")) Then V_SalesmanCSTeam.SalesIndicator = CType(dr("SalesIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitIndicator")) Then V_SalesmanCSTeam.SalesUnitIndicator = CType(dr("SalesUnitIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("MechanicIndicator")) Then V_SalesmanCSTeam.MechanicIndicator = CType(dr("MechanicIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartIndicator")) Then V_SalesmanCSTeam.SparePartIndicator = CType(dr("SparePartIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAdminIndicator")) Then V_SalesmanCSTeam.SPAdminIndicator = CType(dr("SPAdminIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPWareHouseIndicator")) Then V_SalesmanCSTeam.SPWareHouseIndicator = CType(dr("SPWareHouseIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPCounterIndicator")) Then V_SalesmanCSTeam.SPCounterIndicator = CType(dr("SPCounterIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPSalesIndicator")) Then V_SalesmanCSTeam.SPSalesIndicator = CType(dr("SPSalesIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPSalesIndicator")) Then V_SalesmanCSTeam.SPCSIndicator = CType(dr("SPSalesIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsRequestID")) Then V_SalesmanCSTeam.IsRequestID = CType(dr("IsRequestID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then V_SalesmanCSTeam.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then V_SalesmanCSTeam.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then V_SalesmanCSTeam.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then V_SalesmanCSTeam.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_SalesmanCSTeam.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_SalesmanCSTeam.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then V_SalesmanCSTeam.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then V_SalesmanCSTeam.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DivisiID")) Then V_SalesmanCSTeam.DivisiID = CType(dr("DivisiID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DivisiName")) Then V_SalesmanCSTeam.DivisiName = dr("DivisiName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PosisiID")) Then V_SalesmanCSTeam.PosisiID = CType(dr("PosisiID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PosisiName")) Then V_SalesmanCSTeam.PosisiName = dr("PosisiName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LevelID")) Then V_SalesmanCSTeam.LevelID = CType(dr("LevelID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LevelName")) Then V_SalesmanCSTeam.LevelName = dr("LevelName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Salary")) Then V_SalesmanCSTeam.Salary = CType(dr("Salary"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderCode")) Then V_SalesmanCSTeam.LeaderCode = dr("LeaderCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderName")) Then V_SalesmanCSTeam.LeaderName = dr("LeaderName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AreaDesc")) Then V_SalesmanCSTeam.AreaDesc = dr("AreaDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PENDIDIKAN")) Then V_SalesmanCSTeam.PENDIDIKAN = dr("PENDIDIKAN").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EMAIL")) Then V_SalesmanCSTeam.EMAIL = dr("EMAIL").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NO_HP")) Then V_SalesmanCSTeam.NO_HP = dr("NO_HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NOKTP")) Then V_SalesmanCSTeam.NOKTP = dr("NOKTP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("USER_DNET")) Then V_SalesmanCSTeam.User_DNET = dr("USER_DNET").ToString
            Return V_SalesmanCSTeam

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SalesmanCSTeam) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SalesmanCSTeam), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SalesmanCSTeam).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

