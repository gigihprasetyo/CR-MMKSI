#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/4/2007 - 2:50:45 PM
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

    Public Class SalesmanHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanHeader"
        Private m_UpdateStatement As String = "up_UpdateSalesmanHeader"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanHeader As SalesmanHeader = Nothing
            While dr.Read

                salesmanHeader = Me.CreateObject(dr)

            End While

            Return salesmanHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanHeader As SalesmanHeader = Me.CreateObject(dr)
                salesmanHeaderList.Add(salesmanHeader)
            End While

            Return salesmanHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanHeader As SalesmanHeader = CType(obj, SalesmanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanHeader As SalesmanHeader = CType(obj, SalesmanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, salesmanHeader.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, salesmanHeader.Name)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, salesmanHeader.Image)
            DbCommandWrapper.AddInParameter("@PlaceOfBirth", DbType.AnsiString, salesmanHeader.PlaceOfBirth)
            DbCommandWrapper.AddInParameter("@DateOfBirth", DbType.DateTime, salesmanHeader.DateOfBirth)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, salesmanHeader.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, salesmanHeader.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, salesmanHeader.City)
            DbCommandWrapper.AddInParameter("@ShopSiteNumber", DbType.Int32, salesmanHeader.ShopSiteNumber)
            DbCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, salesmanHeader.HireDate)
            DbCommandWrapper.AddInParameter("@JobPositionId_Second", DbType.Int32, salesmanHeader.JobPositionId_Second)
            DbCommandWrapper.AddInParameter("@JobPositionId_Third", DbType.Int32, salesmanHeader.JobPositionId_Third)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, salesmanHeader.LeaderId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Leader", DbType.Int32, salesmanHeader.JobPositionId_Leader)
            DbCommandWrapper.AddInParameter("@RegisterStatus", DbType.AnsiStringFixedLength, salesmanHeader.RegisterStatus)
            DbCommandWrapper.AddInParameter("@MarriedStatus", DbType.AnsiStringFixedLength, salesmanHeader.MarriedStatus)
            DbCommandWrapper.AddInParameter("@ResignType", DbType.Int16, salesmanHeader.ResignType)
            DbCommandWrapper.AddInParameter("@ResignDate", DbType.DateTime, salesmanHeader.ResignDate)
            DbCommandWrapper.AddInParameter("@ResignReason", DbType.AnsiString, salesmanHeader.ResignReason)
            DbCommandWrapper.AddInParameter("@SalesIndicator", DbType.Byte, salesmanHeader.SalesIndicator)
            DbCommandWrapper.AddInParameter("@SalesUnitIndicator", DbType.Byte, salesmanHeader.SalesUnitIndicator)
            DbCommandWrapper.AddInParameter("@MechanicIndicator", DbType.Byte, salesmanHeader.MechanicIndicator)
            DbCommandWrapper.AddInParameter("@SparePartIndicator", DbType.Byte, salesmanHeader.SparePartIndicator)
            DbCommandWrapper.AddInParameter("@SPAdminIndicator", DbType.Byte, salesmanHeader.SPAdminIndicator)
            DbCommandWrapper.AddInParameter("@SPWareHouseIndicator", DbType.Byte, salesmanHeader.SPWareHouseIndicator)
            DbCommandWrapper.AddInParameter("@SPCounterIndicator", DbType.Byte, salesmanHeader.SPCounterIndicator)
            DbCommandWrapper.AddInParameter("@SPSalesIndicator", DbType.Byte, salesmanHeader.SPSalesIndicator)
            DbCommandWrapper.AddInParameter("@IsRequestID", DbType.Byte, salesmanHeader.IsRequestID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, salesmanHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@IsOtherCity", DbType.Int32, salesmanHeader.IsOtherCity)

            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, Me.GetRefObject(salesmanHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchId", DbType.Int32, Me.GetRefObject(salesmanHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@SalesmanAreaId", DbType.Int32, Me.GetRefObject(salesmanHeader.SalesmanArea))
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, Me.GetRefObject(salesmanHeader.SalesmanLevel))
            DbCommandWrapper.AddInParameter("@JobPositionId_Main", DbType.Int32, Me.GetRefObject(salesmanHeader.JobPosition))

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

            Dim salesmanHeader As SalesmanHeader = CType(obj, SalesmanHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanHeader.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, salesmanHeader.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, salesmanHeader.Name)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, salesmanHeader.Image)
            DbCommandWrapper.AddInParameter("@PlaceOfBirth", DbType.AnsiString, salesmanHeader.PlaceOfBirth)
            DbCommandWrapper.AddInParameter("@DateOfBirth", DbType.DateTime, salesmanHeader.DateOfBirth)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, salesmanHeader.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, salesmanHeader.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, salesmanHeader.City)
            DbCommandWrapper.AddInParameter("@ShopSiteNumber", DbType.Int32, salesmanHeader.ShopSiteNumber)
            DbCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, salesmanHeader.HireDate)
            DbCommandWrapper.AddInParameter("@JobPositionId_Second", DbType.Int32, salesmanHeader.JobPositionId_Second)
            DbCommandWrapper.AddInParameter("@JobPositionId_Third", DbType.Int32, salesmanHeader.JobPositionId_Third)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, salesmanHeader.LeaderId)
            DbCommandWrapper.AddInParameter("@JobPositionId_Leader", DbType.Int32, salesmanHeader.JobPositionId_Leader)
            DbCommandWrapper.AddInParameter("@RegisterStatus", DbType.AnsiStringFixedLength, salesmanHeader.RegisterStatus)
            DbCommandWrapper.AddInParameter("@MarriedStatus", DbType.AnsiStringFixedLength, salesmanHeader.MarriedStatus)
            DbCommandWrapper.AddInParameter("@ResignType", DbType.Int16, salesmanHeader.ResignType)
            DbCommandWrapper.AddInParameter("@ResignDate", DbType.DateTime, salesmanHeader.ResignDate)
            DbCommandWrapper.AddInParameter("@ResignReasonType", DbType.Int16, salesmanHeader.ResignReasonType)
            DbCommandWrapper.AddInParameter("@ResignReason", DbType.AnsiString, salesmanHeader.ResignReason)
            DbCommandWrapper.AddInParameter("@SalesIndicator", DbType.Byte, salesmanHeader.SalesIndicator)
            DbCommandWrapper.AddInParameter("@SalesUnitIndicator", DbType.Byte, salesmanHeader.SalesUnitIndicator)
            DbCommandWrapper.AddInParameter("@MechanicIndicator", DbType.Byte, salesmanHeader.MechanicIndicator)
            DbCommandWrapper.AddInParameter("@SparePartIndicator", DbType.Byte, salesmanHeader.SparePartIndicator)
            DbCommandWrapper.AddInParameter("@SPAdminIndicator", DbType.Byte, salesmanHeader.SPAdminIndicator)
            DbCommandWrapper.AddInParameter("@SPWareHouseIndicator", DbType.Byte, salesmanHeader.SPWareHouseIndicator)
            DbCommandWrapper.AddInParameter("@SPCounterIndicator", DbType.Byte, salesmanHeader.SPCounterIndicator)
            DbCommandWrapper.AddInParameter("@SPSalesIndicator", DbType.Byte, salesmanHeader.SPSalesIndicator)
            DbCommandWrapper.AddInParameter("@IsRequestID", DbType.Byte, salesmanHeader.IsRequestID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, salesmanHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@IsOtherCity", DbType.Int32, salesmanHeader.IsOtherCity)

            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, Me.GetRefObject(salesmanHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchId", DbType.Int32, Me.GetRefObject(salesmanHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@SalesmanAreaId", DbType.Int32, Me.GetRefObject(salesmanHeader.SalesmanArea))
            DbCommandWrapper.AddInParameter("@SalesmanLevelID", DbType.Int32, Me.GetRefObject(salesmanHeader.SalesmanLevel))
            DbCommandWrapper.AddInParameter("@JobPositionId_Main", DbType.Int32, Me.GetRefObject(salesmanHeader.JobPosition))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanHeader

            Dim salesmanHeader As SalesmanHeader = New SalesmanHeader

            salesmanHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then salesmanHeader.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then salesmanHeader.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then salesmanHeader.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) Then salesmanHeader.PlaceOfBirth = dr("PlaceOfBirth").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) Then salesmanHeader.DateOfBirth = CType(dr("DateOfBirth"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then salesmanHeader.Gender = CType(dr("Gender"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then salesmanHeader.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then salesmanHeader.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ShopSiteNumber")) Then salesmanHeader.ShopSiteNumber = CType(dr("ShopSiteNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("HireDate")) Then salesmanHeader.HireDate = CType(dr("HireDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Second")) Then salesmanHeader.JobPositionId_Second = CType(dr("JobPositionId_Second"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Third")) Then salesmanHeader.JobPositionId_Third = CType(dr("JobPositionId_Third"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderId")) Then salesmanHeader.LeaderId = CType(dr("LeaderId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Leader")) Then salesmanHeader.JobPositionId_Leader = CType(dr("JobPositionId_Leader"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegisterStatus")) Then salesmanHeader.RegisterStatus = dr("RegisterStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MarriedStatus")) Then salesmanHeader.MarriedStatus = dr("MarriedStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResignType")) Then salesmanHeader.ResignType = CType(dr("ResignType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignDate")) Then salesmanHeader.ResignDate = CType(dr("ResignDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignReasonType")) Then salesmanHeader.ResignReasonType = CType(dr("ResignReasonType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignReason")) Then salesmanHeader.ResignReason = dr("ResignReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesIndicator")) Then salesmanHeader.SalesIndicator = CType(dr("SalesIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitIndicator")) Then salesmanHeader.SalesUnitIndicator = CType(dr("SalesUnitIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("MechanicIndicator")) Then salesmanHeader.MechanicIndicator = CType(dr("MechanicIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartIndicator")) Then salesmanHeader.SparePartIndicator = CType(dr("SparePartIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAdminIndicator")) Then salesmanHeader.SPAdminIndicator = CType(dr("SPAdminIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPWareHouseIndicator")) Then salesmanHeader.SPWareHouseIndicator = CType(dr("SPWareHouseIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPCounterIndicator")) Then salesmanHeader.SPCounterIndicator = CType(dr("SPCounterIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPSalesIndicator")) Then salesmanHeader.SPSalesIndicator = CType(dr("SPSalesIndicator"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsRequestID")) Then salesmanHeader.IsRequestID = CType(dr("IsRequestID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then salesmanHeader.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsOtherCity")) Then salesmanHeader.IsOtherCity = CType(dr("IsOtherCity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then
                salesmanHeader.Dealer = New Dealer(CType(dr("DealerId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchId")) Then
                salesmanHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanAreaId")) Then
                salesmanHeader.SalesmanArea = New SalesmanArea(CType(dr("SalesmanAreaId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevelID")) Then
                salesmanHeader.SalesmanLevel = New SalesmanLevel(CType(dr("SalesmanLevelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Main")) Then
                salesmanHeader.JobPosition = New JobPosition(CType(dr("JobPositionId_Main"), Integer))
            End If

            Return salesmanHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

