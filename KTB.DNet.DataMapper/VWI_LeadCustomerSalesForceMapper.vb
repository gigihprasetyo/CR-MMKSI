
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_LeadCustomerSalesForce Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/05/2018 - 9:11:46
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

    Public Class VWI_LeadCustomerSalesForceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_LeadCustomerSalesForce"
        Private m_UpdateStatement As String = "up_UpdateVWI_LeadCustomerSalesForce"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_LeadCustomerSalesForce"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_LeadCustomerSalesForceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_LeadCustomerSalesForce"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_LeadCustomerSalesForce As VWI_LeadCustomerSalesForce = Nothing
            While dr.Read

                vWI_LeadCustomerSalesForce = Me.CreateObject(dr)

            End While

            Return vWI_LeadCustomerSalesForce

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_LeadCustomerSalesForceList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_LeadCustomerSalesForce As VWI_LeadCustomerSalesForce = Me.CreateObject(dr)
                vWI_LeadCustomerSalesForceList.Add(vWI_LeadCustomerSalesForce)
            End While

            Return vWI_LeadCustomerSalesForceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_LeadCustomerSalesForce As VWI_LeadCustomerSalesForce = CType(obj, VWI_LeadCustomerSalesForce)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, vWI_LeadCustomerSalesForce.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_LeadCustomerSalesForce As VWI_LeadCustomerSalesForce = CType(obj, VWI_LeadCustomerSalesForce)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@DNetID", DbType.Int32, vWI_LeadCustomerSalesForce.DNetID)
            DbCommandWrapper.AddInParameter("@SumberData", DbType.AnsiString, vWI_LeadCustomerSalesForce.SumberData)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vWI_LeadCustomerSalesForce.CreatedBy)
            DbCommandWrapper.AddInParameter("@CreateDate", DbType.AnsiString, vWI_LeadCustomerSalesForce.CreateDate)
            DbCommandWrapper.AddInParameter("@CreateDate_YYYYMMDD", DbType.AnsiString, vWI_LeadCustomerSalesForce.CreateDate_YYYYMMDD)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, vWI_LeadCustomerSalesForce.DealerName)
            DbCommandWrapper.AddInParameter("@CustomerTypeID", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerTypeID)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerType)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_LeadCustomerSalesForce.Name)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerAddress)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, vWI_LeadCustomerSalesForce.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, vWI_LeadCustomerSalesForce.Email)
            DbCommandWrapper.AddInParameter("@SexID", DbType.AnsiString, vWI_LeadCustomerSalesForce.SexID)
            DbCommandWrapper.AddInParameter("@Sex", DbType.AnsiString, vWI_LeadCustomerSalesForce.Sex)
            DbCommandWrapper.AddInParameter("@AgeSegmentID", DbType.AnsiString, vWI_LeadCustomerSalesForce.AgeSegmentID)
            DbCommandWrapper.AddInParameter("@AgeSegment", DbType.AnsiString, vWI_LeadCustomerSalesForce.AgeSegment)
            DbCommandWrapper.AddInParameter("@CustomerStatusID", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerStatusID)
            DbCommandWrapper.AddInParameter("@CustomerStatus", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerStatus)
            DbCommandWrapper.AddInParameter("@InformationTypeID", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationTypeID)
            DbCommandWrapper.AddInParameter("@InformationType", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationType)
            DbCommandWrapper.AddInParameter("@InformationSourceID", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationSourceID)
            DbCommandWrapper.AddInParameter("@InformationSource", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationSource)
            DbCommandWrapper.AddInParameter("@CustomerPurposeID", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerPurposeID)
            DbCommandWrapper.AddInParameter("@CustomerPurpose", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerPurpose)
            DbCommandWrapper.AddInParameter("@ProspectDate", DbType.AnsiString, vWI_LeadCustomerSalesForce.ProspectDate)
            DbCommandWrapper.AddInParameter("@ProspectDate_YYYYMMDD", DbType.AnsiString, vWI_LeadCustomerSalesForce.ProspectDate_YYYYMMDD)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vWI_LeadCustomerSalesForce.Description)
            DbCommandWrapper.AddInParameter("@CurrVehicleType", DbType.AnsiString, vWI_LeadCustomerSalesForce.CurrVehicleType)
            DbCommandWrapper.AddInParameter("@CurrVehicleBrand", DbType.AnsiString, vWI_LeadCustomerSalesForce.CurrVehicleBrand)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, vWI_LeadCustomerSalesForce.Note)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, vWI_LeadCustomerSalesForce.Keterangan)
            DbCommandWrapper.AddInParameter("@StatusResponseID", DbType.AnsiString, vWI_LeadCustomerSalesForce.StatusResponseID)
            DbCommandWrapper.AddInParameter("@StatusResponse", DbType.AnsiString, vWI_LeadCustomerSalesForce.StatusResponse)
            DbCommandWrapper.AddInParameter("@WebID", DbType.AnsiString, vWI_LeadCustomerSalesForce.WebID)


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

            Dim vWI_LeadCustomerSalesForce As VWI_LeadCustomerSalesForce = CType(obj, VWI_LeadCustomerSalesForce)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, vWI_LeadCustomerSalesForce.ID)
            DbCommandWrapper.AddInParameter("@DNetID", DbType.Int32, vWI_LeadCustomerSalesForce.DNetID)
            DbCommandWrapper.AddInParameter("@SumberData", DbType.AnsiString, vWI_LeadCustomerSalesForce.SumberData)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vWI_LeadCustomerSalesForce.CreatedBy)
            DbCommandWrapper.AddInParameter("@CreateDate", DbType.AnsiString, vWI_LeadCustomerSalesForce.CreateDate)
            DbCommandWrapper.AddInParameter("@CreateDate_YYYYMMDD", DbType.AnsiString, vWI_LeadCustomerSalesForce.CreateDate_YYYYMMDD)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, vWI_LeadCustomerSalesForce.DealerName)
            DbCommandWrapper.AddInParameter("@CustomerTypeID", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerTypeID)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerType)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.SalesmanCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_LeadCustomerSalesForce.Name)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerAddress)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, vWI_LeadCustomerSalesForce.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, vWI_LeadCustomerSalesForce.Email)
            DbCommandWrapper.AddInParameter("@SexID", DbType.AnsiString, vWI_LeadCustomerSalesForce.SexID)
            DbCommandWrapper.AddInParameter("@Sex", DbType.AnsiString, vWI_LeadCustomerSalesForce.Sex)
            DbCommandWrapper.AddInParameter("@AgeSegmentID", DbType.AnsiString, vWI_LeadCustomerSalesForce.AgeSegmentID)
            DbCommandWrapper.AddInParameter("@AgeSegment", DbType.AnsiString, vWI_LeadCustomerSalesForce.AgeSegment)
            DbCommandWrapper.AddInParameter("@CustomerStatusID", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerStatusID)
            DbCommandWrapper.AddInParameter("@CustomerStatus", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerStatus)
            DbCommandWrapper.AddInParameter("@InformationTypeID", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationTypeID)
            DbCommandWrapper.AddInParameter("@InformationType", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationType)
            DbCommandWrapper.AddInParameter("@InformationSourceID", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationSourceID)
            DbCommandWrapper.AddInParameter("@InformationSource", DbType.AnsiString, vWI_LeadCustomerSalesForce.InformationSource)
            DbCommandWrapper.AddInParameter("@CustomerPurposeID", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerPurposeID)
            DbCommandWrapper.AddInParameter("@CustomerPurpose", DbType.AnsiString, vWI_LeadCustomerSalesForce.CustomerPurpose)
            DbCommandWrapper.AddInParameter("@ProspectDate", DbType.AnsiString, vWI_LeadCustomerSalesForce.ProspectDate)
            DbCommandWrapper.AddInParameter("@ProspectDate_YYYYMMDD", DbType.AnsiString, vWI_LeadCustomerSalesForce.ProspectDate_YYYYMMDD)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, vWI_LeadCustomerSalesForce.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vWI_LeadCustomerSalesForce.Description)
            DbCommandWrapper.AddInParameter("@CurrVehicleType", DbType.AnsiString, vWI_LeadCustomerSalesForce.CurrVehicleType)
            DbCommandWrapper.AddInParameter("@CurrVehicleBrand", DbType.AnsiString, vWI_LeadCustomerSalesForce.CurrVehicleBrand)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, vWI_LeadCustomerSalesForce.Note)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, vWI_LeadCustomerSalesForce.Keterangan)
            DbCommandWrapper.AddInParameter("@StatusResponseID", DbType.AnsiString, vWI_LeadCustomerSalesForce.StatusResponseID)
            DbCommandWrapper.AddInParameter("@StatusResponse", DbType.AnsiString, vWI_LeadCustomerSalesForce.StatusResponse)
            DbCommandWrapper.AddInParameter("@WebID", DbType.AnsiString, vWI_LeadCustomerSalesForce.WebID)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_LeadCustomerSalesForce

            Dim vWI_LeadCustomerSalesForce As VWI_LeadCustomerSalesForce = New VWI_LeadCustomerSalesForce

            vWI_LeadCustomerSalesForce.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("DNetID")) Then vWI_LeadCustomerSalesForce.DNetID = CType(dr("DNetID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SumberData")) Then vWI_LeadCustomerSalesForce.SumberData = dr("SumberData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vWI_LeadCustomerSalesForce.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreateDate")) Then vWI_LeadCustomerSalesForce.CreateDate = dr("CreateDate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreateDate_YYYYMMDD")) Then vWI_LeadCustomerSalesForce.CreateDate_YYYYMMDD = dr("CreateDate_YYYYMMDD").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_LeadCustomerSalesForce.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then vWI_LeadCustomerSalesForce.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerTypeID")) Then vWI_LeadCustomerSalesForce.CustomerTypeID = dr("CustomerTypeID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerType")) Then vWI_LeadCustomerSalesForce.CustomerType = dr("CustomerType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then vWI_LeadCustomerSalesForce.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vWI_LeadCustomerSalesForce.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCode")) Then vWI_LeadCustomerSalesForce.CustomerCode = dr("CustomerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then vWI_LeadCustomerSalesForce.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerAddress")) Then vWI_LeadCustomerSalesForce.CustomerAddress = dr("CustomerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then vWI_LeadCustomerSalesForce.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then vWI_LeadCustomerSalesForce.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SexID")) Then vWI_LeadCustomerSalesForce.SexID = dr("SexID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sex")) Then vWI_LeadCustomerSalesForce.Sex = dr("Sex").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AgeSegmentID")) Then vWI_LeadCustomerSalesForce.AgeSegmentID = dr("AgeSegmentID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AgeSegment")) Then vWI_LeadCustomerSalesForce.AgeSegment = dr("AgeSegment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerStatusID")) Then vWI_LeadCustomerSalesForce.CustomerStatusID = dr("CustomerStatusID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerStatus")) Then vWI_LeadCustomerSalesForce.CustomerStatus = dr("CustomerStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InformationTypeID")) Then vWI_LeadCustomerSalesForce.InformationTypeID = dr("InformationTypeID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InformationType")) Then vWI_LeadCustomerSalesForce.InformationType = dr("InformationType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InformationSourceID")) Then vWI_LeadCustomerSalesForce.InformationSourceID = dr("InformationSourceID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InformationSource")) Then vWI_LeadCustomerSalesForce.InformationSource = dr("InformationSource").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPurposeID")) Then vWI_LeadCustomerSalesForce.CustomerPurposeID = dr("CustomerPurposeID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPurpose")) Then vWI_LeadCustomerSalesForce.CustomerPurpose = dr("CustomerPurpose").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProspectDate")) Then vWI_LeadCustomerSalesForce.ProspectDate = dr("ProspectDate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProspectDate_YYYYMMDD")) Then vWI_LeadCustomerSalesForce.ProspectDate_YYYYMMDD = dr("ProspectDate_YYYYMMDD").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then vWI_LeadCustomerSalesForce.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vWI_LeadCustomerSalesForce.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CurrVehicleType")) Then vWI_LeadCustomerSalesForce.CurrVehicleType = dr("CurrVehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CurrVehicleBrand")) Then vWI_LeadCustomerSalesForce.CurrVehicleBrand = dr("CurrVehicleBrand").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then vWI_LeadCustomerSalesForce.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then vWI_LeadCustomerSalesForce.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusResponseID")) Then vWI_LeadCustomerSalesForce.StatusResponseID = dr("StatusResponseID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusResponse")) Then vWI_LeadCustomerSalesForce.StatusResponse = dr("StatusResponse").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WebID")) Then vWI_LeadCustomerSalesForce.WebID = dr("WebID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_LeadCustomerSalesForce.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_LeadCustomerSalesForce

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_LeadCustomerSalesForce) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_LeadCustomerSalesForce), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_LeadCustomerSalesForce).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

