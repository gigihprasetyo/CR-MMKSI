
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanDownload Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/20/2009 - 4:02:09 PM
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

    Public Class V_SalesmanDownloadMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SalesmanDownload"
        Private m_UpdateStatement As String = "up_UpdateV_SalesmanDownload"
        Private m_RetrieveStatement As String = "up_RetrieveV_SalesmanDownload"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SalesmanDownloadList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SalesmanDownload"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SalesmanDownload As V_SalesmanDownload = Nothing


            Dim isColumnAll As Boolean = False
            For idx As Integer = 1 To dr.FieldCount
                If dr.GetName(idx) = "LastGrade" Then
                    isColumnAll = True
                    Exit For
                End If
            Next

            While dr.Read

                v_SalesmanDownload = Me.CreateObject(dr, isColumnAll)

            End While

            Return v_SalesmanDownload

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList
            Dim v_SalesmanDownloadList As ArrayList = New ArrayList

            Dim isColumnAll As Boolean = False
            For idx As Integer = 1 To dr.FieldCount
                If dr.GetName(idx) = "LastGrade" Then
                    isColumnAll = True
                    Exit For
                End If
            Next

            While dr.Read
                Dim v_SalesmanDownload As V_SalesmanDownload = Me.CreateObject(dr, isColumnAll)
                v_SalesmanDownloadList.Add(v_SalesmanDownload)
            End While

            Return v_SalesmanDownloadList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SalesmanDownload As V_SalesmanDownload = CType(obj, V_SalesmanDownload)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SalesmanDownload.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SalesmanDownload As V_SalesmanDownload = CType(obj, V_SalesmanDownload)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, v_SalesmanDownload.DealerId)
            DBCommandWrapper.AddInParameter("@DealerBranchName", DbType.AnsiString, v_SalesmanDownload.DealerBranchName)
            DBCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SalesmanDownload.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, v_SalesmanDownload.Name)
            'DbCommandWrapper.AddInParameter("@Image", DbType.Binary, v_SalesmanDownload.Image)
            DbCommandWrapper.AddInParameter("@PlaceOfBirth", DbType.AnsiString, v_SalesmanDownload.PlaceOfBirth)
            DbCommandWrapper.AddInParameter("@DateOfBirth", DbType.DateTime, v_SalesmanDownload.DateOfBirth)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, v_SalesmanDownload.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, v_SalesmanDownload.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, v_SalesmanDownload.City)
            'DbCommandWrapper.AddInParameter("@ShopSiteNumber", DbType.Int32, v_SalesmanDownload.ShopSiteNumber)
            DbCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, v_SalesmanDownload.HireDate)
            'DbCommandWrapper.AddInParameter("@SalesmanAreaId", DbType.Int32, v_SalesmanDownload.SalesmanAreaId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Main", DbType.Int32, v_SalesmanDownload.JobPositionId_Main)
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, v_SalesmanDownload.SalesmanLevelID)
            'DbCommandWrapper.AddInParameter("@JobPositionId_Second", DbType.Int32, v_SalesmanDownload.JobPositionId_Second)
            'DbCommandWrapper.AddInParameter("@JobPositionId_Third", DbType.Int32, v_SalesmanDownload.JobPositionId_Third)
            'DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, v_SalesmanDownload.LeaderId)
            'DbCommandWrapper.AddInParameter("@JobPositionId_Leader", DbType.Int32, v_SalesmanDownload.JobPositionId_Leader)
            'DbCommandWrapper.AddInParameter("@RegisterStatus", DbType.AnsiStringFixedLength, v_SalesmanDownload.RegisterStatus)
            DbCommandWrapper.AddInParameter("@MarriedStatus", DbType.AnsiStringFixedLength, v_SalesmanDownload.MarriedStatus)
            DbCommandWrapper.AddInParameter("@ResignDate", DbType.DateTime, v_SalesmanDownload.ResignDate)
            'DbCommandWrapper.AddInParameter("@ResignReason", DbType.AnsiString, v_SalesmanDownload.ResignReason)
            DbCommandWrapper.AddInParameter("@SalesIndicator", DbType.Byte, v_SalesmanDownload.SalesIndicator)
            'DbCommandWrapper.AddInParameter("@SalesUnitIndicator", DbType.Byte, v_SalesmanDownload.SalesUnitIndicator)
            'DbCommandWrapper.AddInParameter("@MechanicIndicator", DbType.Byte, v_SalesmanDownload.MechanicIndicator)
            'DbCommandWrapper.AddInParameter("@SparePartIndicator", DbType.Byte, v_SalesmanDownload.SparePartIndicator)
            'DbCommandWrapper.AddInParameter("@SPAdminIndicator", DbType.Byte, v_SalesmanDownload.SPAdminIndicator)
            'DbCommandWrapper.AddInParameter("@SPWareHouseIndicator", DbType.Byte, v_SalesmanDownload.SPWareHouseIndicator)
            'DbCommandWrapper.AddInParameter("@SPCounterIndicator", DbType.Byte, v_SalesmanDownload.SPCounterIndicator)
            'DbCommandWrapper.AddInParameter("@SPSalesIndicator", DbType.Byte, v_SalesmanDownload.SPSalesIndicator)
            'DbCommandWrapper.AddInParameter("@IsRequestID", DbType.Byte, v_SalesmanDownload.IsRequestID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, v_SalesmanDownload.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SalesmanDownload.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_SalesmanDownload.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SalesmanDownload.DealerCode)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, v_SalesmanDownload.ProvinceName)
            DbCommandWrapper.AddInParameter("@JobDescription", DbType.AnsiString, v_SalesmanDownload.JobDescription)
            DbCommandWrapper.AddInParameter("@LevelDescription", DbType.AnsiString, v_SalesmanDownload.LevelDescription)
            DbCommandWrapper.AddInParameter("@LeaderCode", DbType.AnsiString, v_SalesmanDownload.LeaderCode)
            DBCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, v_SalesmanDownload.LeaderName)
            DBCommandWrapper.AddInParameter("@AreaDesc", DbType.AnsiString, v_SalesmanDownload.AreaDesc)
            DBCommandWrapper.AddInParameter("@PENDIDIKAN", DbType.AnsiString, v_SalesmanDownload.PENDIDIKAN)
            DBCommandWrapper.AddInParameter("@EMAIL", DbType.AnsiString, v_SalesmanDownload.EMAIL)
            DBCommandWrapper.AddInParameter("@NO_HP", DbType.AnsiString, v_SalesmanDownload.NO_HP)
            DBCommandWrapper.AddInParameter("@NOKTP", DbType.AnsiString, v_SalesmanDownload.NOKTP)
            DbCommandWrapper.AddInParameter("@KATEGORI_TIM", DbType.AnsiString, v_SalesmanDownload.KATEGORI_TIM)

            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SalesmanDownload.DealerName)
            DbCommandWrapper.AddInParameter("@DealerAddress", DbType.AnsiString, v_SalesmanDownload.DealerAddress)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SalesmanDownload.CityName)
            DbCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, v_SalesmanDownload.GroupName)
            DbCommandWrapper.AddInParameter("@Area1Description", DbType.AnsiString, v_SalesmanDownload.Area1Description)
            DbCommandWrapper.AddInParameter("@GenderDescription", DbType.AnsiString, v_SalesmanDownload.GenderDescription)
            DbCommandWrapper.AddInParameter("@MarriedStatusDesc", DbType.AnsiString, v_SalesmanDownload.MarriedStatusDesc)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SalesmanDownload As v_SalesmanDownload = CType(obj, v_SalesmanDownload)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SalesmanDownload.ID)
            DBCommandWrapper.AddInParameter("@DealerId", DbType.Int32, v_SalesmanDownload.DealerId)
            DBCommandWrapper.AddInParameter("@DealerBranchName", DbType.AnsiString, v_SalesmanDownload.DealerBranchName)
            DBCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SalesmanDownload.SalesmanCode)
            DBCommandWrapper.AddInParameter("@Name", DbType.AnsiString, v_SalesmanDownload.Name)
            'DBCommandWrapper.AddInParameter("@Image", DbType.Binary, v_SalesmanDownload.Image)
            DBCommandWrapper.AddInParameter("@PlaceOfBirth", DbType.AnsiString, v_SalesmanDownload.PlaceOfBirth)
            DBCommandWrapper.AddInParameter("@DateOfBirth", DbType.DateTime, v_SalesmanDownload.DateOfBirth)
            DBCommandWrapper.AddInParameter("@Gender", DbType.Byte, v_SalesmanDownload.Gender)
            DBCommandWrapper.AddInParameter("@Address", DbType.AnsiString, v_SalesmanDownload.Address)
            DBCommandWrapper.AddInParameter("@City", DbType.AnsiString, v_SalesmanDownload.City)
            'DBCommandWrapper.AddInParameter("@ShopSiteNumber", DbType.Int32, v_SalesmanDownload.ShopSiteNumber)
            DBCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, v_SalesmanDownload.HireDate)
            'DBCommandWrapper.AddInParameter("@SalesmanAreaId", DbType.Int32, v_SalesmanDownload.SalesmanAreaId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Main", DbType.Int32, v_SalesmanDownload.JobPositionId_Main)
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, v_SalesmanDownload.SalesmanLevelID)
            'DBCommandWrapper.AddInParameter("@JobPositionId_Second", DbType.Int32, v_SalesmanDownload.JobPositionId_Second)
            'DBCommandWrapper.AddInParameter("@JobPositionId_Third", DbType.Int32, v_SalesmanDownload.JobPositionId_Third)
            'DBCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, v_SalesmanDownload.LeaderId)
            'DBCommandWrapper.AddInParameter("@JobPositionId_Leader", DbType.Int32, v_SalesmanDownload.JobPositionId_Leader)
            'DBCommandWrapper.AddInParameter("@RegisterStatus", DbType.AnsiStringFixedLength, v_SalesmanDownload.RegisterStatus)
            DBCommandWrapper.AddInParameter("@MarriedStatus", DbType.AnsiStringFixedLength, v_SalesmanDownload.MarriedStatus)
            DbCommandWrapper.AddInParameter("@ResignDate", DbType.DateTime, v_SalesmanDownload.ResignDate)
            'DBCommandWrapper.AddInParameter("@ResignReason", DbType.AnsiString, v_SalesmanDownload.ResignReason)
            DbCommandWrapper.AddInParameter("@SalesIndicator", DbType.Byte, v_SalesmanDownload.SalesIndicator)
            'DBCommandWrapper.AddInParameter("@SalesUnitIndicator", DbType.Byte, v_SalesmanDownload.SalesUnitIndicator)
            'DBCommandWrapper.AddInParameter("@MechanicIndicator", DbType.Byte, v_SalesmanDownload.MechanicIndicator)
            'DBCommandWrapper.AddInParameter("@SparePartIndicator", DbType.Byte, v_SalesmanDownload.SparePartIndicator)
            'DBCommandWrapper.AddInParameter("@SPAdminIndicator", DbType.Byte, v_SalesmanDownload.SPAdminIndicator)
            'DBCommandWrapper.AddInParameter("@SPWareHouseIndicator", DbType.Byte, v_SalesmanDownload.SPWareHouseIndicator)
            'DBCommandWrapper.AddInParameter("@SPCounterIndicator", DbType.Byte, v_SalesmanDownload.SPCounterIndicator)
            'DBCommandWrapper.AddInParameter("@SPSalesIndicator", DbType.Byte, v_SalesmanDownload.SPSalesIndicator)
            'DBCommandWrapper.AddInParameter("@IsRequestID", DbType.Byte, v_SalesmanDownload.IsRequestID)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, v_SalesmanDownload.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SalesmanDownload.RowStatus)
            'DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_SalesmanDownload.CreatedBy)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SalesmanDownload.DealerCode)
            DBCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, v_SalesmanDownload.ProvinceName)
            DBCommandWrapper.AddInParameter("@JobDescription", DbType.AnsiString, v_SalesmanDownload.JobDescription)
            DBCommandWrapper.AddInParameter("@LevelDescription", DbType.AnsiString, v_SalesmanDownload.LevelDescription)
            DBCommandWrapper.AddInParameter("@LeaderCode", DbType.AnsiString, v_SalesmanDownload.LeaderCode)
            DBCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, v_SalesmanDownload.LeaderName)
            DBCommandWrapper.AddInParameter("@AreaDesc", DbType.AnsiString, v_SalesmanDownload.AreaDesc)
            DBCommandWrapper.AddInParameter("@PENDIDIKAN", DbType.AnsiString, v_SalesmanDownload.PENDIDIKAN)
            DBCommandWrapper.AddInParameter("@EMAIL", DbType.AnsiString, v_SalesmanDownload.EMAIL)
            DBCommandWrapper.AddInParameter("@NO_HP", DbType.AnsiString, v_SalesmanDownload.NO_HP)
            DBCommandWrapper.AddInParameter("@NOKTP", DbType.AnsiString, v_SalesmanDownload.NOKTP)
            DBCommandWrapper.AddInParameter("@KATEGORI_TIM", DbType.AnsiString, v_SalesmanDownload.KATEGORI_TIM)

            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SalesmanDownload.DealerName)
            DbCommandWrapper.AddInParameter("@DealerAddress", DbType.AnsiString, v_SalesmanDownload.DealerAddress)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SalesmanDownload.CityName)
            DbCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, v_SalesmanDownload.GroupName)
            DbCommandWrapper.AddInParameter("@Area1Description", DbType.AnsiString, v_SalesmanDownload.Area1Description)
            DbCommandWrapper.AddInParameter("@GenderDescription", DbType.AnsiString, v_SalesmanDownload.GenderDescription)
            DbCommandWrapper.AddInParameter("@MarriedStatusDesc", DbType.AnsiString, v_SalesmanDownload.MarriedStatusDesc)

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader, ByVal isColumnAll As Boolean) As V_SalesmanDownload

            Dim v_SalesmanDownload As V_SalesmanDownload = New V_SalesmanDownload

            v_SalesmanDownload.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then v_SalesmanDownload.DealerId = CType(dr("DealerId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then v_SalesmanDownload.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchName")) Then v_SalesmanDownload.DealerBranchName = dr("DealerBranchName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Term1")) Then v_SalesmanDownload.Term1 = dr("Term1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then v_SalesmanDownload.Name = dr("Name").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then v_SalesmanDownload.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) Then v_SalesmanDownload.PlaceOfBirth = dr("PlaceOfBirth").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) Then v_SalesmanDownload.DateOfBirth = CType(dr("DateOfBirth"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then v_SalesmanDownload.Gender = CType(dr("Gender"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then v_SalesmanDownload.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then v_SalesmanDownload.City = dr("City").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ShopSiteNumber")) Then v_SalesmanDownload.ShopSiteNumber = CType(dr("ShopSiteNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("HireDate")) Then v_SalesmanDownload.HireDate = CType(dr("HireDate"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesmanAreaId")) Then v_SalesmanDownload.SalesmanAreaId = CType(dr("SalesmanAreaId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Main")) Then v_SalesmanDownload.JobPositionId_Main = CType(dr("JobPositionId_Main"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevelID")) Then v_SalesmanDownload.SalesmanLevelID = CType(dr("SalesmanLevelID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Second")) Then v_SalesmanDownload.JobPositionId_Second = CType(dr("JobPositionId_Second"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Third")) Then v_SalesmanDownload.JobPositionId_Third = CType(dr("JobPositionId_Third"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("LeaderId")) Then v_SalesmanDownload.LeaderId = CType(dr("LeaderId"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Leader")) Then v_SalesmanDownload.JobPositionId_Leader = CType(dr("JobPositionId_Leader"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("RegisterStatus")) Then v_SalesmanDownload.RegisterStatus = dr("RegisterStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MarriedStatus")) Then v_SalesmanDownload.MarriedStatus = dr("MarriedStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResignDate")) Then v_SalesmanDownload.ResignDate = CType(dr("ResignDate"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("ResignReason")) Then v_SalesmanDownload.ResignReason = dr("ResignReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesIndicator")) Then v_SalesmanDownload.SalesIndicator = CType(dr("SalesIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitIndicator")) Then v_SalesmanDownload.SalesUnitIndicator = CType(dr("SalesUnitIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("MechanicIndicator")) Then v_SalesmanDownload.MechanicIndicator = CType(dr("MechanicIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("SparePartIndicator")) Then v_SalesmanDownload.SparePartIndicator = CType(dr("SparePartIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPAdminIndicator")) Then v_SalesmanDownload.SPAdminIndicator = CType(dr("SPAdminIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPWareHouseIndicator")) Then v_SalesmanDownload.SPWareHouseIndicator = CType(dr("SPWareHouseIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPCounterIndicator")) Then v_SalesmanDownload.SPCounterIndicator = CType(dr("SPCounterIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPSalesIndicator")) Then v_SalesmanDownload.SPSalesIndicator = CType(dr("SPSalesIndicator"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("IsRequestID")) Then v_SalesmanDownload.IsRequestID = CType(dr("IsRequestID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_SalesmanDownload.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_SalesmanDownload.RowStatus = CType(dr("RowStatus"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_SalesmanDownload.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_SalesmanDownload.CreatedTime = CType(dr("CreatedTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_SalesmanDownload.LastUpdateBy = dr("LastUpdateBy").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_SalesmanDownload.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SalesmanDownload.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then v_SalesmanDownload.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobDescription")) Then v_SalesmanDownload.JobDescription = dr("JobDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LevelDescription")) Then v_SalesmanDownload.LevelDescription = dr("LevelDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderCode")) Then v_SalesmanDownload.LeaderCode = dr("LeaderCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderName")) Then v_SalesmanDownload.LeaderName = dr("LeaderName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AreaDesc")) Then v_SalesmanDownload.AreaDesc = dr("AreaDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PENDIDIKAN")) Then v_SalesmanDownload.PENDIDIKAN = dr("PENDIDIKAN").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EMAIL")) Then v_SalesmanDownload.EMAIL = dr("EMAIL").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NO_HP")) Then v_SalesmanDownload.NO_HP = dr("NO_HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NOKTP")) Then v_SalesmanDownload.NOKTP = dr("NOKTP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KATEGORI_TIM")) Then v_SalesmanDownload.KATEGORI_TIM = dr("KATEGORI_TIM").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SalesmanDownload.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerAddress")) Then v_SalesmanDownload.DealerAddress = dr("DealerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then v_SalesmanDownload.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupName")) Then v_SalesmanDownload.GroupName = dr("GroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Area1Description")) Then v_SalesmanDownload.Area1Description = dr("Area1Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GenderDescription")) Then v_SalesmanDownload.GenderDescription = dr("GenderDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MarriedStatusDesc")) Then v_SalesmanDownload.MarriedStatusDesc = dr("MarriedStatusDesc").ToString

            If isColumnAll Then
                If Not dr.IsDBNull(dr.GetOrdinal("LastGrade")) Then v_SalesmanDownload.LastGrade = dr("LastGrade").ToString
                If Not dr.IsDBNull(dr.GetOrdinal("LastScore")) Then v_SalesmanDownload.LastScore = dr("LastScore").ToString
                If Not dr.IsDBNull(dr.GetOrdinal("YearLastGrade")) Then v_SalesmanDownload.YearLastGrade = dr("YearLastGrade").ToString
                If Not dr.IsDBNull(dr.GetOrdinal("YearLastScore")) Then v_SalesmanDownload.YearLastScore = dr("YearLastScore").ToString

            End If



            Return v_SalesmanDownload

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SalesmanDownload) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SalesmanDownload), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SalesmanDownload).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

