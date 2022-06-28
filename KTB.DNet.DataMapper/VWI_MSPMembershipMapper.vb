
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : MSPMembership Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 14:31:49
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

    Public Class VWI_MSPMembershipMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPMembership"
        Private m_UpdateStatement As String = "up_UpdateMSPMembership"
        Private m_RetrieveStatement As String = "up_RetrieveMSPMembership"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPMembershipList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPMembership"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPMembership As VWI_MSPMembership = Nothing
            While dr.Read

                mSPMembership = Me.CreateObject(dr)

            End While

            Return mSPMembership

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPMembershipList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPMembership As VWI_MSPMembership = Me.CreateObject(dr)
                mSPMembershipList.Add(mSPMembership)
            End While

            Return mSPMembershipList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPMembership As VWI_MSPMembership = CType(obj, VWI_MSPMembership)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, mSPMembership.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPMembership As VWI_MSPMembership = CType(obj, VWI_MSPMembership)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@MSPExRegistrationDate", DbType.DateTime, mSPMembership.MSPExRegistrationDate)
            DbCommandWrapper.AddInParameter("@MSPExDealerCode", DbType.AnsiString, mSPMembership.MSPExDealerCode)
            DbCommandWrapper.AddInParameter("@MSPExCode", DbType.AnsiString, mSPMembership.MSPExCode)
            DbCommandWrapper.AddInParameter("@MSPExDescription", DbType.AnsiString, mSPMembership.MSPExDescription)
            DbCommandWrapper.AddInParameter("@MSPExKm", DbType.Int32, mSPMembership.MSPExKm)
            DbCommandWrapper.AddInParameter("@MSPExValidUntil", DbType.DateTime, mSPMembership.MSPExValidUntil)
            DbCommandWrapper.AddInParameter("@MSPExDuration", DbType.Int16, mSPMembership.MSPExDuration)
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPMembership.MSPCustomerID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, mSPMembership.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, mSPMembership.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, mSPMembership.DealerName)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPMembership.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@MSPCode", DbType.AnsiString, mSPMembership.MSPCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, mSPMembership.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, mSPMembership.ColorCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, mSPMembership.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, mSPMembership.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@MSPKm", DbType.Int32, mSPMembership.MSPKm)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPMembership.Duration)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, mSPMembership.Description)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.DateTime, mSPMembership.ValidUntil)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, mSPMembership.RegistrationDate)


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

            Dim mSPMembership As VWI_MSPMembership = CType(obj, VWI_MSPMembership)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, mSPMembership.ID)
            DbCommandWrapper.AddInParameter("@MSPExRegistrationDate", DbType.DateTime, mSPMembership.MSPExRegistrationDate)
            DbCommandWrapper.AddInParameter("@MSPExDealerCode", DbType.AnsiString, mSPMembership.MSPExDealerCode)
            DbCommandWrapper.AddInParameter("@MSPExCode", DbType.AnsiString, mSPMembership.MSPExCode)
            DbCommandWrapper.AddInParameter("@MSPExDescription", DbType.AnsiString, mSPMembership.MSPExDescription)
            DbCommandWrapper.AddInParameter("@MSPExKm", DbType.Int32, mSPMembership.MSPExKm)
            DbCommandWrapper.AddInParameter("@MSPExValidUntil", DbType.DateTime, mSPMembership.MSPExValidUntil)
            DbCommandWrapper.AddInParameter("@MSPExDuration", DbType.Int16, mSPMembership.MSPExDuration)
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPMembership.MSPCustomerID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, mSPMembership.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, mSPMembership.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, mSPMembership.DealerName)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPMembership.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@MSPCode", DbType.AnsiString, mSPMembership.MSPCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, mSPMembership.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, mSPMembership.ColorCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, mSPMembership.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, mSPMembership.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@MSPKm", DbType.Int32, mSPMembership.MSPKm)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPMembership.Duration)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, mSPMembership.Description)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.DateTime, mSPMembership.ValidUntil)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, mSPMembership.RegistrationDate)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_MSPMembership

            Dim mSPMembership As VWI_MSPMembership = New VWI_MSPMembership
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExRegistrationDate")) Then mSPMembership.MSPExRegistrationDate = CType(dr("MSPExRegistrationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExDealerCode")) Then mSPMembership.MSPExDealerCode = dr("MSPExDealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExCode")) Then mSPMembership.MSPExCode = dr("MSPExCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExDescription")) Then mSPMembership.MSPExDescription = dr("MSPExDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExKm")) Then mSPMembership.MSPExKm = CType(dr("MSPExKm"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExValidUntil")) Then mSPMembership.MSPExValidUntil = CType(dr("MSPExValidUntil"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExDuration")) Then mSPMembership.MSPExDuration = CType(dr("MSPExDuration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPCustomerID")) Then mSPMembership.MSPCustomerID = CType(dr("MSPCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then mSPMembership.DealerId = CType(dr("DealerId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then mSPMembership.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then mSPMembership.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then mSPMembership.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPCode")) Then mSPMembership.MSPCode = dr("MSPCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then mSPMembership.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then mSPMembership.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then mSPMembership.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then mSPMembership.VehicleTypeDesc = dr("VehicleTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MSPKm")) Then mSPMembership.MSPKm = CType(dr("MSPKm"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then mSPMembership.Duration = CType(dr("Duration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then mSPMembership.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidUntil")) Then mSPMembership.ValidUntil = CType(dr("ValidUntil"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationDate")) Then mSPMembership.RegistrationDate = CType(dr("RegistrationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPMembership.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return mSPMembership

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_MSPMembership) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_MSPMembership), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_MSPMembership).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

